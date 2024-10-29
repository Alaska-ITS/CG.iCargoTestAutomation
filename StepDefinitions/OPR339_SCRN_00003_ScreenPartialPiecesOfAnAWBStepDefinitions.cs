using iCargoUIAutomation.pages;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace iCargoUIAutomation.StepDefinitions
{
    [Binding]
    public class OPR339_SCRN_00003_ScreenPartialPiecesOfAnAWBStepDefinitions : BasePage
    {
        private PageObjectManager pageObjectManager;
        private IWebDriver driver;
        private ScreeningPage sp;

        public OPR339_SCRN_00003_ScreenPartialPiecesOfAnAWBStepDefinitions(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            pageObjectManager = new PageObjectManager(driver);
            sp = pageObjectManager.GetScreeningPage();
        }
        [When(@"User enters the Screening details with screeingMethod as '([^']*)' and ScreeningResult as '([^']*)' and HalfSlatedPieces as ""([^""]*)""")]
        public void WhenUserEntersTheScreeningDetailsWithScreeingMethodAsAndScreeningResultAsAndHalfSlatedPiecesAs(string screeningMethod, string screeningResult, string slatedPieces)
        {
            sp.EnterScreeningDetailsForHalfSlatedPieces(screeningMethod,screeningResult, slatedPieces);
            sp.EnterSecurityDetails();
            sp.VerifyScreeningDetailsSavedSuccessfully();
        }
    }
}
