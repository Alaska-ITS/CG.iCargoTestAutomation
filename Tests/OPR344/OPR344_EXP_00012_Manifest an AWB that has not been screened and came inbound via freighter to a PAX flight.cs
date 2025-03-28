using iCargoXunit.Fixtures;
using iCargoXunit.pages;
using iCargoXunit.utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using Xunit;

namespace iCargoXunit.Tests.OPR344
{
    public class OPR344_EXP_00012_Manifest_an_AWB_that_has_not_been_screened_and_came_inbound_via_freighter_to_a_PAX_flight : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly homePage hp;
        private readonly CreateShipmentPage csp;
        private readonly ExportManifestPage emp;
        private static string totalPaybleAmount;

        public static IEnumerable<object[]> TestData_OPR344_00012 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("OPR344_ExportManifest_TestData.xlsx"), "OPR344_EXP_00012");

        public OPR344_EXP_00012_Manifest_an_AWB_that_has_not_been_screened_and_came_inbound_via_freighter_to_a_PAX_flight(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            csp = pageObjectManager.GetCreateShipmentPage();
            emp = pageObjectManager.GetExportManifestPage();
        }

        [Theory]
        [MemberData(nameof(TestData_OPR344_00012))]
        public void OPR344_EXP_00012_Manifest_AWB_that_has_not_been_screened_and_came_inbound_via_freighter_to_a_PAX_flight(
            string agent, string shipper, string consignee, string origin, string destination, string productCode, string scc, string commodity, string shipmentdesc, string serviceCargoClass, string piece,
            string weight, string chargeType, string modeOfPayment, string cartType, string awbSectionName, string flightNumber)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: OPR344_EXP_00012_Manifest_an_AWB_that_has_not_been_screened_and_came_inbound_via_freighter_to_a_PAX_flight");

                // Step 1: Navigate to Create Shipment page
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
                csp.BookWithSpecificFlightType("Cargo-Only");
                csp.ClickOnContinueFlightDetailsButton();
                csp.EnterChargeDetails(chargeType, modeOfPayment);

                // Step 5: Charge calculation and acceptance details
                csp.ClickOnCalculateChargeButton();
                csp.ClickingYesOnPopupWarnings("");
                csp.ClickOnContinueChargeButton();
                csp.EnterAcceptanceDetails();
                csp.ClickOnContinueAcceptanceButton();
                csp.ClickOnContinueScreeningButton();
                csp.ClickOnAWBVerifiedCheckbox();

                (string awb, totalPaybleAmount) = csp.SaveShipmentDetailsAndHandlePopups();

                // Step 6: Navigate to Export Manifest page and enter flight details
                hp.enterScreenName("OPR344");
                emp.SwitchToManifestFrame();
                emp.ClickOnFlightTextBox();
                csp.EnterFlightinExportManifest(flightNumber);
                csp.EnterFlightDateExportManifest();
                emp.ClickOnListButton();

                // Step 7: Create ULD and filter AWB
                csp.CreateNewULDCartExportManifest(cartType, destination);
                csp.FilterSplitAndAssignAWBToULDExportManifest(awbSectionName, "");

                // Step 8: Validate error message for PAX flight
                emp.ValidateErrorMessageOnPopup("SCREENING MUST BE COMPLETED FOR MOVEMENT ON PAX AIRCRAFT");

                // Step 9: Close Export Manifest screen
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
