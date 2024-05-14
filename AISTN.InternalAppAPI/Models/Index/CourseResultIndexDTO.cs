namespace AISTN.InternalAppAPI.Models.Index
{
    public class CourseResultIndexDTO
    {
        public Guid Id { get; set; }

        public bool? Completed { get; set; }

        public Guid CourseId { get; set; }

        public Guid SyndicId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
