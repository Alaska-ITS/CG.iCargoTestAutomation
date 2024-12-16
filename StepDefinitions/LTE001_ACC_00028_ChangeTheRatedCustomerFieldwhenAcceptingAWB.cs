using iCargoUIAutomation.pages;
using iCargoUIAutomation.utilities;
using log4net;
using log4net.Filter;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using TechTalk.SpecFlow;

namespace iCargoUIAutomation.StepDefinitions
{
    [Binding]
    public class LTE001_ACC_00028_ChangeTheRatedCustomerFieldwhenAcceptingAWB : BasePage
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
        private CreateShipmentPage csp;


        ILog Log = LogManager.GetLogger(typeof(LTE001_ACC_00028_ChangeTheRatedCustomerFieldwhenAcceptingAWB));


        public LTE001_ACC_00028_ChangeTheRatedCustomerFieldwhenAcceptingAWB(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            this.pageObjectManager = new PageObjectManager(driver);
            this.csp = pageObjectManager.GetCreateShipmentPage();

        }

        [When(@"User checks the ThirdParty checkbox and enters the RatedCustomer ""([^""]*)""")]
        public void WhenUserChecksTheThirdPartyCheckboxAndEntersTheRatedCustomer(string ratedCustomerNum)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                csp.CheckThirdPartyCheckbox();
                csp.EnterRatedCustomerNumber(ratedCustomerNum);

            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User validates the CID under Account Info in paymentportal with the RatedCustomer ""([^""]*)""")]
        public void WhenUserValidatesTheCIDUnderAccountInfoInPaymentportalWithTheRatedCustomer(string ratedCustomerNum)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                csp.ValidateRatedCustomerInPaymentPortal(ratedCustomerNum);

            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }














    }
}
