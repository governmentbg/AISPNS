using AISTN.Common.Helper.ApiRequestIntercepting;
using AISTN.Data.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ServiceModel;

namespace IRIWCFService.IRIService
{
    public partial class IRIQueryServiceImplementation : IRIQueryService
    {
        private readonly IDbContextFactory<AistnContext> _dbFactory;
        private readonly IOptions<SimpleSearchSettings> _simpleSearchSettings;

        public IRIQueryServiceImplementation(IDbContextFactory<AistnContext> dbFactory, IOptions<SimpleSearchSettings> simpleSearchSettings)
        {
            _dbFactory = dbFactory;
            _simpleSearchSettings = simpleSearchSettings;
        }
        public advancedSearchResponse advancedSearch(advancedSearchRequest1 request)
        {
            throw new System.NotImplementedException();
        }

        public Task<advancedSearchResponse> advancedSearchAsync(advancedSearchRequest1 request)
        {
            throw new System.NotImplementedException();
        }

        public advancedSearchPersonResponse advancedSearchPerson(advancedSearchPersonRequest1 request)
        {
            throw new System.NotImplementedException();
        }

        public Task<advancedSearchPersonResponse> advancedSearchPersonAsync(advancedSearchPersonRequest1 request)
        {
            throw new System.NotImplementedException();
        }

        public getDataRecordResponse getDataRecord(getDataRecordRequest1 request)
        {
            throw new System.NotImplementedException();
        }

        public async Task<getDataRecordResponse> getDataRecordAsync(getDataRecordRequest1 request)
        {
            System.Guid id = new Guid();

            if (!Guid.TryParse(request.getDataRecordRequest.nationalID, out id))
            {
                var faultDetail = new InternalServiceException() { errorMessage = "Incorrect  national identifier:  " + request.getDataRecordRequest.nationalID };
                throw new FaultException<InternalServiceException>(faultDetail, new FaultReason(faultDetail.errorMessage));
            }

            AistnContext db = await _dbFactory.CreateDbContextAsync();

            var entity = await db.Entities.Where(e => e.Id == id).
                                                Include(x => x.Sides).
                                                ThenInclude(x => x.Case).
                                                SingleOrDefaultAsync<Entity>();

            if (entity == null || entity.Sides.Count == 0 || entity.Sides.First<Side>().Case == null)
            {
                var faultDetail = new InternalServiceException() { errorMessage = "Record not found for identifier:  " + request.getDataRecordRequest.nationalID };
                throw new FaultException<InternalServiceException>(faultDetail, new FaultReason(faultDetail.errorMessage));
            }

            var caseInfo = await db.Cases.Where(e => e.Id == entity.Sides.First().Case.Id).
                                                             Include(x => x.Sides).ThenInclude(x => x.Entity).
                                                             Include(x => x.Sides).ThenInclude(x => x.InvolvementKind).
                                                             Include(x => x.Judges).
                                                             Include(x => x.CaseKind).
                                                             Include(x => x.Court).
                                                             Include(x => x.Experiences).ThenInclude(x => x.Syndic).
                                                             ThenInclude(x=>x.Syndic2Addresses).ThenInclude(x=>x.Address).ThenInclude(x=>x.Settlement).  
                                                             AsSplitQuery().
                                                             SingleOrDefaultAsync<Case>();
            NomCourt appealCourtEntity = null;
            Court appealCourt = new Court();

            if (caseInfo.Court != null && caseInfo.Court.CourtOfAppeal != null)
            {
                appealCourtEntity = await db.NomCourts.Where(x => x.Number == caseInfo.Court.CourtOfAppeal).FirstOrDefaultAsync();
                appealCourt.name = appealCourtEntity.Name;
                appealCourt.contact = new BasicContactInformation { email = appealCourtEntity.Email, faxNumber = appealCourtEntity.FaxNumber };
            }

            var result = new IRIDetailedResult();
            result.context = new BasicContext();
            result.context.status = BasicContextStatus.OK;

            var syndicList = new List<CoreInsolvencyRecordInsolvencyPractitioner>();

            foreach (Experience e in caseInfo.Experiences)
            {
                if (e.Syndic != null)
                {
                    var address = new Address();
                    if (e.Syndic.Syndic2Addresses.FirstOrDefault() != null && e.Syndic.Syndic2Addresses.FirstOrDefault().Address != null && e.Syndic.Syndic2Addresses.FirstOrDefault().Address.Settlement != null)
                    {
                        address.zip = e.Syndic.Syndic2Addresses.FirstOrDefault().Address.PostCode;
                        address.city = e.Syndic.Syndic2Addresses.FirstOrDefault().Address.Settlement.DisplayName ;
                        address.street = e.Syndic.Syndic2Addresses.FirstOrDefault().Address.Additional; 
                    }

                    syndicList.Add(new CoreInsolvencyRecordInsolvencyPractitioner
                    {
                        Item = new Name { givenName = e.Syndic.FirstName, surname = e.Syndic.LastName },
                        address = address,
                        contact = new BasicContactInformation { email = e.Syndic.Email, faxNumber = e.Syndic.Phone }
                      
                                      
                    });
                }
            }

            var ir = new InsolvencyRecord
            {
                caseNumber = caseInfo.Number,
                court = new Court { name = caseInfo.Court.Name, contact = new BasicContactInformation { email = caseInfo.Court.Email, faxNumber = caseInfo.Court.FaxNumber } },
                openingDate = caseInfo.FormationDate.GetValueOrDefault(new DateTime(caseInfo.Year, 1, 1)),
                insolvencyPractitioner = syndicList.ToArray(),
                appealInformation = new AppealInformation { court = appealCourt, deadLine = new Deadline() },
                claimInformation = new ClaimInformation { deadLine = new Deadline() },
                closingDateSpecified = false,
                jurisdiction = "2156",
                debtor = new CoreInsolvencyRecordDebtor { Item = new LegalPerson { name = new List<LegalPersonName> { new LegalPersonName { type = LegalPersonNameType.PRIMARY, Value = entity.Name } }.ToArray(), registrationNumber = entity.Bulstat, address = new LegalPersonAddress { } } },
                type = "1835" //according to Todor  Insolvency proceedings referred to in point (4) of Article 2   
            };

            result.entity = ir;
            return new getDataRecordResponse { getDataRecordResponse1 = result };
        }

