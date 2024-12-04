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
    public class OPR367_IMP_00003_UnarriveCargoThatWasArrivedInErrorStepDefinition : BasePage
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
        private CreateShipmentPage csp;
        private ExportManifestPage emp;
        private MarkFlightMovements mfm;
        private ImportManifestPage imp;


        ILog Log = LogManager.GetLogger(typeof(OPR367_IMP_00003_UnarriveCargoThatWasArrivedInErrorStepDefinition));


        public OPR367_IMP_00003_UnarriveCargoThatWasArrivedInErrorStepDefinition(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            this.pageObjectManager = new PageObjectManager(driver);
            this.csp = pageObjectManager.GetCreateShipmentPage();
            this.emp = pageObjectManager.GetExportManifestPage();
            this.mfm = pageObjectManager.GetMarkFlightMovements();
            this.imp = pageObjectManager.GetImportManifestPage();
        }




        [When(@"User selects the AWB and deletes it to unarrive the cargo")]
        public void WhenUserSelectsTheAWBAndDeletesItToUnarriveTheCargo()
        {

            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                imp.DeleteBreakdownDetails();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }

        }
    }

}
