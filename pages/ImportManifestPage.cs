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

        //Export Manifest Footer Section        
        private By btnCloseImportManifest_Id = By.Id("CMP_OPERATIONS_FLTHANDLING_IMPORTMANIFEST_CLOSE");
        private By btnBreakDownImportManifest_Id = By.Id("CMP_OPERATIONS_FLTHANDLING_IMPORTMANIFEST_BREAKDOWN");
        private By btnSaveImportManifest_Id = By.Id("CMP_OPERATIONS_FLTHANDLING_IMPORTMANIFEST_SAVE");        


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

        public void CloseOPR344Screen()
        {            
            WaitForElementToBeVisible(btnCloseImportManifest_Id, TimeSpan.FromSeconds(5));            
            ClickOnElementIfEnabled(btnCloseImportManifest_Id);
            WaitForElementToBeInvisible(btnCloseImportManifest_Id, TimeSpan.FromSeconds(5));
            Hooks.Hooks.UpdateTest(Status.Info, "Close OPR367 Screen");
            SwitchToDefaultContent();

        }


    }
}
