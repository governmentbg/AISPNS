using AISTN.CommercialRegIntegrator.Helpers;
using AISTN.CommercialRegIntegrator.XMLClasses;
using AISTN.Data.DataModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;
using System.Xml;

namespace AISTN.CommercialRegIntegrator.Services
{
    public class CommercialRegisterImporterService : ICommercialRegisterImporterService
    {
        private readonly ILogger<CommercialRegisterImporterService> _logger;
        private readonly IDbContextFactory<AistnContext> _dbFactory;
        private readonly FolderSettings _folderSettings;

        public CommercialRegisterImporterService(IDbContextFactory<AistnContext> dbFactory, ILogger<CommercialRegisterImporterService> logger, IOptions<FolderSettings> FolderSettings)
        {
            _dbFactory = dbFactory;
            _logger = logger;
            _folderSettings = FolderSettings.Value;
        }

        public async Task<bool> ImportDeedAsync(string filePath)
        {
            try
            {
                var xmlContent = await File.ReadAllTextAsync(filePath);
                var checksum = FileChecksumHelper.CalculateChecksum(xmlContent);


                AistnContext db = await _dbFactory.CreateDbContextAsync();
                var request = db.ImportRequests.SingleOrDefault(x => x.Checksum == checksum && x.Status == Constants.ImportRequestStatuses.SUCCESS);
                if (request != null)
                {
                    db.ImportRequests.Add(new ImportRequest { Status = Constants.ImportRequestStatuses.DUPLICATE_CONTENT, Checksum = checksum, Filename = filePath });
                    await db.SaveChangesAsync();
                    return true;
                }

                request = new ImportRequest() { Status = Constants.ImportRequestStatuses.IN_PROGRESS, Checksum = checksum, Filename = filePath };


                var xM = new XmlManipulator(xmlContent, Constants.XPathQueries.DeedNameSpace, "aa");

                var deedNodes = await xM.extractXMLNodesAsync(@"//aa:Deed[aa:SubDeed[@SubUICType=""Bankruptcy"" or @SubUICType=""Stabilization""]]/aa:IncomingPackageInfo[@LeadingApp=""CourtAct""]/parent::aa:Deed");
                foreach (XmlNode node in deedNodes)
                {
                    if (node != null) request.ImportedDeeds.Add(await ExctractDeedAsync(node));
                }

                xM.AddNameSpace("bb", Constants.XPathQueries.FieldNameSpace);

                var mcNodes = await xM.extractXMLNodesAsync(@"//aa:SubDeed[@SubUICType='MainCircumstances']/bb:Seat/bb:Address/parent::bb:Seat/parent::aa:SubDeed");
                foreach (XmlNode node in mcNodes)
                {
                    if (node != null) request.CompaniesRegistrations.Add(await ExtractCompanyRegistrationAsync(node));
                }


                request.Status = Constants.ImportRequestStatuses.SUCCESS;
                db.ImportRequests.Add(request);



                await db.SaveChangesAsync();
                FileHelper.MoveFile(filePath, _folderSettings.Processed, true);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception while processing file:" + filePath);
                throw;
            }
        }

