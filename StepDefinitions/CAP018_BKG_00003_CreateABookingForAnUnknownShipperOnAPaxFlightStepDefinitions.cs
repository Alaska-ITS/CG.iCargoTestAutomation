using iCargoUIAutomation.pages;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace iCargoUIAutomation.StepDefinitions
{
    [Binding]
    public class CAP018_BKG_00003_CreateABookingForAnUnknownShipperOnAPaxFlightStepDefinitions
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
        private homePage hp;
        private MaintainBookingPage mbp;
        string unkShipper = "";
        string unkConsignee = "";
        string origin = "";
        string destination = "";
        string agentCode = "";
        string productCode = "";
        string commodity = "";
        string piece = "";
        string weight = "";

        public CAP018_BKG_00003_CreateABookingForAnUnknownShipperOnAPaxFlightStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            mbp = pageObjectManager.GetMaintainBookingPage();
        }
        [Then(@"User enters Unknown Shipper ""([^""]*)"" and Consignee ""([^""]*)"" with all details")]
        public void ThenUserEntersUnknownShipperAndConsigneeWithAllDetails(string unkShipper, string unkConsignee)
        {
            this.unkShipper = unkShipper;
            this.unkConsignee = unkConsignee;
            mbp.UnknownShipperConsigneeALLDetails(unkShipper, unkConsignee);
        }

        [Then(@"User enters shipment details with Origin ""([^""]*)"", Destination ""([^""]*)"",Agent Code ""([^""]*)"", Product Code ""([^""]*)""")]
        public void ThenUserEntersShipmentDetailsWithOriginDestinationAgentCodeProductCode(string origin, string destination, string agentcode, string productCode)
        {
            this.origin = origin;
            this.destination = destination;
            this.agentCode = agentcode;
            this.productCode = productCode;
            mbp.NewUnknownAgentShipmentDetails(origin, destination, agentcode, productCode);
            
        }

        [When(@"User enters screen name as '([^']*)'")]
        public void WhenUserEntersScreenNameAs(string screenName)
        {
            hp.enterScreenName(screenName);
            mbp.SwitchToCAP018Frame();
        }

        [Then(@"User clicks on New/List button")]
        public void ThenUserClicksOnNewListButton()
        {            
            mbp.ClickNewListButton();
        }

        [Then(@"User enters shipment details with Origin ""([^""]*)"", Destination ""([^""]*)"", Product Code ""([^""]*)""")]
        public void ThenUserEntersShipmentDetailsWithOriginDestinationShippingDateProductCode(string origin, string destination, string productCode)
        {
            this.origin = origin;
            this.destination = destination;
            this.productCode = productCode;
            mbp.EnterShipmentDetails(origin, destination, productCode);
        }

        [Then(@"User enters commodity details with Commodity ""([^""]*)"", Pieces ""([^""]*)"", Weight ""([^""]*)""")]
        public void ThenUserEntersCommodityDetailsWithCommodityPiecesWeight(string commodity, string piece, string weight)
        {
            this.commodity = commodity;
            this.piece = piece;
            this.weight = weight;
            mbp.EnterCommodityDetails(commodity, piece, weight);
        }

        [Then(@"User clicks on Save button")]
        public void ThenUserClicksOnSaveButton()
        {
            mbp.ClickSaveButton();            
        }        
        [Then(@"User selects flight for ""([^""]*)""")]
        public void ThenUserSelectsFlightFor(string productCode)
        {
            mbp.SelectFlight(productCode);
        }
    }
}
