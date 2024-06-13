namespace AISTN.InternalAppAPI.Models
{
    public class SampleReportDownloadModel
    {
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public byte[] BlobContent { get; set; }
    }
}