        protected async Task<ImportedDeed> ExctractDeedAsync(XmlNode deedNode)
        {
            try
            {
                var result = new ImportedDeed();

                result.DeedRawXml = deedNode.OuterXml;
                result.DeedGuid = deedNode.Attributes["GUID"].Value;
                result.CompanyName = deedNode.Attributes["CompanyName"].Value;
                result.CompanyLegalForm = deedNode.Attributes["LegalForm"].Value;
                result.CompanyUic = deedNode.Attributes["UIC"].Value;

                var xM = new XmlManipulator(deedNode, Constants.XPathQueries.FieldNameSpace, "bb");
                var actNodes = await xM.extractXMLNodesAsync(@"//bb:ActData");

                XmlNode actNode = null;
                foreach (XmlNode node in actNodes)
                {

                    var ad = new ActDetail();
                    ad.ActContainerType = node.ParentNode.Name;


                    var pn = node.ParentNode;

                    foreach (XmlNode cn in pn.ChildNodes)
                    {
                        if (cn.Name == "Title") ad.Title = cn.InnerText;
                        if (cn.Name == "Date") ad.Date = cn.Value;
                    }

                    ad.RecordId = pn.Attributes["RecordID"] != null ? int.Parse(pn.Attributes["RecordID"].Value) : null;


                    var act = ActDataType.FromXmlNode(node);
                    ad.ActDate = ConversionHelper.ParseDateOrNull(act.ActDate);

                    result.ActDetails.Add(ad);


                    if (actNode == null && node != null)
                    {
                        actNode = node;
                    }
                }
                if (actNode != null)
                {

                    try
                    {

                        result.FieldActionDate = actNode.ParentNode.Attributes["FieldActionDate"] != null ? ConversionHelper.ParseDateOrNull(actNode.ParentNode.Attributes["FieldActionDate"].Value) : null;
                        result.FieldEntryDate = actNode.ParentNode.Attributes["FieldEntryDate"] != null ? ConversionHelper.ParseDateOrNull(actNode.ParentNode.Attributes["FieldEntryDate"].Value) : null;
                        result.FieldEntryNumber = actNode.ParentNode.Attributes["FieldEntryNumber"] != null ? actNode.ParentNode.Attributes["FieldEntryNumber"].Value : null;


                        var act = ActDataType.FromXmlNode(actNode);
                        result.ActDate = ConversionHelper.ParseDateOrNull(act.ActDate);

                        if (!string.IsNullOrEmpty(act.ActNumber) && act.ActNumber.Length > 199)
                        {
                            result.ActNumber = act.ActNumber.Substring(0, 199);
                        }
                        else
                        {
                            result.ActNumber = act.ActNumber;
                        }


                        result.CaseNumber = ConversionHelper.GetSafeSubstring(act.CaseNumber, 199);
                        result.CaseYear = int.TryParse(act.CaseYear, out int number) ? (int?)number : null;
                        result.ActType = act.Type.Name;
                        result.CourtNo = int.TryParse(act.BankruptcyCourt.Code, out int number1) ? (int?)number1 : null;
                        result.ActComplaintTerm = act.ComplaintTerm;


                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, actNode.ToString());

                        throw ex;
                    }
                }

                xM = new XmlManipulator(deedNode, Constants.XPathQueries.FieldNameSpace, "bb");
                var trusteesNodes = await xM.extractXMLNodesAsync(@"//bb:Trustees");

                if (trusteesNodes.Count != 0)
                {
                    foreach (XmlNode trusteesNode in trusteesNodes)
                    {
                        var trustees = Trustees.FromXmlNode(trusteesNode);

                        if (trustees.Trustee != null)
                        {

                            foreach (TrusteesTrustee trustee in trustees.Trustee)
                            {
                                var subResult = new Trustee();
                                subResult.Name = trustee.Person.Name;
                                subResult.Indent = trustee.Person.Indent;
                                subResult.IndentType = trustee.Person.IndentType;
                                subResult.CountryName = trustee.Person.CountryName;
                                subResult.Status = trustee.Status.Name;
                                subResult.RecordId = trustee.RecordID;
                                subResult.RecordIncomingNumber = trustee.RecordIncomingNumber;
                                subResult.AcquittanseDate = ConversionHelper.ParseDateOrNull(trustee.AcquittanseDate);
                                subResult.InductionDate = ConversionHelper.ParseDateOrNull(trustee.InductionDate);


                                result.Trustees.Add(subResult);
                            }
                        }
                    }

                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception while processing deed GUID:" + deedNode.Attributes["GUID"].Value);
                throw;
            }
        }


