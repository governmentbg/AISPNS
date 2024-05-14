using AISTN.Common.Models;

namespace AISTN.InternalAppAPI.Models.Index
{
    public class StatisticalReportIndexDTO
    {
        public Guid Id { get; set; }

        public string? IdentificationNumbr { get; set; }

        public Guid? Type { get; set; }

        public Guid? ReportSourceId { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string? Note { get; set; }

        public string? ReportPreparator { get; set; }

        public DateTime? Published { get; set; }

        public Guid? Template { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public DocumentCollectionDTO? DocumentCollection { get; set; }

        public NomReportSourceDTO? ReportSource { get; set; }

        public NomReportTypeDTO? TypeNavigation { get; set; }
    }
}
