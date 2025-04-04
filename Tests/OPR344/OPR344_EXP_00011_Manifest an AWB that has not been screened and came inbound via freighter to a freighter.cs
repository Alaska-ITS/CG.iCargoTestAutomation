using iCargoUIAutomation.Fixtures;
using iCargoUIAutomation.pages;
using iCargoUIAutomation.utilities;
using OpenQA.Selenium;

namespace iCargoUIAutomation.Tests.OPR344
{
    public class OPR344_EXP_00011_Manifest_an_AWB_that_has_not_been_screened_and_came_inbound_via_freighter_to_a_freighter : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly HomePage hp;
        private readonly CreateShipmentPage csp;
        private readonly ExportManifestPage emp;
        private static string totalPaybleAmount;

        public static IEnumerable<object[]> TestData_OPR344_00011 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("OPR344_ExportManifest_TestData.xlsx"), "OPR344_EXP_00011");
        public OPR344_EXP_00011_Manifest_an_AWB_that_has_not_been_screened_and_came_inbound_via_freighter_to_a_freighter(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            csp = pageObjectManager.GetCreateShipmentPage();
            emp = pageObjectManager.GetExportManifestPage();
        }

        [Theory]
        [MemberData(nameof(TestData_OPR344_00011))]
        public void OPR344_EXP_00011_Manifest_AWB_that_has_not_been_screened_and_came_inbound_via_freighter_to_a_freighter(string agent, string shipper, 
        string consignee, string origin, string destination, string productCode, string scc, string commodity, string shipmentdesc,
        string serviceCargoClass, string piece, string weight, string chargeType, string modeOfPayment, 
        string awbSectionName, string cartType)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: OPR344_EXP_00006_Manifest_DG_on_a_thru_flight");

                // 1️⃣ Navigate to CAP018 Maintain Booking Page
                hp.SwitchStation(origin);
                hp.enterScreenName("LTE001");

                // 2️⃣ Create New Booking
                csp.SwitchToLTEContentFrame();
                csp.ClickOnAwbTextBox();
                csp.ClickOnListButton();

                // 3️⃣ Select Flight & Save Booking
                csp.EnterParticipantDetailsAsync(agent, shipper, consignee);
                csp.ClickOnContinueParticipantButton();
                csp.EnterCertificateDetails();
                csp.ClickOnContinueCertificateButton();
                csp.EnterShipmentDetails(origin, destination, productCode, scc, commodity, shipmentdesc, serviceCargoClass, piece, weight);

                // 4️⃣ Verify AWB is Generated
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
                csp.ClickOnContinueScreeningButton();
                csp.ClickOnAWBVerifiedCheckbox();

                (string awb, totalPaybleAmount) = csp.SaveShipmentDetailsAndHandlePopups();
                //Entering the screen name");
                hp.enterScreenName("OPR344");

                emp.SwitchToManifestFrame();
                emp.ClickOnFlightTextBox();
                csp.EnterFlightinExportManifest("");
                csp.EnterFlightDateExportManifest();
                emp.ClickOnListButton();
                csp.CreateNewULDCartExportManifest(cartType, destination);
                csp.FilterOutAWBULDInExportManifest(awbSectionName);
                emp.ValidateErrorMessageOnPopup("SCREENING MUST BE COMPLETED FOR MOVEMENT ON PAX AIRCRAFT");
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


