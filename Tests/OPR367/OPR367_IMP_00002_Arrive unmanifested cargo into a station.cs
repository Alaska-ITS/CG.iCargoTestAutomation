using AngleSharp.Dom.Events;
using iCargoXunit.Fixtures;
using iCargoXunit.pages;
using iCargoXunit.utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenQA.Selenium.BiDi.Modules.BrowsingContext.Locator;


namespace iCargoUIAutomation.Tests.OPR367
{
    public class OPR367_IMP_00002_Arrive_unmanifested_cargo_into_a_station : IClassFixture<TestFixture>
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
        private CreateShipmentPage csp;
        private readonly homePage hp;
        private ExportManifestPage emp;
        private MarkFlightMovements mfm;
        private ImportManifestPage imp;
        private static string totalPaybleAmount;

        public static IEnumerable<object[]> TestData_OPR367_0002 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("OPR367_ImportManifest_TestData.xlsx"), "OPR367_IMP_00002");

        public OPR367_IMP_00002_Arrive_unmanifested_cargo_into_a_station(TestFixture fixture)
        {

            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            csp = pageObjectManager.GetCreateShipmentPage();
            emp = pageObjectManager.GetExportManifestPage();
            mfm = pageObjectManager.GetMarkFlightMovements();
            imp = pageObjectManager.GetImportManifestPage();

        }

        [Theory]
        [MemberData(nameof(TestData_OPR367_0002))]

        public void Arriveunmanifestedcargointoastation(
            string agentCode, string shipperCode, string consigneeCode, string origin,
            string destination, string productCode, string scc, string commodity,
            string shipmentdesc, string serviceCargoClass, string piece,
            string weight, string chargeType, string modeOfPayment, string bdnLocation, string bdnRcvdPcs, string bdnRcvdWt)
        {
            try
            {
                Console.WriteLine("🔹 Starting test:OPR367_IMP_00002_Arrive_unmanifested_cargo_into_a_station");

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

                hp.SwitchStation(destination);

                hp.enterScreenName("FLT006");

                mfm.SwitchToFLT006Frame();
                mfm.EnterFlightDetails();
                mfm.ClickListButton();
                mfm.EnterActualArrivalDepartureDetails("departure");
                mfm.EnterActualArrivalDepartureDetails("arrival", 2);
                mfm.ClickSaveButton();
                mfm.ClickCloseButton();

                hp.enterScreenName("OPR367");

                imp.SwitchToImportManifestFrame();
                imp.ClickOnFlightTextBox();
                imp.EnterFlightNumber();
                imp.EnterFlightDate();
                imp.ClickOnListButton();
                imp.AddULDinImportManifest();
                imp.ClickOnBulkCheckBox();
                imp.ClickOnBreakDownButton();
                imp.HandleWarningsDuringBreakdown();
                imp.AddUpdateBreakDownDetails(bdnLocation, bdnRcvdPcs, bdnRcvdWt);
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
