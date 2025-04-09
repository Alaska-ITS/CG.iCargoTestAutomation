using iCargoUIAutomation.pages;
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


namespace iCargoUIAutomation.Tests.WHS001
{
    public class WHS011_ABND_00002_Abandon_a_partial_shipment : IClassFixture<TestFixture>
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
        private CreateShipmentPage csp;
        private readonly HomePage hp;
        private WarehouseShipmentEnquiry wse;

        private static string totalPaybleAmount;

        public static IEnumerable<object[]> TestData_WHS011_0002 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("WSH011_WarehouseShipmentEnquiry_TestData.xlsx"), "WSH011_WSE_00002");

        public WHS011_ABND_00002_Abandon_a_partial_shipment(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            csp = pageObjectManager.GetCreateShipmentPage();
            wse = pageObjectManager.GetWarehouseShipmentEnquiry();

        }

        [Theory]
        [MemberData(nameof(TestData_WHS011_0002))]
        [Trait("Category", "WSH002")]
        [Trait("Category", "WHS011_WSE_00002")]

        public void Abandonapartial_shipment(
            string agentCode, string shipperCode, string consigneeCode, string origin,
            string destination, string productCode, string scc, string commodity,
            string shipmentdesc, string serviceCargoClass, string piece,
            string weight, string chargeType, string modeOfPayment, string abnparpieces, string abrparweight)
        {
            try
            {
                Console.WriteLine("🔹 Starting test:WHS011_ABND_00002_Abandon_a_partial_shipment");

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

                hp.enterScreenName("WHS011");

                wse.SwitchToWarehouseFrame();
                wse.EnterDestination();
                wse.ClickListButton();
                wse.ClickOnCheckbox();
                wse.ClickAbandonShipmentButton();
                wse.ClickOnAbandonCheckBox();
                wse.SelectfromDropdown();
                wse.EnterPartialPiecesAndWeight(abnparpieces, abrparweight);
                wse.SelectfromDrpdwnReasonCode();
                wse.EnterRemarks("PCS NOT FND ON FLOOR");
                wse.ClickonAbndnSavebttn();
                wse.ValidatePopupAbndnScren("The Selected Shipment has been abandoned");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test Failed! Error: {ex.Message}");
                throw;
            }
        }

    }
}


