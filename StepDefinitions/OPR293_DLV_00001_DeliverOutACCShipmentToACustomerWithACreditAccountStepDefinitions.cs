using iCargoUIAutomation.pages;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace iCargoUIAutomation.StepDefinitions
{
    [Binding]
    public class OPR293_DLV_00001_DeliverOutACCShipmentToACustomerWithACreditAccountStepDefinitions : BasePage
    {

        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
        private DeliveryPage dp;
        private CreateShipmentPage csp;
        private static string totalPaybleAmount;
        public OPR293_DLV_00001_DeliverOutACCShipmentToACustomerWithACreditAccountStepDefinitions(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            pageObjectManager = new PageObjectManager(driver);
            dp = pageObjectManager.GetDeliveryPage();
            csp = pageObjectManager.GetCreateShipmentPage();
        }
        [When(@"User lists the AWB details for delivery")]
        public void WhenUserListsTheAWBDetailsForDelivery()
        {
            dp.SwitchToOPR293Frame();
            dp.EnterAWBNumberOPR293();
        }

        [When(@"process the Delivery Note details")]
        public void WhenProcessTheDNDetails()
        {
            dp.SelectAWBForDelivery();
            dp.ClickGenerateDeliveryNoteButton();
            dp.ClickingYesOnPopupWarnings("");
            dp.GetPaymentAmountValue();
        }

        [When(@"User saves the payment details for ""([^""]*)""")]
        public void WhenUserSavesThePaymentDetailsFor(string chargeType)
        {
            totalPaybleAmount = dp.ClickOnAddButtonHandlePaymentPortal(chargeType);
        }

        [When(@"User clicks on Accept Payment button")]
        public void WhenUserClicksOnAcceptPaymentButton()
        {
            dp.ClickAcceptPaymentButton();
            dp.DeliveryConfirmationDetails();
        }

        [When(@"User Captures the delivery details")]
        public void WhenUserCapturesTheDeliveryDetails()
        {
            dp.CaptureDeliveryDetails(); 
            dp.ClickingYesOnPopupWarnings("");
            dp.DeliveryReceiptWindow();
        }


    }

}
