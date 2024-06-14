using iCargoUIAutomation.Features;
using iCargoUIAutomation.utilities;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V121.Debugger;
using SharpCompress.Compressors.Xz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace iCargoUIAutomation.pages
{
    public class ExportManifestPage : BasePage
    {
        public static string cartUldNum = "";
        ILog Log = LogManager.GetLogger(typeof(ExportManifestPage));


        public ExportManifestPage(IWebDriver driver) : base(driver)
        {

        }

        //OPR344 - Export Manifest/Screen
        private By contentFrameManifest_Xpath = By.XPath("//div[@id='OPR344']/iframe");
        private By txtFlightNoManifest_Name = By.Name("flightnumber.flightNumber");
        private By txtFlightDateManifest_Name = By.Name("flightnumber.flightDate");
        private By btnListManifest_Id = By.Id("CMP_Operations_FltHandling_ExportManifest_List");
        private By btnClearManifest_Id = By.Id("CMP_Operations_FltHandling_ExportManifest_Clear");

        //Export Manifest Headers 
        private By lblStatus_CSS = By.CssSelector(".header-panel .portlet-footer strong");

        //Assigned Shipment;
        private By btnAddULD_Xpath = By.XPath("//button//span[text()='Add ULD']");
        private By chkBoxBarrow_Xpath = By.XPath("//*[text()='Barrow']/preceding-sibling::input");
        private By txtULDNumber_Name = By.Name("uld.uldNumber");
        private By drpdwnPOU_Xpath = By.XPath("//*[contains(@class,'assignshipments')]//*[text()='POU']//following-sibling::div");
        private By drpdwnPOUCOMBO_Xpath = By.XPath("//*[@class='Select-input' and @role='combobox']");
        private By btnSave_Xpath = By.XPath("//button[text()='Save ']");
        private By btnClose_Xpath = By.XPath("//*[contains(@class,'assignshipments')]//button[text()='Close']");
        private By txtAssignShipmentFilter_Xpath = By.XPath("//*[contains(@class,'assignshipments')]//*[@class='table-header__search']/input");
        private By chkBoxSelectCartULDNum_Xpath = By.XPath("//*[contains(@class,'assignshipments')]//input[@id='select-0']");
        private By btnCartULDNum_Xpath = By.XPath("//*[@aria-describedby='uldNumber']");

        // Planned Shipment
        private By chkBoxIncludeQueued_Xpath = By.XPath("//*[text()='Include Queued']//preceding-sibling::input");
        private By txtPlannedShipmentFilter_Xpath = By.XPath("//*[contains(@class,'planned-shipment')]//*[@class='table-header__search']/input");
        private By chkBoxSelectAWBNum_Xpath = By.XPath("//*[contains(@class,'planned-shipment')]//input[@id='select-0']");

        //Lying List
        private By btnLyingList_Xpath = By.XPath("//*[text()='Lying List']");
        private By btnLyingListAWB_Xpath = By.XPath("//button[text()='AWB(s)']");
        private By btnLyingListFilter_Id = By.Id("lyingListTable-datafilter");
        private By txtLyingListFilterOrigin_Name = By.Name("filter.origin");
        private By drpdwnReadyForcarriage_Xpath = By.XPath("//label[text()='Ready For Carriage']/parent::div//*[@class='Select-control']");
        private By btnApplyFilter_Id = By.Id("lyingListTable-datafilter-applybtn");
        private By chkBoxLyingListAWB_Xpath = By.XPath("//*[contains(@class,'lying-list')]//input[@id='select-0']");

        //Export Manifest Warning Popups
        private By WarningPopup_Xpath = By.XPath("//*[@aria-describedby='popupContainer']");
        private By lblWarningPopup_Xpath = By.XPath("//*[@aria-describedby='popupContainer']//span");
        private By framePopupContainerFrame_Id = By.Id("popupContainerFrame");
        private By lblEmbargoDescription_Xpath = By.XPath("//table[@id='showEmbargo']//td[text()='Description']");
        private By btnContinueWarningPopup_Xpath = By.XPath("//button[@name='btContinue']");
        private By lblWarningMessageModal_CSS = By.CssSelector(".modal-content .icdialog_message");
        private By btnOkWarningMessageModal_Xpath = By.XPath("//*[@class='modal-footer']/button[text()='Ok']");
        private By lblErrorPopOver_CSS = By.CssSelector(".errorpopover p");


        //Export Manifest Footer Section
        private By btnManifest_Xpath = By.XPath("//*[@class='footer-panel']//button[text()='Manifest']");
        private By btnCloseManifest_Xpath = By.XPath("//*[@class='footer-panel']//button[text()='Close']");

        //Print Manifest Window
        private By lblPrintManifest_Xpath = By.XPath("//*[text()='Print Manifest']");
        private By framePrintManifest_Id = By.Id("popupContainerFrame");
        private By btnOKPrintManifest_Name = By.Name("btOk");
        private By btnClosePrintManifest_Name = By.Name("btClose");

        //Manifest PDF Report
        private By frameManifestPDF_Id = By.Id("ReportContainerFrame");
        private By btnSavePDFMenu_Xpath = By.XPath("//*[@id='__menuBar']//*[@id='save']");
        private By btnSavePDF_Xpath = By.XPath("//*[@class='__submenu __menuSave open']//div[@id='ok']");


        public void SwitchToManifestFrame()
        {
            SwitchToFrame(contentFrameManifest_Xpath);
        }

        public void ClickOnFlightTextBox()
        {
            Click(txtFlightNoManifest_Name);
        }

        public void EnterFlightNumber(string flightNumber)
        {
            EnterText(txtFlightNoManifest_Name, flightNumber);
        }

        public void EnterFlightDate(string flightDate)
        {
            EnterText(txtFlightDateManifest_Name, flightDate);
        }

        public void ClickOnListButton()
        {
            Click(btnListManifest_Id);
        }

        public string CreateULDOrCart(string cartType, string POU)
        {
            ClickOnADDULDButton();
            int cartUldNumInt = GetCartOrULDNumber(cartType);

            while (true)
            {
                if (cartType.ToLower() == "cart")
                {
                    cartUldNum = "BC" + cartUldNumInt;
                }
                else
                {
                    cartUldNum = "AAA" + cartUldNumInt + "AS";
                }


                EnterTextWithCheck(txtULDNumber_Name, cartUldNum);
                EnterKeys(txtULDNumber_Name, Keys.Tab);
                Thread.Sleep(2000);
                //WaitForElementToBeClickable(drpdwnPOU_Xpath, TimeSpan.FromSeconds(5));


                if (GetAttributeValue(drpdwnPOUCOMBO_Xpath, "aria-disabled") == "false")
                {
                    break;
                }
                else
                {
                    cartUldNumInt++;
                }
            }
            SelectPOU(POU);
            ClickOnSaveULDButton();
            ClickOnCloseULDButton();

            return cartUldNum;
        }

        public void ClickOnADDULDButton()
        {
            Click(btnAddULD_Xpath);
        }

        public int GetCartOrULDNumber(string cartType)
        {
            int cartUldNumInt = 0;
            if (cartType.ToLower() == "cart")
            {
                Click(chkBoxBarrow_Xpath);
                cartUldNumInt = RandomNumber(5);
            }
            else
            {
                cartUldNumInt = RandomNumber(4);
            }

            return cartUldNumInt;
        }

        public void SelectPOU(string POU)
        {

            //WaitForElementToBeClickable(drpdwnPOU_Xpath, TimeSpan.FromSeconds(15)); 
            //WaitForElementToBeVisible(drpdwnPOU_Xpath, TimeSpan.FromSeconds(5));
            Click(drpdwnPOU_Xpath);
            WaitForElementToBeVisible(By.XPath($"//div[contains(@class,'assignshipments')]//div[text()='{POU}']"), TimeSpan.FromSeconds(5));

            By optionXpath = By.XPath($"//div[contains(@class,'assignshipments')]//div[text()='{POU}']");
            Click(optionXpath);

            //SelectDropdownByVisibleTextUsingActions(drpdwnPOU_Xpath, POU);

        }

        public void ClickOnSaveULDButton()
        {
            WaitForElementToBeVisible(btnSave_Xpath, TimeSpan.FromSeconds(5));
            ClickOnElementIfEnabled(btnSave_Xpath);
            WaitForElementToBeDisabled(txtULDNumber_Name, TimeSpan.FromSeconds(10));
        }

        public void ClickOnCloseULDButton()
        {
            //Click(btnClose_Xpath);           
            WaitForElementToBeVisible(btnClose_Xpath, TimeSpan.FromSeconds(5));
            ClickOnElementIfEnabled(btnClose_Xpath);
        }

        //public void FilterOutPlannedAWBAndULD(string AWB_Number, string Cart_Uld_Num)
        //{

        //    Click(txtAssignShipmentFilter_Xpath);

        //    while (true)
        //    {
        //        if (IsElementDisplayed(By.CssSelector("[data-uldnumber='" + Cart_Uld_Num + "']")))
        //        {
        //            break;
        //        }
        //        EnterTextWithCheck(txtAssignShipmentFilter_Xpath, Cart_Uld_Num);
        //        EnterKeys(txtAssignShipmentFilter_Xpath, Keys.Tab);

        //    }
        //    WaitForElementToBeVisible(By.CssSelector("[data-uldnumber='" + Cart_Uld_Num + "']"), TimeSpan.FromSeconds(10));

        //    ClickOnElementIfEnabled(chkBoxSelectCartULDNum_Xpath);
        //    ClickOnElementIfEnabled(chkBoxIncludeQueued_Xpath);
        //    WaitForElementToBeVisible(txtPlannedShipmentFilter_Xpath, TimeSpan.FromSeconds(10));
        //    EnterTextWithCheck(txtPlannedShipmentFilter_Xpath, AWB_Number);
        //    WaitForElementToBeVisible(chkBoxSelectAWBNum_Xpath, TimeSpan.FromSeconds(10));

        //    ClickOnElementIfEnabled(chkBoxSelectAWBNum_Xpath);
        //    ClickOnElementIfEnabled(btnCartULDNum_Xpath);
        //    HandleWarningMessage();
        //}

        public void FilterOutPlannedAWBAndULD(string AWB_Number, string Cart_Uld_Num)
        {
            int attempts = 0;
            while (attempts < 2)
            {
                try
                {
                    Click(txtAssignShipmentFilter_Xpath);

                    while (true)
                    {
                        if (IsElementDisplayed(By.CssSelector("[data-uldnumber='" + Cart_Uld_Num + "']")))
                        {
                            break;
                        }
                        EnterTextWithCheck(txtAssignShipmentFilter_Xpath, Cart_Uld_Num);
                        EnterKeys(txtAssignShipmentFilter_Xpath, Keys.Tab);
                    }

                    ClickOnElementIfEnabled(chkBoxSelectCartULDNum_Xpath);
                    ClickOnElementIfEnabled(chkBoxIncludeQueued_Xpath);
                    WaitForElementToBeVisible(txtPlannedShipmentFilter_Xpath, TimeSpan.FromSeconds(10));
                    EnterTextWithCheck(txtPlannedShipmentFilter_Xpath, AWB_Number);
                    WaitForElementToBeVisible(chkBoxSelectAWBNum_Xpath, TimeSpan.FromSeconds(10));

                    ClickOnElementIfEnabled(chkBoxSelectAWBNum_Xpath);
                    ClickOnElementIfEnabled(btnCartULDNum_Xpath);
                    HandleWarningMessage();
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    attempts++;
                }
            }
        }


        public void FilterOutLyingListAWBAndULD(string Cart_Uld_Num, string readyForCarriageOption="Yes")
        {
            ClickOnLyingList();
            ClickOnLyingListFilter();
            SelectReadyForCarriage(readyForCarriageOption);
            ClickOnApplyFilter();
            ClickOnCheckBoxLyingListAWB();
            PlaceShipmentOnCartToManifest();
            HandleWarningMessage();
        }

        public void ClickOnLyingList()
        {
            Click(btnLyingList_Xpath);            
            WaitForElementToBeVisible(btnLyingListFilter_Id, TimeSpan.FromSeconds(3));
        }

        public void ClickOnLyingListFilter()
        {
            try
            {
                if (IsElementDisplayed(chkBoxLyingListAWB_Xpath))
                {
                    Click(btnLyingListFilter_Id);
                }
                else
                {
                   Log.Error("AWB is not available in Lying List");
                }
            }
            catch
            {
                Log.Error("Error in clicking on the Lying List");
            }           
            
            
        }

        public void SelectReadyForCarriage(string optionReadyForCarriage)
        {
            Click(drpdwnReadyForcarriage_Xpath);
            Click(By.XPath($"//div[contains(@class,'lying-list')]//div[text()='{optionReadyForCarriage}']"));
        }

        public void ClickOnApplyFilter()
        {
            Click(btnApplyFilter_Id);
            WaitForElementToBeInvisible(btnApplyFilter_Id, TimeSpan.FromSeconds(5));

        }

        public void ClickOnCheckBoxLyingListAWB()
        {
            Click(chkBoxLyingListAWB_Xpath);
        }

        public void PlaceShipmentOnCartToManifest()
        {
            EnterTextWithCheck(txtAssignShipmentFilter_Xpath, cartUldNum);
            EnterKeys(txtAssignShipmentFilter_Xpath, Keys.Tab);
            ClickOnElementIfEnabled(chkBoxSelectCartULDNum_Xpath);
            ClickOnElementIfEnabled(btnCartULDNum_Xpath);             
        }

        public void ValidateWarningMessageAndCloseModal(string messageToValidate)
        {
            WaitForElementToBeVisible(lblWarningMessageModal_CSS, TimeSpan.FromSeconds(5));
            string warningModalMessage = GetText(lblWarningMessageModal_CSS);
            if (warningModalMessage.Contains(messageToValidate))
            {
                
                while (IsElementDisplayed(btnOkWarningMessageModal_Xpath))
                {
                    Click(btnOkWarningMessageModal_Xpath);                   
                }   
               
                WaitForElementToBeInvisible(btnOkWarningMessageModal_Xpath, TimeSpan.FromSeconds(3));
            }
            else
            {
                Assert.Fail("Warning message is not displayed as expected");
            }
           
        }


        public void HandleWarningMessage()
        {
            
            if (IsElementDisplayed(WarningPopup_Xpath))
            {
                string popupTitle = GetText(lblWarningPopup_Xpath);

                if (popupTitle == "Check Embargo")
                {
                    SwitchToFrame(framePopupContainerFrame_Id);
                    Click(lblEmbargoDescription_Xpath);  
                    while(IsElementDisplayed(btnContinueWarningPopup_Xpath))
                    {
                        EnterKeys(btnContinueWarningPopup_Xpath, Keys.Enter);
                    }
                   
                    //Click(btnContinueWarningPopup_Xpath);
                                     
                   // WaitForElementToBeInvisible(framePopupContainerFrame_Id, TimeSpan.FromSeconds(2));
                    SwitchToManifestFrame();
                }
            }
        }

        public void clickOnManifestButton()
        {
            WaitForElementToBeVisible(btnManifest_Xpath, TimeSpan.FromSeconds(5));            
            Click(btnManifest_Xpath);
            WaitForElementToBeInvisible(contentFrameManifest_Xpath, TimeSpan.FromSeconds(5));
        }


        public void PrintManifestWindow()
        {
            if (IsElementDisplayed(lblPrintManifest_Xpath))
            {
                SwitchToFrame(framePrintManifest_Id);
                int noOfWindowsOpenedCurrently = GetNumberOfWindowsOpened();
                ClickOnElementIfPresent(btnOKPrintManifest_Name);
                WaitForElementToBeInvisible(framePrintManifest_Id, TimeSpan.FromSeconds(5));
                int noOfWindowsOpenedAfterButtonClicked = GetNumberOfWindowsOpened();
                if (noOfWindowsOpenedCurrently < noOfWindowsOpenedAfterButtonClicked)
                {
                    SwitchToLastWindow();
                    SaveManifestPDF();
                }
                if (IsElementDisplayed(lblPrintManifest_Xpath))
                {
                    SwitchToFrame(framePrintManifest_Id);
                    Click(btnClosePrintManifest_Name);
                    WaitForElementToBeInvisible(framePrintManifest_Id, TimeSpan.FromSeconds(3));
                    CurrentWindowHandle();
                    SwitchToDefaultContent();
                }
            }
        }

        public void ClosePrintPDFWindow()
        {
            if (IsElementDisplayed(lblPrintManifest_Xpath))
            {
                SwitchToFrame(framePrintManifest_Id);
                Click(btnClosePrintManifest_Name);   
                WaitForElementToBeInvisible(framePrintManifest_Id, TimeSpan.FromSeconds(3));
                SwitchToDefaultContent();
                SwitchToManifestFrame();
            }
        }

        public void ValidateAWBStatusInExportManifest(string expectedAWBStatus)
        {
            WaitForElementToBeVisible(lblStatus_CSS, TimeSpan.FromSeconds(5));
            string actualAWBStatus = GetText(lblStatus_CSS);
            if (actualAWBStatus.Contains(expectedAWBStatus))
            {
                Log.Info("AWB status is as expected: " + actualAWBStatus);
            }
            else
            {
                Log.Error("AWB status is not as expected. Expected: " + expectedAWBStatus + " Actual: " + actualAWBStatus);
                Assert.Fail("AWB status is not as expected. Expected: " + expectedAWBStatus + " Actual: " + actualAWBStatus);
            }
        }

        public void SaveManifestPDF()
        {
            SwitchToFrame(frameManifestPDF_Id);
            Click(btnSavePDFMenu_Xpath);
            WaitForElementToBeVisible(btnSavePDF_Xpath, TimeSpan.FromSeconds(5));
            Click(btnSavePDF_Xpath);
            WaitForElementToBeInvisible(btnSavePDF_Xpath, TimeSpan.FromSeconds(5));
            CloseCurrentWindow();
            SwitchToLastWindow();
            SwitchToManifestFrame();
        }
        public void ValidateOPR344WarningMessage(string expectedWarningMessage)
        {

            WaitForElementToBeVisible(lblErrorPopOver_CSS, TimeSpan.FromSeconds(5));
            string actualWarningMessage = GetText(lblErrorPopOver_CSS);
            if (!actualWarningMessage.Contains(expectedWarningMessage))
            {
                Log.Error("Warning message is not as expected. Expected: " + expectedWarningMessage + " Actual: " + actualWarningMessage);
                Assert.Fail("Warning message is not as expected. Expected: " + expectedWarningMessage + " Actual: " + actualWarningMessage);

            }
            else
            {
                //Assert.Pass("Warning message is as expected: " + actualWarningMessage);
                Log.Info("Warning message is as expected: " + actualWarningMessage);
            }
        }

        public void CloseOPR344Screen()
        {
            //SwitchToManifestFrame();
            WaitForElementToBeVisible(btnCloseManifest_Xpath, TimeSpan.FromSeconds(5));
            //Click(btnCloseManifest_Xpath);
            ClickOnElementIfEnabled(btnCloseManifest_Xpath);
            SwitchToDefaultContent();

        }


    }
}
