
     
    

using iCargoXunit.Fixtures;
using iCargoXunit.pages;
using OpenQA.Selenium;
using Xunit;
using System;
using iCargoXunit.utilities;
using System.Reactive;
using static OpenQA.Selenium.BiDi.Modules.Session.ProxyConfiguration;

namespace iCargoXunit.Tests.LTE001
{

    public class LTE001_ACC_00003_Create_a_DG_AWB_in_LTE001 : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly homePage hp;
        private readonly CreateShipmentPage csp;
        public static IEnumerable<object[]> TestData_LTE_0003 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("LTE001_CreateShipment_TestData.xlsx"), "LTE001_ACC_00003");


        public LTE001_ACC_00003_Create_a_DG_AWB_in_LTE001(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            csp = pageObjectManager.GetCreateShipmentPage();
        }
        

        [Theory]
        [MemberData(nameof(TestData_LTE_0003))]
        public void Create_a_New_DG_Shipment_Acceptance_screening_of_that_as_a_CGODG_user(string agentCode,
        string shipperCode, string consigneeCode, string origin,string destination, string productCode, string scc, 
        string commodity,string shipmentdesc, string serviceCargoClass, string piece,
        string weight, string chargeType, string modeOfPayment, string cartType, string unid,
        string propershipmntname, string pi, string netqtyperpkg, string reportable)
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
                csp.EnterParticipantDetailsAsync(agentCode, shipperCode, consigneeCode);

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

                csp.BookFlightWithAllAvailability();

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

                //Clicking on the ContinueAcceptanceDetails butto
                csp.ClickOnContinueAcceptanceButton();

                //Entering the Screening details"
                csp.EnterScreeningDetails(1, "ALT Dangerous Goods", "Pass");

                //Clicking on the ContinueScreeningDetails button
                csp.ClickOnContinueScreeningButton();

                //Save Shipment Capture Checksheet & DG Details");
                (string capturedAWB, string totalpayment) = csp.SaveWithDGAndCheckSheet(chargeType, unid, propershipmntname, pi, piece, netqtyperpkg, reportable);




            }
            catch (Exception ex)
            {
                Console.WriteLine($" Test Failed: {ex.Message}");
                Assert.False(true, $"Test failed due to exception: {ex.Message}");


            }
        }
    }
}

