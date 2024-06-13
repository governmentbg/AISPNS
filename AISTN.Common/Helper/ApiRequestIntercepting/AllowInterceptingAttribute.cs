namespace AISTN.Common.Helper.ApiRequestIntercepting
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowInterceptingAttribute : Attribute
    {
        public bool LimitRate { get; set; }

        public AllowInterceptingAttribute()
        {
        }
    }
}
