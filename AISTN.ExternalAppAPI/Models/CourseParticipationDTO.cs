namespace AISTN.InternalAppAPI.Models
{
    public class CourseParticipationDTO
    {
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }

        public string CourseTopic { get; set; }

        public string CourseKindName { get; set; }

        public Guid SyndicId { get; set; }

        public bool? EnrolledDate1 { get; set; }

        public bool? EnrolledDate2 { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public bool? PassedTheCourse { get; set; }

        public bool? LecturerDate1 { get; set; }

        public bool? LecturerDate2 { get; set; }
    }
}
