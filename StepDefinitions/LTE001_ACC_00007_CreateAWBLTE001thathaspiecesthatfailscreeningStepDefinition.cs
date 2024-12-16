using iCargoUIAutomation.Features;
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
    public class LTE001_ACC_00007_CreateAWBLTE001thathaspiecesthatfailscreening : BasePage
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;       
        private CreateShipmentPage csp;
        ILog Log = LogManager.GetLogger(typeof(LTE001_ACC_00007_CreateAWBLTE001thathaspiecesthatfailscreening));


        public LTE001_ACC_00007_CreateAWBLTE001thathaspiecesthatfailscreening(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            this.pageObjectManager = new PageObjectManager(driver);           
            this.csp = pageObjectManager.GetCreateShipmentPage();
        }


        [When(@"USer adds another screening line")]
        public void WhenUSerAddsAnotherScreeningLine()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                Log.Info("Step: Adding another screening line");
                csp.AddAnotherScreeningLine();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }
       

        [When(@"User saves all the details with ChargeType ""([^""]*)"" and validates the popped up error message as ""([^""]*)""")]
        public void WhenUserSavesAllTheDetailsWithChargeTypeAndValidatesThePoppedUpErrorMessageAs(string chargeType, string expectedWarning)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                Log.Info("Step: Saving all the details with ChargeType");
                csp.SaveDetailsWithChargeType(chargeType, expectedWarning);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User saves all the details with ChargeType ""([^""]*)""")]
        public void WhenUserSavesAllTheDetailsWithChargeType(string chargeType)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                Log.Info("Step: Saving all the details with ChargeType");
                csp.SaveWithChargeType(chargeType);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }





    }
}
