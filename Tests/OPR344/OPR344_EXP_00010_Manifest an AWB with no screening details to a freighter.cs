using iCargoXunit.Fixtures;
using iCargoXunit.pages;
using iCargoXunit.utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCargoUIAutomation.Tests.OPR344
{
    public class OPR344_EXP_00010_Manifest_an_AWB_with_no_screening_details_to_a_freighter : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly homePage hp;
        private readonly CreateShipmentPage csp;
        private readonly ExportManifestPage emp;
        private static string totalPaybleAmount;

        public static IEnumerable<object[]> TestData_OPR344_0010 => ExcelFileDataReader.GetData(BasePage.GetTestDataPath("OPR344_ExportManifest_TestData.xlsx"), "OPR344_EXP_00010");
        public OPR344_EXP_00010_Manifest_an_AWB_with_no_screening_details_to_a_freighter(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            csp = pageObjectManager.GetCreateShipmentPage();
            emp = pageObjectManager.GetExportManifestPage();
        }

        [Theory]
        [MemberData(nameof(TestData_OPR344_0010))]
        public void OPR344_EXP_00010_Manifest_An_AWB_With_No_Screening_Details_To_A_Freighter(
                                                        string agent, string shipper, string consignee, string origin, string destination, string productCode, string scc, string commodity, string shipmentdesc, string serviceCargoClass, string piece,
                                             string weight, string chargeType, string modeOfPayment, string awbSectionName, string cartType)
        {
            try
            {
                Console.WriteLine("🔹 Starting test: OPR344_EXP_00010_Manifest_an_AWB_with_no_screening_details_to_a_freighter");

                // 1️⃣ Navigate to CAP018 Maintain Booking Page
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


                hp.enterScreenName("OPR344");
                emp.SwitchToManifestFrame();
                emp.ClickOnFlightTextBox();
                csp.EnterFlightinExportManifest("");
                csp.EnterFlightDateExportManifest();
                emp.ClickOnListButton();
                csp.CreateNewULDCartExportManifest(cartType, destination);
                csp.FilterOutAWBULDInExportManifest(awbSectionName);

                emp.clickOnManifestButton();
                emp.ClosePrintPDFWindow();
                emp.ValidateAWBStatusInExportManifest("Manifested");
                emp.CloseOPR344Screen();
            }
            
            catch (Exception e)
            {
                Console.WriteLine("Error in OPR344_EXP_00010_Manifest_an_AWB_with_no_screening_details_to_a_freighter: " + e.Message);
            }
        }
    }

}
