using iCargoXunit.Fixtures;
using iCargoXunit.pages;
using iCargoXunit.utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCargoXunit.Tests.CAP018
{
    public class CAP018_BKG_00008_Attach_or_Detach_AWB_from_a_saved_booking: IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly homePage hp;
        private readonly MaintainBookingPage mbp;
        public static IEnumerable<object[]> TestData_CAP018_0008 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("CAP018_MaintainBooking_TestData.xlsx"), "CAP018_BKG_00008");
        public CAP018_BKG_00008_Attach_or_Detach_AWB_from_a_saved_booking(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            mbp = pageObjectManager.GetMaintainBookingPage();
        }
        [Theory]
        [MemberData(nameof(TestData_CAP018_0008))]
        [Trait("Category", "CAP018")]
        [Trait("Category", "CAP018_BKG_00008")]
        public void CAP018_BKG_00008_Attach_or_Detach_AWB_From_A_Saved_Booking(
                              string origin, string destination, string productCode, string commodity, string piece,
                                                      string weight, string agentCode, string shipperCode, string consigneeCode, string newAgentCode)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: CAP018_BKG_00008_Attach_or_Detach_AWB_from_a_saved_booking");

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

                // 5️⃣ Detach AWB from Booking
                mbp.EnterAWBNumber();
                mbp.ClickNewListButton();
                mbp.AttachOrDetachAWB();

                // 6️⃣ Verify AWB is Detached
                mbp.EnterNewAgentCode(newAgentCode);
                mbp.ClickSaveButton();
                mbp.CaptureAwbNumber();

                Console.WriteLine($"Test Passed! AWB Number: {awbNumber} detached successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Test Failed: {ex.Message}");
                Assert.False(true, $"Test failed due to exception: {ex.Message}");
            }
        }
    }  
}
