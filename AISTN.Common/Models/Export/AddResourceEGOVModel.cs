namespace AISTN.Common.Models.Export
{
    public class AddResourceEGOVModel
    {
        public string api_key { get; set; }
        public string resource_uri { get; set; }
        public string extension_format { get; set; }
        public object data { get; set; }
    }
}
