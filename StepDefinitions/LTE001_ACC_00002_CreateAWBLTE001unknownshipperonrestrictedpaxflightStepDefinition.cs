﻿using iCargoUIAutomation.pages;
using iCargoUIAutomation.utilities;
using log4net;
using log4net.Filter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace iCargoUIAutomation.StepDefinitions
{
    [Binding]
    public class LTE001_ACC_00002_CreateAWBLTE001unknownshipperonrestrictedpaxflight : BasePage
    {
        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
        private CreateShipmentPage csp;


        ILog Log = LogManager.GetLogger(typeof(LTE001_ACC_00002_CreateAWBLTE001unknownshipperonrestrictedpaxflight));


        public LTE001_ACC_00002_CreateAWBLTE001unknownshipperonrestrictedpaxflight(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            this.pageObjectManager = new PageObjectManager(driver);
            this.csp = pageObjectManager.GetCreateShipmentPage();

        }



        [When(@"User enters the Participant details with AgentCode ""([^""]*)"",Unknown ShipperCode ""([^""]*)"", ConsigneeCode ""([^""]*)""")]
        public void WhenUserEntersTheParticipantDetailsWithAgentCodeUnknownShipperCodeConsigneeCode(string agent, string unknownshipper, string consignee)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                csp.EnterParticipantDetailsWithUnknownShipper(agent, unknownshipper, consignee);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        [When(@"User saves the shipment details validate error message as ""([^""]*)"" and capture AWB number")]       
        public void WhenUserSavesTheShipmentDetailsValidateErrorMessageAsAndCaptureAWBNumber(string expectedWarnMsg)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                csp.SaveShipmentCaptureAWB(expectedWarnMsg);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
                
        }

        [When(@"User validates the popped up error message as ""([^""]*)""")]
        public void WhenUserValidatesThePoppedUpErrorMessageAs(string expectedWarnMsg)
        {
            if (ScenarioContext.Current["Execute"] == "true")
            {
                Hooks.Hooks.createNode();
                Log.Info("Step: Validating the popped up error message");
                csp.ValidateWarningMessage(expectedWarnMsg);
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }



    }
}
