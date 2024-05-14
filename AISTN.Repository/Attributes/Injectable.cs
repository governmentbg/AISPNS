namespace AISTN.Repository.Attributes
{
    /// <summary>
    /// This class is used to tell if service is injectable
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class Injectable : Attribute
    {
    }
}
