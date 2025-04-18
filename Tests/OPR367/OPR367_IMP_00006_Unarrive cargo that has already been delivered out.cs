using iCargoUIAutomation.Fixtures;
using iCargoUIAutomation.pages;
using iCargoUIAutomation.utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using static OpenQA.Selenium.BiDi.Modules.BrowsingContext.Locator;

namespace iCargoUIAutomation.Tests.OPR367
{
    public class OPR367_IMP_00006_Unarrive_cargo_that_has_already_been_delivered_out : IClassFixture<TestFixture>
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
        private CreateShipmentPage csp;
        private readonly HomePage hp;
        private ExportManifestPage emp;
        private MarkFlightMovements mfm;
        private ImportManifestPage imp;
        private DeliveryPage dp;
        private static string totalPaybleAmount;

        public static IEnumerable<object[]> TestData_OPR367_0006 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("OPR367_ImportManifest_TestData.xlsx"), "OPR367_IMP_00006");

        public OPR367_IMP_00006_Unarrive_cargo_that_has_already_been_delivered_out(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            csp = pageObjectManager.GetCreateShipmentPage();
            emp = pageObjectManager.GetExportManifestPage();
            mfm = pageObjectManager.GetMarkFlightMovements();
            imp = pageObjectManager.GetImportManifestPage();
            dp = pageObjectManager.GetDeliveryPage();
        }

        [Theory]
        [Trait("Category", "OPR367")]
        [Trait("Category", "OPR367_IMP_00006")]
        [MemberData(nameof(TestData_OPR367_0006))]

        public void Unarrivecargothathasalreadybeendeliveredout(
           string agentCode, string shipperCode, string consigneeCode, string origin,
            string destination, string productCode, string scc, string commodity,
            string shipmentdesc, string serviceCargoClass, string piece,
            string weight, string chargeType, string modeOfPayment, string awbSectionName,
            string cartType, string bdnLocation, string bdnRcvdPcs, string bdnRcvdWt)
        {
            try
            {
                Console.WriteLine("🔹 Starting test:OPR367_IMP_00006_Unarrive_cargo_that_has_already_been_delivered_out");

                hp.SwitchStation(origin);
                hp.enterScreenName("LTE001");

                csp.SwitchToLTEContentFrame();
                csp.ClickOnAwbTextBox();
                csp.ClickOnListButton();
                csp.EnterParticipantDetailsAsync(agentCode, shipperCode, consigneeCode);
                csp.ClickOnContinueParticipantButton();
                csp.EnterCertificateDetails();
                csp.ClickOnContinueCertificateButton();
                csp.EnterShipmentDetails(origin, destination, productCode, scc, commodity, shipmentdesc, serviceCargoClass, piece, weight);
                csp.ClickOnContinueShipmentButton();
                csp.ClickOnSelectFlightButton();
                csp.BookWithSpecificFlightType("Combination");
                csp.ClickOnContinueFlightDetailsButton();
                csp.EnterChargeDetails(chargeType, modeOfPayment);
                csp.ClickOnCalculateChargeButton();
                csp.ClickingYesOnPopupWarnings("");
                csp.ClickOnContinueChargeButton();
                csp.EnterAcceptanceDetails();
                csp.ClickOnContinueAcceptanceButton();
                csp.EnterScreeningDetails(1, "Transfer Manifest Verified", "Pass");
                csp.ClickOnContinueScreeningButton();
                csp.ClickOnAWBVerifiedCheckbox();
                (string awb, totalPaybleAmount) = csp.SaveShipmentDetailsAndHandlePopups();

                hp.enterScreenName("OPR344");

                emp.SwitchToManifestFrame();
                emp.ClickOnFlightTextBox();
                csp.EnterFlightinExportManifest("");
                csp.EnterFlightDateExportManifest();
                emp.ClickOnListButton();
                csp.CreateNewULDCartExportManifest(cartType, destination);
                csp.FilterOutAWBULDInExportManifest(awbSectionName);
                emp.clickOnManifestButton();
                emp.ClosePrintPDFWindow();
                emp.ValidateAWBStatusInExportManifest("Manifested");
                emp.CloseOPR344Screen();

                hp.enterScreenName("FLT006");

                mfm.SwitchToFLT006Frame();
                mfm.EnterFlightDetails();
                mfm.ClickListButton();
                mfm.EnterActualArrivalDepartureDetails("Departure");
                mfm.ClickSaveButton();
                mfm.ClickCloseButton();

                hp.enterScreenName("OPR344");

                emp.SwitchToManifestFrame();
                emp.ClickOnFlightTextBox();
                csp.EnterFlightinExportManifest("");
                csp.EnterFlightDateExportManifest();
                emp.ClickOnListButton();
                emp.CheckFlightStatusForFinalized();
                emp.CloseOPR344Screen();

                hp.SwitchStation(destination);

                hp.enterScreenName("FLT006");

                mfm.SwitchToFLT006Frame();
                mfm.EnterFlightDetails();
                mfm.ClickListButton();
                mfm.EnterActualArrivalDepartureDetails("Arrival");
                mfm.ClickSaveButton();
                mfm.ClickCloseButton();

                hp.enterScreenName("OPR367");

                imp.SwitchToImportManifestFrame();
                imp.ClickOnFlightTextBox();
                imp.EnterFlightNumber();
                imp.EnterFlightDate();
                imp.ClickOnListButton();
                imp.ClickOnBulkCheckBox();
                imp.ClickOnBreakDownButton();
                imp.EnterBreakdownDetails(bdnLocation, bdnRcvdPcs, bdnRcvdWt);
                imp.SaveBreakdownAndValidateMessage("Saved successfully. Do you want to list the saved details?");
                imp.CloseBreakdownScreen();
                imp.CloseOPR367Screen();

                hp.enterScreenName("OPR293");

                dp.SwitchToOPR293Frame();
                dp.EnterAWBNumberOPR293();
                dp.SelectAWBForDelivery();
                dp.ClickGenerateDeliveryNoteButton();
                dp.ClickingYesOnPopupWarnings("");
                dp.GetPaymentAmountValue();
                totalPaybleAmount = dp.ClickOnAddButtonHandlePaymentPortal(chargeType);
                dp.ClickAcceptPaymentButton();
                dp.DeliveryConfirmationDetails();
                dp.CaptureDeliveryDetails();
                dp.ClickingYesOnPopupWarnings("");
                dp.DeliveryReceiptWindow();

                hp.enterScreenName("OPR367");

                imp.SwitchToImportManifestFrame();
                imp.ClickOnFlightTextBox();
                imp.EnterFlightNumber();
                imp.EnterFlightDate();
                imp.ClickOnListButton();
                imp.ClickOnBulkCheckBox();
                imp.ClickOnBreakDownButton();
                imp.DeleteBreakdownDetails();
                imp.SaveBreakdownAndValidateMessage("Saved successfully. Do you want to list the saved details?");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test Failed! Error: {ex.Message}");
                throw;
            }
        }
    }
}
