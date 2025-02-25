using iCargoXunit.Fixtures;
using iCargoXunit.pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iCargoXunit.utilities;
using System.Reflection;
using Xunit.Abstractions;


namespace iCargoXunit.Tests.CAP018
{
    public class  CAP018_BKG_00003_Create_a_booking_for_an_unknown_shipper_on_a_pax_flight : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly homePage hp;
        private readonly MaintainBookingPage mbp;       

        public static IEnumerable<object[]> TestData_0003 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("CAP018_MaintainBooking_TestData.xlsx"), "CAP018_BKG_00003");
      
        public CAP018_BKG_00003_Create_a_booking_for_an_unknown_shipper_on_a_pax_flight(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            mbp = pageObjectManager.GetMaintainBookingPage();
        }
        [Theory]
        [MemberData(nameof(TestData_0003))]
        
        public void CAP018_BKG_00003_Create_a_booking_for_an_unknown_shipper_pax_general_flight(
           string origin, string destination, string productCode, string commodity, string piece,
           string weight, string agentCode, string shipperCode, string consigneeCode)
        {
            try
            {

                Console.WriteLine($"🔹Starting test: {MethodBase.GetCurrentMethod().Name}");

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
                Console.WriteLine($"Test Failed: {ex.Message}");
                Assert.Fail($"Test failed due to exception: {ex.Message}");
            }
        }

    }
}
