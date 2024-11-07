using iCargoUIAutomation.pages;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace iCargoUIAutomation.StepDefinitions
{
    [Binding]
    public class CAP018_BKG_00010_CreateABookingFromASavedTemplateStepDefinitions:BasePage
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
        private MaintainBookingPage mbp;
        public CAP018_BKG_00010_CreateABookingFromASavedTemplateStepDefinitions(IWebDriver driver):base(driver)
        {
            this.driver = driver;
            pageObjectManager = new PageObjectManager(driver);
            mbp = pageObjectManager.GetMaintainBookingPage();
        }
        [Then(@"User clicks on Select/Save Template")]
        public void ThenUserClicksOnSelectSaveTemplate()
        {
            mbp.SelectTemplate();
        }
        
    }
}
