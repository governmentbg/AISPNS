namespace AISTN.ExternalAppAPI.Helper
{
    public class TemplateDownloadModel
    {
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public byte[] BlobContent { get; set; }
    }
}
