using iCargoUIAutomation.Fixtures;
using iCargoUIAutomation.pages;
using OpenQA.Selenium;
using Xunit;
using System;
using iCargoUIAutomation.utilities;

namespace iCargoUIAutomation.Tests.CAP018
{
    public class CAP018_BKG_00001_CreateBookingTests : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly homePage hp;
        private readonly MaintainBookingPage mbp;
        

        public static IEnumerable<object[]> TestData_CAP018_0001 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("CAP018_MaintainBooking_TestData.xlsx"), "CAP018_BKG_00001");
        public CAP018_BKG_00001_CreateBookingTests(TestFixture fixture)
        {
            driver = fixture.Driver; 
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            mbp = pageObjectManager.GetMaintainBookingPage();
        }

        [Theory]
        [MemberData(nameof(TestData_CAP018_0001))]
        [Trait("Category", "CAP018")]
        [Trait("Category", "CAP018_BKG_00001")]
        public void CAP018_BKG_00001_LoginAndCreateShipment(
            string origin, string destination, string productCode, string commodity, string piece,
           string weight, string agentCode, string shipperCode, string consigneeCode)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: CAP018_BKG_00001_LoginAndCreateShipment");

                // 1️⃣ Navigate to CAP018 Maintain Booking Page
                hp.enterScreenName("CAP018");
                mbp.SwitchToCAP018Frame();                

                // 2️⃣ Create New Booking
                mbp.ClickNewListButton();
                mbp.EnterShipmentDetails(origin, destination, productCode, agentCode);
                mbp.EnterShipperConsigneeDetails(shipperCode, consigneeCode);
                mbp.EnterCommodityDetails(commodity, piece, weight);

                // 3️⃣ Select Flight & Save Booking
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
