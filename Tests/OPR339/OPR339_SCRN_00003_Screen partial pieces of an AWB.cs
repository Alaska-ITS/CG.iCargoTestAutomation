using iCargoUIAutomation.pages;
using iCargoUIAutomation.Fixtures;
using iCargoUIAutomation.pages;
using iCargoUIAutomation.utilities;
using log4net.Util;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenQA.Selenium.BiDi.Modules.BrowsingContext.Locator;
namespace iCargoUIAutomation.Tests.OPR339
{
    public class OPR339_SCRN_00003_Screen_partial_pieces_of_an_AWB : IClassFixture<TestFixture>
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
        private CreateShipmentPage csp;
        private readonly HomePage hp;
        private ScreeningPage sp;

        private static string totalPaybleAmount;

        public static IEnumerable<object[]> TestData_OPR339_0003 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("OPR339_SecurityScreening_TestData.xlsx"), "OPR339_SCRN_00003");

        public OPR339_SCRN_00003_Screen_partial_pieces_of_an_AWB(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            csp = pageObjectManager.GetCreateShipmentPage();
            sp = pageObjectManager.GetScreeningPage();
        }

        [Theory]
        [Trait("Category", "OPR339")]
        [Trait("Category", "OPR339_SCRN_00003")]
        [MemberData(nameof(TestData_OPR339_0003))]

        public void ScreenpartialpiecesofanAWB(
            string agentCode, string shipperCode, string consigneeCode, string origin,
            string destination, string productCode, string scc, string commodity,
            string shipmentdesc, string serviceCargoClass, string piece,
            string weight, string chargeType, string modeOfPayment, string slatedPieces)
        {
            try
            {
                Console.WriteLine("🔹 Starting test:OPR339_SCRN_00003_Screen_partial_pieces_of_an_AWB");

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
                csp.ClickOnContinueScreeningButton();
                csp.ClickOnAWBVerifiedCheckbox();
                csp.SaveDetailsWithChargeType(chargeType, "Blocked for screening");

                hp.enterScreenName("OPR339");

                sp.SwitchToScreeningOPR339Frame();
                sp.EnterAWBNumber();
                sp.ClickListButton();
                sp.ClickScreeningPopUp();
                sp.EnterScreeningDetailsForHalfSlatedPieces("Transfer Manifest Verified", "Pass", slatedPieces);
                sp.EnterSecurityDetails();
                sp.VerifyScreeningDetailsSavedSuccessfully();
                
                hp.enterScreenName("LTE001");

                sp.EnterAwbNumberLTE();
                csp.VerifyAndUpdateScreeningDetails();
                csp.ClickOnContinueScreeningButton();
                (string awb, totalPaybleAmount) = csp.SaveShipmentDetailsAndHandlePopups();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test Failed! Error: {ex.Message}");
                throw;
            }
        }
    }
}
