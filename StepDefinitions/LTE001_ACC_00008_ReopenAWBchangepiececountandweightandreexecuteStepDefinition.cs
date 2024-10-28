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
    public class LTE001_ACC_00008_ReopenAWBchangepiececountandweightandreexecute : BasePage
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
        private CreateShipmentPage csp;
        ILog Log = LogManager.GetLogger(typeof(LTE001_ACC_00008_ReopenAWBchangepiececountandweightandreexecute));

        public LTE001_ACC_00008_ReopenAWBchangepiececountandweightandreexecute(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            this.pageObjectManager = new PageObjectManager(driver);
            this.csp = pageObjectManager.GetCreateShipmentPage();
        }

        [When(@"User landed on the Homepage again")]
        public void WhenUserLandedOnTheHomepageAgain()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                Log.Info("Step: Landing on the Homepage again");
                SwitchToDefaultContent();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }


        [When(@"User enters the Executed AWB number")]
        public void WhenUserEntersTheExecutedAWBNumber()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                Log.Info("Step: Entering the Executed AWB number");
                csp.alreadyExecutedAWB();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }


        [When(@"User Reopens the AWB")]
        public void WhenUserReopensTheAWB()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                Log.Info("Step: Reopening the AWB");
                csp.reOpenAWB();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

       

        [When(@"User verifies and Update the field '([^']*)' with updated value as ""([^""]*)"" in the Shipment Details")]       
        public void WhenUserVerifiesAndUpdateTheFieldWithUpdatedValueAsInTheShipmentDetails(string fieldToBeUpdated, string value)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                Log.Info("Step: Verifying and Updating the Shipment Details for " + fieldToBeUpdated + " with value " + value);
                csp.VerifyAndUpdateShipmentDetails(fieldToBeUpdated, value);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User verifies and Update the Flight Details with '([^']*)'")]
        public void WhenUserVerifiesAndUpdateTheFlightDetailsWith(string fieldToUpdate)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                Log.Info("Step: Verifying and Updating the Flight Details");
                csp.VerifyAndUpdateFlightDetails(fieldToUpdate);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User opens & deletes the flight details")]
        public void WhenUserOpensDeletesTheFlightDetails()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                Log.Info("Step: Opening and deleting the Flight Details");
                csp.OpenAndDeleteFlight();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }



        [When(@"User verifies and Update the Acceptance Details")]
        public void WhenUserVerifiesAndUpdateTheAcceptanceDetails()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                Log.Info("Step: Verifying and Updating the Acceptance Details");
                csp.VerifyAndUpdateAcceptanceDetails();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User verifies and Update the Screening Details")]
        public void WhenUserVerifiesAndUpdateTheScreeningDetails()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                Log.Info("Step: Verifying and Updating the Screening Details");
                csp.VerifyAndUpdateScreeningDetails();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User validates the AWB is ""([^""]*)""")]
        public void WhenUserValidatesTheAWBIs(string expectedStatus)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                csp.ValidateAWBStatus(expectedStatus);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }



    }
}
