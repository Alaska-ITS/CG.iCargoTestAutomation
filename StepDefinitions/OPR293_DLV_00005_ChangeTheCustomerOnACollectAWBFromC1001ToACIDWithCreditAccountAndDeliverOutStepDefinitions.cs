using iCargoUIAutomation.pages;
using OpenQA.Selenium;
using System;
using System.Runtime.Intrinsics.Arm;
using TechTalk.SpecFlow;

namespace iCargoUIAutomation.StepDefinitions
{
    [Binding]
    public class OPR293_DLV_00005_ChangeTheCustomerOnACollectAWBFromC1001ToACIDWithCreditAccountAndDeliverOutStepDefinitions:BasePage
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
        private CreateShipmentPage csp; 
        private DeliveryPage dp;

        public OPR293_DLV_00005_ChangeTheCustomerOnACollectAWBFromC1001ToACIDWithCreditAccountAndDeliverOutStepDefinitions(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            pageObjectManager = new PageObjectManager(driver);
            csp = pageObjectManager.GetCreateShipmentPage();
            dp = pageObjectManager.GetDeliveryPage();
        }

        [When(@"User enters the Participant details with AgentCode ""([^""]*)"", ShipperCode ""([^""]*)"", unknown ConsigneeCode ""([^""]*)""")]
        public void WhenUserEntersTheParticipantDetailsWithAgentCodeShipperCodeUnknownConsigneeCode(string agent, string shipper, string consignee)
        {
            if (ScenarioContext.Current["Execute"]=="true")
            {
                Hooks.Hooks.createNode();
                csp.EnterParticipantDetailsWithUnknownConsignee(agent, shipper, consignee);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }   
        }


        [When(@"User enters customer code ""([^""]*)"" to process the delivery")]
        public void WhenUserEntersCustomerCodeToProcessTheDelivery(string customerCode)
        {
            if (ScenarioContext.Current["Execute"]=="true")
            {
                Hooks.Hooks.createNode();
                dp.EnterCustomerCodeForUnknownConsignee(customerCode);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }   
        }
    }
}
