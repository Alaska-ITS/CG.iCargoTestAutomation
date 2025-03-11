using iCargoXunit.Fixtures;
using iCargoXunit.pages;
using OpenQA.Selenium;
using Xunit;
using System;
using iCargoXunit.utilities;
using AventStack.ExtentReports.Gherkin.Model;

namespace iCargoXunit.Tests.CAP018
{
    public class CAP018_BKG_00004_CreateABookingGivenAnAWBFromStock : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly homePage hp;
        private readonly MaintainBookingPage mbp;


        public static IEnumerable<object[]> TestData_CAP018_0004 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("CAP018_MaintainBooking_TestData.xlsx"), "CAP018_BKG_00004");
        public CAP018_BKG_00004_CreateABookingGivenAnAWBFromStock(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            mbp = pageObjectManager.GetMaintainBookingPage();
        }

        [Theory]
        [MemberData(nameof(TestData_CAP018_0004))]
        [Trait("Category", "CAP018")]
        [Trait("Category", "CAP018_BKG_00004")]
        public void CAP018_BKG_00001_LoginAndCreateShipment(
            string awb, string origin, string destination, string productCode, string commodity, string piece,
           string weight, string agentCode, string shipperCode, string consigneeCode)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: CAP018_BKG_00004_CreateABookingGivenAnAWBFromStock");

                // 1️⃣ Navigate to CAP018 Maintain Booking Page
                hp.enterScreenName("CAP018");
                mbp.SwitchToCAP018Frame();

                // 2️⃣ Rebook the already executed AWB
                mbp.EnterAWBNumberFromStock(awb);
                mbp.ClickNewListButton();
                mbp.AWBBookingfromStock();
                mbp.EnterShipmentDetails(origin, destination, productCode, agentCode);
                mbp.UnknownShipperConsigneeDetails(shipperCode, consigneeCode);

                // 3️⃣ Delete Flight & Save New Flight
                mbp.EnterCommodityDetails(commodity, piece, weight);
                mbp.SelectFlight(productCode);
                mbp.ClickSaveButton();

                // 4️⃣ Verify AWB is Generated
                string awbNumber = mbp.CaptureAwbNumber();
                Assert.False(string.IsNullOrEmpty(awbNumber), "AWB Number should be generated.");

                Console.WriteLine($"Test Passed! AWB Number: {awbNumber}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Test Failed: {ex.Message}");
                Assert.False(true, $"Test failed due to exception: {ex.Message}");
            }
        }
    }
}
