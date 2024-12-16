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
    public class OPR344_EXP_00002_ManifestAWBfromlyinglistStepDefinition : BasePage
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;       
        private CreateShipmentPage csp;
        private ExportManifestPage emp;
     

        ILog Log = LogManager.GetLogger(typeof(OPR344_EXP_00002_ManifestAWBfromlyinglistStepDefinition));


        public OPR344_EXP_00002_ManifestAWBfromlyinglistStepDefinition(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            this.pageObjectManager = new PageObjectManager(driver);
           
            this.csp = pageObjectManager.GetCreateShipmentPage();
            this.emp= pageObjectManager.GetExportManifestPage();           

        }

        [When(@"User clicks on the lying list")]
        public void WhenUserClicksOnTheLyingList()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.ClickOnLyingList();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
            
        }

        [When(@"User clicks on the filter button")]
        public void WhenUserClicksOnTheFilterButton()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.ClickOnLyingListFilter();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
            
        }

        [When(@"User selects ""([^""]*)"" from the Ready For Carriage dropdown")]
        public void WhenUserSelectsFromTheReadyForCarriageDropdown(string option)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.SelectReadyForCarriage(option);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
           
        }

        [When(@"User clicks on the Apply button")]
        public void WhenUserClicksOnTheApplyButton()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.ClickOnApplyFilter();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
           
        }

        [When(@"User selects the checkbox of the first shipment from Lying list")]
        public void WhenUserSelectsTheCheckboxOfTheFirstShipment()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.ClickOnCheckBoxLyingListAWB();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
           
            
        }

        [When(@"User place the shipment on the cart to manifest")]
        public void WhenUserPlaceTheShipmentOnTheCartToManifest()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.PlaceShipmentOnCartToManifest();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
           
        }

        [When(@"User handles any pop up error message")]
        public void WhenUserHandlesAnyPopUpErrorMessage()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.HandleWarningMessage();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
           
        }


        [When(@"User validates the warning message ""([^""]*)""")]
        public void WhenUserValidatesTheWarningMessage(string messageToValidate)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.ValidateWarningMessageAndCloseModal(messageToValidate);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
         
        }

        [When(@"User validates the warning message ""([^""]*)"" and Cancel the warning message")]
        public void WhenUserValidatesTheWarningMessageAndCancelTheWarningMessage(string messageToValidate)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.ValidateWarningMessageAndCancelModal(messageToValidate);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
          
        }

    }
}
