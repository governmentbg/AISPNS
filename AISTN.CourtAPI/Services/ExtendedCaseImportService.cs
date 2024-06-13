using AISTN.Common.Helper;
using AISTN.Common.Models.CourtApis.ExtendedRequest;
using AISTN.Common.Services.CourtApis;
using AISTN.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace AISTN.CourtAPI.Services
{
    public class ExtendedCaseImportService : CaseImportServiceBase
    {
        private Transfer transferObj;

        public ExtendedCaseImportService(IDbContextFactory<AistnContextLoggable> dbFactory, ILogger<CaseImportServiceBase> logger, CacheManager cache)
            : base(dbFactory, logger, cache) { }

        protected override bool _isExtendedTransfer => true;

        protected override async Task<string> GetXSD()
        {
            string xsdFilePath = "Models/Requests/bankrupt_transfer_extended.xsd";

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, xsdFilePath);
            string xsdString = await File.ReadAllTextAsync(path);

            return xsdString;
        }


        protected override async Task validateTransferContents(Transfer transfer)
        {
            await base.validateTransferContents(transfer);

            List<string> errors = new List<string>();

            parseYear(transfer.Case.case_year);

            if (errors.Any())
            {
                throw new ValidationErrorsException("Validation errors", errors);
            }
        }
    }
}