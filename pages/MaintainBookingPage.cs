using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using FluentAssertions.Execution;
using iCargoUIAutomation.Hooks;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V121.Debugger;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace iCargoUIAutomation.pages
{

    public class MaintainBookingPage : BasePage
    {
        string shippingDate = DateTime.Now.ToString("dd-MMM-yyyy");
        string presentdate = DateTime.Now.ToString("dd-MMM");
        public static string firstflightnum = "";
        public static ExtentTest _scenario= Hooks.Hooks._scenario;
        public static ExtentTest test;
        ILog Log = LogManager.GetLogger(typeof(MaintainBookingPage));
        public MaintainBookingPage(IWebDriver driver) : base(driver)
        {
        }

        private By CAP018Frame_XPATH = By.XPath("//iframe[@name='iCargoContentFrameCAP018']");
        private By newList_XPATH = By.XPath("//button[@id='btDisplay']");
        private By homePage_CSS = By.CssSelector(".ic-home-tab");
        // Shipment Details
        private By origin_ID = By.Id("origin");
        private By destination_XPATH = By.XPath("//input[@name='destination']");
        private By agentCode_ID = By.Id("agentCode");
        private By shippingDate_ID = By.Id("shippingDate");
        private By product_XPATH = By.XPath("//input[@name='productName']");
        private By shipperConsigneeBtn_ID = By.Id("CMP_Capacity_Booking_MaintainReservation_ShipperDetails_Button");

        // Shipper Details
        private By shipperConsigneePopup_CLASS = By.ClassName("iCargoPopUpContent");
        private By shipperCode_XPATH = By.XPath("//input[@name='shipperCode']");
        private By consigneeCode_XPATH = By.XPath("//input[@name='consigneeCode']");
        private By shipperConsigneeOkBtn_ID = By.Id("CMP_Capacity_Booking_MaintainReservation_ShipperConsignee_OK");

        // Commodity Details
        private By bookingPage_XPATH = By.XPath("//div[@class='ic-main-container']");
        private By commoityDetailSection_XPATH = By.XPath("//div[@id='handlingInfRow']");
        private By commodityCode_XPATH = By.XPath("//input[@name='commodityCode']");
        private By pieces_XPATH = By.XPath("//input[@name='pieces']");
        private By weight_XPATH = By.XPath("//input[@name='weight']");

        // Flight Details
        private By flightOrigin_XPATH = By.XPath("//input[@name='flightOrigin']");
        private By flightDestination_XPATH = By.XPath("//input[@name='flightDestination']");
        private By flightNumber_XPATH = By.XPath("//input[@name='flightNumber']");
        private By flightDate_XPATH = By.XPath("//input[@name='flightDate']");
        private By flightPieces_XPATH = By.XPath("//input[@name='flightPieces']");
        private By flightWeight_XPATH = By.XPath("//input[@name='flightWeight']");

        // Generate AWB
        private By saveBtn_ID = By.Id("btSave");

        // Warning Popups
        private By popupAlertMessage_CSS = By.CssSelector(".alert-messages-list");
        private By popupAlertWarningBooking_CSS = By.CssSelector(".alert-messages-ui");
        private By popupAlertMessageBooking_XPATH = By.XPath("//*[@class='alert-messages-list']//span");
        private By btnYesAlertMessageBooking_XPATH = By.XPath("//*[@class='ui-dialog-buttonpane ui-widget-content ui-helper-clearfix']//*[text()=' Yes ']");

        // Booking Summary
        private By bookingSummaryPopup_CSS = By.CssSelector(".iCargoPopUpContent");
        private By awbNumber_XPATH = By.XPath("//label[contains(text(), 'AWB : ')]/b");
        private By btnOkBookingSummaryPopup_XPATH = By.XPath("//button[@id='CMP_Capacity_Booking_BookingSummary_Ok_Button']");


        //Close Maintain Booking Page
        private By btnCloseMb_XPATH = By.Id("CMP_Capacity_Booking_MaintainReservation_Close_Button");

        //Rebook an already executed awb
        private By awbTextbox_ID = By.Id("awbNum_b");
        private By alreadyExecutedPopup_XPATH = By.XPath("//div[@id='app-message-wrap']/div/span");
        private By flightCheckBox_ID = By.Id("shipmentFlightSelectAll");
        private By deleteFlightDetails_ID = By.Id("deleteFlightLink");
        private By addFlight_ID = By.Id("addFlightLink");
        string dynamicFlightOrigin = "flightOrigin";
        private By dynamicFlightOrigin_ID = By.Id("flightOrigin1");
        string dynamicFlightDestination = "flightDestination";
        private By dynamicFlightDestination_ID = By.Id("flightDestination1");
        string dynamicFlightNumber = "flightNumber";
        private By dynamicFlightNumber_ID = By.Id("flightNumber1");
        string dynamicFlightDate = "fltDate";
        private By dynamicFlightDate_ID = By.Id("fltDate1");
        string dynamicFlightPieces = "flightPieces";
        private By dynamicFlightPieces_ID = By.Id("flightPieces1");
        private By dynamicFlightWeight_XPATH = By.XPath("//input[@name='flightWeight' and @rowcount = '1']");
        private By bookingIrregularityFrame_ID = By.Id("popupContainerFrame");
        private By irregularityScrollHori_XPATH = By.XPath("//div[@class='portlet row form-body scroll-z-index-fix']//div[@class='slimScrollArrowUpX']");
        private By irregularityTextbox_ID = By.Id("irregularityCodeID_00");
        private By irregularityRemarks_XPATH = By.XPath("//textarea[@id='CMP_Operations_Shipment_Cto_CaptureIrregularity_Remarks0']");
        private By irregularitySaveBtn_ID = By.Id("CMP_Operations_Shipment_Ux_Cto_CaptureIrregularity_Ok");
        private By selectFlightBtn_Xpath = By.XPath("//div[@class='ReactVirtualized__Table__rowColumn']//button[text()='Select']");
        private By rebookFlightDetails_Xpath = By.XPath("//div[@aria-colindex='6']/div/span/strong/label");
        private By rebookSelectFlightbtn_Xpath = By.XPath("//div[@aria-colindex='8']/div/button");
        private By rebookRateStatus_btn_Xpath = By.XPath("//div[@data-id='rateIndicator']");

        //AVI Booking Checksheet Details
        private By aviBookingChecksheet_XPATH = By.XPath("//select[@name='questionwithAnswer[0].templateAnswer']");
        private By aviBookingChecksheetOkBtn_XPATH = By.XPath("//button[@id='btnSave']");
        private By aviTotalChkSheetSections_Xpath = By.XPath("//*[@id='tabs-1']//div[@id='configId']/h2");
        private By aviChecksheetFrame_XPath = By.XPath("//*[text()='Capture Check Sheet']//parent::div//following-sibling::div/iframe");

        //Attach /Detach AWB
        private By attachDetachBtn_ID = By.Id("btDetach");
        private By attachDetachAwbField_ID = By.Id("awbNumNew_b");
        private By attachDetachPopupBtn_ID = By.Id("CMP_CAPACITY_BOOKING_DETACHAWB_DETACH_BUTTON");

        //Minimum Connection Time for MultiLeg Flight
        private By selectFlightBtn_ID = By.Id("btSelectFlight");
        private By flightSearchTextBox_XPATH = By.XPath("//input[@placeholder='Enter the keywords to search']");
        private By resColor_Xpath = By.XPath("//label[@class='badge-red']");
        private By resErrorMessage_Xpath = By.XPath("//div[@class='fs12 mar-t-xs text-gray multy-list-flight']/p");
        private By multilegFlightsfilter = By.Id("flightsTable-datafiltercontainer");
        private By oneStopFilter_Xpath = By.XPath("//input[@name='stops.stop1']");
        private By twoStopFilter_Xpath = By.XPath("//input[@name='stops.stop2']");
        private By twoplusStopFilter_Xpath = By.XPath("//input[@name='stops.stop2plus']");
        private By filterApplyBtn_Xpath = By.XPath("//button[@id='flightsTable-datafilter-applybtn']");
        private By multilegFlights_Xpath = By.XPath("//div[@data-id='totalNoOfflights']/div/span/strong[1]");

        //Select Flight
        private By cap_Xpath = By.XPath("//label[contains(@id,'capStatus')]");
        private By rest_Xpath = By.XPath("//label[contains(@id,'restStatus')]");
        private By emb_Xpath = By.XPath("//label[contains(@id,'embargoStatus')]");
        private By rate_Xpath = By.XPath("//label[contains(@id,'rateStatus')]");
        private By flightDetailsOkbtn_Xpath = By.XPath("//button[@accesskey='K']");
        private By flightDetailsSection_XPATH = By.CssSelector(".table_grid .ReactVirtualized__Table__row");
        private By flightProductCode_Xpath = By.XPath("//div[@class='d-flex justify-content-between']/strong[1]");
        private By flightDate_Xpath = By.XPath("//div[@data-id='departureArrivalDate']");
        private By flightDepartureTime_Xpath = By.XPath("//div[@data-id='departureTime']");
        private By selectFlightNumber_Xpath = By.XPath("//div[contains(@class, 'ReactVirtualized__Table__rowColumn')]//strong[@class='align-self-center']/label");
        private By generalProdBtn_Xpath = By.XPath("//strong[text()='GENERAL']/parent::div/parent::div/parent::div");
        private By priorityProdBtn_Xpath = By.XPath("//strong[text()='PRIORITY']/parent::div/parent::div/parent::div");
        private By employeeProdBtn_Xpath = By.XPath("//strong[text()='EMPLOYEE SHIPMENT']/parent::div/parent::div/parent::div");
        private By goldstreakProdBtn_Xpath = By.XPath("//strong[text()='GOLDSTREAK']/parent::div/parent::div/parent::div");
        private By petConnectProdBtn_Xpath = By.XPath("//strong[text()='PET CONNECT']/parent::div/parent::div/parent::div");

        //Unknown Shipper Consignee Details
        private By unkShipperName_ID = By.Id("CMP_Capacity_Booking_MaintainReservation_ShipperConsignee_ShipperName_NEW");
        private By unkShipperFirstAddress_ID = By.Id("CMP_Capacity_Booking_MaintainReservation_ShipperConsignee_ShipperFirstAddress_NEW");
        private By unkShipperSecondAddress = By.Id("CMP_Capacity_Booking_MaintainReservation_ShipperConsignee_ShipperSecondAddress_NEW");
        private By unkShipperCity_ID = By.Id("CMP_Capacity_Booking_MaintainReservation_ShipperConsignee_ShipperCity_NEW");
        private By unkShipperState_ID = By.Id("CMP_Capacity_Booking_MaintainReservation_ShipperConsignee_ShipperState");
        private By unkShipperCountry_ID = By.Id("CMP_Capacity_Booking_MaintainReservation_ShipperConsignee_ShipperCountry");
        private By unkShipperZip_ID = By.Id("CMP_Capacity_Booking_MaintainReservation_ShipperConsignee_ShipperPostalCode");
        private By unkShipperEmail_ID = By.Id("CMP_Capacity_Booking_MaintainReservation_ShipperConsignee_ShipperEmail");
        private By unkConsigneeName_ID = By.Id("CMP_Capacity_Booking_MaintainReservation_ShipperConsignee_ConsigneeName_NEW");
        private By unkConsigneeFirstAddress_ID = By.Id("CMP_Capacity_Booking_MaintainReservation_ShipperConsignee_ConsigneeFirstAddress_NEW");
        private By unkConsigneeSecondAddress = By.Id("CMP_Capacity_Booking_MaintainReservation_ShipperConsignee_ConsigneeSecondAddress_NEW");
        private By unkConsigneeCity_ID = By.Id("CMP_Capacity_Booking_MaintainReservation_ShipperConsignee_ConsigneeCity_NEW");
        private By unkConsigneeState_ID = By.Id("CMP_Capacity_Booking_MaintainReservation_ShipperConsignee_ConsigneeState");
        private By unkConsigneeCountry_ID = By.Id("CMP_Capacity_Booking_Permanent_ShipperConsignee_ConsigneeCountry");
        private By unkConsigneeZip_ID = By.Id("CMP_Capacity_Booking_MaintainReservation_ShipperConsignee_ConsigneePostalCode");
        private By unkConsigneeEmail_ID = By.Id("CMP_Capacity_Booking_MaintainReservation_ShipperConsignee_ConsigneeEmail");
        public void SwitchToCAP018Frame()
        {
            test = _scenario.CreateNode<Scenario>("Switch to CAP018 Frame");
            try
            {                
                SwitchToFrame(CAP018Frame_XPATH);                
                test.Pass("Switched to CAP018 Frame");                
                
            }
            catch (Exception e)
            {
                test.Fail("Error in Switching to CAP018 Frame: " + e.Message);
            }
        }

        public void ClickNewListButton()
        {
            test = _scenario.CreateNode<Scenario>("Click New List Button");
            try
            {
                WaitForElementToBeInvisible(homePage_CSS, TimeSpan.FromSeconds(5));
                ClickOnElementIfPresent(newList_XPATH);                
                test.Pass("Clicked New List Button");                
            }
            catch (Exception e)
            {
                test.Fail("Error in Clicking New List Button: " + e.Message);
            }
        }

        public void EnterShipmentDetails(string origin, string destination, string ProductCode)
        {
            test = _scenario.CreateNode<Scenario>("Enter Shipment Details");
            try
            {                                
                if (IsElementEnabled(origin_ID))
                {
                    EnterTextWithCheck(origin_ID, origin);
                    test.Pass("Entered Origin: " + origin);
                }
                EnterText(destination_XPATH, destination);
                test.Pass("Entered Destination: " + destination);
                ClickOnElementIfPresent(agentCode_ID);
                int agentcode = 10763;
                EnterTextWithCheck(agentCode_ID, agentcode.ToString());
                test.Pass("Entered Agent Code: " + agentcode);
                EnterText(shippingDate_ID, shippingDate);
                test.Pass("Entered Shipping Date: " + shippingDate);
                EnterText(product_XPATH, ProductCode);
                test.Pass("Entered Product Code: " + ProductCode);
                Click(shipperConsigneeBtn_ID);
                test.Pass("Clicked on Shipper Consignee Button");
            }
            catch (Exception e)
            {
                test.Fail("Error in Entering Shipment Details: " + e.Message);
            }
        }        

        public void EnterCommodityDetails(string commodityCode, string pieces, string weight)
        {
            test = _scenario.CreateNode<Scenario>("Enter Commodity Details");
            try
            {                
                SwitchToCAP018Frame();
                WaitForElementToBeVisible(commodityCode_XPATH, TimeSpan.FromSeconds(10));              
                ClickOnElementIfPresent(commodityCode_XPATH);
                Click(commodityCode_XPATH);
                EnterText(commodityCode_XPATH, commodityCode);
                test.Pass("Entered Commodity Code: " + commodityCode);
                EnterText(pieces_XPATH, pieces);
                test.Pass("Entered Pieces: " + pieces);
                EnterText(weight_XPATH, weight);
                test.Pass("Entered Weight: " + weight);
            }
            catch (Exception e)
            {
                test.Fail("Error in Entering Commodity Details: " + e.Message);
            }
        }        

        public string ClickingYesOnPopupWarnings()
        {
            string errorText = "";
            SwitchToDefaultContent();

            if (IsElementDisplayed(popupAlertWarningBooking_CSS))
            {
                errorText = GetText(popupAlertMessageBooking_XPATH);
                WaitForElementToBeVisible(btnYesAlertMessageBooking_XPATH, TimeSpan.FromSeconds(10));
                Click(btnYesAlertMessageBooking_XPATH);
            }            
            return errorText;
        }

        public void ClickSaveButton()
        {
            test = _scenario.CreateNode<Scenario>("Click Save Button");
            try
            {                
                int noOfWindowsBefore = GetNumberOfWindowsOpened();                
                WaitForElementToBeInvisible(btnYesAlertMessageBooking_XPATH, TimeSpan.FromSeconds(15));
                ClickOnElementIfPresent(saveBtn_ID);
                test.Pass("Clicked Save Button");
                ClickingYesOnPopupWarnings();
                WaitForNewWindowToOpen(TimeSpan.FromSeconds(20), noOfWindowsBefore + 1);
                int noOfWindowsAfter = GetNumberOfWindowsOpened();
                if (noOfWindowsAfter > noOfWindowsBefore)
                {
                    SwitchToLastWindow();
                    WaitForElementToBeInvisible(btnYesAlertMessageBooking_XPATH, TimeSpan.FromSeconds(15));
                    WaitForElementToBeVisible(awbNumber_XPATH, TimeSpan.FromSeconds(15));
                    string awbNumber = GetText(awbNumber_XPATH);
                    test.Pass("AWB Number: "+awbNumber);
                    Console.WriteLine(awbNumber);
                    if (IsElementEnabled(btnOkBookingSummaryPopup_XPATH))
                    {
                        WaitForElementToBeClickable(btnOkBookingSummaryPopup_XPATH, TimeSpan.FromSeconds(10));
                        Click(btnOkBookingSummaryPopup_XPATH);
                        test.Pass("Clicked OK Button on Booking Summary Popup");
                    }                    
                    SwitchToLastWindow();
                    SwitchToCAP018Frame();
                }
                ClickOnElementIfPresent(btnCloseMb_XPATH);
                test.Pass("Clicked Close Button on Maintain Booking Page");
            }
            catch (Exception e)
            {
                test.Fail("Error in Clicking Save Button: " + e.Message);
            }
        }                                        

       public void NewUnknownAgentShipmentDetails(string org, string dest,string agtcode, string prodcode)
        {
            test = _scenario.CreateNode<Scenario>("Unknown Agent Shipment Details");
            try
            {
                if (IsElementEnabled(origin_ID))
                {
                    EnterTextWithCheck(origin_ID, org);
                    test.Pass("Entered Origin: " + org);
                }                
                test.Pass("Entered Origin: " + org);
                EnterText(destination_XPATH, dest);
                test.Pass("Entered Destination: " + dest);
                ClickOnElementIfPresent(agentCode_ID);
                EnterTextWithCheck(agentCode_ID, agtcode.ToString());
                test.Pass("Entered Agent Code: " + agtcode);
                EnterText(shippingDate_ID, shippingDate);
                test.Pass("Entered Shipping Date: " + shippingDate);
                EnterText(product_XPATH, prodcode);
                test.Pass("Entered Product Code: " + prodcode);
                Click(shipperConsigneeBtn_ID);
                test.Pass("Clicked on Shipper Consignee Button");
            }
            catch (Exception e)
            {
                test.Fail("Error in Entering Unknown Agent Shipment Details: " + e.Message);
            }
        }      

        public void UnknownShipperConsigneeALLDetails(string unkshppr, string unkconsgn)
        {
            test = _scenario.CreateNode<Scenario>("Unknown Shipper Consignee ALL Details");
            try
            {                
                SwitchToSecondPopupWindow();
                WaitForElementToBeInvisible(CAP018Frame_XPATH, TimeSpan.FromSeconds(10));
                EnterText(shipperCode_XPATH, unkshppr);
                test.Pass("Entered Shipper Code: " + unkshppr);
                string shipperName = "Test Shipper";               
                if (checkTextboxIsNotEmpty(unkShipperName_ID))
                {
                    ClickOnElementIfPresent(unkShipperName_ID);                                                            
                }    
                else
                {
                    WaitForElementToBeInvisible(CAP018Frame_XPATH, TimeSpan.FromSeconds(10));
                    EnterTextWithCheck(unkShipperName_ID, shipperName);
                    test.Pass("Entered Shipper Name: " + shipperName);
                }
                string shipperFirstAddress = "Test Address1";
                EnterTextWithCheck(unkShipperFirstAddress_ID, shipperFirstAddress);
                test.Pass("Entered Shipper First Address: " + shipperFirstAddress);
                string shipperSecondAddress = "Test Address2";
                EnterTextWithCheck(unkShipperSecondAddress, shipperSecondAddress);
                test.Pass("Entered Shipper Second Address: " + shipperSecondAddress);
                string shipperCity = "Test City";
                EnterTextWithCheck(unkShipperCity_ID, shipperCity);
                test.Pass("Entered Shipper City: " + shipperCity);
                string shipperState = "Test State";
                EnterTextWithCheck(unkShipperState_ID, shipperState);
                test.Pass("Entered Shipper State: " + shipperState);
                string shipperCountry = "US";
                EnterTextWithCheck(unkShipperCountry_ID, shipperCountry);
                test.Pass("Entered Shipper Country: " + shipperCountry);
                string shipperZip = "67890";
                EnterTextWithCheck(unkShipperZip_ID, shipperZip);
                test.Pass("Entered Shipper Zip: " + shipperZip);
                string shipperEmail = "TEST@GMAIL.COM.INVALID";
                EnterTextWithCheck(unkShipperEmail_ID, shipperEmail);
                test.Pass("Entered Shipper Email: " + shipperEmail);    
                EnterText(consigneeCode_XPATH, unkconsgn);
                test.Pass("Entered Consignee Code: " + unkconsgn);
                string consigneeName = "Test Consignee";                
                if (checkTextboxIsNotEmpty(unkConsigneeName_ID))
                {
                    ClickOnElementIfPresent(unkConsigneeName_ID);                                                            
                }
                else
                {
                    WaitForElementToBeInvisible(CAP018Frame_XPATH, TimeSpan.FromSeconds(10));
                    EnterTextWithCheck(unkConsigneeName_ID, consigneeName);
                    test.Pass("Entered Consignee Name: " + consigneeName);
                }
                test.Pass("Entered Consignee Name: " + consigneeName);
                string consigneeFirstAddress = "Test Address1";
                EnterTextWithCheck(unkConsigneeFirstAddress_ID, consigneeFirstAddress);
                test.Pass("Entered Consignee First Address: " + consigneeFirstAddress);
                string consigneeSecondAddress = "Test Address2";
                EnterTextWithCheck(unkConsigneeSecondAddress, consigneeSecondAddress);
                test.Pass("Entered Consignee Second Address: " + consigneeSecondAddress);
                string consigneeCity = "Test City";
                EnterTextWithCheck(unkConsigneeCity_ID, consigneeCity);
                test.Pass("Entered Consignee City: " + consigneeCity);
                string consigneeState = "Test State";
                EnterTextWithCheck(unkConsigneeState_ID, consigneeState);
                test.Pass("Entered Consignee State: " + consigneeState);
                string consigneeCountry = "US";
                EnterTextWithCheck(unkConsigneeCountry_ID, consigneeCountry);
                test.Pass("Entered Consignee Country: " + consigneeCountry);
                string consigneeZip = "67890";
                EnterTextWithCheck(unkConsigneeZip_ID, consigneeZip);
                test.Pass("Entered Consignee Zip: " + consigneeZip);
                string consigneeEmail = "TEST@GMAIL.COM.INVALID";
                EnterTextWithCheck(unkConsigneeEmail_ID, consigneeEmail);
                test.Pass("Entered Consignee Email: " + consigneeEmail);
                Click(shipperConsigneeOkBtn_ID);
                test.Pass("Clicked on Shipper Consignee OK Button");
                SwitchToPopupWindow();
            }
            catch (Exception e)
            {
               test.Fail("Error in Entering Unknown Shipper Consignee ALL Details: " + e.Message);
            }
        }

        public void SelectFlight(string givenproductcode)
        {
            test = _scenario.CreateNode<Scenario>("Select Flight");
            try
            {                
                Click(selectFlightBtn_ID);
                test.Pass("Clicked Select Flight Button");
                WaitForElementToBeInvisible(CAP018Frame_XPATH, TimeSpan.FromSeconds(10));
                SwitchToFrame(bookingIrregularityFrame_ID);
                List<IWebElement> noofflights = GetElements(flightDetailsSection_XPATH);
                List<IWebElement> ratestatusbtn = GetElements(rate_Xpath);
                List<IWebElement> capstatusbtn = GetElements(cap_Xpath);
                List<IWebElement> reststatusbtn = GetElements(rest_Xpath);
                List<IWebElement> embstatusbtn = GetElements(emb_Xpath);
                List<IWebElement> prodcodes = GetElements(flightProductCode_Xpath);
                List<IWebElement> flightdates = GetElements(flightDate_Xpath);
                List<IWebElement> GeneralProd = GetElements(generalProdBtn_Xpath);
                List<IWebElement> PriorityProd = GetElements(priorityProdBtn_Xpath);
                List<IWebElement> EmployeeProd = GetElements(employeeProdBtn_Xpath);
                List<IWebElement> GoldstreakProd = GetElements(goldstreakProdBtn_Xpath);
                List<IWebElement> PetConnectProd = GetElements(petConnectProdBtn_Xpath);
                for (int i = 0; i < noofflights.Count; i++)
                {
                    IWebElement item = prodcodes[i];
                    string productcode = GetTextFromElement(item);
                    IWebElement capstatus = capstatusbtn[i];
                    string capColor = GetAttributeValueFromElement(capstatus, "class");
                    IWebElement reststatus = reststatusbtn[i];
                    string rescolor = GetAttributeValueFromElement(reststatus, "class");
                    IWebElement embstatus = embstatusbtn[i];
                    string embcolor = GetAttributeValueFromElement(embstatus, "class");
                    IWebElement ratestatus = ratestatusbtn[i];
                    string ratecolor = GetAttributeValueFromElement(ratestatus, "class");
                    IWebElement flightdate = flightdates[i];
                    string flightdatevalue = GetTextFromElement(flightdate);
                    if (presentdate == flightdatevalue)
                    {
                        if (productcode == givenproductcode && productcode == "GENERAL")
                        {
                            if (capColor != "badge-red" && rescolor != "badge-red" && embcolor != "badge-red" && ratecolor != "badge-red")
                            {
                                IWebElement GeneralProdBtn = GeneralProd[i];
                                if (IsElementDisplayed(generalProdBtn_Xpath))
                                {
                                    ClickOnElement(GeneralProdBtn);
                                    test.Pass("Selected General Product");
                                }
                                Click(flightDetailsOkbtn_Xpath);
                                test.Pass("Clicked Flight Details OK Button");
                                break;
                            }
                        }
                        else if (productcode == givenproductcode && productcode == "PRIORITY")
                        {
                            if (capColor != "badge-red" && rescolor != "badge-red" && embcolor != "badge-red" && ratecolor != "badge-red")
                            {
                                IWebElement PriorityProdBtn = PriorityProd[i];
                                if (IsElementDisplayed(priorityProdBtn_Xpath))
                                {
                                    ClickOnElement(PriorityProdBtn);
                                    test.Pass("Selected Priority Product");
                                }
                                Click(flightDetailsOkbtn_Xpath);
                                test.Pass("Clicked Flight Details OK Button");
                                break;
                            }
                        }
                        else if (productcode == givenproductcode && productcode == "EMPLOYEE SHIPMENT")
                        {
                            if (capColor != "badge-red" && rescolor != "badge-red" && embcolor != "badge-red" && ratecolor != "badge-red")
                            {
                                IWebElement EmployeeProdBtn = EmployeeProd[i];
                                if (IsElementDisplayed(employeeProdBtn_Xpath))
                                {
                                    ClickOnElement(EmployeeProdBtn);
                                    test.Pass("Selected Employee Shipment Product");
                                }
                                Click(flightDetailsOkbtn_Xpath);
                                test.Pass("Clicked Flight Details OK Button");
                                break;
                            }
                        }
                        else if (productcode == givenproductcode && productcode == "GOLDSTREAK")
                        {
                            if (capColor != "badge-red" && rescolor != "badge-red" && embcolor != "badge-red" && ratecolor != "badge-red")
                            {
                                IWebElement GoldstreakProdBtn = GoldstreakProd[i];
                                if (IsElementDisplayed(goldstreakProdBtn_Xpath))
                                {
                                    ClickOnElement(GoldstreakProdBtn);
                                    test.Pass("Selected Goldstreak Product");
                                }
                                Click(flightDetailsOkbtn_Xpath);
                                test.Pass("Clicked Flight Details OK Button");
                                break;
                            }
                        }
                        else if (productcode == givenproductcode && productcode == "PET CONNECT")
                        {
                            if (capColor != "badge-red" && rescolor != "badge-red" && embcolor != "badge-red" && ratecolor != "badge-red")
                            {
                                IWebElement PetConnectProdBtn = PetConnectProd[i];
                                if (IsElementDisplayed(petConnectProdBtn_Xpath))
                                {
                                    ClickOnElement(PetConnectProdBtn);
                                    test.Pass("Selected Pet Connect Product");
                                }
                                Click(flightDetailsOkbtn_Xpath);
                                test.Pass("Clicked Flight Details OK Button");
                                break;
                            }
                        }
                    }
                }
                SwitchToCAP018Frame();
            }
            catch (Exception e)
            {
                test.Fail(e.ToString());
            }
        }       
    }
}