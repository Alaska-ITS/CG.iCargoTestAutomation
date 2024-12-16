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
    public class OPR344_EXP_00014_AssignAWBtoPreBuiltCartbyTypingIntheAWBStepDefinition : BasePage
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;       
        private CreateShipmentPage csp;
        private ExportManifestPage emp;
     

        ILog Log = LogManager.GetLogger(typeof(OPR344_EXP_00014_AssignAWBtoPreBuiltCartbyTypingIntheAWBStepDefinition));


        public OPR344_EXP_00014_AssignAWBtoPreBuiltCartbyTypingIntheAWBStepDefinition(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            this.pageObjectManager = new PageObjectManager(driver);           
            this.csp = pageObjectManager.GetCreateShipmentPage();
            this.emp= pageObjectManager.GetExportManifestPage();           

        }


        [When(@"User clicks on the Edit ULD button for the pre-build ULD/Cart")]
        public void WhenUserClicksOnTheEditULDButtonForThePre_BuildULDCart()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                emp.ClickOnEditULDButton();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
            
        }
        


        [When(@"User types in the AWB number and pieces ""([^""]*)"" inside the pre-built ULD/Cart")]
        public void WhenUserTypesInTheAWBNumberAndPiecesInsideThePre_BuiltULDCart(string piecesToAssign)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                csp.AssignAWBToPreBuiltCartByAWBTypingExportManifest(piecesToAssign);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
           
        }



    }
}
