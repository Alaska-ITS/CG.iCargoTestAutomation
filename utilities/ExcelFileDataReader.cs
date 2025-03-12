using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCargoXunit.utilities
{
    public class ExcelFileDataReader
    {
        public static IEnumerable<object[]> GetData(string filePath, string sheetName)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var data = new List<object[]>();

            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Error: File not found at {filePath}");
                    yield break;
                }

                using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                using var reader = ExcelReaderFactory.CreateReader(stream);
                var result = reader.AsDataSet();

                if (result == null || result.Tables.Count == 0)
                {
                    Console.WriteLine("Error: No data found in Excel file.");
                    yield break;
                }

                if (!result.Tables.Contains(sheetName))
                {
                    Console.WriteLine($"Error: Sheet '{sheetName}' not found in the Excel file.");
                    yield break;
                }

                var dataTable = result.Tables[sheetName];

                if (dataTable.Rows.Count == 0)
                {
                    Console.WriteLine($"Error: Sheet '{sheetName}' is empty.");
                    yield break;
                }

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
