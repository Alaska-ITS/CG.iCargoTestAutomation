
using iCargoXunit.Fixtures;
using iCargoXunit.pages;
using OpenQA.Selenium;
using Xunit;
using System;
using iCargoXunit.utilities;
using System.Reactive;
using static OpenQA.Selenium.BiDi.Modules.Session.ProxyConfiguration;

namespace iCargoXunit.Tests.LTE001
{

    public class LTE001_ACC_00006_Accept_a_prebooked_AWB_in_LTE001 : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly homePage hp;
        private readonly CreateShipmentPage csp;
        private static string totalPaybleAmount;
        public static IEnumerable<object[]> TestData_LTE_0006 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("LTE001_CreateShipment_TestData.xlsx"), "LTE001_ACC_00006");


        public LTE001_ACC_00006_Accept_a_prebooked_AWB_in_LTE001(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            csp = pageObjectManager.GetCreateShipmentPage();
        }

        //NOTE:- We need to integrate CCC and CGODG in one scenario

        [Theory]
        [Trait("Category", "LTE001")]
        [Trait("Category", "LTE001_ACC_00006")]
        [MemberData(nameof(TestData_LTE_0006))]
        public void Create_a_New_Shipment_Acceptance_of_that_new_shipment_screening_as_a_CGO_or_CGODG_user(string preBookedAWB, string origin, string chargeType, string modeOfPayment, string cartType)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: LTE001_ACC_00001_LoginAndCreateShipment");
                // 1️⃣ Navigate to LTE001 create shipment Page
                hp.SwitchStation(origin);
                hp.enterScreenName("LTE001");

                //Entering the AWB of a PreBooked Shipment");
                preBookedAWB = preBookedAWB.Split("-")[1];
                csp.SwitchToLTEContentFrame();
                csp.ClickOnAwbTextBox();
                csp.EnterAWBTextBox(preBookedAWB);
                csp.ClickOnListButton();

                //Opening and Verifying the Participant Details");
                csp.OpenAndVerifyParticipants();

                //Clicking on the ContinueParticipant button");
                csp.ClickOnContinueParticipantButton();

                //Entering the Certificate details");
                csp.EnterCertificateDetails();

                //Clicking on the ContinueCertificate button");
                csp.ClickOnContinueCertificateButton();

                //Opening and Verifying the Shipment Details");
                csp.OpenAndVerifyShipments();

                //Clicking on the ContinueShipment button");
                csp.ClickOnContinueShipmentButton();

                //Opening and Verifying the Flight Details");
                csp.OpenAndVerifyFlightDetails();

                //Clicking on the ContinueFlightDetails button");
                csp.ClickOnContinueFlightDetailsButton();

                //Opening the Charge Details");
                csp.OpenAndVerifyChargeDetails();

                //Entering the Charge details");
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

                //Entering the Screening details"
                csp.EnterScreeningDetails(1, "Transfer Manifest Verified", "Pass");

                //Clicking on the ContinueScreeningDetails button
                csp.ClickOnContinueScreeningButton();

                //Checking the AWB_Verified checkbox
                csp.ClickOnAWBVerifiedCheckbox();

                csp.ClickSave();
                csp.ClickingYesOnPopupWarnings("");
                csp.ClosePaymentPortalWindow();
                csp.ClickingYesOnPopupWarnings("");
                csp.ClickingYesOnPopupWarnings("");
                csp.CloseLTE001Screen();
                //Logging out from the application");
                hp.logoutiCargo();


            }
            catch (Exception ex)
            {
                Console.WriteLine($" Test Failed: {ex.Message}");
                Assert.False(true, $"Test failed due to exception: {ex.Message}");


            }
        }
    }
}