        protected async Task<CompaniesRegistration> ExtractCompanyRegistrationAsync(XmlNode subDeedNode)
        {
            try
            {
                var result = new CompaniesRegistration();


                var xM = new XmlManipulator(subDeedNode, Constants.XPathQueries.FieldNameSpace, "bb");
                var addressNodes = await xM.extractXMLNodesAsync(@"//bb:Seat/bb:Address");

                if (addressNodes.Count != 1) throw new Exception("node not fount");


                var address = AddressType.FromXmlNode(addressNodes[0]);

                result.Apartment = ConversionHelper.GetSafeSubstring(address.Apartment, 499);
                result.Area = address.Area;
                result.AreaId = ConversionHelper.SafeConvertToInt(address.AreaID);
                result.AreaEkatte = address.AreaEkatte;
                result.Country = address.Country;
                result.CountryCode = address.CountryCode;
                result.CountryId = ConversionHelper.SafeConvertToInt(address.CountryID);
                result.District = address.District;
                result.DistrictEkatte = address?.DistrictEkatte;
                result.DistrictId = ConversionHelper.SafeConvertToInt(address?.DistrictID);
                result.HousingEstate = address.HousingEstate;
                result.Entrance = address.Entrance;
                result.Floor = address.Floor;
                result.ForeignPlace = address.ForeignPlace;
                result.IsForeign = ConversionHelper.SafeConvertToBool(address.IsForeign);
                result.Municipality = address.Municipality;
                result.MunicipalityEkatte = address.MunicipalityEkatte;
                result.Municipalityid = ConversionHelper.SafeConvertToInt(address.Municipalityid);
                result.PostCode = address.PostCode;
                result.Settlement = address.Settlement;
                result.SettlementEkatte = address.SettlementEKATTE;
                result.SettlementId = ConversionHelper.SafeConvertToInt(address.SettlementID );
                result.Street = address.Street;
                result.StreetNumber = address.StreetNumber;
                result.Block = address.Block;


                var deedNode = subDeedNode.ParentNode;

                result.CompanyName = deedNode.Attributes["CompanyName"].Value;
                result.LegalForm = deedNode.Attributes["LegalForm"].Value;
                result.Bulstat = deedNode.Attributes["UIC"].Value;
                
                result.FieldActionDate = addressNodes[0].ParentNode.Attributes["FieldActionDate"] != null ? ConversionHelper.ParseDateOrNull(addressNodes[0].ParentNode.Attributes["FieldActionDate"].Value) : null;


                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception while processing deed GUID:" + subDeedNode.Attributes["GUID"].Value);
                throw;
            }
        }




        public async Task<bool> ConnectEntitiesAsync()
        {
            try
            {

                AistnContext db = await _dbFactory.CreateDbContextAsync();
                var cutoffDaysInPast = new SqlParameter("@CutoffDaysInPast", SqlDbType.Int)
                {
                    Value = _folderSettings.CutOffDaysInPast
                };


                var result = await db.Database.ExecuteSqlRawAsync("EXEC tr.usp_Connect_ImportedDeed2Case @CutoffDaysInPast", cutoffDaysInPast);
                _logger.LogInformation(@"Connected " + result.ToString() + "Deeds to Cases");

                result = await db.Database.ExecuteSqlRawAsync("EXEC tr.usp_Connect_Trustee2Syndic @CutoffDaysInPast", cutoffDaysInPast);
                _logger.LogInformation(@"Connected " + result.ToString() + "Trustees to Syndices");

                result = await db.Database.ExecuteSqlRawAsync("EXEC tr.usp_Connect_InsertIntoExperience @CutoffDaysInPast", cutoffDaysInPast);
                _logger.LogInformation(@"Connected " + result.ToString() + "Experience");

                result = await db.Database.ExecuteSqlRawAsync("EXEC tr.usp_Connect_SetISDebtorSide @CutoffDaysInPast", cutoffDaysInPast);
                _logger.LogInformation(@"Connected " + result.ToString() + "Set debtors");





                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception while connecting entities");
                throw;
            }
        }

    }
}

