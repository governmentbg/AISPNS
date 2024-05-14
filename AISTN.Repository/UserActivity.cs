namespace AISTN.Repository
{
    public class UserActivity
    {
        public Guid? UserID { get; set; }
        public eUserActionType ActionType { get; set; }
        public string Message { get; set; }
        public string IpAddress { get; set; }
        public int? SubjectedDocumentId { get; set; }

        private UserActivity(CurrentUser? user, eUserActionType actionType, int? subjectedDocumentId = null, string message = null)
        {
            UserID = user?.UserId;
            ActionType = actionType;
            Message = message;
            IpAddress = user?.IpAddress;
            SubjectedDocumentId = subjectedDocumentId;
        }

        public static UserActivity? NewActivity(CurrentUser? user, eUserActionType actionType, int? subjectedDocumentId = null, string message = null)
        {
            if (user != null && user.IsAuthenticated)
                return new UserActivity(user, actionType, subjectedDocumentId, message);

            return null;
        }
    }
}
