using iCargoUIAutomation.pages;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;

namespace iCargoUIAutomation.utilities
{
    public class ExcelFileConfig
    {
        public void AppendDataToExcel(string filePath, string date, string time, string screenName, string scenarioName, string awbNumber, string origin, string destination, string productCode, string agentCode, string shipperCode, string consigneeCode, string commodityCode, string pieces, string weight)
        {
            // Set the license context for EPPlus 5.x or later
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            FileInfo fileInfo = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                if (worksheet == null)
                {
                    // Create a worksheet if it does not exist
                    worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    // Add headers
                    worksheet.Cells["A1"].Value = "Date";
                    worksheet.Cells["B1"].Value = "Time";
                    worksheet.Cells["C1"].Value = "Screen Name";
                    worksheet.Cells["D1"].Value = "Scenario Name";
                    worksheet.Cells["E1"].Value = "AWB Number";
                    worksheet.Cells["F1"].Value = "Origin";
                    worksheet.Cells["G1"].Value = "Destination";
                    worksheet.Cells["H1"].Value = "Product Code";
                    worksheet.Cells["I1"].Value = "Agent Code";
                    worksheet.Cells["J1"].Value = "Shipper Code";
                    worksheet.Cells["K1"].Value = "Consignee Code";
                    worksheet.Cells["L1"].Value = "Commodity Code";
                    worksheet.Cells["M1"].Value = "Pieces";
                    worksheet.Cells["N1"].Value = "Weight";
                }

                int rowCount = worksheet.Dimension?.Rows ?? 0;

                // Append new row with data
                worksheet.Cells[rowCount + 1, 1].Value = date;
                worksheet.Cells[rowCount + 1, 2].Value = time;
                worksheet.Cells[rowCount + 1, 3].Value = screenName;
                worksheet.Cells[rowCount + 1, 4].Value = scenarioName;
                worksheet.Cells[rowCount + 1, 5].Value = awbNumber;
                worksheet.Cells[rowCount + 1, 6].Value = origin;
                worksheet.Cells[rowCount + 1, 7].Value = destination;
                worksheet.Cells[rowCount + 1, 8].Value = productCode;
                worksheet.Cells[rowCount + 1, 9].Value = agentCode;
                worksheet.Cells[rowCount + 1, 10].Value = shipperCode;
                worksheet.Cells[rowCount + 1, 11].Value = consigneeCode;
                worksheet.Cells[rowCount + 1, 12].Value = commodityCode;
                worksheet.Cells[rowCount + 1, 13].Value = pieces;
                worksheet.Cells[rowCount + 1, 14].Value = weight;

                package.Save();
            }
            MaintainBookingPage.awbNumber = "";
            MaintainBookingPage.globalOrigin = "";
            MaintainBookingPage.globalDestination = "";
            MaintainBookingPage.globalProductCode = "";
            MaintainBookingPage.globalAgentCode = "";
            MaintainBookingPage.globalShipperCode = "";
            MaintainBookingPage.globalConsigneeCode = "";
            MaintainBookingPage.globalCommodityCode = "";
            MaintainBookingPage.globalPieces = "";
            MaintainBookingPage.globalWeight = "";
            CreateShipmentPage.awb_num = "";
            CreateShipmentPage.origin = "";
            CreateShipmentPage.destination = "";
            CreateShipmentPage.agentCode = "";
            CreateShipmentPage.shipperCode = "";
            CreateShipmentPage.consigneeCode = "";
            CreateShipmentPage.productCode = "";
            CreateShipmentPage.commodityCode = "";
            CreateShipmentPage.pieces = "";
            CreateShipmentPage.weight = "";
        }
    }
}
