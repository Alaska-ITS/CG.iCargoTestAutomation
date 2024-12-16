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
    public class LTE001_ACC_00015_CreateAWBCAODGshipmentandbookonpaxflight : BasePage
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;       
        private CreateShipmentPage csp;

        ILog Log = LogManager.GetLogger(typeof(LTE001_ACC_00015_CreateAWBCAODGshipmentandbookonpaxflight));


        public LTE001_ACC_00015_CreateAWBCAODGshipmentandbookonpaxflight(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            this.pageObjectManager = new PageObjectManager(driver);
           
            this.csp = pageObjectManager.GetCreateShipmentPage();
        }

        [When(@"User validates the error message ""([^""]*)"" in the Embargo Details popup")]
        public void WhenUserValidatesTheErrorMessageInTheEmbargoDetailsPopup(string expectedErrorMessage)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                csp.ValidateEmbargoPopupErrorMessage(expectedErrorMessage);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }


        [When(@"User enters details for CAO DG shipment with ChargeType ""([^""]*)"",UNID ""([^""]*)"", ProperShipmentName ""([^""]*)"", PackingInstruction ""([^""]*)"",NoOfPkg ""([^""]*)"", NetQtyPerPkg ""([^""]*)"", ReportableQnty ""([^""]*)""")]
        public void UserEnterDetailsForCAODGShipment(string chargetype, string unid, string propershipmntname, string pi, string noOFPkg, string netqtyperpkg, string reportable)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                csp.EnterCAODGDetails(chargetype, unid, propershipmntname, pi, noOFPkg, netqtyperpkg, reportable);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }
        

        [When(@"User saves the CAO DG shipment")]
        public void WhenUserSavesTheCAODGShipment()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                csp.SaveCAODGshipment();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }



    }
}
