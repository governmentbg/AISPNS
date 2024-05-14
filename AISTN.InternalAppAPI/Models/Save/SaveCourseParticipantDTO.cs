namespace AISTN.InternalAppAPI.Models.Save
{
    public class SaveCourseParticipantDTO
    {
        public Guid? Id { get; set; }

        public Guid? CourseId { get; set; }

        public Guid? SyndicId { get; set; }

        public bool? EnrolledDate1 { get; set; }

        public bool? EnrolledDate2 { get; set; }

        public bool? PassedTheCourse { get; set; }
    }
}