namespace AISTN.InternalAppAPI.Models.Save
{
    public class SaveCourseApplicationDTO
    {
        public Guid? Id { get; set; }

        public Guid? CourseId { get; set; }

        public Guid? SyndicId { get; set; }

        public bool? LecturerDate1 { get; set; }

        public bool? LecturerDate2 { get; set; }

        public Guid? AlternateCourseId { get; set; }

        public bool? HotelReservationDate1 { get; set; }

        public bool? HotelReservationDate2 { get; set; }

        public string? ActualEmail { get; set; }

        public string? ActualPhone { get; set; }

        public DateTime? FromDate1 { get; set; }

        public DateTime? ToDate1 { get; set; }

        public DateTime? FromDate2 { get; set; }

        public DateTime? ToDate2 { get; set; }

        /// <summary>
        /// TRUE then course first date are chosen/FALSE then course second dates are chosen
        /// </summary>
        public bool CourseDate1 { get; set; }

        /// <summary>
        /// TRUE then alternate course first date are chosen/FALSE then alternate course second dates are chosen
        /// </summary>
        public bool AlternateCourseDate2 { get; set; }
    }
}
