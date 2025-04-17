

using iCargoUIAutomation.Fixtures;
using iCargoUIAutomation.pages;
using OpenQA.Selenium;
using Xunit;
using System;
using iCargoUIAutomation.utilities;

namespace iCargoUIAutomation.Tests.LTE001
{

    public class LTE001_ACC_00009_Create_and_accept_an_AWB_for_a_known_shipper_booked_on_pax_flights_without_screening_details : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly HomePage hp;
        private readonly CreateShipmentPage csp;
        private static string totalPaybleAmount;
        public static IEnumerable<object[]> TestData_LTE_0009 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("LTE001_CreateShipment_TestData_17Apr.xlsx"), "LTE001_ACC_00009");


        public LTE001_ACC_00009_Create_and_accept_an_AWB_for_a_known_shipper_booked_on_pax_flights_without_screening_details(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            csp = pageObjectManager.GetCreateShipmentPage();
        }



        [Theory]
        [Trait("Category", "LTE001")]
        [Trait("Category", "LTE001_ACC_00009")]
        [MemberData(nameof(TestData_LTE_0009))]
        public void Create_and_accept_an_AWB_for_a_known_shipper_booked_on_pax_flights_without_screening_details(string agentCode,
        string shipperCode, string consigneeCode, string origin,
        string destination, string productCode, string scc, string commodity,
        string shipmentdesc, string serviceCargoClass, string piece,
        string weight, string chargeType, string modeOfPayment)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: LTE001_ACC_00001_LoginAndCreateShipment");
                // 1️⃣ Navigate to LTE001 create shipment Page
                hp.SwitchStation(origin);
                hp.enterScreenName("LTE001");

                //Clicking on the List button
                csp.SwitchToLTEContentFrame();
                csp.ClickOnAwbTextBox();
                csp.ClickOnListButton();

                //Entering the Participant details
                csp.EnterParticipantDetailsAsync(agentCode, shipperCode, consigneeCode);

                //Clicking on the ContinueParticipant button
                csp.ClickOnContinueParticipantButton();

                //Entering the Certificate details
                csp.EnterCertificateDetails();

                //Clicking on the ContinueCertificate button
                csp.ClickOnContinueCertificateButton();

                //Entering the Shipment details
                csp.EnterShipmentDetails(origin, destination, productCode, scc, commodity, shipmentdesc, serviceCargoClass, piece, weight);

                //Clicking on the ContinueShipment button
                csp.ClickOnContinueShipmentButton();

                csp.ClickOnSelectFlightButton();

                csp.BookWithSpecificFlightType("Combination");

                //Clicking on the ContinueFlightDetails button
                csp.ClickOnContinueFlightDetailsButton();

                csp.EnterChargeDetails(chargeType, modeOfPayment);

                //Clicking on the CalculateCharges button
                csp.ClickOnCalculateChargeButton();
                csp.ClickingYesOnPopupWarnings("");

                //Clicking on the ContinueChargeDetails button
                csp.ClickOnContinueChargeButton();

                //Entering the Acceptance details
                csp.EnterAcceptanceDetails();

                //Clicking on the ContinueAcceptanceDetails butto
                csp.ClickOnContinueAcceptanceButton();

                //Clicking on the ContinueScreeningDetails button
                csp.ClickOnContinueScreeningButton();

                //Checking the AWB_Verified checkbox
                csp.ClickOnAWBVerifiedCheckbox();

                //Saving shipment and validate the popped up messages for a Confirmed AWB");
                string awb = csp.SaveShipmentValidateWarningConfirmedAWB("Blocked for screening");




            }
            catch (Exception ex)
            {
                Console.WriteLine($" Test Failed: {ex.Message}");
                Assert.False(true, $"Test failed due to exception: {ex.Message}");


            }
        }
    }
}




