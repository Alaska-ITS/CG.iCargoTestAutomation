using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCargoUIAutomation.utilities
{
    public class ExcelFileDataReader
    {
        public static IEnumerable<object[]> GetData(string filePath, string sheetName)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var data = new List<object[]>();

            try
            {
                using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                using var reader = ExcelReaderFactory.CreateReader(stream);
                var result = reader.AsDataSet();
                var dataTable = result.Tables[sheetName];

                // Skip the header row
                bool isHeader = true;
                foreach (DataRow row in dataTable.Rows)
                {
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }

                    var values = new object[row.ItemArray.Length];
                    row.ItemArray.CopyTo(values, 0);
                    data.Add(values);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading Excel file: {ex.Message}");
            }

            foreach (var item in data)
            {
                yield return item;
            }
        }
    }
}

//using ExcelDataReader;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;

//namespace iCargoUIAutomation.utilities
//{
//    public static class ExcelFileDataReader
//    {
//        public static IEnumerable<object[]> GetData(string filePath, string sheetName)
//        {
//            var dataRows = ReadExcelData(filePath, sheetName);
//            foreach (var row in dataRows)
//            {
//                yield return row;
//            }
//        }

//        private static List<object[]> ReadExcelData(string filePath, string sheetName)
//        {
//            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
//            if (!File.Exists(filePath))
//            {
//                throw new FileNotFoundException($"Excel file not found: {filePath}");
//            }

//            var dataRows = new List<object[]>();
//            try
//            {
//                using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
//                using var reader = ExcelReaderFactory.CreateReader(stream);
//                var result = reader.AsDataSet(new ExcelDataSetConfiguration
//                {
//                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
//                    {
//                        UseHeaderRow = true
//                    }
//                });

//                if (!result.Tables.Contains(sheetName))
//                {
//                    throw new ArgumentException($"Sheet '{sheetName}' not found in {filePath}");
//                }

//                var dataTable = result.Tables[sheetName];
//                int columnCount = dataTable.Columns.Count;
//                if (columnCount == 0)
//                {
//                    throw new InvalidOperationException($"Sheet '{sheetName}' in {filePath} has no columns.");
//                }

//                foreach (DataRow row in dataTable.Rows)
//                {
//                    var values = new object[columnCount];
//                    for (int i = 0; i < columnCount; i++)
//                    {
//                        values[i] = row[i]?.ToString() ?? string.Empty;
//                    }
//                    dataRows.Add(values);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw new InvalidOperationException(
//                    $"Failed to read Excel file '{filePath}' (sheet: '{sheetName}'): {ex.Message}", ex);
//            }

//            return dataRows;
//        }
//    }
//}