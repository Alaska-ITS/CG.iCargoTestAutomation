using iCargoXunit.Fixtures;
using iCargoXunit.pages;
using iCargoXunit.utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using Xunit;

namespace iCargoXunit.Tests.OPR344
{
    public class OPR344_EXP_00014_Assign_an_AWB_to_a_pre_built_cart_by_typing_in_the_AWB_number : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly homePage hp;
        private readonly CreateShipmentPage csp;
        private readonly ExportManifestPage emp;
        private static string totalPaybleAmount;

        public static IEnumerable<object[]> TestData_OPR344_00014 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("OPR344_ExportManifest_TestData.xlsx"), "OPR344_EXP_00014");

        public OPR344_EXP_00014_Assign_an_AWB_to_a_pre_built_cart_by_typing_in_the_AWB_number(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            csp = pageObjectManager.GetCreateShipmentPage();
            emp = pageObjectManager.GetExportManifestPage();
        }

        [Theory]
        [MemberData(nameof(TestData_OPR344_00014))]
        public void OPR344_EXP_00014_Assign_AWB_to_a_pre_built_cart_by_typing_in_the_AWB_number(
            string agent, string shipper, string consignee, string origin, string destination, string productCode, string scc, string commodity, string shipmentdesc, string serviceCargoClass, string piece,
            string weight, string chargeType, string modeOfPayment, string cartType, string awbNumber, string flightNumber)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: OPR344_EXP_00014_Assign_an_AWB_to_a_pre_built_cart_by_typing_in_the_AWB_number");

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
                csp.EnterFlightinExportManifest(flightNumber);
                csp.EnterFlightDateExportManifest();
                emp.ClickOnListButton();

                // Step 7: Create ULD/Cart and assign AWB to pre-built cart
                csp.CreateNewULDCartExportManifest(cartType, destination);

                //emp.ClickOnEditULDButtonForPreBuiltCart(); // Edit pre-built cart ERROR

                // Step 8: Type the AWB number and assign pieces to the pre-built cart
                csp.AssignAWBToPreBuiltCartByAWBTypingExportManifest(piece);

                // Step 9: Manifest the AWB
                emp.clickOnManifestButton();

                // Step 10: Close the Print PDF window and validate the AWB status
                emp.ClosePrintPDFWindow();
                emp.ValidateAWBStatusInExportManifest("Manifested");

                // Step 11: Close Export Manifest screen
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
