using AventStack.ExtentReports;
using log4net;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCargoUIAutomation.pages
{
    public class DeliveryPage:BasePage
    {
        private PageObjectManager pageObjectManager;
        private PaymentPortalPage ppp;
        private CreateShipmentPage csp;
        private CaptureIrregularityPage cip;
        private string totalPaybleAmount = "";
        private string accountInfoCIDNum = "";

        string chargeType = "";
        ILog Log = LogManager.GetLogger(typeof(CreateShipmentPage));

        public DeliveryPage(IWebDriver driver) : base(driver)
        {
            this.pageObjectManager = new PageObjectManager(driver);
            this.ppp = pageObjectManager.GetPaymentPortalPage();
            this.csp = pageObjectManager.GetCreateShipmentPage();
            this.cip = pageObjectManager.GetCaptureIrregularityPage();
        }

        //OPR293 Delivery Screen header
        private By deliveryScreenOPR293Frame_Xpath = By.XPath("//iframe[@name='iCargoContentFrameOPR293']");
        private By txtAWBNumber_CssSelector = By.CssSelector("#awb_b");
        private By btnList_CssSelector = By.CssSelector("#CMP_GENERATECOLLECTIONLIST_ADDTOLIST_BUTTON");

        //Delivery Details and Payment
        private By chkboxDeliveryAwb_Xpath = By.XPath("//input[@name='subRowId']");
        private By btnGenerateDeliveryNote_CssSelector = By.CssSelector("#CMP_GENERATECOLLECTIONLIST_GENERATEGROUPID_BUTTON");
        private By txtboxDeliveryPaymentCollectCash_CssSelector = By.CssSelector("#CSH_Cashiering_Defaults_ChargeCollect_PaymentAmount");
        private By btnAuthCode_Xpath = By.XPath("//button[@name='btAddGateway']");
        private By lblEmbargoDetails_Xpath = By.XPath("//*[text()='Embargo Details']");
        private By btnContinueEmbargo_Xpath = By.XPath("//*[text()='Embargo Details']//following::button[@id='okBtn']");        

        //Delivery Confirmation
        private By txtDeliveryNoteDetailsColor_Xpath = By.XPath("//a[@name='DNDetails_lnk']/div[contains(@style,'color:green')]");
        private By btnAcceptPayment_Xpath = By.XPath("//button[@name='btnAcceptPayment']");
        private By txtPaymentConfirmation_Xpath = By.XPath("//a[@name='DNDetails_lnk']/div[contains(text(),'Paid')]");
        private By btnCapturDelivery_Xpath = By.XPath("//button[@name='btnCaptureDelivery']");
        private By txtBoxDeiveryTo_Xpath = By.XPath("//input[@name='deliveredTo']");
        private By btnSaveDeliveryDetails_Xpath = By.XPath("//button[@name='btnSave']"); 
        private By btnCloseDeliveryScreen_Xpath = By.XPath("//button[@name='btnClose']");

        //Payment Details
        private By txtPleaseCloseTabRetry_Xpath = By.XPath("//*[text()='Please close the tab and retry.']");
        private By lblAccountInfo_Xpath = By.XPath("//*[text()='Account information']");
        private By lblTotalAmount_Xpath = By.XPath("(//*[@class='aiBoxTwo'])[2]//label[13]");
        private By btnDone_Xpath = By.XPath("//*[text()='Done']");

        //Popup Warnings
        private By btnYesActiveCashDraw_Xpath = By.XPath("//*[@class='ui-dialog-buttonset']/button[text()=' Yes ']");
        private By popupAlertMessage_Xpath = By.XPath("//*[@class='alert-messages-list']//span");
        private By popupWarning_Css = By.CssSelector(".alert-messages-ui");
        private By lblCaptureIrregularity_Xpath = By.XPath("//span[text()='Capture Irregularity']");

        //Customer Details
        private By txtCustomerCode_Xpath = By.XPath("//input[@name='customerCode']");

        private By ErrorPopOver_CSS = By.CssSelector("div#error-body div.screen-error-item span");

        public void SwitchToOPR293Frame()
        {
            try
            {
                SwitchToFrame(deliveryScreenOPR293Frame_Xpath);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Switched to OPR293 Frame");
                Log.Info("Switched to OPR293 Frame");
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in SwitchToOPR293Frame: " + e.Message);
                Log.Error("Error in SwitchToOPR293Frame: " + e.Message);
            }
        }

        public void EnterAWBNumberOPR293()
        {
            try
            {
                EnterText(txtAWBNumber_CssSelector, CreateShipmentPage.awb_num);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered AWB Number: " + CreateShipmentPage.awb_num);
                Log.Info("Entered AWB Number: " + CreateShipmentPage.awb_num);
                Click(btnList_CssSelector);
                Log.Info("Clicked on List Button");
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in EnterAWBNumberOPR293: " + e.Message);
                Log.Error("Error in EnterAWBNumberOPR293: " + e.Message);
            }
        }

        public void SelectAWBForDelivery()
        {
            try
            {
                Click(chkboxDeliveryAwb_Xpath);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Selected AWB for Delivery");
                Log.Info("Selected AWB for Delivery");
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in SelectAWBForDelivery: " + e.Message);
                Log.Error("Error in SelectAWBForDelivery: " + e.Message);
            }
        }

        public void ClickGenerateDeliveryNoteButton()
        {
            try
            {
                Click(btnGenerateDeliveryNote_CssSelector);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Generate Delivery Note Button");
                Log.Info("Clicked on Generate Delivery Note Button");
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in ClickGenerateDeliveryNoteButton: " + e.Message);
                Log.Error("Error in ClickGenerateDeliveryNoteButton: " + e.Message);
            }
        }   
        
        public void GetPaymentAmountValue()
        {
            try
            {
                SwitchToSecondPopupWindow();
                ScrollDown();
                string paymentAmount = GetAttributeValue(txtboxDeliveryPaymentCollectCash_CssSelector, "value");
                //convert the string to double
                double paymentAmountDouble = Convert.ToDouble(paymentAmount);
                Console.WriteLine("Payment Amount: " + paymentAmountDouble);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Payment Amount: " + paymentAmountDouble);
                Log.Info("Payment Amount: " + paymentAmountDouble);
                ScrollDown();
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in GetPaymentAmountValue: " + e.Message);
                Log.Error("Error in GetPaymentAmountValue: " + e.Message);
            }

        }

        public string ClickOnAddButtonHandlePaymentPortal(string chargeType)
        {
            try 
            { 
            log.Info("ClickOnSaveButtonHandlePaymentPortal function");
            int noOfWindowsBefore = GetNumberOfWindowsOpened();
            Click(btnAuthCode_Xpath);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Auth Code Button");
                Log.Info("Clicked on Auth Code Button");
            if (IsElementDisplayed(lblEmbargoDetails_Xpath, 1))
            {
                Click(btnContinueEmbargo_Xpath);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Continue button for Embargo");
            }
            WaitForNewWindowToOpen(TimeSpan.FromSeconds(10), noOfWindowsBefore + 1);
            int noOfWindowsAfter = GetNumberOfWindowsOpened();
            if (noOfWindowsAfter > noOfWindowsBefore)
            {
                SwitchToLastWindow();                
                if (!IsElementDisplayed(txtPleaseCloseTabRetry_Xpath, 5) || !IsElementDisplayed(lblAccountInfo_Xpath, 5))
                {
                    RefreshPage();
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Refreshed Payment Portal");
                }

                if (IsElementDisplayed(txtPleaseCloseTabRetry_Xpath, 5))
                {
                    CloseCurrentWindow();
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Closed Payment Portal Tab & Retrying");
                }
                if (chargeType.Equals("CC"))
                {
                    ppp.ConfirmManualPayment();
                    ScrollDown();
                    WaitForElementToBeVisible(lblTotalAmount_Xpath, TimeSpan.FromSeconds(5));
                    totalPaybleAmount = GetText(lblTotalAmount_Xpath).Split("$")[1];
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Total Payable Amount is: " + totalPaybleAmount);
                    ClickOnElementIfEnabled(btnDone_Xpath);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Done Button");
                    WaitForNewWindowToOpen(TimeSpan.FromSeconds(10), noOfWindowsBefore);                    
                }                
                else
                {
                    //Hooks.Hooks.UpdateTest(Status.Fail, "Invalid Charge Type");
                }
                //Hooks.Hooks.UpdateTest(Status.Pass, "Back from Payment Portal");
                SwitchToLastWindow();                
            }
            return totalPaybleAmount;
        }
         catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in ClickOnSaveButtonHandlePaymentPortal: " + e.Message);
                Log.Error("Error in ClickOnSaveButtonHandlePaymentPortal: " + e.Message);
                return totalPaybleAmount;
            }
        }

        public void ClickAcceptPaymentButton()
        {
            try
            {
                ClickOnElementIfPresent(btnAcceptPayment_Xpath);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Accept Payment Button");
                Log.Info("Clicked on Accept Payment Button");
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in ClickAcceptPaymentButton: " + e.Message);
                Log.Error("Error in ClickAcceptPaymentButton: " + e.Message);
            }
        }

        public void DeliveryConfirmationDetails()
        {
            try
            {
                SwitchToPopupWindow();
                SwitchToOPR293Frame();
                string paymentConfirmationColor = GetAttributeValue(txtDeliveryNoteDetailsColor_Xpath, "style");
                string paymentConfirmation = GetText(txtPaymentConfirmation_Xpath);
                if (paymentConfirmationColor.ToLower().Contains("green") && paymentConfirmation.ToLower().Contains("paid"))
                {
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Delivery Note Details are displayed in Green Color and Paid");
                    Log.Info("Delivery Note Details are displayed in Green Color and Paid");
                }
                else
                {
                    //Hooks.Hooks.UpdateTest(Status.Fail, "Delivery Note Details are not displayed in Green Color and Paid");
                    Log.Error("Delivery Note Details are not displayed in Green Color and not Paid");
                }
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in Delivery Confirmation Details: " + e.Message);
                Log.Error("Error in Delivery Confirmation Details: " + e.Message);
            }
        }

        public void CaptureDeliveryDetails()
        {
            try
            {
                Click(btnCapturDelivery_Xpath);                
                Log.Info("Clicked on Capture Delivery Button");
                EnterText(txtBoxDeiveryTo_Xpath, "Test");                
                Log.Info("Entered Delivery To Details");
                ClickOnElementIfPresent(btnSaveDeliveryDetails_Xpath);   
                Log.Info("Clicked on Save Delivery Details Button");
                //Hooks.Hooks.UpdateTest(Status.Pass,"Delivery Details Captured Successfully");
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in Capture Delivery Details: " + e.Message);
                Log.Error("Error in Capture Delivery Details: " + e.Message);
            }
        }



        public void DeliveryReceiptWindow()
        {
            try
            {
                // Ensure the second popup window is available and switch to it
                SwitchToSecondPopupWindow();

                // Get the page title (if needed for logging or validation)
                string pageTitle = GetTitle();

                // Define the screenshot file name and directory
                string screenshotFileName = "DeliveryReceipt.png";
                //string screenshotDirectory = Path.Combine(//Hooks.Hooks.reportPath, "Screenshots");

                // Ensure the directory exists before saving the screenshot
                //if (!Directory.Exists(screenshotDirectory))
                //{
                //    Directory.CreateDirectory(screenshotDirectory);
                //}

                //// Construct the full path for the screenshot
                //string screenshotPath = Path.Combine(screenshotDirectory, screenshotFileName);

                // Take a screenshot and save it to the specified path
                //Hooks.Hooks.captureScreenshot(screenshotPath);

                // Log the screenshot path in the report
                //Hooks.Hooks.UpdateTest(Status.Info, "Delivery Receipt Window Screenshot: " + screenshotPath);

                // Close the current window
                CloseCurrentWindow();

                SwitchToLastWindow();
                SwitchToOPR293Frame();
                Click(btnCloseDeliveryScreen_Xpath);

            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., window switch failure, screenshot failure)
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in Delivery Receipt Window: " + ex.Message);
                throw; // Rethrow the exception if necessary, or log as needed
            }
        }

        public string ClickingYesOnPopupWarnings(string errortype = null)
        {
            string errorText = "";

            if (errortype.Equals("Embargo"))
            {
                if (IsElementDisplayed(lblEmbargoDetails_Xpath, 1))
                {
                    Click(btnContinueEmbargo_Xpath);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Continue button for Embargo");
                }
            }
            else if (errortype.Equals("Capture Irregularity"))
            {
                if (IsElementDisplayed(lblCaptureIrregularity_Xpath, 1))
                {                   
                    cip.captureIrregularity(CreateShipmentPage.pieces, CreateShipmentPage.weight);                   
                }
            }

            else
            {
                SwitchToDefaultContent();
                if (IsElementDisplayed(popupWarning_Css, 1))
                {
                    errorText = GetText(popupAlertMessage_Xpath);
                    if (errorText.Contains("Active Cash Draw"))
                    {
                        Click(btnYesActiveCashDraw_Xpath);
                        //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Yes for Active Cash Draw");
                        WaitForElementToBeInvisible(btnYesActiveCashDraw_Xpath, TimeSpan.FromMilliseconds(500));
                    }

                    else
                    {
                        Click(btnYesActiveCashDraw_Xpath);
                        //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Yes for " + errorText);
                        WaitForTextToBeInvisible(errorText, TimeSpan.FromMilliseconds(500));
                    }
                }               
            }
            return errorText;
        }

        public void EnterCustomerCodeForUnknownConsignee(string customerCode)
        {
            try
            {
                Click(txtCustomerCode_Xpath);
                EnterText(txtCustomerCode_Xpath, customerCode);
                EnterKeys(txtCustomerCode_Xpath, Keys.Enter);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Customer Code: " + customerCode);
                Log.Info("Entered Customer Code: " + customerCode);
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in EnterCustomerCodeForUnknownConsignee: " + e.Message);
                Log.Error("Error in EnterCustomerCodeForUnknownConsignee: " + e.Message);
            }
        }

        public void ValidateOPR393WarningMessage(string expectedWarningMessage)
        {

            WaitForElementToBeVisible(ErrorPopOver_CSS, TimeSpan.FromSeconds(5));
            string actualWarningMessage = GetText(ErrorPopOver_CSS);
            if (!actualWarningMessage.Contains(expectedWarningMessage))
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Warning message is not as expected. Expected: " + expectedWarningMessage + " Actual: " + actualWarningMessage);
                Log.Error("Warning message is not as expected. Expected: " + expectedWarningMessage + " Actual: " + actualWarningMessage);
                Assert.Fail("Warning message is not as expected. Expected: " + expectedWarningMessage + " Actual: " + actualWarningMessage);

            }
            else
            {
                //Hooks.Hooks.UpdateTest(Status.Info, "Warning message is as expected: " + actualWarningMessage);
                Log.Info("Warning message is as expected: " + actualWarningMessage);
            }

        }

    }
}
