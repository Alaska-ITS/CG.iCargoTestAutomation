using iCargoUIAutomation.Fixtures;
using iCargoUIAutomation.pages;
using iCargoUIAutomation.utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenQA.Selenium.BiDi.Modules.BrowsingContext.Locator;

namespace iCargoUIAutomation.Tests.OPR344
{
    public class OPR344_EXP_00001_Manifest_an_AWB_for_an_Unknown_Shipper_on_a_pax_flight : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly HomePage hp;
        private readonly CreateShipmentPage csp;
        private readonly ExportManifestPage emp;
        

        public static IEnumerable<object[]> TestData_OPR344_0001 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("OPR344_ExportManifest_TestData.xlsx"), "OPR344_EXP_00001");
        public OPR344_EXP_00001_Manifest_an_AWB_for_an_Unknown_Shipper_on_a_pax_flight(TestFixture fixture)
        {
            driver = fixture.Driver; 
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            csp = pageObjectManager.GetCreateShipmentPage();
            emp = pageObjectManager.GetExportManifestPage();
        }

        [Theory]
        [Trait("Category", "OPR344")]
        [Trait("Category", "OPR344_EXP_00001")]
        [MemberData(nameof(TestData_OPR344_0001))]        
        public void OPR344_EXP_00001_Manifest_An_AWB_For_An_Unknown_Shipper_on_Pax_Flight(
                       string agent, string unknownShipper, string consignee, string origin, string destination, string productCode, string scc, string commodity, string shipmentdesc, string serviceCargoClass, string piece,
                         string weight,  string chargeType, string modeOfPayment, string cartType,
                         string awbSectionName)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: OPR344_EXP_00001_Manifest_an_AWB_for_an_Unknown_Shipper_on_a_pax_flight");               

                // 1️⃣ Navigate to CAP018 Maintain Booking Page
                hp.enterScreenName("LTE001");

                // 2️⃣ Create New Booking
                csp.SwitchToLTEContentFrame();
                csp.ClickOnAwbTextBox();
                csp.ClickOnListButton();

                // 3️⃣ Select Flight & Save Booking
                csp.EnterParticipantDetailsWithUnknownShipper(agent, unknownShipper, consignee);
                csp.ClickOnContinueParticipantButton();
                csp.EnterCertificateDetails();
                csp.ClickOnContinueCertificateButton();
                csp.EnterShipmentDetails(origin, destination, productCode, scc, commodity, shipmentdesc, serviceCargoClass, piece, weight);

                // 4️⃣ Verify AWB is Generated
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

                csp.SaveShipmentCaptureAWB("The Shipper does not have a Valid Certificate Type");


                hp.enterScreenName("OPR344");
                emp.SwitchToManifestFrame();
                emp.ClickOnFlightTextBox();
                csp.EnterFlightinExportManifest("");
                csp.EnterFlightDateExportManifest();
                emp.ClickOnListButton();
                csp.CreateNewULDCartExportManifest(cartType,destination);
                csp.FilterOutAWBULDInExportManifest(awbSectionName);
                emp.ValidateOPR344WarningMessage("AWB is not accepted");
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
