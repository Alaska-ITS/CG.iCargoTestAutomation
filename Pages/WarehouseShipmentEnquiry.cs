using AventStack.ExtentReports;
using iCargoUIAutomation.pages;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;


namespace iCargoUIAutomation.pages
{
    public class WarehouseShipmentEnquiry : BasePage
    {

        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
        //private CreateShipmentPage csp;
        ILog Log = LogManager.GetLogger(typeof(WarehouseShipmentEnquiry));

        public WarehouseShipmentEnquiry(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            this.pageObjectManager = new PageObjectManager(driver);
            //this.csp =pageObjectManager.GetCreateShipmentPage();
        }

        private By destinationField = By.XPath("//input[@name='destination' and contains(@class, 'iCargoTextFieldSmall')]");
        private By contentFrameWarehouseShipmentEnquiry = By.XPath("//iframe[@name='iCargoContentFrameWHS011']");
        private By ListButton = By.Id("CMP_Warehouse_Defaults_WarehouseShipmentEnq_btList");
        private By Checkbox = By.Name("shipmentSubCheck");
        private By AbandonShipment = By.Name("btAbndShip");
        private By AbandonCheckBox = By.XPath("//input[@name='checkBox']");
        private By AbandonTypeDrpdown = By.Name("abandonType");
        private By AbandonPiece = By.Name("abandonpieces");
        private By AbandonWeight = By.Name("abandonwt");
        private By ReasonCodeDrpdown = By.Name("reasonCode");
        private By Remarks = By.Name("remarks");
        private By AbndnSave = By.Id("CMP_WAREHOUSE_DEFAULTS_ABANDONSHIPMENT_SAVE_BTN");
        //private By Validatemessaged = By.XPath("//span[contains(text(),'The Selected Shipment has been abandoned')]");
        private By Validatemessage = By.CssSelector(".ui-dialog .alert-messages-detail span");
        private object e;

        public void SwitchToWarehouseFrame()
        {
            SwitchToFrame(contentFrameWarehouseShipmentEnquiry);
            //Hooks.Hooks.UpdateTest(Status.Info, "Switched to Manifest Frame");
        }

        public void EnterDestination()
        {
            try
            {
                WaitForElementToBeEnabled(destinationField, TimeSpan.FromSeconds(15));
                string shipmentDestination = CreateShipmentPage.destination;

                if (!string.IsNullOrEmpty(shipmentDestination))
                {
                    Click(destinationField);
                    EnterText(destinationField, shipmentDestination);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Destination in Warehouse Shipment Enquiry: " + shipmentDestination);
                }
                else
                {
                    //Hooks.Hooks.UpdateTest(Status.Fail, "Failed to fetch destination from CreateShipmentPage");
                    Log.Error("Destination cannot be entered from CreatShipmentpage");

                }
            }
            catch (NoSuchElementException)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Destination input field not found.");
                Log.Error("Error in entering: " + e.ToString());
            }
        }

        public void ClickListButton()
        {
            Click(ListButton);
            //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on List button");
        }

        public void ClickOnCheckbox()
        {
            try
            {
                if (!IsCheckboxChecked(Checkbox))
                {
                    Click(Checkbox);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on the AWB details checkbox");
                }
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in clicking on AWB details checkbox: " + e.ToString());
                Log.Error("Error in clicking on check box: " + e.ToString());
            }
        }

        public void ClickAbandonShipmentButton()
        {
            Click(AbandonShipment);
            //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Abandon Shipment button");
        }

        public void ClickOnAbandonCheckBox()
        {
            try
            {
                if (!IsCheckboxChecked(AbandonCheckBox))
                {
                    Click(AbandonCheckBox);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Check box after clicking on Abandon Shipment button");
                }
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in clicking on Check box after clicking on Abandon Shipment button: " + e.ToString());
                Log.Error("Error in clicking on abandon check box: " + e.ToString());
            }
        }

