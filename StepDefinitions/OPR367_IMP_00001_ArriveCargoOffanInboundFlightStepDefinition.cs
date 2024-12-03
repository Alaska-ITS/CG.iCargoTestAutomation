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
    public class OPR367_IMP_00001_ArriveCargoOffanInboundFlightStepDefinition : BasePage
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
        private CreateShipmentPage csp;
        private ExportManifestPage emp;
        private MarkFlightMovements mfm;
        private ImportManifestPage imp;


        ILog Log = LogManager.GetLogger(typeof(OPR367_IMP_00001_ArriveCargoOffanInboundFlightStepDefinition));


        public OPR367_IMP_00001_ArriveCargoOffanInboundFlightStepDefinition(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            this.pageObjectManager = new PageObjectManager(driver);
            this.csp = pageObjectManager.GetCreateShipmentPage();
            this.emp = pageObjectManager.GetExportManifestPage();
            this.mfm = pageObjectManager.GetMarkFlightMovements();
            this.imp = pageObjectManager.GetImportManifestPage();

        }


        [When(@"User enters the flight details in Mark Flight Movement screen")]
        public void WhenUserEntersTheFlightDetailsInMarkFlightMovementScreen()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                mfm.SwitchToFLT006Frame();
                mfm.EnterFlightDetails();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User clicks on the List button to fetch the flight movement details")]
        public void WhenUserClicksOnTheListButtonToFetchTheFlightMovementDetails()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                mfm.ClickListButton();

            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }




        [When(@"User enters the flight movement details for '([^']*)' and clicks on save button")]
        public void WhenUserEntersTheFlightMovementDetailsForAndClicksOnSaveButton(string movementType)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();                
                mfm.EnterActualArrivalDepartureDetails(movementType);                
                mfm.ClickSaveButton();

            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User enters the flight details and movement details for '([^']*)' and clicks on save button")]
        public void WhenUserEntersTheFlightDetailsAndMovementDetailsForAndClicksOnSaveButton(string movementType)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                mfm.SwitchToFLT006Frame();
                mfm.EnterFlightDetails();
                mfm.ClickListButton();
                mfm.EnterActualArrivalDepartureDetails(movementType);
                mfm.ClickSaveButton();
                mfm.ClickCloseButton();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User enters the flight details and movement details for '([^']*)' and '([^']*)' and clicks on save button")]
        public void WhenUserEntersTheFlightDetailsAndMovementDetailsForAndAndClicksOnSaveButton(string departure, string arrival)
        {
            {
                if (ScenarioContext.Current["Execute"] == "true")
                {
                    Hooks.Hooks.createNode();
                    mfm.SwitchToFLT006Frame();
                    mfm.EnterFlightDetails();
                    mfm.ClickListButton();
                    mfm.EnterActualArrivalDepartureDetails(departure);
                    mfm.EnterActualArrivalDepartureDetails(arrival,2);
                    mfm.ClickSaveButton();
                    mfm.ClickCloseButton();
                }
                else
                {
                    ScenarioContext.Current.Pending();
                }
            }
        }




        [Then(@"User closes the Mark flight movement screen")]
        public void ThenUserClosesTheMarkFlightMovementScreen()
        {

            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                mfm.ClickCloseButton();

            }
            else
            {
                ScenarioContext.Current.Pending();
            }

        }

        [When(@"User checks for the flight status to be finalized")]
        public void WhenUserChecksForTheFlightStatusToBeFinalized()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.CheckFlightStatusForFinalized();

            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User enters the Flight details to fetch the uld details")]
        public void WhenUserEntersTheFlightDetailsToFetchTheUldDetails()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                imp.SwitchToImportManifestFrame();
                imp.ClickOnFlightTextBox();
                imp.EnterFlightNumber();
                imp.EnterFlightDate();
                imp.ClickOnListButton();

            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User selects ULD and clicks on the breakdown button to breakdown")]
        public void WhenUserSelectsULDAndClicksOnTheBreakdownButtonToBreakdown()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                imp.ClickOnBulkCheckBox();
                imp.ClickOnBreakDownButton();

            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User enters the breakdown details with BreakdownLocation ""([^""]*)"", receivedPieces ""([^""]*)"", receivedWeight ""([^""]*)""")]
        public void WhenUserEntersTheBreakdownDetailsWithBreakdownLocationReceivedPiecesReceivedWeight(string bdnLocation, string bdnRcvdPcs, string bdnRcvdWt)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                imp.EnterBreakdownDetails(bdnLocation, bdnRcvdPcs,bdnRcvdWt);

            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }


        [Then(@"User Clicks on the save button and validates the popup message as '([^']*)'")]
        public void ThenUserClicksOnTheSaveButtonAndValidatesThePopupMessageAs(string expectedMsg)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                imp.SaveBreakdownAndValidateMessage(expectedMsg);

            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }


        [Then(@"User validates the popup warning message as '([^']*)'")]
        public void ThenUserValidatesThePopupWarningMessageAs(string expectedMsg)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                imp.SaveBreakdownAndValidateErrorMessage(expectedMsg);

            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }










    }
}
