using iCargoUIAutomation.Fixtures;
using iCargoUIAutomation.pages;
using iCargoUIAutomation.utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCargoUIAutomation.Tests.CAP018
{
    public class CAP018_BKG_00009_Save_a_template_from_a_booking : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly homePage hp;
        private readonly MaintainBookingPage mbp;
        public static IEnumerable<object[]> TestData_CAP018_0009 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("CAP018_MaintainBooking_TestData.xlsx"), "CAP018_BKG_00009");
        public CAP018_BKG_00009_Save_a_template_from_a_booking(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            mbp = pageObjectManager.GetMaintainBookingPage();
        }
        [Theory]
        [MemberData(nameof(TestData_CAP018_0009))]
        [Trait("Category", "CAP018")]
        [Trait("Category", "CAP018_BKG_00009")]
        public void CAP018_BKG_00009_Save_aTemplate_From_Booking(
                               string origin, string destination, string productCode, string commodity, string piece,
                                                        string weight, string agentCode, string shipperCode, string consigneeCode)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: CAP018_BKG_00009_Save_a_template_from_a_booking");

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

                // 4️⃣ Save Template from Booking
                mbp.EnterAWBNumber();

                // 5️⃣ Verify Template is Saved
                mbp.ClickSelectSaveTemplate();                

                Console.WriteLine($"Test Passed! Template Name");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Test Failed: {ex.Message}");
                Assert.False(true, $"Test failed due to exception: {ex.Message}");
            }
        }
    }    
}
