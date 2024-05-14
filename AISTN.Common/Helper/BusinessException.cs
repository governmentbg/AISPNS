namespace AISTN.Common.Helper
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
        public BusinessException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ValidationErrorsException : BusinessException
    {
        public List<string>? Errors { get; set; }
        public ValidationErrorsException(string message, List<string>? errors = null) : base(message)
        {
            Errors = errors;
        }

        public string GetWholeMessage
        {
            get
            {
                return (string)(Message + "; " + string.Join(", ", Errors ?? [])).Trim().TrimEnd(';');
            }
        }
    }

    public class EntityNotFoundException<TId> : BusinessException// where TId : struct
    {
        public string EntityType { get; set; }
        public TId Identifier { get; set; }


        public EntityNotFoundException(Type entityType, TId id) : base($"Entity of type:{entityType.Name} with Identifier:{id} not found")
        {
            EntityType = entityType.Name;
            Identifier = id;
        }
    }

}
