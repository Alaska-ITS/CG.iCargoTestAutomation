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
    public class OPR344_EXP_00001_ManifestAWBUnknownShipperonpaxflightStepDefinition : BasePage
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;       
        private CreateShipmentPage csp;
        private ExportManifestPage emp;
     

        ILog Log = LogManager.GetLogger(typeof(LTE001_ACC_00016_CreateAWBEmployeeShipmentStepDefinition));


        public OPR344_EXP_00001_ManifestAWBUnknownShipperonpaxflightStepDefinition(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            this.pageObjectManager = new PageObjectManager(driver);           
            this.csp = pageObjectManager.GetCreateShipmentPage();
            this.emp= pageObjectManager.GetExportManifestPage();           

        }
       

        [When(@"User enters the Booked FlightNumber with ""([^""]*)""")]
        public void WhenUserEntersTheBookedFlightNumberWith(string fltnumber)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.SwitchToManifestFrame();
                emp.ClickOnFlightTextBox();
                csp.EnterFlightinExportManifest(fltnumber);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }

        }


        [When(@"User enters Booked ShipmentDate")]
        public void WhenUserEntersBookedShipmentDate()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                csp.EnterFlightDateExportManifest();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
           
        }

        [When(@"User clicks on the List button to fetch the Booked Shipment")]
        public void WhenUserClicksOnTheListButtonToFetchTheBookedShipment()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.ClickOnListButton();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
            
        }
       

        [When(@"User creates new ULD/Cart in Assigned Shipment with cartType ""([^""]*)"" and pou ""([^""]*)""")]
        public void WhenUserCreatesNewULDCartInAssignedShipmentWithCartTypeAndPou(string cartType, string pou)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                csp.CreateNewULDCartExportManifest(cartType, pou);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
            
        }



        [When(@"User filterouts the Booked AWB from '([^']*)' and Created ULD_Cart")]
        public void WhenUserFilteroutsTheBookedAWBFromAndCreatedULD_Cart(string awbSectionName)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                csp.FilterOutAWBULDInExportManifest(awbSectionName);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
           
        }          



        [When(@"User clicks on the Manifest button")]
        public void WhenUserClicksOnTheManifestButton()
        {
           if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.clickOnManifestButton();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
           
        }

        [When(@"User generates the Manifest PDF from the PrintPDF window")]
        public void WhenUserGeneratesTheManifestPDFFromThePrintPDFWindow()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.PrintManifestWindow();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
            
        }

        [When(@"User closes the PrintPDF window")]
        public void WhenUserClosesThePrintPDFWindow()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.ClosePrintPDFWindow();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
            
        }

        [When(@"User validates the AWB is ""([^""]*)"" in the Export Manifest screen")]
        public void WhenUserValidatesTheAWBIsInTheExportManifestScreen(string awbStatus)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.ValidateAWBStatusInExportManifest(awbStatus);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
           
        }


        [Then(@"User closes the Export Manifest screen")]
        public void ThenUserClosesTheOPRScreen()
        {
           if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.CloseOPR344Screen();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
            
        }

        [When(@"User validates the error popover message as ""([^""]*)""")]
        public void WhenUserValidatesTheErrorPopoverMessageAs(string expectedWarnMsg)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.ValidateOPR344WarningMessage(expectedWarnMsg);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
            
        }
    }
}
