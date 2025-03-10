 
 using iCargoXunit.Fixtures;
using iCargoXunit.pages;
using OpenQA.Selenium;
using Xunit;
using System;
using iCargoXunit.utilities;
using System.Reactive;
using static OpenQA.Selenium.BiDi.Modules.Session.ProxyConfiguration;
using AventStack.ExtentReports.Gherkin.Model;

namespace iCargoXunit.Tests.LTE001
{

    public class LTE001_ACC_00008_Reopen_an_AWB_and_change_piece_count_and_weight_and_reexecute : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly homePage hp;
        private readonly CreateShipmentPage csp;
        private static string totalPaybleAmount;
        public static IEnumerable<object[]> TestData_LTE_0008 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("LTE001_CreateShipment_TestData.xlsx"), "LTE001_ACC_00008");


        public LTE001_ACC_00008_Reopen_an_AWB_and_change_piece_count_and_weight_and_reexecute(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            csp = pageObjectManager.GetCreateShipmentPage();
        }

        

        [Theory]
        [MemberData(nameof(TestData_LTE_0008))]
        public void Create_a_New_Shipment_Acceptance_of_that_new_shipment_screening_as_a_CGO_or_CGODG_user(string agentCode,
        string shipperCode, string consigneeCode, string origin,
        string destination, string productCode, string scc, string commodity,
        string shipmentdesc, string serviceCargoClass, string piece,
        string weight, string chargeType, string modeOfPayment, string cartType,string updatedValue, string execute)
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

                //Entering the Screening details"
                csp.EnterScreeningDetails(1, "Transfer Manifest Verified", "Pass");

                //Clicking on the ContinueScreeningDetails button
                csp.ClickOnContinueScreeningButton();

                //Checking the AWB_Verified checkbox
                csp.ClickOnAWBVerifiedCheckbox();

                //Saving all the details & handling all the popups
                (string awb, totalPaybleAmount) = csp.SaveShipmentDetailsAndHandlePopups();

                //Entering the Executed AWB number");
                csp.alreadyExecutedAWB();

                //Reopening the AWB");
                csp.reOpenAWB();

                //Verifying and Updating the Shipment Details for " + fieldToBeUpdated + " with value " + value);
                csp.VerifyAndUpdateShipmentDetails("piece&weight", updatedValue);

                //Clicking on the ContinueShipment button");
                csp.ClickOnContinueShipmentButton();

                //Verifying and Updating the Flight Details");
                csp.VerifyAndUpdateFlightDetails("piece&weight");

                //Opening the Charge Details");
                csp.OpenAndVerifyChargeDetails();

                //Clicking on the CalculateCharges button");
                csp.ClickOnCalculateChargeButton();
                csp.ClickingYesOnPopupWarnings("");

                //Clicking on the ContinueChargeDetails button");
                csp.ClickOnContinueChargeButton();

                //Verifying and Updating the Acceptance Details");
                csp.VerifyAndUpdateAcceptanceDetails();

                //Clicking on the ContinueAcceptanceDetails button");
                csp.ClickOnContinueAcceptanceButton();

                //Verifying and Updating the Screening Details");
                csp.VerifyAndUpdateScreeningDetails();

                //Clicking on the ContinueScreeningDetails button");
                csp.ClickOnContinueScreeningButton();

                //Checking the AWB_Verified checkbox");
                csp.ClickOnAWBVerifiedCheckbox();

                csp.ClickSave();
                csp.ClickingYesOnPopupWarnings("");
                csp.ClosePaymentPortalWindow();
                csp.ClickingYesOnPopupWarnings("");
                csp.ClickingYesOnPopupWarnings("");
                csp.ValidateAWBStatus("EXECUTED");




            }
            catch (Exception ex)
            {
                Console.WriteLine($" Test Failed: {ex.Message}");
                Assert.False(true, $"Test failed due to exception: {ex.Message}");


            }
        }
    }
}


