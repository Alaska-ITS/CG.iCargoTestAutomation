using iCargoUIAutomation.Fixtures;
using iCargoUIAutomation.pages;
using iCargoUIAutomation.utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using Xunit;

namespace iCargoUIAutomation.Tests.OPR344
{
    public class OPR344_EXP_00015_Bump_a_cart_to_another_flight : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly HomePage hp;
        private readonly CreateShipmentPage csp;
        private readonly ExportManifestPage emp;
        private static string totalPaybleAmount;

        public static IEnumerable<object[]> TestData_OPR344_00015 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("OPR344_ExportManifest_TestData.xlsx"), "OPR344_EXP_00015");

        public OPR344_EXP_00015_Bump_a_cart_to_another_flight(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            csp = pageObjectManager.GetCreateShipmentPage();
            emp = pageObjectManager.GetExportManifestPage();
        }

        [Theory]
        [Trait("Category", "OPR344")]
        [Trait("Category", "OPR344_EXP_00015")]
        [MemberData(nameof(TestData_OPR344_00015))]
        public void OPR344_EXP_00015_Bump_cart_to_another_flight(
            string agent, string shipper, string consignee, string origin, string destination, string productCode, string scc, 
            string commodity, string shipmentdesc, string serviceCargoClass, string piece,
            string weight, string chargeType, string modeOfPayment, string awbSectionName, string newFlightNumber,string cartType)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: OPR344_EXP_00015_Bump_a_cart_to_another_flight");

                // Step 1: Navigate to Create Shipment page
                hp.SwitchStation(origin);
                hp.enterScreenName("LTE001");

                // Step 2: Create New Booking
                csp.SwitchToLTEContentFrame();
                csp.ClickOnAwbTextBox();
                csp.ClickOnListButton();

                // Step 3: Enter participant, certificate, and shipment details
                csp.EnterParticipantDetailsAsync(agent, shipper, consignee);
                csp.ClickOnContinueParticipantButton();
                csp.EnterCertificateDetails();
                csp.ClickOnContinueCertificateButton();
                csp.EnterShipmentDetails(origin, destination, productCode, scc, commodity, shipmentdesc, serviceCargoClass, piece, weight);

                // Step 4: Flight selection and charge details
                csp.ClickOnContinueShipmentButton();
                csp.ClickOnSelectFlightButton();
                csp.BookWithSpecificFlightType("Combination");
                csp.ClickOnContinueFlightDetailsButton();
                csp.EnterChargeDetails(chargeType, modeOfPayment);

                // Step 5: Charge calculation and acceptance details
                csp.ClickOnCalculateChargeButton();
                csp.ClickingYesOnPopupWarnings("");
                csp.ClickOnContinueChargeButton();
                csp.EnterAcceptanceDetails();
                csp.ClickOnContinueAcceptanceButton();
                csp.EnterScreeningDetails(1, "Transfer Manifest Verified", "Pass");
                csp.ClickOnContinueScreeningButton();
                csp.ClickOnAWBVerifiedCheckbox();

                (string awb, totalPaybleAmount) = csp.SaveShipmentDetailsAndHandlePopups();

                // Step 6: Navigate to Export Manifest page and enter flight details
                hp.enterScreenName("OPR344");
                emp.SwitchToManifestFrame();
                emp.ClickOnFlightTextBox();
                csp.EnterFlightinExportManifest("");
                csp.EnterFlightDateExportManifest();
                emp.ClickOnListButton();

                // Step 7: Create ULD/Cart and manifest
                csp.CreateNewULDCartExportManifest(cartType, destination);
                csp.FilterOutAWBULDInExportManifest(awbSectionName);
                emp.clickOnManifestButton();

                // Step 8: Close Print PDF window and proceed to offload
                emp.ClosePrintPDFWindow();

                // Step 9: Click on the offload ULD button and enter new flight details
                emp.ClickOnOffloadULDButton();
                emp.FillOffloadFormAndMoveToAnotherFlight(newFlightNumber, destination, "ULD");

                // Step 10: Validate warning message
                emp.ValidateErrorMessageOnPopup("The shipment is not booked to the flight");

                // Step 11: Validate AWB status in Export Manifest screen
                emp.ValidateAWBStatusInExportManifest("Offloaded");

                // Step 12: Edit the manifest and update flight details

                emp.ClickOrangePencilToEditManifest();
                emp.ClickOnFlightTextBox();
                csp.EnterFlightinExportManifest(newFlightNumber);
                csp.EnterFlightDateExportManifest();
                emp.ClickOnListButton();
                emp.clickOnManifestButton();

                // Step 13: Close the Print PDF window
                emp.ClosePrintPDFWindow();

                // Step 14: Close the Export Manifest screen
                emp.CloseOPR344Screen();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test Failed! Error: {ex.Message}");
                throw;
            }
        }
    }
}
