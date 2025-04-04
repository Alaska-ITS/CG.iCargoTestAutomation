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
    public class CAP018_BKG_00005_Create_MultiLeg_Booking_With_Flights_That_Do_Not_Meet_Minimum_Connection_Time : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly homePage hp;
        private readonly MaintainBookingPage mbp;


        public static IEnumerable<object[]> TestData_CAP018_0005 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("CAP018_MaintainBooking_TestData.xlsx"), "CAP018_BKG_00005");
        public CAP018_BKG_00005_Create_MultiLeg_Booking_With_Flights_That_Do_Not_Meet_Minimum_Connection_Time(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            mbp = pageObjectManager.GetMaintainBookingPage();
        }

        [Theory]
        [MemberData(nameof(TestData_CAP018_0005))]
        [Trait("Category", "CAP018")]
        [Trait("Category", "CAP018_BKG_00005")]
        public void CAP018_BKG_00005_Create_a_multi_leg_booking_with_flights_that_do_not_meet_minimum_connection_time(
            string origin, string destination, string productCode, string commodity, string piece,
           string weight, string agentCode, string shipperCode, string consigneeCode)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: CAP018_BKG_00005_Create_a_multi_leg_booking_with_flights_that_do_not_meet_minimum_connection_time");

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
                string rescolr = "red";
                string reswarning = "Minimum Handling / Connection Time Fails";
                mbp.SelectMultilegflight(rescolr, reswarning, productCode);               
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Test Failed: {ex.Message}");
                Assert.False(true, $"Test failed due to exception: {ex.Message}");
            }
        }
    }
}
