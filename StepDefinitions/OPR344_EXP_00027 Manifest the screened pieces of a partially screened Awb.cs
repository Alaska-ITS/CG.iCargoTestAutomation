using iCargoUIAutomation.pages;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace iCargoUIAutomation.StepDefinitions
{
    [Binding]
    public class OPR344_EXP_00027_Manifest_the_screened_pieces_of_a_partially_screened_Awb : BasePage
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
        private CreateShipmentPage csp;
        private ExportManifestPage emp;

        public OPR344_EXP_00027_Manifest_the_screened_pieces_of_a_partially_screened_Awb(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            this.pageObjectManager = new PageObjectManager(driver);
            this.csp = pageObjectManager.GetCreateShipmentPage();
            this.emp = pageObjectManager.GetExportManifestPage();

        }

        [When(@"User enters the Screening details for just single piece as ""([^""]*)"" with screeingMethod as '([^']*)' and ScreeningResult as '([^']*)'")]
        public void WhenUserEntersTheScreeningDetailsForJustSinglePieceAsWithScreeingMethodAsAndScreeningResultAs(string piece, string method, string result)
        {
            Hooks.Hooks.createNode();
            csp.WhenUserEntersTheScreeningDetailsForJustSinglePieceAsWithScreeingMethodAsAndScreeningResultAs(piece, method, result);
        }






    }
}
