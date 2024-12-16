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
            if (ScenarioContext.Current["Execute"]=="true")
            {
                Hooks.Hooks.createNode();
                dp.SwitchToOPR293Frame();
                dp.EnterAWBNumberOPR293();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }            
        }

        [When(@"process the Delivery Note details")]
        public void WhenProcessTheDNDetails()
        {
            if (ScenarioContext.Current["Execute"]=="true")
            {
                Hooks.Hooks.createNode();
                dp.SelectAWBForDelivery();
                dp.ClickGenerateDeliveryNoteButton();
                dp.ClickingYesOnPopupWarnings("");
                dp.GetPaymentAmountValue();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
            
        }

        [When(@"User saves the payment details for ""([^""]*)""")]
        public void WhenUserSavesThePaymentDetailsFor(string chargeType)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                totalPaybleAmount = dp.ClickOnAddButtonHandlePaymentPortal(chargeType);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User clicks on Accept Payment button")]
        public void WhenUserClicksOnAcceptPaymentButton()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                dp.ClickAcceptPaymentButton();
                dp.DeliveryConfirmationDetails();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }            
        }

        [When(@"User Captures the delivery details")]
        public void WhenUserCapturesTheDeliveryDetails()
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                dp.CaptureDeliveryDetails();
                dp.ClickingYesOnPopupWarnings("");
                dp.DeliveryReceiptWindow();
            }
            else
            {
                ScenarioContext.Current.Pending();
            }            
        }


    }

}
