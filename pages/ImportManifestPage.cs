using AventStack.ExtentReports;
using iCargoUIAutomation.Features;
using iCargoUIAutomation.utilities;
using log4net;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
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
    public class ImportManifestPage : BasePage
    {
        private CreateShipmentPage csp;
        private ExportManifestPage emp;
        private PageObjectManager pageObjectManager;
        public static string cartUldNum = "";
        ILog Log = LogManager.GetLogger(typeof(ImportManifestPage));

        public ImportManifestPage(IWebDriver driver) : base(driver)
        {
            pageObjectManager = new PageObjectManager(driver);
            emp = pageObjectManager.GetExportManifestPage();
            csp = pageObjectManager.GetCreateShipmentPage();
        }

        //OPR367 - Import Manifest/Screen
        private By contentFrameImportManifest_Xpath = By.XPath("//div[@id='OPR367']/iframe");
        private By txtFlightNoImpManifest_Name = By.Name("flightnumber.flightNumber");
        private By txtFlightDateImpManifest_Name = By.Name("flightnumber.flightDate");
        private By btnListImpManifest_Id = By.Id("CMP_OPERATIONS_FLTHANDLING_IMPORTMANIFEST_LIST");
        private By btnClearImpManifest_Id = By.Id("CMP_OPERATIONS_FLTHANDLING_IMPORTMANIFEST_CLEAR");

        //Import Manifest Headers        
        private By btnOrangePencilEditImportManifest_Css = By.CssSelector(".header-panel .ico-pencil-rounded-orange");

        // ULD Details
        private By btnAddUld_Css = By.CssSelector("#idAddUldButton");
        private By popupAddULD_Xpath = By.XPath("//h5[text()='Add ULD']");
        private By txtUldNum_Xpath = By.XPath("//input[@id='uld_uldNumber']");
        private By btnAdd_Id = By.Id("CMP_OPERATIONS_FLTHANDLING_IMPORTMANIFEST_VALIDATEULD");
        private By lblBulk_Id = By.Id("Bulk");
        private By btnOk_Id = By.Id("CMP_OPERATIONS_FLTHANDLING_IMPORTMANIFEST_ADDULDPOPUP_OK");
        private By btnCancel_Id = By.Id("CMP_OPERATIONS_FLTHANDLING_IMPORTMANIFEST_ADDULDPOPUP_CANCEL");
        private By lblULDdetails_Css = By.CssSelector("#uldDetails");
        private By chkBoxBulk_Css = By.CssSelector("input[name='uldManifestDetailsTable-select']");

        // Import Manifest Warning Messages
        private By lblBreakdownWarningMessageUnsavedData_Css = By.CssSelector(".modal-content .icdialog_message"); // Unsaved data exists. Do you want to continue?
        private By btnOkBreakdownWarningMessageUnsavedData_Xpath = By.XPath("//*[@class='modal-content']//button[text()='Ok']");
        private By lblUldBulkNotManifestedWarningMessage_Xpath = By.XPath("//*[@class='alert-messages-list']//span"); // ULD/BULK is not manifested, Do you want to continue?
        private By btnYesUldBulkNotManifestedWarningMessage_Xpath = By.XPath("//*[@class='ui-dialog-buttonset']/button[text()=' Yes ']");


        //Import Manifest Footer Section        
        private By btnCloseImportManifest_Id = By.Id("CMP_OPERATIONS_FLTHANDLING_IMPORTMANIFEST_CLOSE");
        private By btnBreakDownImportManifest_Id = By.Id("CMP_OPERATIONS_FLTHANDLING_IMPORTMANIFEST_BREAKDOWN");
        private By btnSaveImportManifest_Id = By.Id("CMP_OPERATIONS_FLTHANDLING_IMPORTMANIFEST_SAVE");

        // Breakdown details in OPR004 Breakdown Screen
        private By lblBreakdownDetails_Css = By.CssSelector(".paddR5 h4");
        private By btnAddBreakDownDetail_id = By.Id("btnCreate");
        private By txtBdnLocation_Css = By.CssSelector("input[name='loccode']");
        private By txtRcvdPcs_Css = By.CssSelector("input[name='rcvdPcs']");
        private By txtRcvdWgt_Css = By.CssSelector("input[name='rcvdWgt']");
        private By btnSaveBreakDownDetails_Css = By.CssSelector("#CMP_Operations_Flthandling_Cto_Breakdown_Save");
        private By lblSaveSuccessfulMessage_Css = By.CssSelector(".alert-messages-detail span"); //Saved successfully. Do you want to list the saved details?    
        private By btnYesSaveSuccessfulMessage_Css = By.XPath("//div[@class='ui-dialog-buttonset']/button[normalize-space(text())='Yes']");
        private By btnCloseDialog_Css = By.CssSelector(".dialog-close");

        // Add/ Update Breakdown Details window
        private By txtAWBNumShipmentPrefix_Xpath = By.XPath("//input[@id='awb_p']");
        private By txtAWBNumDocumentNum_Xpath = By.XPath("//input[@id='awb_b']");
        private By btnListAwbDetails_Id = By.Id("CMP_Operations_Flthandling_Cto_Breakdown_Addupdate_List");
        private By txtBreakDownOrigin_Id = By.Id("CMP_Operations_Flthandling_Cto_Breakdown_Addupdate_Origin");
        private By txtBreakDownStatedPieces_id = By.Id("CMP_Operations_Flthandling_Cto_Breakdown_Addupdate_StatedPcs");
        private By btnAddStorageDetails_Css = By.CssSelector(".ic-button-container #btnCreateTwo");
        private By txtBreakDownRcvdPcs_id = By.Id("CMP_Operations_Flthandling_Cto_Breakdown_Addupdate_ReceivedPcs");
        private By txtBreakDownRcvdWgt_Xpath = By.XPath("//input[contains(@id,'receivedWeightID')]");
        private By txtBreakDownLocation_Id = By.Id("CMP_Operations_Flthandling_Cto_Breakdown_Addupdate_Location");
        private By btnOkBreakDown_id = By.Id("CMP_Operations_Flthandling_Cto_Breakdown_Addupdate_OK");
        private By popupWarningRcvdPCWgtGreaterThanManifestedPCWgt_Xpath = By.XPath("//*[@class='alert-messages-list']//span"); //Received Pcs/Weight greater than Manifested Pcs/Weight.Do you want to continue?
        private By btnYesWarningRcvdPCWgtGreaterThanManifestedPCWgt_Xpath = By.XPath("//*[@class='ui-dialog-buttonset']/button[text()=' Yes ']");



        public void SwitchToImportManifestFrame()
        {
            SwitchToFrame(contentFrameImportManifest_Xpath);
            Hooks.Hooks.UpdateTest(Status.Info, "Switched to Import Manifest Frame");
        }

        public void ClickOnFlightTextBox()
        {
            Click(txtFlightNoImpManifest_Name);
            Hooks.Hooks.UpdateTest(Status.Info, "Clicked on Flight Number Text Box");
        }

        public void EnterFlightNumber()
        {            
            EnterText(txtFlightNoImpManifest_Name, CreateShipmentPage.flightNum);
            Hooks.Hooks.UpdateTest(Status.Info, "Entered Flight Number: " + CreateShipmentPage.flightNum);
        }

        public void EnterFlightDate()
        {            
            EnterText(txtFlightDateImpManifest_Name, CreateShipmentPage.shippingDatePST);
            Hooks.Hooks.UpdateTest(Status.Info, "Entered Flight Date: " + CreateShipmentPage.shippingDatePST);
        }

        public void ClickOnListButton()
        {
            Click(btnListImpManifest_Id);
            WaitForElementToBeVisible(lblULDdetails_Css, TimeSpan.FromSeconds(3));
            Hooks.Hooks.UpdateTest(Status.Info, "Clicked on List Button");
        }

        public void ClickOrangePencilToEditManifest()
        {
            Click(btnOrangePencilEditImportManifest_Css);
            Hooks.Hooks.UpdateTest(Status.Info, "Clicked on Orange Pencil to Edit Manifest");
            WaitForElementToBeVisible(btnListImpManifest_Id, TimeSpan.FromSeconds(3));
        }

        public void ClickOnBulkCheckBox()
        {
            Click(chkBoxBulk_Css);
            Hooks.Hooks.UpdateTest(Status.Info, "Clicked on Bulk CheckBox");
        }

        public void ClickOnBreakDownButton()
        {
            Click(btnBreakDownImportManifest_Id);
            Hooks.Hooks.UpdateTest(Status.Info, "Clicked on Breakdown Button");
        }

        public void HandleWarningsDuringBreakdown()
        {
            WaitForElementToBeVisible(lblBreakdownWarningMessageUnsavedData_Css, TimeSpan.FromSeconds(3));
            string warningMessage = GetText(lblBreakdownWarningMessageUnsavedData_Css);
            if (warningMessage.Contains("Unsaved data exists. Do you want to continue?"))
            {
                Click(btnOkBreakdownWarningMessageUnsavedData_Xpath);
                WaitForElementToBeInvisible(lblBreakdownWarningMessageUnsavedData_Css, TimeSpan.FromSeconds(3));
                Hooks.Hooks.UpdateTest(Status.Info, "Clicked on Ok Button for Unsaved Data Warning Message");
            }
            SwitchToDefaultContent();
            WaitForElementToBeVisible(lblUldBulkNotManifestedWarningMessage_Xpath, TimeSpan.FromSeconds(3));
            string warningMessage2 = GetText(lblUldBulkNotManifestedWarningMessage_Xpath);
            if (warningMessage2.Contains("ULD/BULK is not manifested, Do you want to continue?"))
            {
                Click(btnYesUldBulkNotManifestedWarningMessage_Xpath);
                WaitForElementToBeInvisible(lblUldBulkNotManifestedWarningMessage_Xpath, TimeSpan.FromSeconds(3));
                Hooks.Hooks.UpdateTest(Status.Info, "Clicked on Yes Button for ULD/BULK not manifested Warning Message");
            }
            SwitchToImportManifestFrame();
        }


        // Click on Add breakdown details button, switch to the Add/Update Breakdown Details window and enter the details
        public void AddUpdateBreakDownDetails(string bdnLocation, string bdnRcvdPcs, string bdnRcvdWt)
        {
            int noOfWindowsBefore = GetNumberOfWindowsOpened();            
            string awbNum = CreateShipmentPage.awb_num;
            Click(btnAddBreakDownDetail_id);
            WaitForNewWindowToOpen(TimeSpan.FromSeconds(10), noOfWindowsBefore + 1);
            int noOfWindowsAfter = GetNumberOfWindowsOpened();
            if (noOfWindowsAfter > noOfWindowsBefore)
            {
                SwitchToLastWindow();
                Hooks.Hooks.UpdateTest(Status.Info, "Switched to Add/Update Breakdown Details Window");
                WaitForElementToBeVisible(txtAWBNumShipmentPrefix_Xpath, TimeSpan.FromSeconds(3));
                EnterText(txtAWBNumShipmentPrefix_Xpath, awbNum.Split('-')[0]);
                EnterText(txtAWBNumDocumentNum_Xpath, awbNum.Split('-')[1]);
                //Click(btnListAwbDetails_Id);

                string actualOrigin = GetAttributeValue(txtBreakDownOrigin_Id, "value");
                while (true)
                {
                    if (actualOrigin == CreateShipmentPage.origin)                    
                    {
                        break;
                    }
                    else
                    {
                        DoubleClick(btnListAwbDetails_Id);
                        actualOrigin = GetAttributeValue(txtBreakDownOrigin_Id, "value");
                    }
                }
                Thread.Sleep(2000);                         
                EnterText(txtBreakDownRcvdPcs_id, bdnRcvdPcs);
                EnterKeys(txtBreakDownRcvdPcs_id, Keys.Tab);
                EnterText(txtBreakDownLocation_Id, bdnLocation);
                Click(btnOkBreakDown_id);
                SwitchToDefaultContent();
                WaitForElementToBeVisible(popupWarningRcvdPCWgtGreaterThanManifestedPCWgt_Xpath, TimeSpan.FromSeconds(3));
                string warningMessage = GetText(popupWarningRcvdPCWgtGreaterThanManifestedPCWgt_Xpath);
                if (warningMessage.Contains("Received Pcs/Weight greater than Manifested Pcs/Weight.Do you want to continue?"))
                {
                    Click(btnYesWarningRcvdPCWgtGreaterThanManifestedPCWgt_Xpath);                   
                    Hooks.Hooks.UpdateTest(Status.Info, "Clicked on Yes Button for Warning Message");
                    log.Info("Clicked on Yes Button for Warning Message");
                }
                WaitForNewWindowToOpen(TimeSpan.FromSeconds(3), noOfWindowsBefore);
                SwitchToLastWindow();
                SwitchToImportManifestFrame();

            }

        }



        public void EnterBreakdownDetails(string bdnLocation, string bdnRcvdPcs, string bdnRcvdWt)
        {
            WaitForElementToBeVisible(lblBreakdownDetails_Css, TimeSpan.FromSeconds(3));
            EnterText(txtBdnLocation_Css, bdnLocation);
            EnterKeys(txtBdnLocation_Css, Keys.Tab);
            Hooks.Hooks.UpdateTest(Status.Info, "Entered Breakdown Location: " + bdnLocation);

            // if break down received pieces and weight is not none then enter the values
            if (bdnRcvdPcs != "None" && bdnRcvdWt != "None")
            {
                Click(txtRcvdPcs_Css);
                EnterText(txtRcvdPcs_Css, bdnRcvdPcs);
                EnterKeys(txtRcvdPcs_Css, Keys.Tab);
                Hooks.Hooks.UpdateTest(Status.Info, "Entered Received Pieces: " + bdnRcvdPcs);
                EnterText(txtRcvdWgt_Css, bdnRcvdWt);
                EnterKeys(txtRcvdWgt_Css, Keys.Tab);
                Hooks.Hooks.UpdateTest(Status.Info, "Entered Received Weight: " + bdnRcvdWt);
            }

        }

        // validate the expected message after saving the breakdown details
        public void SaveBreakdownAndValidateMessage(string expectedMessage)
        {
            Click(btnSaveBreakDownDetails_Css);
            Hooks.Hooks.UpdateTest(Status.Info, "Clicked on Save Button");
            SwitchToDefaultContent();
            WaitForElementToBeVisible(lblSaveSuccessfulMessage_Css, TimeSpan.FromSeconds(3));
            string actualMessage = GetText(lblSaveSuccessfulMessage_Css);
            if (expectedMessage.Contains(actualMessage))
            {
                Hooks.Hooks.UpdateTest(Status.Pass, "Validated the Message: " + actualMessage);
                if (actualMessage.Contains("Do you want to list the saved details?"))
                {
                    Click(btnYesSaveSuccessfulMessage_Css);
                }
            }
            else
            {
                Hooks.Hooks.UpdateTest(Status.Fail, "Expected Message: " + expectedMessage + " Actual Message: " + actualMessage);
                Click(btnCloseDialog_Css);
            }

            SwitchToImportManifestFrame();
        }

        // adds an ULD through Add ULD button
        public void AddULDinImportManifest()
        {
            Click(btnAddUld_Css);
            WaitForElementToBeVisible(popupAddULD_Xpath, TimeSpan.FromSeconds(3));
            Hooks.Hooks.UpdateTest(Status.Info, "Clicked on Add ULD Button");
            EnterText(txtUldNum_Xpath, "BULK");
            Hooks.Hooks.UpdateTest(Status.Info, "Entered ULD Number: BULK");
            Click(btnAdd_Id);
            WaitForElementToBeVisible(lblBulk_Id, TimeSpan.FromSeconds(3));
            Hooks.Hooks.UpdateTest(Status.Info, "Added ULD: BULK");
            Click(btnOk_Id);
            WaitForElementToBeInvisible(popupAddULD_Xpath, TimeSpan.FromSeconds(3));
        }


        public void CloseOPR367Screen()
        {
            WaitForElementToBeVisible(btnCloseImportManifest_Id, TimeSpan.FromSeconds(5));
            ClickOnElementIfEnabled(btnCloseImportManifest_Id);
            WaitForElementToBeInvisible(btnCloseImportManifest_Id, TimeSpan.FromSeconds(5));
            Hooks.Hooks.UpdateTest(Status.Info, "Close OPR367 Screen");
            SwitchToDefaultContent();

        }


    }
}
