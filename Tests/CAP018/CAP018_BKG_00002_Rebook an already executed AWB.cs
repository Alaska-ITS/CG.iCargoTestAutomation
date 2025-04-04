using iCargoUIAutomation.Fixtures;
using iCargoUIAutomation.pages;
using OpenQA.Selenium;
using Xunit;
using System;
using iCargoUIAutomation.utilities;

namespace iCargoUIAutomation.Tests.CAP018
{
    public class CAP018_BKG_00002_RebookAnAlreadyExecutedAWB : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly homePage hp;
        private readonly MaintainBookingPage mbp;


        public static IEnumerable<object[]> TestData_CAP018_0002 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("CAP018_MaintainBooking_TestData.xlsx"), "CAP018_BKG_00002");
        public CAP018_BKG_00002_RebookAnAlreadyExecutedAWB(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            mbp = pageObjectManager.GetMaintainBookingPage();
        }

        [Theory]
        [MemberData(nameof(TestData_CAP018_0002))]
        [Trait("Category", "CAP018")]
        [Trait("Category", "CAP018_BKG_00002")]
        public void CAP018_BKG_00001_LoginAndCreateShipment(
            string awb)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: CAP018_BKG_00002_RebookAnAlreadyExecutedAWB");

                // 1️⃣ Navigate to CAP018 Maintain Booking Page
                hp.enterScreenName("CAP018");
                mbp.SwitchToCAP018Frame();

                // 2️⃣ Rebook the already executed AWB
                mbp.EnterAWBNumberFromStock(awb);
                mbp.ClickNewListButton();


                // 3️⃣ Delete Flight & Save New Flight                 
                mbp.DeleteAddFlights();
                mbp.AddNewFlightDetails();
                mbp.clickOnSaveButtonToSaveNewFlightDetails();
                mbp.CaptureIrregularity();

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
