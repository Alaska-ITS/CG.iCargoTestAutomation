using iCargoUIAutomation.pages;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace iCargoUIAutomation.StepDefinitions
{
    [Binding]
    public class CAP018_BKG_00009_SaveATemplateFromABookingStepDefinitions:BasePage
    {
        private PageObjectManager pageObjectManager;
        private homePage hp;
        private MaintainBookingPage mbp;        

        public CAP018_BKG_00009_SaveATemplateFromABookingStepDefinitions(IWebDriver driver):base(driver)
        {
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            mbp = pageObjectManager.GetMaintainBookingPage();            
        }    

        [Then(@"User clicks on Select/Save Template to save the template")]
        public void ThenUserClicksOnSelectSaveTemplateToSaveTheTemplate()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                mbp.ClickSelectSaveTemplate();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

    }
}
