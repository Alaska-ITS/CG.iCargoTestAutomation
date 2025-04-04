using iCargoUIAutomation.Fixtures;
using iCargoUIAutomation.pages;
using OpenQA.Selenium;
using iCargoUIAutomation.utilities;

namespace iCargoUIAutomation.Tests.LTE001
{
    public class LTE001_ACC_00016_Create_AWB_Employee_Shipment : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly HomePage hp;
        private readonly CreateShipmentPage csp;
        private static string totalPaybleAmount;

        public static IEnumerable<object[]> TestData_LTE_00016 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("LTE001_CreateShipment_TestData.xlsx"), "LTE001_ACC_00016");

        public LTE001_ACC_00016_Create_AWB_Employee_Shipment(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            csp = pageObjectManager.GetCreateShipmentPage();
        }

        [Theory]
        [Trait("Category", "LTE001")]
        [Trait("Category", "LTE001_ACC_00016")]
        [MemberData(nameof(TestData_LTE_00016))]

        public void Create_AWB_Employee_Shipment(string agentCode, string shipperCode, string consigneeCode, string origin,
        string destination, string productCode, string scc, string commodity,
        string shipmentdesc, string serviceCargoClass, string piece,
        string weight, string chargeType, string modeOfPayment)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: LTE001_ACC_00016_Create_AWB_Employee_Shipment");

                // 1️⃣ Navigate to LTE001 Create Shipment Page
                hp.SwitchStation(origin);
                hp.enterScreenName("LTE001");

                // Clicking on the List button
                csp.SwitchToLTEContentFrame();
                csp.ClickOnAwbTextBox();
                csp.ClickOnListButton();

                // Entering the Participant details
                csp.EnterParticipantDetailsAsync(agentCode, shipperCode, consigneeCode);
                csp.ClickOnContinueParticipantButton();

                // Entering the Certificate details
                csp.EnterCertificateDetails();
                csp.ClickOnContinueCertificateButton();

                // Entering the Shipment details
                csp.EnterShipmentDetails(origin, destination, productCode, scc, commodity, shipmentdesc, serviceCargoClass, piece, weight);
                csp.ClickOnContinueShipmentButton();

                // Selecting a flight
                csp.ClickOnSelectFlightButton();
                csp.BookWithSpecificFlightType("Combination");
                csp.ClickOnContinueFlightDetailsButton();

                // Entering the Charge details
                csp.EnterChargeDetails(chargeType, modeOfPayment);
                csp.ClickOnCalculateChargeButton();
                csp.ClickingYesOnPopupWarnings("");
                csp.ClickOnContinueChargeButton();

                // Entering the Acceptance details
                csp.EnterAcceptanceDetails();
                csp.ClickOnContinueAcceptanceButton();

                // Entering the Screening details
                csp.EnterScreeningDetails(1, "Transfer Manifest Verified", "Pass");
                csp.ClickOnContinueScreeningButton();

                //Click on save  button
                csp.ClickSave();
                // capture the check sheet for DG
                csp.CaptureCheckSheetForDG();

               // Checking the AWB_Verified checkbox");
                csp.ClickOnAWBVerifiedCheckbox();
                //click on save button
                csp.ClickSave();
                // ✅ Handling error popups and finalizing
                csp.ClickingYesOnPopupWarnings("");
                //  Saving all the details &handling all the popups");
                (string awb, totalPaybleAmount) = csp.SaveShipmentDetailsAndHandlePopups();


            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Test Failed: {ex.Message}");
                Assert.False(true, $"Test failed due to exception: {ex.Message}");
            }
        }
    }
}