        public initIRIQueryResponse1 initIRIQuery(initIRIQueryRequest request)
        {
            throw new System.NotImplementedException();
        }

        public async Task<initIRIQueryResponse1> initIRIQueryAsync(initIRIQueryRequest request)
        {
            try
            {
                var result = new initIRIQueryResponse();
                result.context = new BasicContext();
                result.context.status = BasicContextStatus.WARNING;

                var fieldList = new List<FieldRenderingInfo>();

                fieldList.Add(new FieldRenderingInfo()
                {
                    key = "1499",
                    type = FieldRenderingInfoType.ID,
                    description = new List<QualifiedText> { new QualifiedText { lang = "en", Value = "Internal identifier" } }.ToArray()
                });

                fieldList.Add(new FieldRenderingInfo()
                {
                    key = "1498",
                    type = FieldRenderingInfoType.TEXT,
                    description = new List<QualifiedText> { new QualifiedText { lang = "en", Value = "Debtor Name" } }.ToArray()
                });

                fieldList.Add(new FieldRenderingInfo()
                {
                    key = "2044",
                    type = FieldRenderingInfoType.TEXT,
                    description = new List<QualifiedText> { new QualifiedText { lang = "en", Value = "National registration number" } }.ToArray()
                });

                result.field = fieldList.ToArray();

                var resultWrapper = new initIRIQueryResponse1();
                resultWrapper.initIRIQueryResponse = result;
                return resultWrapper;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public simpleSearchResponse simpleSearch(simpleSearchRequest1 request)
        {
            throw new System.NotImplementedException();
        }

        [AllowInterceptingAttribute]
        public async Task<simpleSearchResponse> simpleSearchAsync(simpleSearchRequest1 request)
        {

            int maxResult = 0;

            if (!int.TryParse(request.simpleSearchRequest.maxResult, out maxResult))
            {
                var faultDetail = new InternalServiceException() { errorMessage = "Incorrect parameter simpleSearchRequest.maxResult:  " + request.simpleSearchRequest.maxResult };
                throw new FaultException<InternalServiceException>(faultDetail, new FaultReason(faultDetail.errorMessage));
            }

            if (maxResult > _simpleSearchSettings.Value.MaxResultHardLimit) maxResult = _simpleSearchSettings.Value.MaxResultHardLimit;
            var realMaxResult = maxResult + 1; //for the query we are using maxResult + 1 in order to determine if the result is truncated
            
            var tempResult = new simpleSearchResponse();

            var bulstatSearchString = request.simpleSearchRequest.nationalId;
            var nameSearchString = request.simpleSearchRequest.name;

            if (
                    (string.IsNullOrEmpty(bulstatSearchString) && string.IsNullOrEmpty(nameSearchString)) ||
                    (!string.IsNullOrEmpty(bulstatSearchString) && bulstatSearchString.Length < _simpleSearchSettings.Value.MinimalBulstatSearchStringLength) ||
                    (!string.IsNullOrEmpty(nameSearchString) && nameSearchString.Length < _simpleSearchSettings.Value.MinimalNameSearchStringLength)
               )
            {

                var faultDetail = new InternalServiceException() { errorMessage = "TOO_GENERIC" };
                throw new FaultException<InternalServiceException>(faultDetail, new FaultReason(faultDetail.errorMessage));
            }

            tempResult.simpleSearchResponse1 = new IRISearchResult();
            tempResult.simpleSearchResponse1.context = new BasicContext();

            AistnContext db = await _dbFactory.CreateDbContextAsync();

            var entities = await db.Entities
                .Where(e => (string.IsNullOrEmpty(bulstatSearchString) || e.Bulstat.Contains(bulstatSearchString)) &&
                            (string.IsNullOrEmpty(nameSearchString) || e.Name.Contains(nameSearchString)) &&
                            (e.Sides.Any(a => a.IsDebtor)))
                .OrderBy(x => x.Name)
                .Take(realMaxResult)
                .Select(e => new { e.Id, e.Name, e.Bulstat })
                .ToListAsync();



            if (entities.Count == 0)
            {
                tempResult.simpleSearchResponse1.context.remark = new List<Remark> { new Remark { code = "0", detail = "No results were found" } }.ToArray();
            }

            var resultTable = new ValueTable();

            var buffer = new List<ValueTableColumn>();
            buffer.Add(new ValueTableColumn { key = "1499" });
            buffer.Add(new ValueTableColumn { key = "1498" });
            buffer.Add(new ValueTableColumn { key = "2044" });

            resultTable.header = buffer.ToArray();

            var rowList = new List<ValueTableRow>();

            int i = 0; //this is used to obey the request.maxResult
            foreach (var entity in entities)
            {
                i++;
                var row = new ValueTableRow();
                var fieldsList = new List<ComplexValue>();
                fieldsList.Add(new ComplexValue() { Item = entity.Id.ToString(), ItemElementName = ItemChoiceType1.text });
                fieldsList.Add(new ComplexValue() { Item = entity.Name, ItemElementName = ItemChoiceType1.text });
                fieldsList.Add(new ComplexValue() { Item = entity.Bulstat, ItemElementName = ItemChoiceType1.text });

                row.field = fieldsList.ToArray();

                if (i <= maxResult) rowList.Add(row);
            }

            resultTable.row = rowList.ToArray();

            if (entities.Count <= maxResult)
            { 
                tempResult.simpleSearchResponse1.context.status = BasicContextStatus.OK; 
            }
            else
            {
                tempResult.simpleSearchResponse1.context.status = BasicContextStatus.WARNING;
                tempResult.simpleSearchResponse1.context.remark = new List<Remark> { new Remark { code = "0", detail = "The result has been truncated to " + maxResult.ToString()+  " records" } }.ToArray();

            }
            
            tempResult.simpleSearchResponse1.resultTable = resultTable;
            return tempResult;
        }

        public submitRequestFormResponse1 submitRequestForm(submitRequestFormRequest1 request)
        {
            throw new System.NotImplementedException();
        }

        public Task<submitRequestFormResponse1> submitRequestFormAsync(submitRequestFormRequest1 request)
        {
            throw new System.NotImplementedException();
        }
    }
}
