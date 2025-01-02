using iCargoUIAutomation.pages;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace iCargoUIAutomation.StepDefinitions
{
    [Binding]
    public class OPR339_SCRN_00002_ScreenFullStatedPiecesOfAnAWBStepDefinitions : BasePage
    {
        private PageObjectManager pageObjectManager;
        private IWebDriver driver;
        private ScreeningPage sp;

        public OPR339_SCRN_00002_ScreenFullStatedPiecesOfAnAWBStepDefinitions(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            pageObjectManager = new PageObjectManager(driver);
            sp = pageObjectManager.GetScreeningPage();
        }

        [When(@"User enters the AWB number")]
        public void WhenUserEntersTheAWBNumber()
        {
            sp.SwitchToScreeningOPR339Frame();
            sp.EnterAWBNumber();            
        }

        [When(@"User clicks on List button")]
        public void WhenUserClicksOnListButton()
        {
            sp.ClickListButton();
            sp.ClickScreeningPopUp();
        }

        [When(@"User enters the Screening details with screeingMethod as '([^']*)' and ScreeningResult as '([^']*)'")]
        public void WhenUserEntersTheScreeningDetailsWithScreeingMethodAsAndScreeningResultAs(string screeningMethod, string screeningResult)
        {
            sp.EnterScreeningDetails(screeningMethod, screeningResult);
            sp.EnterSecurityDetails();
            sp.VerifyScreeningDetailsSavedSuccessfully();
        }

        [When(@"User enters the Executed and Screened AWB number")]
        public void WhenUserEntersTheExecutedAndScreenedAWBNumber()
        {
            sp.EnterAwbNumberLTE();
        }

    }
}
