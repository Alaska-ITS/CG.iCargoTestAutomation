using iCargoUIAutomation.Features;
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
    public class LTE001_ACC_00006_AcceptprebookedAWBLTE001 : BasePage
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
       
        private CreateShipmentPage csp;
        public static string preBookedpieces="";
        ILog Log = LogManager.GetLogger(typeof(LTE001_ACC_00006_AcceptprebookedAWBLTE001));


        public LTE001_ACC_00006_AcceptprebookedAWBLTE001(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            this.pageObjectManager = new PageObjectManager(driver);          
            this.csp = pageObjectManager.GetCreateShipmentPage();
        }


        [When(@"User enters an AWB ""([^""]*)"" of a PreBooked Shipment")]
        public void WhenUserEntersAnAWBOfAPreBookedShipment(string preBookedAWB)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                Log.Info("Step: Entering the AWB of a PreBooked Shipment");
                preBookedAWB = preBookedAWB.Split("-")[1];
                csp.SwitchToLTEContentFrame();
                csp.ClickOnAwbTextBox();
                csp.EnterAWBTextBox(preBookedAWB);
                csp.ClickOnListButton();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User opens & verifies the Participant Details")]
        public void WhenUserOpensVerifiesTheParticipantDetails()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                Log.Info("Step: Opening and Verifying the Participant Details");
                csp.OpenAndVerifyParticipants();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User opens & verifies the Shipment Details")]
        public void WhenUserOpensVerifiesTheShipmentDetails()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                Log.Info("Step: Opening and Verifying the Shipment Details");
                csp.OpenAndVerifyShipments();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User opens & verifies the Flight Details")]
        public void WhenUserOpensVerifiesTheFlightDetails()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                Log.Info("Step: Opening and Verifying the Flight Details");
                csp.OpenAndVerifyFlightDetails();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
           
        }

        [When(@"user opens the Charge Details")]
        public void WhenUserOpensTheChargeDetails()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                Log.Info("Step: Opening the Charge Details");
                csp.OpenAndVerifyChargeDetails();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User enters the Acceptance details with PreBooked pieces")]
        public void WhenUserEntersTheAcceptanceDetailsWithPreBookedPieces()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                Log.Info("Step: Entering the Acceptance details with PreBooked pieces");
                csp.EnterAcceptanceDetails();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

    }
}
