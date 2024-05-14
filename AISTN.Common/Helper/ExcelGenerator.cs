using AISTN.Repository.Attributes;
using AISTN.Repository;
using System.Reflection;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace AISTN.Common.Helper
{
    [Injectable]
    public class ExcelGenerator
    {
        private readonly ExceptionLogger _logger;

        public ExcelGenerator(ExceptionLogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Export generic list to excel. The columns of that excel are all of the list's class properties that match headerRename list.
        /// </summary>
        /// <param name="itemsToExport">The data to be exported</param>
        /// <param name="headerRename">The name of the columns(property names) that have to be included in the excel file with their desired header name</param>
        /// <returns></returns>
        public byte[] ExportGridToExcelXlsxFile<T>(List<T> itemsToExport, List<KeyValuePair<string, string>> headerRename)
        {
            List<string> excludeColumns = new List<string>();

            foreach (var prop in typeof(T).GetProperties())
            {
                if (!headerRename.Any(x => string.Equals(x.Key.Split(new[] { '.' })[0].ToUpper(), prop.Name.ToUpper())))
                    excludeColumns.Add(prop.Name);
            }

            return ExportGridToExcelXlsxFile(itemsToExport, excludeColumns, headerRename, true);
        }

        /// <summary>
        /// Export generic list to excel. The columns of that excel are all of the list's class properties.
        /// </summary>
        /// <param name="itemsToExport">The data to be exported</param>
        /// <param name="excludeColumns">The columns(property names) that have to be excluded in the excel file</param>
        /// <param name="headerRename">The desired header names of the columns/properties</param>
        /// <returns></returns>
        public byte[] ExportGridToExcelXlsxFile<T>(List<T> itemsToExport, List<string> excludeColumns = null, List<KeyValuePair<string, string>>? headerRename = null, bool orderByHeader = false)
        {
            const string NULL_VALUE = "";
            const int AUTO_RESIZE_LIMIT = 10000;
            MemoryStream stream = new MemoryStream();

            if (excludeColumns == null) excludeColumns = new List<string>();

            try
            {
                var workbook = new XSSFWorkbook();
                XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("Data");
                Dictionary<int, XSSFSheet> WorkSheets = new Dictionary<int, XSSFSheet>();
                WorkSheets.Add(1, sheet);


                var rowIndex = 0;
                int cellindex = 0;

                var row = sheet.CreateRow(rowIndex);


                /// headers font style
                var headerfont = workbook.CreateFont();
                headerfont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                headerfont.FontHeightInPoints = 11;
                headerfont.FontName = "Calibri";
                ICellStyle headerStyle = workbook.CreateCellStyle();
                headerStyle.SetFont(headerfont);
                headerStyle.BorderBottom = BorderStyle.Thin;
                headerStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
                headerStyle.FillPattern = FillPattern.SolidForeground;

                /// datetime cell format
                IDataFormat newDataTimeFormat = workbook.CreateDataFormat();
                var dateTimeFormat = newDataTimeFormat.GetFormat("dd.MM.yyyy HH:mm:ss");
                var dateTimeStyle = workbook.CreateCellStyle();
                dateTimeStyle.DataFormat = dateTimeFormat;

                /// date cell format
                IDataFormat newDataFormat = workbook.CreateDataFormat();
                var dateFormat = newDataFormat.GetFormat("dd.MM.yyyy");
                var dateStyle = workbook.CreateCellStyle();
                dateStyle.DataFormat = dateFormat;

                /// integer cell format
                ICellStyle integerCellStyle = workbook.CreateCellStyle();
                integerCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0");

                /// floating point cell format
                ICellStyle doubleCellStyle = workbook.CreateCellStyle();
                doubleCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.###");

                // Get column names from the class been exported and adjust them with header renames
                var columnNames = new List<string>();

                var properties = typeof(T).GetProperties().ToList();

                if (orderByHeader == true)
                    properties = properties.OrderBy(x => headerRename?.FindIndex(y => IsMatch(y.Key, x.Name))).ToList();

                foreach (var prop in properties)
                {
                    var headerMatches = headerRename?.Where(x => IsMatch(x.Key, prop.Name)).ToList();

                    if (headerMatches?.Count > 0)
                    {
                        foreach (var header in headerMatches)
                        {
                            if (excludeColumns.Any(x => string.Equals(x.ToUpper(), header.Key.ToUpper()))) continue;

                            var cell = row.CreateCell(cellindex++);
                            cell.CellStyle = headerStyle;
                            cell.SetCellValue(header.Value);

                            columnNames.Add(header.Key);
                        }
                    }
                    else
                    {
                        if (excludeColumns.Any(x => string.Equals(x.ToUpper(), prop.Name.ToUpper()))) continue;

                        var cell = row.CreateCell(cellindex++);
                        cell.CellStyle = headerStyle;
                        cell.SetCellValue(prop.Name);

                        columnNames.Add(prop.Name);
                    }
                }

                /// Fill in data row by row
                foreach (T item in itemsToExport)
                {
                    rowIndex++;
                    cellindex = 0;
                    row = sheet.CreateRow(rowIndex);

                    /// Iterate row content
                    foreach (var name in columnNames)
                    {
                        var propInfo = GetPropInfo(item, name);

                        if (propInfo?.Value == null)
                        {
                            row.CreateCell(cellindex++).SetCellValue(NULL_VALUE);
                        }
                        else if (propInfo.Type == typeof(int) || propInfo.Type == typeof(int?) || propInfo.Type == typeof(long) || propInfo.Type == typeof(long?))
                        {
                            var cell = row.CreateCell(cellindex++, NPOI.SS.UserModel.CellType.Numeric);
                            cell.CellStyle = integerCellStyle;
                            cell.SetCellValue(Convert.ToInt64(propInfo.Value.ToString()));

                        }
                        else if (propInfo.Type == typeof(decimal) || propInfo.Type == typeof(double) || propInfo.Type == typeof(float)
                                 || propInfo.Type == typeof(decimal?) || propInfo.Type == typeof(double?) || propInfo.Type == typeof(float?))
                        {
                            var cell = row.CreateCell(cellindex++, NPOI.SS.UserModel.CellType.Numeric);
                            cell.CellStyle = doubleCellStyle;
                            cell.SetCellValue(Convert.ToDouble(propInfo.Value.ToString()));
                        }
                        else if (propInfo.Type == typeof(DateTime) || propInfo.Type == typeof(DateTime?))
                        {

                            var val = DateTime.Parse(propInfo.Value.ToString());
                            row.CreateCell(cellindex++).SetCellValue(val);

                            // Set the style to properly display the datetime format
                            if (((DateTime?)propInfo.Value).HasValue && ((DateTime?)propInfo.Value).Value.TimeOfDay == TimeSpan.Zero)
                            {
                                row.Cells.Last().CellStyle = dateStyle;
                                row.Cells.Last().CellStyle.DataFormat = dateFormat;
                            }
                            else
                            {
                                row.Cells.Last().CellStyle = dateTimeStyle;
                                row.Cells.Last().CellStyle.DataFormat = dateTimeFormat;
                            }
                        }
                        else if (propInfo.Type == typeof(bool) || propInfo.Type == typeof(bool?))
                        {
                            row.CreateCell(cellindex++).SetCellValue((bool)propInfo.Value ? "Да" : "Не");
                        }
                        else
                        {
                            row.CreateCell(cellindex++).SetCellValue(propInfo.Value.ToString());
                        }
                    }
                }

                // The automatic resizing takes really long to resize the columns with big amount of data (~30 seconds for 20k rows)
                // Autosize the columns
                if (itemsToExport.Count < AUTO_RESIZE_LIMIT)
                {
                    int numberOfColumns = sheet.GetRow(0).PhysicalNumberOfCells;
                    for (int i = 0; i <= numberOfColumns; i++)
                    {
                        sheet.AutoSizeColumn(i);
                        //put some more space
                        sheet.SetColumnWidth(i, Math.Min(sheet.GetColumnWidth(i) + 3 * 255, 256 * 255));

                        // Add this line to ensure that the GC has collected everything up
                        // In some versions (e.g. 2.0.1) NPOI is not disposing the BitMap objects that are needed for the AutoSize feature
                        GC.Collect();
                    }
                }

                // Save the Excel spreadsheet to a MemoryStream and return it to the client
                using (var exportData = new MemoryStream())
                {
                    workbook.Write(exportData);
                    return exportData.ToArray();
                }
            }
            catch (OutOfMemoryException ex) // This error will occur if the file is too big
            {
                _logger.LogException(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw ex;
            }
        }

        private PropInfo? GetPropInfo(object? src, string propName)
        {
            if (propName == null) throw new ArgumentNullException(nameof(propName));

            if (propName.Contains('.'))
            {
                var parts = propName.Split(new[] { '.' }, 2);
                return GetPropInfo(GetPropInfo(src, parts[0])?.Value, parts[1]);
            }

            var property = src?.GetType().GetProperty(propName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);

            return property != null ? new PropInfo
            {
                Value = property.GetValue(src, null),
                Type = property.PropertyType
            } : null;
        }

        private bool IsMatch(string key, string name)
        {
            return string.Equals(key.Split(new[] { '.' })[0].ToUpper(), name.ToUpper());
        }

        private class PropInfo
        {
            public Type Type { get; set; } = null!;

            public object? Value { get; set; }
        }
    }
}
