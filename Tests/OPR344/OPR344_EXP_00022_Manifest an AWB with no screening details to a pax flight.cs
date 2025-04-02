using iCargoXunit.Fixtures;
using iCargoXunit.pages;
using iCargoXunit.utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace iCargoUIAutomation.Tests.OPR344
{
    public class OPR344_EXP_00022_Manifest_an_AWB_with_no_screening_details_to_a_pax_flight : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly homePage hp;
        private readonly CreateShipmentPage csp;
        private readonly ExportManifestPage emp;
        private static string totalPaybleAmount;

        public static IEnumerable<object[]> TestData_OPR344_00022 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("OPR344_ExportManifest_TestData.xlsx"), "OPR344_EXP_00022");

        public OPR344_EXP_00022_Manifest_an_AWB_with_no_screening_details_to_a_pax_flight(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            csp = pageObjectManager.GetCreateShipmentPage();
            emp = pageObjectManager.GetExportManifestPage();
        }

        [Theory]
        [MemberData(nameof(TestData_OPR344_00022))]

        public void ManifestanAWBwithnoscreeningdetailstoapaxlight(
              string agent, string shipper, string consignee, string origin,
              string destination, string productCode, string scc, string commodity,
              string shipmentdesc, string serviceCargoClass, string piece,
               string weight, string chargeType, string modeOfPayment,
                string awbSectionName, string cartType)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: OPR344_EXP_00022_Manifest_an_AWB_with_no_screening_details_to_a_pax_flight");

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
                csp.BookWithSpecificFlightType("Combination");
                csp.ClickOnContinueFlightDetailsButton();
                csp.EnterChargeDetails(chargeType, modeOfPayment);
                csp.ClickOnCalculateChargeButton();
                csp.ClickingYesOnPopupWarnings("");
                csp.ClickOnContinueChargeButton();
                csp.EnterAcceptanceDetails();
                csp.ClickOnContinueAcceptanceButton();
                csp.ClickOnContinueScreeningButton();
                csp.ClickOnAWBVerifiedCheckbox();
                csp.SaveWithChargeType(chargeType);

                hp.enterScreenName("OPR344");

                emp.SwitchToManifestFrame();
                emp.ClickOnFlightTextBox();
                csp.EnterFlightinExportManifest("");
                csp.EnterFlightDateExportManifest();
                emp.ClickOnListButton();
                csp.CreateNewULDCartExportManifest(cartType, destination);
                csp.FilterOutAWBULDInExportManifest(awbSectionName);
                emp.ValidateOPR344WarningMessage("Blocked for screening");
                emp.CloseOPR344Screen();
            }
            catch (Exception e)
            {
                Console.WriteLine("🔴 Exception: " + e.Message);
                throw;
            }
        }
    }
}
