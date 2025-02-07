using iCargoXunit.Fixtures;
using iCargoXunit.pages;
using OpenQA.Selenium;
using Xunit;
using System;

namespace iCargoXunit.Tests.UI
{
    public class CAP018_BKG_00001_CreateBookingTests : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly homePage hp;
        private readonly MaintainBookingPage mbp;

        public CAP018_BKG_00001_CreateBookingTests(TestFixture fixture)
        {
            driver = fixture.Driver; 
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            mbp = pageObjectManager.GetMaintainBookingPage();
        }

        [Theory]
        [InlineData("SEA", "ANC", "GENERAL", "11377", "11377", "11377", "NONSCR", "10", "200")]
        public void CAP018_BKG_00001_LoginAndCreateShipment(
            string origin, string destination, string productCode, string agentCode,
            string shipperCode, string consigneeCode, string commodity, string piece, string weight)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: CAP018_BKG_00001_LoginAndCreateShipment");

                // 1️⃣ Navigate to CAP018 Maintain Booking Page
                hp.enterScreenName("CAP018");
                mbp.SwitchToCAP018Frame();
                //Assert.True(mbp.IsOnMaintainBookingPage(), "Failed to reach Maintain Booking page.");

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
