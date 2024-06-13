namespace AISTN.Common.Models.Export
{
    public class ExportToExcelModel
    {
        public ExportToExcelModel()
        {
            HeaderRename = new List<KeyValuePair<string, string>>();
        }

        public List<KeyValuePair<string, string>>? HeaderRename { get; set; }
    }
}
