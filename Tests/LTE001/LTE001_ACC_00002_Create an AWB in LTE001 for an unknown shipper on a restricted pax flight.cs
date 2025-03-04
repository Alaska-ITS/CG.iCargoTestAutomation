using iCargoXunit.Fixtures;
using iCargoXunit.pages;
using OpenQA.Selenium;
using Xunit;
using System;
using iCargoXunit.utilities;
using System.Reactive;
using AngleSharp.Dom.Events;

namespace iCargoXunit.Tests.LTE001
{

    public class LTE001_ACC_00002_Create_an_AWB_in_LTE001_for_an_unknown_shipper_on_a_restricted_pax_flight : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly homePage hp;
        private readonly CreateShipmentPage csp;
        public static IEnumerable<object[]> TestData_LTE_0002 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("LTE001_CreateShipment_TestData.xlsx"), "LTE001_ACC_00002");


        public LTE001_ACC_00002_Create_an_AWB_in_LTE001_for_an_unknown_shipper_on_a_restricted_pax_flight(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            csp = pageObjectManager.GetCreateShipmentPage();
        }

        [Theory]
        [MemberData(nameof(TestData_LTE_0002))]
        public void LTE001_ACC_00002_LoginAndCreateShipment(string agentCode,
       string unknownshipper, string consigneeCode, string origin,
       string destination, string productCode, string scc, string commodity,
       string shipmentdesc, string serviceCargoClass, string piece,
       string weight, string chargeType, string modeOfPayment, string cartType)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: LTE001_ACC_00001_LoginAndCreateShipment");
                // 1️⃣ Navigate to LTE001 create shipment Page
                hp.SwitchStation(origin);
                hp.enterScreenName("LTE001");

                //NOTE:- need to add "User enters into the  iCargo 'Create Shipment' page successfully"

                //Clicking on the List button
                csp.SwitchToLTEContentFrame();
                csp.ClickOnAwbTextBox();
                csp.ClickOnListButton();

                //Entering the Participant details
                csp.EnterParticipantDetailsWithUnknownShipper(agentCode, unknownshipper, consigneeCode);

                //Clicking on the ContinueParticipant button
                csp.ClickOnContinueParticipantButton();

                //Entering the Certificate details
                csp.EnterCertificateDetails();

                //Clicking on the ContinueCertificate button
                csp.ClickOnContinueCertificateButton();

                //Entering the Shipment details");
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

                //ser saves the shipment details validate error message and capture AWB number
                csp.SaveShipmentCaptureAWB("The Shipper does not have a Valid Certificate Type");


            }
            catch (Exception ex)
            {
                Console.WriteLine($" Test Failed: {ex.Message}");
                Assert.False(true, $"Test failed due to exception: {ex.Message}");
            }
        }
    }
}
