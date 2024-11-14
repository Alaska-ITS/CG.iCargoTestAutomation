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

        //Export Manifest Headers        
        private By btnOrangePencilEditImportManifest_Css = By.CssSelector(".header-panel .ico-pencil-rounded-orange");

        // ULD Details
        private By lblULDdetails_Css = By.CssSelector("#uldDetails");
        private By chkBoxBulk_Css = By.CssSelector("input[name='uldManifestDetailsTable-select']");

        //Export Manifest Footer Section        
        private By btnCloseImportManifest_Id = By.Id("CMP_OPERATIONS_FLTHANDLING_IMPORTMANIFEST_CLOSE");
        private By btnBreakDownImportManifest_Id = By.Id("CMP_OPERATIONS_FLTHANDLING_IMPORTMANIFEST_BREAKDOWN");
        private By btnSaveImportManifest_Id = By.Id("CMP_OPERATIONS_FLTHANDLING_IMPORTMANIFEST_SAVE");        

        // Breakdown details in OPR004 Breakdown Screen
        private By lblBreakdownDetails_Css = By.CssSelector(".paddR5 h4");
        private By txtBdnLocation_Css = By.CssSelector("input[name='loccode']");
        private By txtRcvdPcs_Css = By.CssSelector("input[name='rcvdPcs']");
        private By txtRcvdWgt_Css = By.CssSelector("input[name='rcvdWgt']");
        private By btnSaveBreakDownDetails_Css = By.CssSelector("#CMP_Operations_Flthandling_Cto_Breakdown_Save");
        private By lblSaveSuccessfulMessage_Css = By.CssSelector(".alert-messages-detail span"); //Saved successfully. Do you want to list the saved details?    
        private By btnYesSaveSuccessfulMessage_Css = By.XPath("//div[@class='ui-dialog-buttonset']/button[normalize-space(text())='Yes']");
        private By btnCloseDialog_Css = By.CssSelector(".dialog-close");


        public void SwitchToImportManifestFrame()
        {
            SwitchToFrame(contentFrameImportManifest_Xpath);
            Hooks.Hooks.UpdateTest(Status.Info,"Switched to Import Manifest Frame");
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
            WaitForElementToBeVisible(lblBreakdownDetails_Css, TimeSpan.FromSeconds(3));
        }

        public void EnterBreakdownDetails(string bdnLocation, string bdnRcvdPcs, string bdnRcvdWt)
        {
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
