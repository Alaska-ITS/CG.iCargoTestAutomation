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
    public class OPR344_EXP_00018_ManifestAWBunknownshipperfromLyingListToPaxFlight : BasePage
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;        
        private ExportManifestPage emp;
       


        ILog Log = LogManager.GetLogger(typeof(OPR344_EXP_00018_ManifestAWBunknownshipperfromLyingListToPaxFlight));


        public OPR344_EXP_00018_ManifestAWBunknownshipperfromLyingListToPaxFlight(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            this.pageObjectManager = new PageObjectManager(driver);            
            this.emp = pageObjectManager.GetExportManifestPage();

        }

        [When(@"User validates the error message in Check Embargo as ""([^""]*)""")]
        public void WhenUserValidatesTheErrorMessageInCheckEmbargoAs(string expectedMessage)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.ValidateErrorCheckEmbargoPopup(expectedMessage);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }

        }
       

    }
}
