namespace AISTN.Common.Models.Save
{
    public class SaveCourseDTO
    {
        public Guid? Id { get; set; }

        public Guid? ProgramId { get; set; }

        public Guid? CourseKindId { get; set; }

        public string? Topic { get; set; }

        public DateTime FromDate1 { get; set; }

        public DateTime ToDate1 { get; set; }

        public string? LengthDate1 { get; set; }

        public DateTime? FromDate2 { get; set; }

        public DateTime? ToDate2 { get; set; }

        public string? LengthDate2 { get; set; }

        public int? MaximumParticipants { get; set; }

        public string? Lecturers { get; set; }

        public string? SubTopics { get; set; }

        public DateTime? ExamDate1 { get; set; }

        public DateTime? ExamDate2 { get; set; }

        public Guid? DocumentCollectionId { get; set; }

        public SaveAddressDTO? Address1Navigation { get; set; }

        public SaveAddressDTO? Address2Navigation { get; set; }

        public DocumentCollectionDTO? DocumentCollection { get; set; }
    }
}
