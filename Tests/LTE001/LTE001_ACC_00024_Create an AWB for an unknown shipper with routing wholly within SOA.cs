using iCargoUIAutomation.Fixtures;
using iCargoUIAutomation.pages;
using OpenQA.Selenium;
using iCargoUIAutomation.utilities;
namespace iCargoUIAutomation.Tests.LTE001
{

    public class LTE001_ACC_00024_Create_an_AWB_for_an_unknown_shipper_with_routing_wholly_within_SOA : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly homePage hp;
        private readonly CreateShipmentPage csp;
        private static string totalPaybleAmount;
        public static IEnumerable<object[]> TestData_LTE_00024 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("LTE001_CreateShipment_TestData.xlsx"), "LTE001_ACC_00024");

        public LTE001_ACC_00024_Create_an_AWB_for_an_unknown_shipper_with_routing_wholly_within_SOA(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            csp = pageObjectManager.GetCreateShipmentPage();
        }

        [Theory]
        [Trait("Category", "LTE001")]
        [Trait("Category", "LTE001_ACC_00024")]
        [MemberData(nameof(TestData_LTE_00024))]

        public void Create_a_New_Shipment_Acceptance_of_that_new_shipment_screening_as_a_CGO_or_CGODG_user(string agentCode,
        string unknownshipperCode, string consigneeCode, string origin,
        string destination, string productCode, string scc, string commodity,
        string shipmentdesc, string serviceCargoClass, string piece,
        string weight, string chargeType, string modeOfPayment, string updatedValue
)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: LTE001_ACC_00001_LoginAndCreateShipment");
                // 1️⃣ Navigate to LTE001 create shipment Page
                hp.SwitchStation(origin);
                hp.enterScreenName("LTE001");

                //Clicking on the List button
                csp.SwitchToLTEContentFrame();
                csp.ClickOnAwbTextBox();
                csp.ClickOnListButton();

                //Entering the Participant details
                csp.EnterParticipantDetailsWithUnknownShipper(agentCode, unknownshipperCode, consigneeCode);

                //Clicking on the ContinueParticipant button
                csp.ClickOnContinueParticipantButton();

                //Entering the Certificate details
                csp.EnterCertificateDetails();

                //Clicking on the ContinueCertificate button
                csp.ClickOnContinueCertificateButton();

                //Entering the Shipment details
                csp.EnterShipmentDetails(origin, destination, productCode, scc, commodity, shipmentdesc, serviceCargoClass, piece, weight);

                //Clicking on the ContinueShipment button
                csp.ClickOnContinueShipmentButton();

                csp.ClickOnSelectFlightButton();

                csp.BookWithSpecificFlightType("Combination");

                //Clicking on the ContinueFlightDetails button
                csp.ClickOnContinueFlightDetailsButton();

                csp.EnterChargeDetails(chargeType, modeOfPayment);

                //Clicking on the CalculateCharges button
                csp.ClickOnCalculateChargeButton();
                csp.ClickingYesOnPopupWarnings("");

                //Clicking on the ContinueChargeDetails button
                csp.ClickOnContinueChargeButton();

                //Entering the Acceptance details
                csp.EnterAcceptanceDetails();

                //Clicking on the ContinueAcceptanceDetails button
                csp.ClickOnContinueAcceptanceButton();
                // Entering the Screening details
                csp.EnterScreeningDetails(1, "Transfer Manifest Verified", "Pass");

                //Clicking on the ContinueScreeningDetails button
                csp.ClickOnContinueScreeningButton();

                csp.ClickSave();
                csp.CaptureCheckSheetForDG();
                //Checking the AWB_Verified checkbox");
                csp.ClickOnAWBVerifiedCheckbox();
                csp.ClickSave();
                //Saving all the details & handling all the popups");
                (string awb, totalPaybleAmount) = csp.SaveShipmentDetailsAndHandlePopups();


            }
            catch (Exception ex)
            {
                Console.WriteLine($" Test Failed: {ex.Message}");
                Assert.False(true, $"Test failed due to exception: {ex.Message}");


            }
        }
    }
}









