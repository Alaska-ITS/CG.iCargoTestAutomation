using Azure.Storage.Blobs.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCargoUIAutomation.pages
{
    public class ScreeningPage:BasePage
    {
        private MaintainBookingPage mbp;
        private CreateShipmentPage csp;
        private PageObjectManager pageObjectManager;
        public ScreeningPage(IWebDriver driver):base(driver)
        {    
            pageObjectManager = new PageObjectManager(driver);
            mbp = pageObjectManager.GetMaintainBookingPage();
            csp = pageObjectManager.GetCreateShipmentPage();
        }  
        
        //Screening OPR339 Frame
        private By screeningOPR339Frame_ID = By.XPath("//iframe[@name='iCargoContentFrameOPR339']");
        private By awbNumberTextBoxSuffix_ID = By.Id("awbfilter_b");
        private By awbNumberTextBoxPrefix_ID = By.Id("awbfilter_p");
        private By listButton_ID = By.Id("button_list");

        //Screening Pop Up
        private By screeningPopUp_Class = By.ClassName("ui-dialog-buttonset");
        private By screeningPopUpAccept_Class = By.XPath("//button[contains(@class, 'btn-primary') and contains(@class, 'ui-button')]");

        //Screening Details
        private By screeningMethod_ID = By.Id("CMP_Operations_Shipment_AWBAcceptance_Ux_ScreeningMethod");
        private By screeningPieces_ID = By.Id("CMP_Operations_Shipment_AWBAcceptance_Ux_Standardpcs");
        private By screeningWeight_Xpath = By.XPath("//input[contains(@id,'statedWeightID')]");
        private By screeningResult_ID = By.Id("CMP_Operations_Shipment_AWBAcceptance_Ux_result");
        private By screeningAddBtn_Xpath = By.XPath("//button[@name='btnAdd']");

        //Security Details CheckBox
        private By securityDataCheckBox_Xpath = By.XPath("//input[@name='dataReviewed']");
        private By securityStatus_Xpath = By.XPath("//input[@name='newSecurityStatus']");
        private By saveScreeningDetails_Xpath = By.XPath("//button[@name='btSave']");

        //Screening Details saved successfully
        private By screeningDetailsSavedPopup_Xpath = By.XPath("//*[@class='btn btn-default ui-button ui-corner-all ui-widget']");
        private By ScreeningDetailsMessage_Xpath = By.XPath("//*[text()='Screening details saved succesfully']");

        //Create Shipment Page
        private By txtAwbNo_Id = By.Id("awb_b");
        private By btnOrangePencilShipment_Css = By.CssSelector("#view_shipmentDtls a");
        private By lblScreeningDetails_Xpath = By.XPath("//div[@id='screening']");

        public void SwitchToScreeningOPR339Frame()
        {
            SwitchToFrame(screeningOPR339Frame_ID);
        }
        public void EnterAWBNumber()
        {
            string[] awbNumber = CreateShipmentPage.awb_num.Split('-');
            EnterText(awbNumberTextBoxPrefix_ID, awbNumber[0]);
            EnterText(awbNumberTextBoxSuffix_ID, awbNumber[1]);
            Click(awbNumberTextBoxPrefix_ID);
        }

        public void ClickListButton()
        {
            DoubleClick(listButton_ID);
        }

        public void ClickScreeningPopUp()
        {
            SwitchToDefaultContent();
            if (IsElementDisplayed(screeningPopUp_Class))
            {
                WaitForElementToBeVisible(screeningPopUp_Class, TimeSpan.FromSeconds(10));
                Click(screeningPopUpAccept_Class);
            }
            SwitchToScreeningOPR339Frame();
        }

        public void EnterScreeningDetails(string screeningMethod, string screeningResult)
        {
            SelectDropdownByVisibleText(screeningMethod_ID, screeningMethod);
            EnterText(screeningPieces_ID, CreateShipmentPage.pieces);
            EnterText(screeningWeight_Xpath, CreateShipmentPage.weight);            
            SelectDropdownByVisibleText(screeningResult_ID, screeningResult);
            Click(screeningAddBtn_Xpath);
        }

        public void EnterScreeningDetailsForHalfSlatedPieces(string screeningMethod, string screeningResult, string piece)
        {
            SelectDropdownByVisibleText(screeningMethod_ID, screeningMethod);
            EnterText(screeningPieces_ID, piece);
            //divide the weight by pieces to get the weight of each piece
            int pieceWeight = Convert.ToInt32(CreateShipmentPage.weight) / Convert.ToInt32(CreateShipmentPage.pieces);
            int weight = pieceWeight * Convert.ToInt32(piece);
            EnterText(screeningWeight_Xpath, weight.ToString());
            SelectDropdownByVisibleText(screeningResult_ID, screeningResult);
            Click(screeningAddBtn_Xpath);
        }

        public void EnterSecurityDetails()
        {
            Click(securityDataCheckBox_Xpath);            
            ClickElementUsingJavaScript(securityStatus_Xpath);
            Click(saveScreeningDetails_Xpath);
            mbp.ClickingYesOnPopupWarnings(); 
            //mbp.ClickingYesOnPopupWarnings();
        }

        public void VerifyScreeningDetailsSavedSuccessfully()
        {
            SwitchToLastWindow();
            if (IsElementDisplayed(screeningDetailsSavedPopup_Xpath))
            {
                WaitForElementToBeVisible(screeningDetailsSavedPopup_Xpath, TimeSpan.FromSeconds(10));
                string screeningPopup = GetText(ScreeningDetailsMessage_Xpath);
                Console.WriteLine("Screening Details Saved Successfully: " + screeningPopup);
                Click(screeningDetailsSavedPopup_Xpath);
            }                      
        }

        public void EnterAwbNumberLTE()
        {
            csp.SwitchToLTEContentFrame();
            //EnterText(txtAwbNo_Id, "33511085");
            string[] screenedAwbNumber = CreateShipmentPage.awb_num.Split('-');
            EnterText(txtAwbNo_Id, screenedAwbNumber[1]);
            csp.ClickOnListButton();
            ScrollDown();
            Click(lblScreeningDetails_Xpath);
            ScrollDown();
        }
    }
}
