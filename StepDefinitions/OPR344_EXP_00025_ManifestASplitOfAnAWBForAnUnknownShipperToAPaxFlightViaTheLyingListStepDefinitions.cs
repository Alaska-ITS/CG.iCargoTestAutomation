using iCargoUIAutomation.pages;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace iCargoUIAutomation.StepDefinitions
{    
    [Binding]
    public class OPR344_EXP_00025_ManifestASplitOfAnAWBForAnUnknownShipperToAPaxFlightViaTheLyingListStepDefinitions:BasePage
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
        private CreateShipmentPage csp;
        private ExportManifestPage emp;
        public OPR344_EXP_00025_ManifestASplitOfAnAWBForAnUnknownShipperToAPaxFlightViaTheLyingListStepDefinitions(IWebDriver driver):base(driver)
        {
            this.driver = driver;
            this.pageObjectManager = new PageObjectManager(driver);
            this.csp = pageObjectManager.GetCreateShipmentPage();
            this.emp = pageObjectManager.GetExportManifestPage();

        }
        [When(@"User filterouts the Booked AWB from the Lying List, Split And assign with Pieces ""([^""]*)""")]
        public void WhenUserFilteroutsTheBookedAWBFromTheLyingListSplitAndAssignWithPieces(string splitPieces)
        {
            Hooks.Hooks.createNode();
            emp.FilterOutLyingListAWBSplitAndAssign(splitPieces);
        }

    }
}
