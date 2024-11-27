using iCargoUIAutomation.pages;
using iCargoUIAutomation.utilities;
using log4net;
using log4net.Filter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace iCargoUIAutomation.StepDefinitions
{
    [Binding]
    public class OPR367_IMP_00002_ArriveUnmanifestedCargoIntoaStationStepDefinition : BasePage
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
        private CreateShipmentPage csp;
        private ExportManifestPage emp;
        private MarkFlightMovements mfm;
        private ImportManifestPage imp;


        ILog Log = LogManager.GetLogger(typeof(OPR367_IMP_00002_ArriveUnmanifestedCargoIntoaStationStepDefinition));


        public OPR367_IMP_00002_ArriveUnmanifestedCargoIntoaStationStepDefinition(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            this.pageObjectManager = new PageObjectManager(driver);
            this.csp = pageObjectManager.GetCreateShipmentPage();
            this.emp = pageObjectManager.GetExportManifestPage();
            this.mfm = pageObjectManager.GetMarkFlightMovements();
            this.imp = pageObjectManager.GetImportManifestPage();

        }
       

        [When(@"User adds an ULD through Add ULD button")]
        public void WhenUserAddsAnULDThroughAddULDButton()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                imp.AddULDinImportManifest();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User handles the warning popups during breakdown process")]
        public void WhenUserHandlesTheWarningPopupsDuringBreakdownProcess()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                imp.HandleWarningsDuringBreakdown();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User adds breakdown details through Add / Update Breakdown Details window with BreakdownLocation ""([^""]*)"", receivedPieces ""([^""]*)"", receivedWeight ""([^""]*)""")]
        public void WhenUserAddsBreakdownDetailsThroughAddUpdateBreakdownDetailsWindowWithBreakdownLocationReceivedPiecesReceivedWeight(string bdnLocn, string rcvdPcs, string rcvdWgt)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                imp.AddUpdateBreakDownDetails(bdnLocn, rcvdPcs, rcvdWgt);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }










    }
}
