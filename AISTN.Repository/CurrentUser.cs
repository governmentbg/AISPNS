using AISTN.Data.DataModel;

namespace AISTN.Repository
{
    public class CurrentUser
    {
        public bool IsAuthenticated { get; set; }

        public Guid? UserId { get; set; }

        public string UserName { get; set; }

        public string IpAddress { get; set; }

        public int? PersonId { get; set; }

        public string? FullName { get; set; }

        public IEnumerable<string>? Roles { get; set; }

        public CurrentUser()
        {
            IsAuthenticated = false;
        }
    }
}