        public void SelectfromDropdown()
        {
            try
            {
                SelectDropdownByVisibleText(AbandonTypeDrpdown, "Released to airline");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Id Type: Released to airline");
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in selecting Abandon Type details: " + e.ToString());
                Log.Error("Error in selecting from dropdown: " + e.ToString());
            }
        }

        public void EnterPieceAndWeight()
        {
            try
            {
                WaitForElementToBeEnabled(AbandonPiece, TimeSpan.FromSeconds(15));
                string abnpices = CreateShipmentPage.pieces;
                WaitForElementToBeEnabled(AbandonWeight, TimeSpan.FromSeconds(15));
                string abnwght = CreateShipmentPage.weight;

                if (!string.IsNullOrEmpty(abnpices))
                {
                    Click(AbandonPiece);
                    EnterText(AbandonPiece, abnpices);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Pieces in Abandon Shipment Screen: " + abnpices);
                }
                if (!string.IsNullOrEmpty(abnwght))
                {
                    Click(AbandonWeight);
                    EnterText(AbandonWeight, abnwght);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Weight in Abandon Shipment Screen: " + abnwght);
                }
                else
                {
                    //Hooks.Hooks.UpdateTest(Status.Fail, "Failed to fetch pieces and weight from Abandon Shipment Screen");
                    Log.Error("Pieces and Weight cannot be entered");

                }
            }
            catch (NoSuchElementException)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Pieces and Weight input field not found.");
                Log.Error("Error in entering piece and weight: " + e.ToString());
            }
        }

        public void SelectfromDrpdwnReasonCode()
        {
            try
            {
                SelectDropdownByVisibleText(ReasonCodeDrpdown, "W/h Adjustment");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Id Type: W/h Adjustment");
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in selecting Reason Code details: " + e.ToString());
                Log.Error("Error in selecting from dropdown reason code: " + e.ToString());
            }
        }

        public void EnterRemarks(string remark)
        {
            try
            {
                WaitForElementToBeEnabled(Remarks, TimeSpan.FromSeconds(15));
                Click(Remarks);
                EnterText(Remarks, remark);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Id Type: W/h Adjustment");
            }
            catch (NoSuchElementException)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Remarks input field not found.");
                Log.Error("Error in entering remarks: " + e.ToString());
            }
        }

        public void ClickonAbndnSavebttn()
        {
            Click(AbndnSave);
            //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on the Save button");

        }

        public void ValidatePopupAbndnScren(string expectedWarningMessage)
        {
            SwitchToPopupWindow();
            //WaitForElementToBeVisible(Validatemessage, TimeSpan.FromSeconds(20));
            WaitForElementToBeEnabled(Validatemessage, TimeSpan.FromSeconds(30));
            string actualWarningMessage = GetText(Validatemessage);
            if (!actualWarningMessage.Contains(expectedWarningMessage))
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Warning message is not as expected. Expected: " + expectedWarningMessage + " Actual: " + actualWarningMessage);
                //Log.Error("Warning message is not as expected. Expected: " + expectedWarningMessage + " Actual: " + actualWarningMessage);
                Assert.Fail("Warning message is not as expected. Expected: " + expectedWarningMessage + " Actual: " + actualWarningMessage);

            }
            else
            {
                //Hooks.Hooks.UpdateTest(Status.Info, "Warning message is as expected: " + actualWarningMessage);
                //Log.Info("Warning message is as expected: " + actualWarningMessage);
                Log.Error("PopUp cannot be validated");
            }

        }

        public void EnterPartialPiecesAndWeight(string abnparpieces, string abrparweight)
        {
            if (abnparpieces != "None" && abrparweight != "None")
            {
                Click(AbandonPiece);
                EnterText(AbandonPiece, abnparpieces);
                EnterKeys(AbandonPiece, Keys.Tab);
                //Hooks.Hooks.UpdateTest(Status.Info, "Entered Received Pieces: " + abnparpieces);
                EnterText(AbandonWeight, abrparweight);
                EnterKeys(AbandonWeight, Keys.Tab);
                //Hooks.Hooks.UpdateTest(Status.Info, "Entered Received Weight: " + abrparweight);
            }
        }
    }
}
