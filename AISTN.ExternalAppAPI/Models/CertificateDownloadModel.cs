namespace AISTN.ExternalAppAPI.Models
{
    public class CertificateDownloadModel
    {
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public byte[] BlobContent { get; set; }
    }
}
