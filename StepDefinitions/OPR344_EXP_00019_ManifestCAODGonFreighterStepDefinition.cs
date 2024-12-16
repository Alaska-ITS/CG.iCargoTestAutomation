using iCargoUIAutomation.pages;
using iCargoUIAutomation.utilities;
using log4net;
using log4net.Filter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using TechTalk.SpecFlow;

namespace iCargoUIAutomation.StepDefinitions
{
    [Binding]
    public class OPR344_EXP_00020_ManifestCAODGonFreighterStepDefinition : BasePage
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
        private CreateShipmentPage csp;
        private FogsQAPage fogsQAPage;


        ILog Log = LogManager.GetLogger(typeof(OPR344_EXP_00020_ManifestCAODGonFreighterStepDefinition));


        public OPR344_EXP_00020_ManifestCAODGonFreighterStepDefinition(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            this.pageObjectManager = new PageObjectManager(driver);
            this.csp = pageObjectManager.GetCreateShipmentPage();
            this.fogsQAPage = pageObjectManager.GetFogsQAPage();

        }


        [Given(@"User lauches the Url of Fogs QA Portal")]
        public void GivenUserLauchesTheUrlOfFogsQAPortal()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                fogsQAPage.OpenFogsPortal();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [Then(@"User enters into the  Fogs QA Portal '([^']*)' page successfully")]
        public void ThenUserEntersIntoTheFogsQAPortalPageSuccessfully(string pageName)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                Log.Info("Step: Verifying the page title");
                string expectedPageTitle = pageName;
                string actualPageTitle = driver.Title;
                Assert.AreEqual(expectedPageTitle, actualPageTitle);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User enters the station '([^']*)' and flightNumber in Fogs QA Portal")]
        public void WhenUserEntersTheStationAndFlightNumberInFogsQAPortal(string stationCode)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                fogsQAPage.EnterStation(stationCode);
                csp.EnterFlightinFogsQA();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }


        [When(@"User clicks on the View button")]
        public void WhenUserClicksOnTheViewButton()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                fogsQAPage.ClickView();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User should see the manifest details appeared")]
        public void WhenUserShouldSeeTheManifestDetailsAppeared()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                fogsQAPage.ValidateManifestDetails();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }


        [Then(@"User validates the handling code '([^']*)' under Manifest details")]
        public void ThenUserValidatesTheHandlingCodeUnderManifestDetails(string handlingCode)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                csp.ValidateHandlingCodeinFogsQA(handlingCode);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [Then(@"User logs out from the Fogs QA application")]
        public void ThenUserLogsOutFromTheFogsQAApplication()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                fogsQAPage.LogOutFogsQAPortal();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

    }
}
