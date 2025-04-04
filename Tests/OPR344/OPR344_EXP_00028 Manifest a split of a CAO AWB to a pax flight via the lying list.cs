using iCargoUIAutomation.Fixtures;
using iCargoUIAutomation.pages;
using iCargoUIAutomation.utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
namespace iCargoUIAutomation.Tests.OPR344
{
    public class OPR344_EXP_00028_Manifest_a_split_of_a_CAO_AWB_to_a_pax_flight_via_the_lying_list : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly HomePage hp;
        private readonly CreateShipmentPage csp;
        private readonly ExportManifestPage emp;
        private static string totalPaybleAmount;

        public static IEnumerable<object[]> TestData_OPR344_00028 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("OPR344_ExportManifest_TestData.xlsx"), "OPR344_EXP_00028");

        public OPR344_EXP_00028_Manifest_a_split_of_a_CAO_AWB_to_a_pax_flight_via_the_lying_list(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            csp = pageObjectManager.GetCreateShipmentPage();
            emp = pageObjectManager.GetExportManifestPage();
        }

        [Theory]
        [MemberData(nameof(TestData_OPR344_00028))]

        public void ManifestasplitofaCAOAWBtoapaxflightviathelyinglist(
              string agent, string shipper, string consignee, string origin, 
              string destination, string productCode, string scc, string commodity, 
              string shipmentdesc, string serviceCargoClass, string piece,
               string weight, string chargeType, string modeOfPayment, 
                string unid, string propershipmntname, 
               string pi,  string netqtyperpkg, string reportable,  
               string cartType, string splitPieces, string fltnumber)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: OPR344_EXP_00028_Manifest_a_split_of_a_CAO_AWB_to_a_pax_flight_via_the_lying_list");

                hp.SwitchStation(origin);
                hp.enterScreenName("LTE001");

                csp.SwitchToLTEContentFrame();
                csp.ClickOnAwbTextBox();
                csp.ClickOnListButton();
                csp.EnterParticipantDetailsAsync(agent, shipper, consignee);
                csp.ClickOnContinueParticipantButton();
                csp.EnterCertificateDetails();
                csp.ClickOnContinueCertificateButton();
                csp.EnterShipmentDetails(origin, destination, productCode, scc, commodity, shipmentdesc, serviceCargoClass, piece, weight);
                csp.ClickOnContinueShipmentButton();
                csp.ClickOnSelectFlightButton();
                csp.BookWithSpecificFlightType("Cargo-Only");
                csp.ClickOnContinueFlightDetailsButton();
                csp.EnterChargeDetails(chargeType, modeOfPayment);
                csp.ClickOnCalculateChargeButton();
                csp.ClickingYesOnPopupWarnings("");
                csp.ClickOnContinueChargeButton();
                csp.EnterAcceptanceDetails();
                csp.ClickOnContinueAcceptanceButton();
                csp.EnterScreeningDetails(1, "ALT Dangerous Goods", "Pass");
                csp.ClickOnContinueScreeningButton();
                csp.EnterCAODGDetails(chargeType, unid, propershipmntname, pi, piece, netqtyperpkg, reportable);
                csp.SaveCAODGshipment();
                csp.CaptureCheckSheetForDG();
                csp.ClickOnAWBVerifiedCheckbox();

                (string awb, totalPaybleAmount) = csp.SaveShipmentDetailsAndHandlePopups();

                hp.enterScreenName("OPR344");

                emp.SwitchToManifestFrame();
                emp.ClickOnFlightTextBox();
                csp.EnterFlightinExportManifest(fltnumber);
                csp.EnterFlightDateExportManifest();
                emp.ClickOnListButton();
                csp.CreateNewULDCartExportManifest(cartType, destination);
                emp.FilterOutLyingListAWBSplitAndAssign(splitPieces);
                emp.ValidateWarningMessageAndCancelModal("The CAO validations failed");
            }
            catch (Exception e)
            {
                Console.WriteLine("🔴 Exception: " + e.Message);
                throw;
            }
        }
    }
}
