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
//using System.Collections;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace iCargoXunit.utilities
//{
//    public class ExcelFileDataReader
//    {
//        public static IEnumerable<object[]> GetData(string filePath, string sheetName)
//        {
//            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
//            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
//            using var reader = ExcelReaderFactory.CreateReader(stream);
//            var result = reader.AsDataSet();
//            var dataTable = result.Tables[sheetName];

//            foreach (DataRow row in dataTable.Rows)
//            {
//                var values = new object[row.ItemArray.Length];
//                row.ItemArray.CopyTo(values, 0);
//                yield return values;
//            }
//        }
//    }
//}

