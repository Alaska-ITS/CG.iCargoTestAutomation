using AventStack.ExtentReports;
using log4net;
using OpenQA.Selenium;
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

namespace iCargoXunit.pages
{
    public class CreateShipmentPage : BasePage
    {

        private IWebDriver driver;
        private PageObjectManager pageObjectManager;
        private DangerousGoodsPage dgp;
        private PaymentPortalPage ppp;
        private CaptureIrregularityPage cip;
        private ExportManifestPage emp;
        private FogsQAPage fogsQAPage;
        public static string awb_num = "";
        public static string totalPaybleAmount = "";
        public static string accountInfoCIDNum = "";
        public static string totalAmountCharged = "";
        string IATACharge = "";
        string MarketCharge = "";
        public static string origin = "";
        public static string destination = "";
        //public static string shippingDate = DateTime.Now.ToString("dd-MMM-yyyy");
        public static string shippingDatePST = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")).ToString("dd-MMM-yyyy");
        public static string shippingDatePSTDDMMM = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")).ToString("dd-MMM");
        string scc = "";
        string serviceCargoClass = "";
        public static string pieces = "";
        public static string weight = "";
        string chargeType = "";
        string modeOfPayment = "";
        string iataCharge = "";
        string marketCharge = "";
        string totalChagre = "";
        public static string flightNum = "";
        public static string ConnectingflightNum = "";
        string flightSegment = "";
        string generatedAWB = "";
        public static string cartULDNum = "";
        public static int noOfWindowBefore = 0;
        public static int noOfWindowAfter = 0;
        ILog Log = LogManager.GetLogger(typeof(CreateShipmentPage));


        public CreateShipmentPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            this.pageObjectManager = new PageObjectManager(driver);
            this.ppp = pageObjectManager.GetPaymentPortalPage();
            this.dgp = pageObjectManager.GetDangerousGoodsPage();
            this.cip = pageObjectManager.GetCaptureIrregularityPage();
            this.emp = pageObjectManager.GetExportManifestPage();
            this.fogsQAPage = pageObjectManager.GetFogsQAPage();
        }

        // LTE001 Header Section   //
        private By contentFrame_Xpath = By.XPath("//div[@id='LTE001']/iframe");
        private By txtAwbNo_Id = By.Id("awb_b");
        private By btnList_Id = By.Id("button_list");
        private By btnClear_Id = By.Id("button_clear");
        private By lblShipmentDetails_Css = By.CssSelector(".card-header h4");
        private By lblAWBNo_Css = By.CssSelector(".awb-no-data a");
        private By lblBookingConfirmation_Xpath = By.XPath("//header//*[text()='Booking']/following-sibling::div");
        private By lblDataCapture_Xpath = By.XPath("//header//*[text()='Data Capture']//following::div[@id='_ajax_shipmentStatus']");
        private By lblAcceptance_Xpath = By.XPath("//header//*[text()='Acceptance']//following::div[@id='_ajax_acceptance_status']");
        private By lblColorReadyForCarriage_Xpath = By.XPath(".//*[@id='_ajax_readyforcarriage_status']//i");
        private By lblColorCaptureChecksheet_Xpath = By.XPath("//*[normalize-space(text())='Capture check sheet']/preceding-sibling::i");
        private By lblColorBlock_Xpath = By.XPath(".//*[normalize-space(text())='Block']/preceding-sibling::i");
        private By btnOrangePencilEditBooking_Css = By.CssSelector("header i.ico-pencil-rounded-orange");

        //   Participants   //
        public static string agentCode = "";
        public static string shipperCode = "";
        public static string consigneeCode = "";
        private By lblParticipantDetails_Id = By.Id("participant");
        private By btnOrangePencilParticipant_Css = By.CssSelector("#view_participant a");
        private By txtAgentCode_Name = By.Name("agentCode");
        private By txtAgentName_Name = By.Name("agentName");
        private By txtShipperCode_Name = By.Name("shipperCode");
        private By txtShipperName_Name = By.Name("shipperName");
        private By txtShipperContact_Name = By.Name("shipperCntctNumber");
        private By btnMoreShipper_Name = By.Name("btnMoreShipper");
        private By txtShipperAddress_Name = By.Name("shipperAddress");
        private By txtShipperCity_Name = By.Name("shipperCity");
        private By txtShipperState_Name = By.Name("shipperState");
        private By txtShipperZip_Name = By.Name("shipperZipCode");
        private By txtShipperCountry_Name = By.Name("shipperCountry");
        private By txtShipperEmail_Name = By.Name("shipperEmail");
        private By btnShipperOk_Name = By.Name("btnShipperOK");

        private By txtConsigneeCode_Name = By.Name("consigneeCode");
        private By txtConsigneeName_Id = By.Id("CMP_Operations_Shipment_Lite_CreateShipment_ConsigneeName");
        private By txtConsigneeContact_Id = By.Id("CMP_Operations_Shipment_Lite_CreateShipment_ConsigneeContactNumber");
        private By btnConsigneeMore_Id = By.Id("CMP_Operations_Shipment_Lite_CreateShipment_btnConsigneeMore");
        private By txtConsigneeAddress_Name = By.Name("consigneeAddress");
        private By txtConsigneeCity_Name = By.Name("consigneeCity");
        private By txtConsigneeState_Name = By.Name("consigneeState");
        private By txtConsigneeZip_Name = By.Name("consigneeZipCode");
        private By txtConsigneeCountry_Name = By.Name("consigneeCountry");
        private By txtConsigneeEmail_Name = By.Name("consigneeEmail");
        private By btnConsigneeOk_Name = By.Name("btnConsigneeOK");
        private By btnContinueParticipants_Id = By.Id("CMP_Operations_Shipment_Lite_CreateShipment_btnParticpantContinue");

        private By btnContinueCommodity_Name = By.Name("btnCommodityCont");

        //   Certificates   //

        private By txtNameOnId_Name = By.Name("driverName");
        private By drpdwnIdType_Name = By.Name("driverIDType");
        private By txtIdIssueState_Name = By.Name("stateOrCountryIssuingID");
        private By drpdwn_PhotoMatched_Name = By.Name("photoIDVerified");
        private By btnContinueCertificates_Name = By.Name("btnCertificateDetailsCont");

        //   Shipment Commodity Details   //
        public static string commodityCode = "";
        public static string productCode = "";
        private By btnOrangePencilShipment_Css = By.CssSelector("#view_shipmentDtls a");
        private By lblShipmentDetails_Id = By.Id("shipmentaccordion");
        private By txtOrigin_Name = By.Name("origin");
        private By txtDestination_Name = By.Name("destination");
        private By txtShipmentDate_Name = By.Name("shippingDate");
        private By txtProductCode_Name = By.Name("productCode");
        private By txtSCCCode_Name = By.Name("scc");
        private By drpdwnServiceCargoClass_Name = By.Name("serviceCargoClass");
        private By txtCommodityCode_Name = By.Name("commodityCode");
        private By txtShipmentDescription_Name = By.Name("shipmentDesc");
        private By txtPieces_Name = By.Name("shpPcs");
        private By txtWeight_Name = By.Name("shpWgt");
        private By btnContinueShipmentCommodity_Name = By.Name("btnShipmentCont");

        //    Flight Details    //
        private By lblFlightDetails_ID = By.Id("booking");
        private By btnOrangePencilFlight_Css = By.CssSelector("#view_bookingDtls a");
        private By txtCarrierCode_Name = By.Name("flightCarrierCode");
        private By txtFlightNo_Name = By.Name("flightNumber");
        private By txtFlightDate_Name = By.Name("flightDate");
        private By txtSegment_Name = By.Name("flightSegment");
        private By txtBookedPiece_Name = By.Name("bookedPieces");
        private By txtBookedWgt_Name = By.Name("bookedWeight");
        private By btnTrashIcon_Css = By.CssSelector("#flightdetails i[title='Delete']");
        /* Select Flight Functionality */
        private By btnSelectFlight_Name = By.Name("btnSearchFlight");
        private By lblFlightListHeader_Xpath = By.XPath("//*[@id='flight_details']//thead//th[text()='Flight List']");
        private By listAllFlights_Xpath = By.XPath("//*[@id='flight_details']//tbody//tr");
        private string lblFlightType = "";
        private string btnBookFlight = "//*[@id='flight_details']//tbody//tr[1]//input[@type='button']";
        private By btnContinueFlightDetails_Name = By.Name("btnFlightDtlsCont");
        private string lblAvailabilityCap = "";
        private string lblAvailabilityEMB = "";
        private string lblAvailabilityLOAD = "";
        private string lblAvailabilityRES = "";
        private string lblFlightDate = "";

        //    Charge Details    //
        private By lblChargeDetails_Id = By.Id("charge");
        private By btnOrangePencilCharge_Css = By.CssSelector("#view_chargeDetails a");
        private By btnPaymentType_Xpath = By.XPath("//div[@id='pptype']");
        private By chkBoxThirdParty_Id = By.Id("thirdPartyId");
        private By txtRatedCustomerNumber_Name = By.Name("ratedCustomer");
        private By btnCalculateCharges_Name = By.Name("btnCalculateCharges");
        private By txtIATACharge_Xpath = By.XPath("//input[@name='iataCharge']");
        private By txtMarketCharge_Xpath = By.XPath("//input[@name='marketCharge']");
        private By txtTotalCharge_Xpath = By.XPath("(//span[starts-with(normalize-space(text()), 'Total Charge(Inc. Tax):')])[2]");
        private By drpdwnModeOfPayment_Name = By.Name("modeOfPay");
        private By txtPaymentRemarks_Css = By.CssSelector("#remarks");
        private By btnContinueChargeDetails_Name = By.Name("btnChargeDtlsCont");
        private By popupActiveCashDraw_Xpath = By.XPath("//*[text()='No Active Cash draw exists, Do you want to open a new Cash draw?']");
        private By btnYesActiveCashDraw_Xpath = By.XPath("//*[@class='ui-dialog-buttonset']/button[normalize-space(text())='Yes']");
        private By btnNoActiveCashDraw_Xpath = By.XPath("//*[@class='ui-dialog-buttonset']/button[text()='No']");

        //   Acceptance Details    //
        private By btnOrangePencilAcceptance_Css = By.CssSelector("#view_acceptance a");
        private By txtPieceAccepted_Name = By.Name("acceptedPieces");
        private By btnContinueAcceptanceDetails_Name = By.Name("btnAcceptanceDtlsCont");

        //  Screening Details    //
        private By btnOrangePencilScreening_Css = By.CssSelector("#view_screeningDetails a");
        private By btnAddScreeningRow_Css = By.CssSelector(".templateParentScreening i[title='Add']");

        // string screeningRows = "(//*[@class='row templateParentScreening'])[1]";
        private By txtScreeningAirport1_Xpath = By.XPath("(//*[@class='row templateParentScreening'])[1]//input[@name='screeningAirport']");
        private By drpdwnScreeningMethod1_Xpath = By.XPath("(//*[@class='row templateParentScreening'])[1]//select[@name='screeningMethod']");
        private By txtScreeningPieces1_Xpath = By.XPath("(//*[@class='row templateParentScreening'])[1]//input[@name='screeningPieces']");
        private By drpdwnScreeningResult1_Xpath = By.XPath("(//*[@class='row templateParentScreening'])[1]//select[@name='screeningResult']");
        private By txtScreeningAirport2_Xpath = By.XPath("(//*[@class='row templateParentScreening'])[2]//input[@name='screeningAirport']");
        private By drpdwnScreeningMethod2_Xpath = By.XPath("(//*[@class='row templateParentScreening'])[2]//select[@name='screeningMethod']");
        private By txtScreeningPieces2_Xpath = By.XPath("(//*[@class='row templateParentScreening'])[2]//input[@name='screeningPieces']");
        private By drpdwnScreeningResult2_Xpath = By.XPath("(//*[@class='row templateParentScreening'])[2]//select[@name='screeningResult']");
        private By btnContinueScreeningDetails_Name = By.Name("btnScreeningDtlsCont");

        // LTE001 Footer Section  //
        private By chkBoxAwbVerified_Name = By.Name("verfiedCharges");
        private By btnSaveShipment_Name = By.Name("btnSave");
        private By btnDeleteAwb_Name = By.Name("btnDeleteAWB");
        private By btnCloseShipment_Name = By.Name("btnClose");
        private By btnCloseLTEWindow_Css = By.CssSelector("a.remove");
        private By btnError_CSS = By.CssSelector("#error-tab");

        //   Warning Popups //
        private By frameCalculateCharges_Css = By.CssSelector("#jsonDataHolder1711185182553");
        private By popupWarning_Css = By.CssSelector(".alert-messages-ui");
        private By popupAlertMessage_Xpath = By.XPath("//*[@class='alert-messages-list']//span");
        private By btnYesAlertMessage_Xpath = By.XPath("//*[@class='ui-dialog-buttonpane ui-widget-content ui-helper-clearfix']//*[text()=' Yes ']");
        private By lblWarningMessage_Css = By.CssSelector("#error-body span");
        private By lblEmbargoDetails_Xpath = By.XPath("//*[text()='Embargo Details']");
        private By btnContinueEmbargo_Xpath = By.XPath("//*[text()='Embargo Details']//following::button[@id='cancalBtn']");
        private By btnCloseEmbargo_Xpath = By.XPath("//*[text()='Embargo Details']//following::button[@id='cancalBtn']");
        private By lblEmbargoErrorDescription_Xpath = By.XPath("//*[text()='Embargo Details']//following::div[@class='dataTables_scrollBody']/table/tbody//td[3]");
        private By lblCaptureIrregularity_Xpath = By.XPath("//span[text()='Capture Irregularity']");

        // Capture Check Sheet //
        private By lnkCaptureChecksheet_Xpath = By.XPath("//*[@id='_ajax_shipmentChecksheet']//parent::a");
        private By lblCpatureChecksheetWarning = By.XPath("//*[@id='_ajax_shipmentChecksheet']/i");
        private By popupContainerFrameChksheet = By.XPath("//*[text()='Capture Check Sheet']//parent::div//following-sibling::div/iframe");
        private By lblTotalChkSheetSections_Xpath = By.XPath("//*[@id='tabs-1']//div[@id='configId']/h2");
        /* For Employee Shipment checksheet */
        private By txtDateOfHire_Xpath = By.XPath("//*[text()='EMPLOYEE SHIPMENT VERIFICATION']//following::input[@id='calendar2']");
        private By txtPeoplesoftNumber_Xpath = By.XPath("//*[text()='EMPLOYEE SHIPMENT VERIFICATION']//following::textarea[@id='CMP_Checksheet_Defaults_CaptureCheckSheet_Remarks']");
        private By txtSecuritySSERemarks_Id = By.Id("CMP_Checksheet_Defaults_CaptureCheckSheet_Remarks");
        private By btnOKCaptureChkSheet_Xpath = By.XPath("//*[@class='btmbtnpane btm-fixed']/button[@id='btnSave']");
        private By btnOKSuccessCheckSheet_Xpath = By.XPath("//*[@class='alert-messages-list']//parent::div//following-sibling::div//button");



        public void SwitchToLTEContentFrame()
        {
            try
            {
                SwitchToFrame(contentFrame_Xpath);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Switched to LTE Frame");
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Info, "Error in switching to LTE001 content frame: " + e.ToString());
                Log.Error("Error in switching to LTE001 content frame: " + e.ToString());
            }

        }

        public void ClickOnAwbTextBox()
        {
            try
            {
                EnterKeys(txtAwbNo_Id, Keys.Enter);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on AWB text box");
                Log.Info("Clicked on AWB text box");
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in clicking on AWB text box: " + e.ToString());
                Log.Error("Error in clicking on AWB text box: " + e.ToString());
            }

        }

        public void alreadyExecutedAWB()
        {
            awb_num = awb_num.Split('-')[1];
            SwitchToLTEContentFrame();
            EnterAWBTextBox(awb_num);
            //Hooks.Hooks.UpdateTest(Status.Pass, "Entered AWB number: " + awb_num);
            ClickOnListButton();

        }

        public void EnterAWBTextBox(string awb)
        {
            try
            {
                EnterText(txtAwbNo_Id, awb);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered AWB number: " + awb);
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in entering AWB number: " + e.ToString());
                Log.Error("Error in entering AWB number: " + e.ToString());
            }

        }


        public void ClickOnListButton()
        {
            try
            {

                Click(btnList_Id);
                WaitForElementToBeInvisible(btnList_Id, TimeSpan.FromSeconds(5));
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on List button");
                Log.Info("Clicked on List button");

            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in clicking on List button: " + e.ToString());
                Log.Error("Error in clicking on List button: " + e.ToString());
            }

        }

        public void OpenAndVerifyParticipants()
        {
            try
            {
                Click(lblParticipantDetails_Id);
                Click(btnOrangePencilParticipant_Css);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Orange Pencil for Participants");
                WaitForElementToBeInvisible(btnOrangePencilParticipant_Css, TimeSpan.FromSeconds(1));
                agentCode = GetAttributeValue(txtAgentCode_Name, "value");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Agent Code: " + agentCode);
                shipperCode = GetAttributeValue(txtShipperCode_Name, "value");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Shipper Code: " + shipperCode);
                consigneeCode = GetAttributeValue(txtConsigneeCode_Name, "value");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Consignee Code: " + consigneeCode);

                //Assert.IsNotNull(agentCode, "Agent code is null");
                //Assert.IsNotNull(shipperCode, "Shipper code is null");
                //Assert.IsNotNull(consigneeCode, "Consignee code is null");
                Assert.True(agentCode != "", "Agent code is null");
                Assert.True(shipperCode != "", "Shipper code is null");
                Assert.True(consigneeCode != "", "Consignee code is null");



            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in opening and verifying participants: " + e.ToString());
                Log.Error("Error in opening and verifying participants: " + e.ToString());
            }

        }

        public void EnterParticipantDetailsAsync(string agent, string shipper, string consignee)
        {
            agentCode = agent;
            shipperCode = shipper;
            consigneeCode = consignee;
            try
            {
                if (!IsElementDisplayed(txtAgentCode_Name))
                {
                    Click(lblParticipantDetails_Id);
                }
                Click(txtAgentCode_Name);
                EnterTextWithCheck(txtAgentCode_Name, agentCode);
                if (!checkTextboxIsNotEmpty(txtAgentName_Name))
                {
                    EnterTextWithCheck(txtAgentCode_Name, agentCode);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Agent Code: " + agentCode);
                    Log.Info("Entered Agent Code: " + agentCode);
                }
                Click(txtAgentName_Name);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Agent Code: " + agentCode);

                Click(txtShipperCode_Name);
                EnterTextWithCheck(txtShipperCode_Name, shipperCode);
                if (!checkTextboxIsNotEmpty(txtShipperName_Name))
                {
                    EnterTextWithCheck(txtShipperCode_Name, shipperCode);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Shipper Code: " + shipperCode);
                    Log.Info("Entered Shipper Code: " + shipperCode);
                }
                Click(txtShipperName_Name);
                Click(txtShipperContact_Name);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Shipper Code: " + shipperCode);

                Click(txtConsigneeCode_Name);
                EnterTextWithCheck(txtConsigneeCode_Name, consigneeCode);
                if (!checkTextboxIsNotEmpty(txtConsigneeCode_Name))
                {
                    EnterTextWithCheck(txtConsigneeCode_Name, consigneeCode);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Consignee Code: " + consigneeCode);
                    Log.Info("Entered Consignee Code: " + consigneeCode);
                }
                Click(txtConsigneeName_Id);
                Click(txtConsigneeContact_Id);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Consignee Code: " + consigneeCode);

            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in entering participant details: " + e.ToString());
                Log.Error("Error in entering participant details: " + e.ToString());
            }

        }

        public void EnterParticipantDetailsWithUnknownConsignee(string agent, string shipper, string consignee)
        {
            agentCode = agent;
            shipperCode = shipper;
            consigneeCode = consignee;
            try
            {
                if (!IsElementDisplayed(txtAgentCode_Name))
                {
                    Click(lblParticipantDetails_Id);
                }
                Click(txtAgentCode_Name);
                EnterTextWithCheck(txtAgentCode_Name, agentCode);
                if (!checkTextboxIsNotEmpty(txtAgentName_Name))
                {
                    EnterTextWithCheck(txtAgentCode_Name, agentCode);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Agent Code: " + agentCode);
                    Log.Info("Entered Agent Code: " + agentCode);
                }
                Click(txtAgentName_Name);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Agent Code: " + agentCode);

                Click(txtShipperCode_Name);
                EnterTextWithCheck(txtShipperCode_Name, shipperCode);
                if (!checkTextboxIsNotEmpty(txtShipperName_Name))
                {
                    EnterTextWithCheck(txtShipperCode_Name, shipperCode);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Shipper Code: " + shipperCode);
                    Log.Info("Entered Shipper Code: " + shipperCode);
                }
                Click(txtShipperName_Name);
                Click(txtShipperContact_Name);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Shipper Code: " + shipperCode);

                Click(txtConsigneeCode_Name);
                EnterTextWithCheck(txtConsigneeCode_Name, consigneeCode);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Consignee Code: " + consigneeCode);
                Log.Info("Entered Consignee Code: " + consigneeCode);
                EnterTextWithCheck(txtConsigneeName_Id, "Test Unknown Consignee");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Consignee Name: Test Unknown Consignee");
                EnterTextWithCheck(txtConsigneeContact_Id, "1234567890");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Consignee Contact: 1234567890");
                Click(btnConsigneeMore_Id);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on More Consignee button");
                WaitForElementToBeVisible(txtConsigneeAddress_Name, TimeSpan.FromSeconds(5));
                EnterTextWithCheck(txtConsigneeAddress_Name, "Test Address");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Consignee Address: Test Address");
                EnterTextWithCheck(txtConsigneeCity_Name, "ANCHORAGE");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Consignee City: ANCHORAGE");
                EnterTextWithCheck(txtConsigneeState_Name, "Alaska");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Consignee State: Alaska");
                EnterTextWithCheck(txtConsigneeCountry_Name, "US");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Consignee Country: US");
                EnterTextWithCheck(txtConsigneeZip_Name, "99505");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Consignee Zip: 99505");
                EnterTextWithCheck(txtConsigneeEmail_Name, "TEST@GMAIL.COM.INVALID");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Consignee Email:");
                Click(btnConsigneeOk_Name);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Consignee OK button");
                WaitForElementToBeInvisible(btnConsigneeOk_Name, TimeSpan.FromSeconds(5));

            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in entering participant details: " + e.ToString());
                Log.Error("Error in entering participant details: " + e.ToString());
            }

        }

        public void EnterParticipantDetailsWithUnknownShipper(string agent, string shipper, string consignee)
        {
            agentCode = agent;
            shipperCode = shipper;
            consigneeCode = consignee;
            try
            {
                Click(txtAgentCode_Name);
                EnterTextWithCheck(txtAgentCode_Name, agentCode);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Agent Code: " + agentCode);
                Click(txtShipperCode_Name);
                EnterTextWithCheck(txtShipperCode_Name, shipperCode);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Shipper Code: " + shipperCode);
                EnterTextWithCheck(txtShipperName_Name, "Test Unknown Shipper");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Shipper Name: Test Unknown Shipper");
                EnterTextWithCheck(txtShipperContact_Name, "1234567890");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Shipper Contact: 1234567890");
                Click(btnMoreShipper_Name);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on More Shipper button");
                WaitForElementToBeVisible(txtShipperAddress_Name, TimeSpan.FromSeconds(5));
                EnterTextWithCheck(txtShipperAddress_Name, "Test Address");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Shipper Address: Test Address");
                EnterTextWithCheck(txtShipperCity_Name, "ANCHORAGE");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Shipper City: ANCHORAGE");
                EnterTextWithCheck(txtShipperState_Name, "Alaska");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Shipper State: Alaska");
                EnterTextWithCheck(txtShipperCountry_Name, "US");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Shipper Country: US");
                EnterTextWithCheck(txtShipperZip_Name, "99505");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Shipper Zip: 99505");
                EnterTextWithCheck(txtShipperEmail_Name, "test@mail.com");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Shipper Email:test@mail.com");
                ScrollDown();
                Click(btnShipperOk_Name);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Shipper OK button");
                WaitForElementToBeInvisible(btnShipperOk_Name, TimeSpan.FromSeconds(5));

                Click(txtConsigneeCode_Name);
                EnterTextWithCheck(txtConsigneeCode_Name, consigneeCode);
                EnterKeys(txtConsigneeCode_Name, Keys.Tab);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Consignee Code: " + consigneeCode);
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in entering participant details: " + e.ToString());
                Log.Error("Error in entering participant details: " + e.ToString());
            }



        }


        public void reOpenAWB()
        {
            try
            {
                Click(lblShipmentDetails_Id);
                Click(btnOrangePencilShipment_Css);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Orange Pencil for Shipment Details");
                ClickingYesOnPopupWarnings("");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Re-Opened AWB");
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in re-opening AWB: " + e.ToString());
                Log.Error("Error in re-opening AWB: " + e.ToString());
            }

        }


        public void ClickOnContinueParticipantButton()
        {
            try
            {
                EnterKeys(btnContinueParticipants_Id, Keys.Enter);
                WaitForElementToBeInvisible(btnContinueParticipants_Id, TimeSpan.FromSeconds(5));
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Continue button for participants");
                Log.Info("Clicked on Continue button for participants");
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Info, "Error in clicking on Continue button for participants: " + e.ToString());
                Log.Error("Error in clicking on Continue button for participants: " + e.ToString());

            }

        }

        public void EnterCertificateDetails()
        {
            try
            {
                EnterText(txtNameOnId_Name, "Test");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Name on Id: Test");
                SelectDropdownByVisibleText(drpdwnIdType_Name, "Driving License");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Id Type: Driving License");
                EnterText(txtIdIssueState_Name, "WA");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Id Issue State: WA");
                SelectDropdownByVisibleText(drpdwn_PhotoMatched_Name, "Yes");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Photo Matched: Yes");
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in entering certificate details: " + e.ToString());
                Log.Error("Error in entering certificate details: " + e.ToString());
            }

        }

        public void ClickOnContinueCertificateButton()
        {
            try
            {
                ScrollDown();
                Click(btnContinueCertificates_Name);
                WaitForElementToBeInvisible(btnContinueCertificates_Name, TimeSpan.FromSeconds(5));
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Continue button for certificates");
                Log.Info("Clicked on Continue button for certificates");
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in clicking on Continue button for certificates: " + e.ToString());
                Log.Error("Error in clicking on Continue button for certificates: " + e.ToString());
            }


        }

        public string OpenAndVerifyShipments()
        {
            try
            {
                WaitForElementToBeClickable(btnOrangePencilShipment_Css, TimeSpan.FromSeconds(10));
                Click(btnOrangePencilShipment_Css);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Orange Pencil for Shipment Details");
                WaitForElementToBeInvisible(btnOrangePencilShipment_Css, TimeSpan.FromSeconds(5));
                ScrollDown();

                origin = GetAttributeValue(txtOrigin_Name, "value");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Origin: " + origin);
                destination = GetAttributeValue(txtDestination_Name, "value");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Destination: " + destination);
                shippingDatePST = GetAttributeValue(txtShipmentDate_Name, "value");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Shipping Date: " + shippingDatePST);
                pieces = GetAttributeValue(txtPieces_Name, "value");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Pieces: " + pieces);
                weight = GetAttributeValue(txtWeight_Name, "value");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Weight: " + weight);
            }
            catch (Exception e)
            {
                Log.Error("Error in opening and verifying shipment details: " + e.ToString());
            }

            return pieces;
        }

        public void EnterShipmentDetails(string neworigin, string newdestination, string product, string scc,
                                         string commodity, string shipmentdesc, string serviceCargoClass, string newpiece, string newweight)
        {
            origin = neworigin;
            destination = newdestination;
            this.scc = scc;
            this.serviceCargoClass = serviceCargoClass;
            pieces = newpiece;
            weight = newweight;
            productCode = product;
            commodityCode = commodity;

            try
            {
                EnterText(txtOrigin_Name, origin);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Origin: " + origin);
                EnterText(txtDestination_Name, destination);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Destination: " + destination);
                EnterText(txtShipmentDate_Name, shippingDatePST);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Shipping Date: " + shippingDatePST);

                if (productCode == "Employee Shipment")
                {
                    EnterText(txtProductCode_Name, productCode);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Product Code: " + productCode);
                    Thread.Sleep(1000);

                    EnterKeys(txtProductCode_Name, Keys.ArrowDown);
                    EnterKeys(txtProductCode_Name, Keys.Tab);
                }
                else
                {
                    EnterText(txtProductCode_Name, productCode);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Product Code: " + productCode);
                }

                if (scc != "None")
                {
                    EnterText(txtSCCCode_Name, scc);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered SCC Code: " + scc);
                }
                EnterText(txtCommodityCode_Name, commodity);
                EnterKeys(txtCommodityCode_Name, Keys.Tab);
                if (shipmentdesc != "None")
                {
                    EnterText(txtShipmentDescription_Name, shipmentdesc);
                    EnterKeys(txtShipmentDescription_Name, Keys.Tab);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Shipment Description: " + shipmentdesc);
                }
                SelectDropdownByVisibleText(drpdwnServiceCargoClass_Name, serviceCargoClass);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Service Cargo Class: " + serviceCargoClass);
                EnterText(txtPieces_Name, pieces);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Pieces: " + pieces);
                EnterText(txtWeight_Name, weight);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Weight: " + weight);
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in entering shipment details: " + e.ToString());
                Log.Error("Error in entering shipment details: " + e.ToString());
            }
        }


        public void VerifyAndUpdateShipmentDetails(string fieldToUpdate, string newvalue)
        {
            try
            {
                if (fieldToUpdate.Equals("piece&weight"))
                {
                    pieces = GetAttributeValue(txtPieces_Name, "value");
                    pieces = (int.Parse(pieces) + int.Parse(newvalue)).ToString();
                    weight = GetAttributeValue(txtWeight_Name, "value");
                    weight = (int.Parse(weight) + int.Parse(newvalue)).ToString();
                    EnterText(txtPieces_Name, pieces);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Pieces: " + pieces);
                    EnterText(txtWeight_Name, weight);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Weight: " + weight);
                    EnterKeys(txtWeight_Name, Keys.Tab);
                }
                else if (fieldToUpdate.Equals("destination"))
                {
                    destination = newvalue;
                    EnterText(txtDestination_Name, destination);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Destination: " + destination);
                    EnterKeys(txtDestination_Name, Keys.Tab);
                }


            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in verifying and updating shipment details: " + e.ToString());
                Log.Error("Error in verifying and updating shipment details: " + e.ToString());
            }

        }


        public void ClickOnContinueShipmentButton()
        {
            try
            {
                ScrollDown();
                Click(btnContinueShipmentCommodity_Name);
                WaitForElementToBeInvisible(btnContinueShipmentCommodity_Name, TimeSpan.FromSeconds(5));
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Continue button for shipment details");
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in clicking on Continue button for shipment details: " + e.ToString());
                Log.Error("Error in clicking on Continue button for shipment details: " + e.ToString());
            }

        }

        public void OpenAndVerifyFlightDetails()
        {
            try
            {
                Click(btnOrangePencilFlight_Css);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Orange Pencil for Flight Details");
                WaitForElementToBeInvisible(btnOrangePencilFlight_Css, TimeSpan.FromSeconds(5));

                flightNum = GetAttributeValue(txtFlightNo_Name, "value");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Flight Number: " + flightNum);
                this.flightSegment = GetAttributeValue(txtSegment_Name, "value");
                //Hooks.Hooks.UpdateTest(Status.Pass, "Flight Segment: " + flightSegment);
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in opening and verifying flight details: " + e.ToString());
                Log.Error("Error in opening and verifying flight details: " + e.ToString());
            }

        }

        public void EnterFlightDetails(string carrierCode, string fltnum)
        {

            this.flightSegment = origin + destination;
            flightNum = fltnum;
            try
            {
                EnterText(txtCarrierCode_Name, carrierCode);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Carrier Code: " + carrierCode);
                EnterText(txtFlightNo_Name, flightNum);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Flight Number: " + flightNum);
                EnterText(txtFlightDate_Name, shippingDatePST);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Flight Date: " + shippingDatePST);
                EnterText(txtSegment_Name, flightSegment);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Flight Segment: " + flightSegment);
                EnterText(txtBookedPiece_Name, pieces);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Booked Pieces: " + pieces);
                EnterText(txtBookedWgt_Name, weight);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Booked Weight: " + weight);
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in entering flight details: " + e.ToString());
                Log.Error("Error in entering flight details: " + e.ToString());
            }


        }

        public void VerifyAndUpdateFlightDetails(string fieldToUpdate)
        {
            try
            {
                Click(btnOrangePencilFlight_Css);
                WaitForElementToBeInvisible(btnOrangePencilFlight_Css, TimeSpan.FromSeconds(5));
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Orange Pencil for Flight Details");
                if (fieldToUpdate.Equals("piece&weight"))
                {
                    EnterText(txtBookedPiece_Name, pieces);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Booked Pieces: " + pieces);
                    EnterText(txtBookedWgt_Name, weight);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Booked Weight: " + weight);
                }
                else if (fieldToUpdate.Equals("destination"))
                {

                    DoubleClick(btnTrashIcon_Css);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Trash Icon to delete flight details");
                    WaitForTextToBeCleared(txtFlightNo_Name, TimeSpan.FromSeconds(10));
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Booked Flight is cleared");

                }


            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in verifying and updating flight details: " + e.ToString());
                Log.Error("Error in verifying and updating flight details: " + e.ToString());
            }


        }


        public void OpenAndDeleteFlight()
        {
            try
            {
                //Click(lblFlightDetails_ID);
                Click(btnOrangePencilFlight_Css);
                WaitForElementToBeInvisible(btnOrangePencilFlight_Css, TimeSpan.FromSeconds(5));
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Orange Pencil for Flight Details");
                DoubleClick(btnTrashIcon_Css);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Trash Icon to delete flight details");
                WaitForTextToBeCleared(txtFlightNo_Name, TimeSpan.FromSeconds(10));
                //Hooks.Hooks.UpdateTest(Status.Pass, "Booked Flight is cleared");

            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in deleting flight details: " + e.ToString());
                Log.Error("Error in deleting flight details: " + e.ToString());
            }

        }

        public void ClickOnSelectFlightButton()
        {
            try
            {
                ClickOnElementIfPresent(btnSelectFlight_Name);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Select Flight button");
                WaitForElementToBeVisible(lblFlightListHeader_Xpath, TimeSpan.FromSeconds(30));
                while (IsElementDisplayed(lblFlightListHeader_Xpath))
                {
                    Click(btnSelectFlight_Name);
                    break;
                }


            }
            catch (Exception e)
            {
                Log.Error("Error in clicking on Select Flight button: " + e.ToString());
            }

        }

        public void BookFlightWithAllAvailability()
        {
            try
            {
                int noOfFlights = GetElementCount(listAllFlights_Xpath);
                if (noOfFlights > 0)
                {
                    for (int i = 1; i <= noOfFlights; i++)
                    {
                        if (GetAttributeValue(By.XPath("//*[@id='flight_details']//tbody//tr[" + (i + 1) + "]"), "class").Contains("row-border-merge"))
                        {
                            if (!GetCAPAvailabilityStatus(i).Contains("error") && !GetEMBAvailabilityStatus(i).Contains("error") && !GetLOADAvailabilityStatus(i).Contains("error") && !GetRESAvailabilityStatus(i).Contains("error") && !GetCAPAvailabilityStatus(i + 1).Contains("error") && !GetEMBAvailabilityStatus(i + 1).Contains("error") && !GetLOADAvailabilityStatus(i + 1).Contains("error") && !GetRESAvailabilityStatus(i + 1).Contains("error"))
                            {
                                flightNum = GetText(By.XPath("//*[@id='flight_details']//tbody//tr[" + i + "]//td[1]")).Trim().Split("AS")[1].Trim();
                                ConnectingflightNum = GetText(By.XPath("//*[@id='flight_details']//tbody//tr[" + (i + 1) + "]//td[1]")).Trim().Split("AS")[1].Trim();
                                btnBookFlight = btnBookFlight.Replace("1", i.ToString());
                                ScrollDown();
                                EnterKeys(By.XPath(btnBookFlight), Keys.Enter);
                                //Hooks.Hooks.UpdateTest(Status.Pass, "Flight " + flightNum + " & connecting flightNum " + ConnectingflightNum + " is booked successfully");
                                Log.Info("Flight " + flightNum + " & connecting flightNum " + ConnectingflightNum + " is booked successfully");
                                shippingDatePST = GetAttributeValue(txtFlightDate_Name, "value");
                                break;
                            }
                            i = i + 1;
                        }
                        else
                        {
                            if (!(GetCAPAvailabilityStatus(i).Contains("error") && GetEMBAvailabilityStatus(i).Contains("error") && GetLOADAvailabilityStatus(i).Contains("error") && GetRESAvailabilityStatus(i).Contains("error")))
                            {
                                flightNum = GetText(By.XPath("//*[@id='flight_details']//tbody//tr[" + i + "]//td[1]")).Trim().Split("AS")[1].Trim();

                                btnBookFlight = btnBookFlight.Replace("1", i.ToString());
                                ScrollDown();
                                EnterKeys(By.XPath(btnBookFlight), Keys.Enter);
                                //Hooks.Hooks.UpdateTest(Status.Pass, "Flight " + flightNum + " is booked successfully");
                                Log.Info("Flight " + flightNum + " is booked successfully");
                                shippingDatePST = GetAttributeValue(txtFlightDate_Name, "value");
                                break;
                            }
                        }



                    }


                }
                else
                {
                    //Hooks.Hooks.UpdateTest(Status.Fail, "No flight is available for booking from " + origin + " to " + destination + " on " + shippingDatePST);
                    Log.Info("No flight is available for booking from " + origin + " to " + destination + " on " + shippingDatePST);
                }


            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in booking flight: " + e.ToString());
                Log.Error("Error in booking flight: " + e.ToString());
            }

        }

        public string BookWithSpecificFlightType(string typeOfFlight)
        {
            try
            {

                int noOfFlights = GetElementCount(listAllFlights_Xpath);
                bool isFlightExists = false;
                if (noOfFlights > 0)
                {
                    for (int i = 1; i <= noOfFlights; i++)
                    {
                        if (!GetAttributeValue(By.XPath("//*[@id='flight_details']//tbody//tr[" + (i + 1) + "]"), "class").Contains("row-border-merge"))
                        {
                            if (!GetCAPAvailabilityStatus(i).Contains("error") && !GetEMBAvailabilityStatus(i).Contains("error") && !GetLOADAvailabilityStatus(i).Contains("error") && !GetRESAvailabilityStatus(i).Contains("error") && GetFlightType(i).Contains(typeOfFlight) && GetFlightDate(i).Contains(shippingDatePSTDDMMM))
                            {
                                isFlightExists = true;
                                flightNum = GetText(By.XPath("//*[@id='flight_details']//tbody//tr[" + i + "]//td[1]")).Trim().Split("AS")[1].Trim();

                                btnBookFlight = btnBookFlight.Replace("1", i.ToString());
                                if (IsElementPresent(By.XPath(btnBookFlight)))
                                {
                                    ScrollDown();
                                    EnterKeys(By.XPath(btnBookFlight), Keys.Enter);
                                }
                                else
                                {
                                    btnBookFlight = btnBookFlight.Replace(i.ToString(), (i - 1).ToString());
                                    ScrollDown();
                                    EnterKeys(By.XPath(btnBookFlight), Keys.Enter);
                                }

                                shippingDatePST = GetAttributeValue(txtFlightDate_Name, "value");
                                break;
                            }

                        }

                    }// End of for loop

                }

                if (isFlightExists == false)
                {
                    //Hooks.Hooks.UpdateTest(Status.Fail, "No flight is available for booking from " + origin + " to " + destination + " on " + shippingDatePST);
                    Log.Info("No flight is available for booking from " + origin + " to " + destination + " on " + shippingDatePST);
                }
                else
                {
                    //Hooks.Hooks.UpdateTest(Status.Pass, typeOfFlight + " Flight: " + flightNum + " is booked successfully");
                    Log.Info(typeOfFlight + " Flight: " + flightNum + " is booked successfully");

                }

                return flightNum;
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in booking flight:No flights available for yor selected criteria! " + e.ToString());
                Log.Error("Error in booking flight: " + e.ToString());
                return null;
            }

        }

        public string BookSpecificFlightWithEmbargo(string typeOfFlight)
        {
            try
            {
                int noOfFlights = GetElementCount(listAllFlights_Xpath);
                if (noOfFlights > 0)
                {
                    for (int i = 1; i <= noOfFlights; i++)
                    {
                        if (!GetAttributeValue(By.XPath("//*[@id='flight_details']//tbody//tr[" + (i + 1) + "]"), "class").Contains("row-border-merge"))
                        {
                            if (!GetCAPAvailabilityStatus(i).Contains("error") && !GetLOADAvailabilityStatus(i).Contains("error") && !GetRESAvailabilityStatus(i).Contains("error") && GetFlightType(i).Contains(typeOfFlight))
                            {
                                flightNum = GetText(By.XPath("//*[@id='flight_details']//tbody//tr[" + i + "]//td[1]")).Trim().Split("AS")[1].Trim();

                                btnBookFlight = btnBookFlight.Replace("1", i.ToString());
                                ScrollDown();
                                EnterKeys(By.XPath(btnBookFlight), Keys.Enter);
                                //Hooks.Hooks.UpdateTest(Status.Pass, typeOfFlight + " Flight: " + flightNum + " is booked successfully");
                                Log.Info(typeOfFlight + " Flight: " + flightNum + " is booked successfully");
                                shippingDatePST = GetAttributeValue(txtFlightDate_Name, "value");
                                break;
                            }

                        }

                    }

                }
                else
                {
                    //Hooks.Hooks.UpdateTest(Status.Fail, "No flight is available for booking from " + origin + " to " + destination + " on " + shippingDatePST);
                    Log.Info("No flight is available for booking from " + origin + " to " + destination + " on " + shippingDatePST);
                }


            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in booking flight: " + e.ToString());
                Log.Error("Error in booking flight: " + e.ToString());
            }

            return flightNum;

        }

        public void BookConnectingFlightWithDifferentFlightTypes(string firstflttyp, string secondflttype)
        {
            try
            {
                int noOfFlights = GetElementCount(listAllFlights_Xpath);
                if (noOfFlights > 0)
                {
                    for (int i = 1; i <= noOfFlights; i++)
                    {
                        if (GetAttributeValue(By.XPath("//*[@id='flight_details']//tbody//tr[" + (i + 1) + "]"), "class").Contains("row-border-merge"))
                        {
                            if (GetFlightType(i).Contains(firstflttyp) && GetFlightType(i + 1).Contains(secondflttype))
                            {
                                if (!GetCAPAvailabilityStatus(i).Contains("error") && !GetEMBAvailabilityStatus(i).Contains("error") && !GetLOADAvailabilityStatus(i).Contains("error") && !GetRESAvailabilityStatus(i).Contains("error") && !GetCAPAvailabilityStatus(i + 1).Contains("error") && !GetEMBAvailabilityStatus(i + 1).Contains("error") && !GetLOADAvailabilityStatus(i + 1).Contains("error") && !GetRESAvailabilityStatus(i + 1).Contains("error"))
                                {
                                    flightNum = GetText(By.XPath("//*[@id='flight_details']//tbody//tr[" + i + "]//td[1]")).Trim().Split("AS")[1].Trim();
                                    ConnectingflightNum = GetText(By.XPath("//*[@id='flight_details']//tbody//tr[" + (i + 1) + "]//td[1]")).Trim().Split("AS")[1].Trim();
                                    btnBookFlight = btnBookFlight.Replace("1", i.ToString());
                                    ScrollDown();
                                    EnterKeys(By.XPath(btnBookFlight), Keys.Enter);
                                    //Hooks.Hooks.UpdateTest(Status.Pass, firstflttyp + " Flight " + flightNum + " & connecting " + secondflttype + " Flight " + ConnectingflightNum + " is booked successfully");
                                    Log.Info(firstflttyp + " Flight " + flightNum + " & connecting " + secondflttype + " Flight " + ConnectingflightNum + " is booked successfully");
                                    shippingDatePST = GetAttributeValue(txtFlightDate_Name, "value");
                                    break;
                                }

                            }
                            i += 1;
                        }

                    }

                }
                else
                {
                    //Hooks.Hooks.UpdateTest(Status.Fail, "No flight is available for booking from " + origin + " to " + destination + " on " + shippingDatePST);
                    Log.Info("No flight is available for booking from " + origin + " to " + destination + " on " + shippingDatePST);
                }
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in booking flight: " + e.ToString());
                Log.Error("Error in booking flight: " + e.ToString());
            }

        }


        public void SelectFlightWithRestriction()
        {
            try
            {
                int noOfFlights = GetElementCount(listAllFlights_Xpath);
                if (noOfFlights > 0)
                {
                    for (int i = 1; i <= noOfFlights; i++)
                    {
                        if (GetAttributeValue(By.XPath("//*[@id='flight_details']//tbody//tr[" + (i + 1) + "]"), "class").Contains("row-border-merge"))
                        {
                            if (!GetCAPAvailabilityStatus(i).Contains("error") && !GetEMBAvailabilityStatus(i).Contains("error") && !GetLOADAvailabilityStatus(i).Contains("error") || GetRESAvailabilityStatus(i).Contains("error") && !GetCAPAvailabilityStatus(i + 1).Contains("error") && !GetEMBAvailabilityStatus(i + 1).Contains("error") && !GetLOADAvailabilityStatus(i + 1).Contains("error") || GetRESAvailabilityStatus(i + 1).Contains("error"))
                            {
                                flightNum = GetText(By.XPath("//*[@id='flight_details']//tbody//tr[" + i + "]//td[1]")).Trim().Split("AS")[1].Trim();
                                ConnectingflightNum = GetText(By.XPath("//*[@id='flight_details']//tbody//tr[" + (i + 1) + "]//td[1]")).Trim().Split("AS")[1].Trim();
                                btnBookFlight = btnBookFlight.Replace("1", i.ToString());
                                ScrollDown();
                                EnterKeys(By.XPath(btnBookFlight), Keys.Enter);
                                //Hooks.Hooks.UpdateTest(Status.Pass, "Flight " + flightNum + " & connecting flightNum " + ConnectingflightNum + "having minimum connection time restriction, is booked successfully");
                                Log.Info("Flight " + flightNum + " & connecting flightNum " + ConnectingflightNum + "having minimum connection time restriction, is booked successfully");
                                shippingDatePST = GetAttributeValue(txtFlightDate_Name, "value");
                                break;
                            }
                            i += 1;

                        }

                    }

                }
                else
                {
                    //Hooks.Hooks.UpdateTest(Status.Fail, "No flight is available for booking from " + origin + " to " + destination + " on " + shippingDatePST);
                    Log.Info("No flight is available for booking from " + origin + " to " + destination + " on " + shippingDatePST);
                }


            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in booking flight: " + e.ToString());
                Log.Error("Error in booking flight: " + e.ToString());
            }

        }


        public string GetFlightType(int flightRowNum)
        {
            lblFlightType = "//*[@id='flight_details']//tbody//tr[" + flightRowNum + "]//td[3]";
            string flightType = GetText(By.XPath(lblFlightType)).Trim();
            return flightType;
        }

        public string GetCAPAvailabilityStatus(int flightRowNum)
        {
            lblAvailabilityCap = "//*[@id='flight_details']//tbody//tr[" + flightRowNum + "]//span[text()='CAP']";
            string status = GetAttributeValue(By.XPath(lblAvailabilityCap), "class");
            return status;
        }

        public string GetEMBAvailabilityStatus(int flightRowNum)
        {
            lblAvailabilityEMB = "//*[@id='flight_details']//tbody//tr[" + flightRowNum + "]//span[text()='EMB']";
            string status = GetAttributeValue(By.XPath(lblAvailabilityEMB), "class");
            return status;
        }

        public string GetLOADAvailabilityStatus(int flightRowNum)
        {
            lblAvailabilityLOAD = "//*[@id='flight_details']//tbody//tr[" + flightRowNum + "]//span[text()='LOAD']";
            string status = GetAttributeValue(By.XPath(lblAvailabilityLOAD), "class");
            return status;
        }

        public string GetRESAvailabilityStatus(int flightRowNum)
        {
            lblAvailabilityRES = "//*[@id='flight_details']//tbody//tr[" + flightRowNum + "]//span[text()='RES']";
            string status = GetAttributeValue(By.XPath(lblAvailabilityRES), "class");
            return status;
        }

        public string GetFlightDate(int flightRowNum)
        {
            lblFlightDate = "//*[@id='flight_details']//tbody//tr[" + flightRowNum + "]/td[2]/div[2]";
            string flightDate = GetText(By.XPath(lblFlightDate)).Trim();
            return flightDate;
        }

        public void ClickOnContinueFlightDetailsButton()
        {
            try
            {
                ScrollDown();
                EnterKeys(btnContinueFlightDetails_Name, Keys.Enter);
                WaitForElementToBeInvisible(btnContinueFlightDetails_Name, TimeSpan.FromSeconds(5));
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Continue button for flight details");
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in clicking on Continue button for flight details: " + e.ToString());
                Log.Error("Error in clicking on Continue button for flight details: " + e.ToString());
            }

        }

        public void OpenAndVerifyChargeDetails()
        {
            try
            {
                WaitForElementToBeClickable(btnOrangePencilCharge_Css, TimeSpan.FromSeconds(10));
                Click(btnOrangePencilCharge_Css);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Orange Pencil for Charge Details");
                WaitForElementToBeInvisible(btnOrangePencilCharge_Css, TimeSpan.FromSeconds(5));
                ScrollDown();
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in opening and verifying charge details: " + e.ToString());
                Log.Error("Error in opening and verifying charge details: " + e.ToString());
            }

        }

        public void EnterChargeDetails(string paymentType, string modeOfPayment)
        {
            this.chargeType = paymentType;
            this.modeOfPayment = modeOfPayment;

            try
            {
                WaitForElementToBeVisible(btnPaymentType_Xpath, TimeSpan.FromSeconds(10));
                string paymentTypeDisplayed = GetText(btnPaymentType_Xpath);

                if (!paymentTypeDisplayed.Equals(chargeType))
                {
                    Click(btnPaymentType_Xpath);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Payment Type: " + chargeType);
                }
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in selecting payment type: " + e.ToString());
                Log.Error("Error in entering charge details: " + e.ToString());
            }

        }

        public void CheckThirdPartyCheckbox()
        {
            Click(chkBoxThirdParty_Id);
            WaitForElementToBeEnabled(txtRatedCustomerNumber_Name, TimeSpan.FromSeconds(5));
            //Hooks.Hooks.UpdateTest(Status.Pass, "Checked Third Party Checkbox");
        }

        public void EnterRatedCustomerNumber(string ratedCustomerNumber)
        {
            EnterText(txtRatedCustomerNumber_Name, ratedCustomerNumber);
            EnterKeys(txtRatedCustomerNumber_Name, Keys.Tab);
            //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Rated Customer Number: " + ratedCustomerNumber);
        }


        //public void ClickOnCalculateChargeButton()
        //{

        //    while (!checkTextboxIsNotEmpty(txtIATACharge_Xpath))
        //    {
        //        try
        //        {
        //            ClickElementUsingJavaScript(btnCalculateCharges_Name);
        //            Thread.Sleep(1000);
        //            //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Calculate Charge button");
        //        }

        //        catch (Exception e)
        //        {
        //            //Hooks.Hooks.UpdateTest(Status.Info, "Error in clicking on Calculate Charge button: " + e.ToString());
        //            Log.Error("Error in clicking on Calculate Charge button: " + e.ToString());
        //        }

        //    }

        //}

        public void ClickOnCalculateChargeButton()
        {
            ClickOnElementIfPresent(btnCalculateCharges_Name);
            SwitchToDefaultContent();
            if (IsElementDisplayed(popupWarning_Css, 3))
            {
               Click(btnYesActiveCashDraw_Xpath);                
               WaitForElementToBeInvisible(btnYesActiveCashDraw_Xpath, TimeSpan.FromMilliseconds(3000));
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Yes for Active Cash Draw");
            }
            SwitchToLTEContentFrame();            
            
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
                    cip.captureIrregularity(pieces, weight);
                    SwitchToLTEContentFrame();
                }
            }

            else
            {
                SwitchToDefaultContent();
                if (IsElementDisplayed(popupWarning_Css, 1))
                {
                    errorText = GetText(popupAlertMessage_Xpath);
                    if (errorText.ToLower().Contains("cash draw"))
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

                SwitchToLTEContentFrame();
            }



            return errorText;
        }

        public void SelectModeOfPayment(string modeOfPayment)
        {
            try
            {
                ScrollDown();

                Click(drpdwnModeOfPayment_Name);
                SelectDropdownByVisibleText(drpdwnModeOfPayment_Name, modeOfPayment);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Mode of Payment: " + modeOfPayment);

            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in selecting mode of payment: " + e.ToString());
                Log.Error("Error in selecting mode of payment: Retrying... " + e.ToString());

            }

        }

        public string ClickOnContinueChargeButton()
        {

            try
            {
                if (chargeType.Equals("PP") && !serviceCargoClass.Equals("Free of Charge") && !serviceCargoClass.Equals("COMAT"))
                {
                    SelectModeOfPayment(modeOfPayment);
                }

            }
            catch (Exception)
            {
                ClickingYesOnPopupWarnings("");
                if (chargeType.Equals("PP") && !serviceCargoClass.Equals("Free of Charge") && !serviceCargoClass.Equals("COMAT"))
                {
                    SelectModeOfPayment(modeOfPayment);
                }

                ScrollDown();

            }
            totalAmountCharged = GetText(txtTotalCharge_Xpath).Split(':')[1].Trim();
            totalAmountCharged = totalAmountCharged.Split("USD")[0];
            ClickElementUsingJavaScript(btnContinueChargeDetails_Name);
            WaitForElementToBeInvisible(btnContinueChargeDetails_Name, TimeSpan.FromSeconds(5));
            //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Continue button for charge details");
            return totalAmountCharged;

        }

        public void EnterAcceptanceDetails()
        {
            try
            {
                EnterText(txtPieceAccepted_Name, pieces);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Pieces Accepted: " + pieces);
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in entering acceptance details: " + e.ToString());
                Log.Error("Error in entering acceptance details: " + e.ToString());
            }

        }

        public void VerifyAndUpdateAcceptanceDetails()
        {
            try
            {
                Click(btnOrangePencilAcceptance_Css);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Orange Pencil for Acceptance Details");
                WaitForElementToBeInvisible(btnOrangePencilAcceptance_Css, TimeSpan.FromSeconds(5));

            }
            catch (Exception)
            {
                Log.Error("Error in clicking the acceptance details, retrying.. ");
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in clicking the acceptance details, retrying.. ");
                Click(btnOrangePencilAcceptance_Css);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Orange Pencil for Acceptance Details");
                WaitForElementToBeInvisible(btnOrangePencilAcceptance_Css, TimeSpan.FromSeconds(5));
            }
            EnterText(txtPieceAccepted_Name, pieces);
            //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Pieces Accepted: " + pieces);
            EnterKeys(txtPieceAccepted_Name, Keys.Tab);


        }

        public void ClickOnContinueAcceptanceButton()
        {
            try
            {
                ScrollDown();
                Click(btnContinueAcceptanceDetails_Name);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Continue button for acceptance details");
                WaitForElementToBeInvisible(btnContinueAcceptanceDetails_Name, TimeSpan.FromSeconds(5));
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in clicking on Continue button for acceptance details: " + e.ToString());
                Log.Error("Error in clicking on Continue button for acceptance details: " + e.ToString());
            }

        }

        public void EnterScreeningDetails(int rownum, string screeningMethod, string screeningResult)
        {
            try
            {

                if (rownum > 1)
                {
                    int totalPieces = int.Parse(pieces);
                    string pieceFirstRow = (totalPieces - 1).ToString();
                    string pieceSecondRow = "1";

                    EnterText(txtScreeningAirport2_Xpath, origin);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Screening Airport: " + origin);
                    SelectDropdownByVisibleText(drpdwnScreeningMethod2_Xpath, screeningMethod);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Screening Method: " + screeningMethod);
                    EnterText(txtScreeningPieces1_Xpath, pieceFirstRow);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Pieces: " + pieceFirstRow);
                    EnterText(txtScreeningPieces2_Xpath, pieceSecondRow);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Pieces: " + pieceSecondRow);
                    SelectDropdownByVisibleText(drpdwnScreeningResult2_Xpath, screeningResult);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Screening Result: " + screeningResult);

                }
                else
                {
                    EnterText(txtScreeningAirport1_Xpath, origin);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Screening Airport: " + origin);
                    SelectDropdownByVisibleText(drpdwnScreeningMethod1_Xpath, screeningMethod);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Screening Method: " + screeningMethod);
                    EnterText(txtScreeningPieces1_Xpath, pieces);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Pieces: " + pieces);
                    SelectDropdownByVisibleText(drpdwnScreeningResult1_Xpath, screeningResult);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Screening Result: " + screeningResult);
                }
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in entering screening details: " + e.ToString());
                Log.Error("Error in entering screening details: " + e.ToString());
            }


        }

        public void AddAnotherScreeningLine()
        {
            try
            {
                Click(btnAddScreeningRow_Css);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Add another screening line");
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in adding another screening line: " + e.ToString());
                Log.Error("Error in adding another screening line: " + e.ToString());
            }

        }

        public void VerifyAndUpdateScreeningDetails()
        {
            try
            {
                Click(btnOrangePencilScreening_Css);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Orange Pencil for Screening Details");
                WaitForElementToBeInvisible(btnOrangePencilScreening_Css, TimeSpan.FromSeconds(5));

            }
            catch (Exception)
            {
                Log.Error("Error in verifying and updating screening details,retrying..:");
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in verifying and updating screening details,retrying..:");
                Click(btnOrangePencilScreening_Css);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Orange Pencil for Screening Details");
                WaitForElementToBeInvisible(btnOrangePencilScreening_Css, TimeSpan.FromSeconds(5));
            }
            EnterText(txtScreeningPieces1_Xpath, pieces);
            //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Pieces: " + pieces);

        }

        public void ClickOnContinueScreeningButton()
        {
            try
            {
                ScrollDown();
                EnterKeys(btnContinueScreeningDetails_Name, Keys.Enter);
                WaitForElementToBeInvisible(btnContinueScreeningDetails_Name, TimeSpan.FromSeconds(5));
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Continue button for screening details");

            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in clicking on Continue button for screening details: " + e.ToString());
                Log.Error("Error in clicking on Continue button for screening details: " + e.ToString());
            }

        }

        public void ClickOnAWBVerifiedCheckbox()
        {
            try
            {
                if (!IsCheckboxChecked(chkBoxAwbVerified_Name))
                {
                    Click(chkBoxAwbVerified_Name);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on AWB verified checkbox");
                }
            }
            catch (Exception e)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Error in clicking on AWB verified checkbox: " + e.ToString());
                Log.Error("Error in clicking on AWB verified checkbox: " + e.ToString());
            }

        }

        public void ClickSave()
        {
            noOfWindowBefore = GetNumberOfWindowsOpened();
            Click(btnSaveShipment_Name);
            if (IsElementDisplayed(lblEmbargoDetails_Xpath, 1))
            {
                Click(btnContinueEmbargo_Xpath);
                WaitForElementToBeInvisible(btnContinueEmbargo_Xpath, TimeSpan.FromSeconds(5));
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Continue button for Embargo");
            }
            //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Save button");
        }
        public void ClosePaymentPortalWindow()
        {
            WaitForNewWindowToOpen(TimeSpan.FromSeconds(5), noOfWindowBefore + 1);
            SwitchToLastWindow();
            ppp.ClosePaymentPortal();
            SwitchToLastWindow();
            SwitchToLTEContentFrame();
        }

        public void PaymentWithPPCCinPortal(string chargeType)
        {
            WaitForNewWindowToOpen(TimeSpan.FromSeconds(10), noOfWindowBefore + 1);
            SwitchToLastWindow();
            ppp.PaymentWithPPCC(chargeType);
            SwitchToLastWindow();
            SwitchToLTEContentFrame();
        }


        public (string, string) SaveShipmentDetailsAndHandlePopups()
        {
            log.Info("Saving Shipment Details and handling all popup function");
            int retryCount = 0;
            const int maxRetries = 3; // Maximum number of retries
            //int chkBookingStatusCnt = 0;

            while (true)
            {
                //chkBookingStatusCnt+=1;
                try
                {
                    if ((CaptureBookingStatus() is "Confirmed" or "Queued") && (CaptureDataCaptureStatus() == "EXECUTED") && (CaptureAcceptanceStatus() == "Finalised") && (CaptureColorReadyForCarriageStatus().Contains("green")) && (CaptureColorCaptureCheckSheetStatus().Contains("green")) && (CaptureColorBlockStatus().Contains("green")))
                    {
                        awb_num = captureAWBNumber();
                        //Hooks.Hooks.UpdateTest(Status.Info, "AWB Number: " + awb_num);
                        ClickOnElementIfPresent(btnOrangePencilEditBooking_Css);
                        WaitForElementToBeVisible(btnClear_Id, TimeSpan.FromSeconds(5));
                        ClickElementUsingActions(btnClear_Id);
                        WaitForElementToBeInvisible(lblShipmentDetails_Css, TimeSpan.FromSeconds(10));
                        //Hooks.Hooks.UpdateTest(Status.Info, "Clicked on Clear button to refesh the AWB details");
                        SwitchToDefaultContent();
                        break;
                    }

                    try
                    {
                        totalPaybleAmount = ClickOnSaveButtonHandlePaymentPortal();
                    }
                    catch (Exception)
                    {
                        int noOfWindowsBefore = GetNumberOfWindowsOpened();
                        ClickingYesOnPopupWarnings("");
                        if (IsElementDisplayed(lblEmbargoDetails_Xpath, 1))
                        {
                            Click(btnContinueEmbargo_Xpath);
                            //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Continue button for Embargo");
                        }

                        int noOfWindowsAfter = GetNumberOfWindowsOpened();
                        if (noOfWindowsAfter > noOfWindowsBefore)
                        {
                            SwitchToLastWindow();
                            (totalPaybleAmount, accountInfoCIDNum) = ppp.HandlePaymentInPaymentPortal(this.chargeType);
                            WaitForNewWindowToOpen(TimeSpan.FromSeconds(3), noOfWindowsBefore);
                            SwitchToLastWindow();
                            SwitchToLTEContentFrame();
                        }

                    }
                }
                catch (StaleElementReferenceException)
                {
                    if (retryCount >= maxRetries)
                    {
                        throw; // Rethrow the exception if max retries are exceeded
                    }
                    //Hooks.Hooks.UpdateTest(Status.Info, "Encountered StaleElementReferenceException, retrying... Attempt" + (retryCount + 1));
                    log.Info($"Encountered StaleElementReferenceException, retrying... Attempt {retryCount + 1}");
                    retryCount++;
                    continue; // Retry the loop
                }
            }

            return (awb_num, totalPaybleAmount);
        }

        public string SaveShipmentValidateWarningConfirmedAWB(string expectedWarningMessage)
        {
            log.Info("Saving shipment and validate the popped up messages for a Confirmed AWB function");
            int retryCount = 0;
            const int maxRetries = 3; // Maximum number of retries
           
            while (true)
            {
               
                try
                {
                    if (CaptureBookingStatus() is "Confirmed" or "Queued")
                    {

                        WaitForElementToBeVisible(lblWarningMessage_Css, TimeSpan.FromSeconds(10));
                        string actualWarningMessage = GetText(lblWarningMessage_Css);
                        if (!actualWarningMessage.Contains(expectedWarningMessage))
                        {
                            //Hooks.Hooks.UpdateTest(Status.Fail, "Warning message is not as expected. Expected: " + expectedWarningMessage + " Actual: " + actualWarningMessage);
                            Log.Error("Warning message is not as expected. Expected: " + expectedWarningMessage + " Actual: " + actualWarningMessage);

                        }
                        else
                        {
                            //Hooks.Hooks.UpdateTest(Status.Pass, "Warning message is as expected: " + actualWarningMessage);
                            Log.Info("Warning message is as expected: " + actualWarningMessage);
                        }
                        awb_num = captureAWBNumber();
                        //Hooks.Hooks.UpdateTest(Status.Info, "AWB Number: " + awb_num);
                        ClickOnElementIfPresent(btnOrangePencilEditBooking_Css);
                        WaitForElementToBeVisible(btnClear_Id, TimeSpan.FromSeconds(5));
                        ClickElementUsingActions(btnClear_Id);
                        WaitForElementToBeInvisible(lblShipmentDetails_Css, TimeSpan.FromSeconds(10));
                        //Hooks.Hooks.UpdateTest(Status.Info, "Clicked on Clear button to refesh the AWB details");
                        SwitchToDefaultContent();
                        break;
                    }

                    try
                    {
                        totalPaybleAmount = ClickOnSaveButtonHandlePaymentPortal();
                    }
                    catch (Exception)
                    {
                        int noOfWindowsBefore = GetNumberOfWindowsOpened();
                        ClickingYesOnPopupWarnings("");
                        if (IsElementDisplayed(lblEmbargoDetails_Xpath, 1))
                        {
                            Click(btnContinueEmbargo_Xpath);
                            //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Continue button for Embargo");
                        }

                        int noOfWindowsAfter = GetNumberOfWindowsOpened();
                        if (noOfWindowsAfter > noOfWindowsBefore)
                        {
                            SwitchToLastWindow();
                            (totalPaybleAmount, accountInfoCIDNum) = ppp.HandlePaymentInPaymentPortal(this.chargeType);
                            WaitForNewWindowToOpen(TimeSpan.FromSeconds(3), noOfWindowsBefore);
                            SwitchToLastWindow();
                            SwitchToLTEContentFrame();
                        }

                    }
                }
                catch (StaleElementReferenceException)
                {
                    if (retryCount >= maxRetries)
                    {
                        throw; // Rethrow the exception if max retries are exceeded
                    }
                    //Hooks.Hooks.UpdateTest(Status.Info, "Encountered StaleElementReferenceException, retrying... Attempt" + (retryCount + 1));
                    log.Info($"Encountered StaleElementReferenceException, retrying... Attempt {retryCount + 1}");
                    retryCount++;
                    continue; // Retry the loop
                }
            }

            return awb_num;
        }


        public string ClickOnSaveButtonHandlePaymentPortal()
        {
            log.Info("ClickOnSaveButtonHandlePaymentPortal function");
            int noOfWindowsBefore = GetNumberOfWindowsOpened();
            ClickSave();
            WaitForNewWindowToOpen(TimeSpan.FromSeconds(10), noOfWindowsBefore + 1);
            int noOfWindowsAfter = GetNumberOfWindowsOpened();
            if (noOfWindowsAfter > noOfWindowsBefore)
            {
                SwitchToLastWindow();
                (totalPaybleAmount, accountInfoCIDNum) = ppp.HandlePaymentInPaymentPortal(this.chargeType);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Back from Payment Portal");
                SwitchToLastWindow();
                SwitchToLTEContentFrame();
            }
            return totalPaybleAmount;

        }

        public string SaveWithChargeType(string chargeType)
        {
            int retryCount = 0;
            const int maxRetries = 3; // Maximum number of retries
            this.chargeType = chargeType;
            try
            {

                int noOfWindowsBefore = GetNumberOfWindowsOpened();
                ClickSave();
                WaitForNewWindowToOpen(TimeSpan.FromSeconds(30), noOfWindowsBefore + 1);
                int noOfWindowsAfter = GetNumberOfWindowsOpened();
                if (noOfWindowsAfter > noOfWindowsBefore)
                {
                    SwitchToLastWindow();
                    ppp.HandlePaymentInPaymentPortal(chargeType);
                    SwitchToLastWindow();
                    SwitchToLTEContentFrame();
                }

            }
            catch (StaleElementReferenceException)
            {
                if (retryCount >= maxRetries)
                {
                    throw; // Rethrow the exception if max retries are exceeded
                }
                log.Info($"Encountered StaleElementReferenceException, retrying... Attempt {retryCount + 1}");
                retryCount++;
                SaveWithChargeType(chargeType); // Retry the function
            }

            awb_num = captureAWBNumber();
            //Hooks.Hooks.UpdateTest(Status.Info, "AWB Number: " + awb_num);
            return awb_num;

        }

        public string SaveDetailsWithChargeType(string chargeType, string expectedWarningMessage)
        {

            this.chargeType = chargeType;
            int noOfWindowsBefore = GetNumberOfWindowsOpened();
            ClickSave();
            WaitForNewWindowToOpen(TimeSpan.FromSeconds(10), noOfWindowsBefore + 1);
            int noOfWindowsAfter = GetNumberOfWindowsOpened();
            if (noOfWindowsAfter > noOfWindowsBefore)
            {
                SwitchToLastWindow();
                ppp.HandlePaymentInPaymentPortal(chargeType);
                SwitchToLastWindow();
                SwitchToLTEContentFrame();
            }
            WaitForElementToBeVisible(lblWarningMessage_Css, TimeSpan.FromSeconds(10));
            string actualWarningMessage = GetText(lblWarningMessage_Css);
            if (!actualWarningMessage.Contains(expectedWarningMessage))
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Warning message is not as expected. Expected: " + expectedWarningMessage + " Actual: " + actualWarningMessage);
                Log.Error("Warning message is not as expected. Expected: " + expectedWarningMessage + " Actual: " + actualWarningMessage);

            }
            else
            {
                //Hooks.Hooks.UpdateTest(Status.Pass, "Warning message is as expected: " + actualWarningMessage);
                Log.Info("Warning message is as expected: " + actualWarningMessage);
            }

            awb_num = captureAWBNumber();
            //Hooks.Hooks.UpdateTest(Status.Info, "AWB Number: " + awb_num);
            ClickOnElementIfPresent(btnOrangePencilEditBooking_Css);
            WaitForElementToBeVisible(btnClear_Id, TimeSpan.FromSeconds(5));
            ClickElementUsingActions(btnClear_Id);
            WaitForElementToBeInvisible(lblShipmentDetails_Css, TimeSpan.FromSeconds(10));
            //Hooks.Hooks.UpdateTest(Status.Info, "Clicked on Clear button to refesh the AWB details");
            return awb_num;
        }

        public void SaveDetailsWithCapturingIrregularity(string chargetyp)
        {
            this.chargeType = chargetyp;
            int noOfWindowsBefore = GetNumberOfWindowsOpened();
            ClickSave();
            ClickingYesOnPopupWarnings("");
            ClickingYesOnPopupWarnings("Embargo");
            ClickingYesOnPopupWarnings("Capture Irregularity");
            WaitForNewWindowToOpen(TimeSpan.FromSeconds(10), noOfWindowsBefore + 1);
            int noOfWindowsAfter = GetNumberOfWindowsOpened();
            if (noOfWindowsAfter > noOfWindowsBefore)
            {
                SwitchToLastWindow();
                ppp.HandlePaymentInPaymentPortal(chargeType);
                SwitchToLastWindow();
                SwitchToLTEContentFrame();
            }
            ClickingYesOnPopupWarnings("");

        }

        public string SaveShipmentCaptureAWB(string expectedWarningMessage)
        {
            ClickSave();
            WaitForElementToBeVisible(lblWarningMessage_Css, TimeSpan.FromSeconds(10));
            string actualWarningMessage = GetText(lblWarningMessage_Css);
            if (!actualWarningMessage.Contains(expectedWarningMessage))
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Warning message is not as expected. Expected: " + expectedWarningMessage + " Actual: " + actualWarningMessage);
                Log.Error("Warning message is not as expected. Expected: " + expectedWarningMessage + " Actual: " + actualWarningMessage);
            }
            else
            {
                //Hooks.Hooks.UpdateTest(Status.Pass, "Warning message is as expected: " + actualWarningMessage);
                Log.Info("Warning message is as expected: " + actualWarningMessage);
            }

            awb_num = captureAWBNumber();
            //Hooks.Hooks.UpdateTest(Status.Info, "AWB Number: " + awb_num);
            ClickOnElementIfPresent(btnOrangePencilEditBooking_Css);
            WaitForElementToBeVisible(btnClear_Id, TimeSpan.FromSeconds(5));
            ClickElementUsingActions(btnClear_Id);
            WaitForElementToBeInvisible(lblShipmentDetails_Css, TimeSpan.FromSeconds(10));
            //Hooks.Hooks.UpdateTest(Status.Info, "Clicked on Clear button to refesh the AWB details");
            SwitchToDefaultContent();
            return awb_num;
        }

        public (string, string) SaveWithDGAndCheckSheet(string chargetype, string unid, string propershipmntname, string pi, string noofpkg, string netqtyperpkg, string reportable)
        {
            int retryCount = 0;
            const int maxRetries = 3; // Maximum number of retries
            this.chargeType = chargetype;
            Click(btnSaveShipment_Name);            
            //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Save button");            
            ClickingYesOnPopupWarnings("");
            dgp.HandleDGShipment(unid, propershipmntname, pi, noofpkg, netqtyperpkg, reportable);
            SwitchToLTEContentFrame();
            Click(btnSaveShipment_Name);
            ClickingYesOnPopupWarnings("");
            CaptureCheckSheetForDG();
            ClickOnAWBVerifiedCheckbox();

            //string awb_num;
            while (true)
            {
                try
                {
                    if ((CaptureBookingStatus() is "Confirmed" or "Queued") && (CaptureDataCaptureStatus() == "EXECUTED") && (CaptureAcceptanceStatus() == "Finalised") && (CaptureColorReadyForCarriageStatus().Contains("green")) && (CaptureColorCaptureCheckSheetStatus().Contains("green")) && (CaptureColorBlockStatus().Contains("green")))
                    {
                        awb_num = captureAWBNumber();
                        //Hooks.Hooks.UpdateTest(Status.Info, "AWB Number: " + awb_num);
                        ClickOnElementIfPresent(btnOrangePencilEditBooking_Css);
                        WaitForElementToBeVisible(btnClear_Id, TimeSpan.FromSeconds(5));
                        ClickElementUsingActions(btnClear_Id);
                        WaitForElementToBeInvisible(lblShipmentDetails_Css, TimeSpan.FromSeconds(10));
                        //Hooks.Hooks.UpdateTest(Status.Info, "Clicked on Clear button to refesh the AWB details");
                        break;
                    }

                    try
                    {
                        totalPaybleAmount = ClickOnSaveButtonHandlePaymentPortal();

                    }
                    catch (Exception)
                    {
                        int noOfWindowsBefore = GetNumberOfWindowsOpened();
                        ClickingYesOnPopupWarnings("");
                        int noOfWindowsAfter = GetNumberOfWindowsOpened();
                        if (noOfWindowsAfter > noOfWindowsBefore)
                        {
                            SwitchToLastWindow();
                            RefreshPage();
                            (totalPaybleAmount, accountInfoCIDNum) = ppp.HandlePaymentInPaymentPortal(this.chargeType);
                            WaitForNewWindowToOpen(TimeSpan.FromSeconds(3), noOfWindowsBefore);
                            SwitchToLastWindow();
                            SwitchToLTEContentFrame();
                        }
                    }
                }
                catch (StaleElementReferenceException)
                {
                    if (retryCount >= maxRetries)
                    {
                        throw; // Rethrow the exception if max retries are exceeded
                    }
                    log.Info($"Encountered StaleElementReferenceException, retrying... Attempt {retryCount + 1}");
                    retryCount++;
                    continue; // Retry the loop
                }


            }
            return (awb_num, totalPaybleAmount);

        }

        public string captureAWBNumber()
        {
            WaitForElementToBeVisible(lblAWBNo_Css, TimeSpan.FromSeconds(5));
            return GetText(lblAWBNo_Css);
        }

        public string CaptureBookingStatus()
        {
            return GetText(lblBookingConfirmation_Xpath);
        }

        public string CaptureDataCaptureStatus()
        {
            return GetText(lblDataCapture_Xpath);
        }

        public string CaptureAcceptanceStatus()
        {
            return GetText(lblAcceptance_Xpath);
        }

        public string CaptureColorReadyForCarriageStatus()
        {
            return GetAttributeValue(lblColorReadyForCarriage_Xpath, "class");
        }

        public string CaptureColorCaptureCheckSheetStatus()
        {
            return GetAttributeValue(lblColorCaptureChecksheet_Xpath, "class");
        }

        public string CaptureColorBlockStatus()
        {
            return GetAttributeValue(lblColorBlock_Xpath, "class");
        }

        public void CaptureCheckSheetForDG()
        {
            Click(lnkCaptureChecksheet_Xpath);
            //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Capture Checksheet link");
            SwitchToFrame(popupContainerFrameChksheet);

            List<IWebElement> DgSections = GetElements(lblTotalChkSheetSections_Xpath);
            int totalQuestions = 0;

            foreach (var section in DgSections)
            {
                string sectionText = section.Text;
                if (section.Text == "Non Radioactive Checklist")
                {
                    string drpDwnQn = "//*[@id='tabs-1']//div[@id='configId']/h2[text()='dgSectionName']/parent::div/following-sibling::div//select";
                    drpDwnQn = drpDwnQn.Replace("dgSectionName", "Non Radioactive Checklist");
                    totalQuestions = GetElementCount(By.XPath(drpDwnQn));
                    drpDwnQn = drpDwnQn + "[@name= 'questionwithAnswer[0].templateAnswer']";
                    if (!IsDropdownSelectedByVisibleText((By.XPath(drpDwnQn)), "Yes"))
                    {
                        for (int j = 0; j < totalQuestions; j++)
                        {
                            SelectDropdownByVisibleText(By.XPath(drpDwnQn.Replace("0", j.ToString())), "Yes");
                            EnterKeys(By.XPath(drpDwnQn), Keys.Tab);
                            //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Yes for Non Radioactive Checklist");
                        }
                    }

                }
                else if (section.Text == "DGR HANDLING INFORMATION")
                {
                    string drpDwnQn = "//*[@id='tabs-1']//div[@id='configId']/h2[text()='dgSectionName']/parent::div/following-sibling::div//select";
                    drpDwnQn = drpDwnQn.Replace("dgSectionName", "DGR HANDLING INFORMATION");
                    totalQuestions = GetElementCount(By.XPath(drpDwnQn));
                    drpDwnQn = drpDwnQn + "[@name= 'questionwithAnswer[0].templateAnswer']";
                    if (!IsDropdownSelectedByVisibleText((By.XPath(drpDwnQn)), "Yes"))
                    {
                        for (int j = 0; j < totalQuestions; j++)
                        {
                            SelectDropdownByVisibleText(By.XPath(drpDwnQn.Replace("0", j.ToString())), "Yes");
                            EnterKeys(By.XPath(drpDwnQn), Keys.Tab);
                            //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Yes for DGR HANDLING INFORMATION");
                        }

                    }

                }

                else if (section.Text == "RDS HANDLING INFORMATION")
                {
                    string drpDwnQn = "//*[@id='tabs-1']//div[@id='configId']/h2[text()='dgSectionName']/parent::div/following-sibling::div//select";
                    drpDwnQn = drpDwnQn.Replace("dgSectionName", "RDS HANDLING INFORMATION");
                    totalQuestions = GetElementCount(By.XPath(drpDwnQn));
                    drpDwnQn = drpDwnQn + "[@name= 'questionwithAnswer[0].templateAnswer']";
                    if (!IsDropdownSelectedByVisibleText((By.XPath(drpDwnQn)), "Yes"))
                    {
                        for (int j = 0; j < totalQuestions; j++)
                        {
                            SelectDropdownByVisibleText(By.XPath(drpDwnQn.Replace("0", j.ToString())), "Yes");
                            EnterKeys(By.XPath(drpDwnQn), Keys.Tab);
                            //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Yes for RDS HANDLING INFORMATION");
                        }

                    }

                }

                else if (section.Text == "ICE HANDLING INFORMATION")
                {
                    string drpDwnQn = "//*[@id='tabs-1']//div[@id='configId']/h2[text()='dgSectionName']/parent::div/following-sibling::div//select";
                    drpDwnQn = drpDwnQn.Replace("dgSectionName", "ICE HANDLING INFORMATION");
                    totalQuestions = GetElementCount(By.XPath(drpDwnQn));
                    drpDwnQn = drpDwnQn + "[@name= 'questionwithAnswer[0].templateAnswer']";
                    if (!IsDropdownSelectedByVisibleText((By.XPath(drpDwnQn)), "Yes"))
                    {
                        for (int j = 0; j < totalQuestions; j++)
                        {
                            SelectDropdownByVisibleText(By.XPath(drpDwnQn.Replace("0", j.ToString())), "Yes");
                            EnterKeys(By.XPath(drpDwnQn), Keys.Tab);
                            //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Yes for ICE HANDLING INFORMATION");
                        }
                    }

                }

                else if (section.Text == "UN1845 FULLY REGULATED")
                {
                    string drpDwnQn = "//*[@id='tabs-1']//div[@id='configId']/h2[text()='dgSectionName']/parent::div/following-sibling::div//select";
                    drpDwnQn = drpDwnQn.Replace("dgSectionName", "UN1845 FULLY REGULATED");
                    totalQuestions = GetElementCount(By.XPath(drpDwnQn));
                    drpDwnQn = drpDwnQn + "[@name= 'questionwithAnswer[0].templateAnswer']";
                    if (!IsDropdownSelectedByVisibleText((By.XPath(drpDwnQn)), "Yes"))
                    {
                        for (int j = 0; j < totalQuestions; j++)
                        {
                            SelectDropdownByVisibleText(By.XPath(drpDwnQn.Replace("0", j.ToString())), "Yes");
                            EnterKeys(By.XPath(drpDwnQn), Keys.Tab);
                            //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Yes for UN1845 FULLY REGULATED");
                        }
                    }

                }


                else if (section.Text == "CAO HANDLING INFORMATION")
                {
                    string drpDwnQn = "//*[@id='tabs-1']//div[@id='configId']/h2[text()='dgSectionName']/parent::div/following-sibling::div//select";

                    drpDwnQn = drpDwnQn.Replace("dgSectionName", "CAO HANDLING INFORMATION");
                    totalQuestions = GetElementCount(By.XPath(drpDwnQn));
                    drpDwnQn = drpDwnQn + "[@name= 'questionwithAnswer[0].templateAnswer']";
                    if (!IsDropdownSelectedByVisibleText((By.XPath(drpDwnQn)), "Yes"))
                    {
                        for (int j = 0; j < totalQuestions; j++)
                        {
                            SelectDropdownByVisibleText(By.XPath(drpDwnQn.Replace("0", j.ToString())), "Yes");
                            EnterKeys(By.XPath(drpDwnQn), Keys.Tab);
                            //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Yes for CAO HANDLING INFORMATION");
                        }

                    }

                }
                else if (section.Text == "EMPLOYEE SHIPMENT VERIFICATION")
                {
                    string drpDwnQn = "//*[@id='tabs-1']//div[@id='configId']/h2[text()='dgSectionName']/parent::div/following-sibling::div//select";

                    drpDwnQn = drpDwnQn.Replace("dgSectionName", "EMPLOYEE SHIPMENT VERIFICATION");
                    totalQuestions = GetElementCount(By.XPath(drpDwnQn));
                    drpDwnQn = drpDwnQn + "[@name= 'questionwithAnswer[0].templateAnswer']";
                    if (!IsDropdownSelectedByVisibleText((By.XPath(drpDwnQn)), "Yes"))
                    {
                        for (int j = 0; j < totalQuestions; j++)
                        {
                            SelectDropdownByVisibleText(By.XPath(drpDwnQn.Replace("0", j.ToString())), "Yes");
                            EnterKeys(By.XPath(drpDwnQn), Keys.Tab);
                            //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Yes for EMPLOYEE SHIPMENT VERIFICATION");
                        }

                    }
                    EnterText(txtDateOfHire_Xpath, "01-Apr-2020");
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Date of Hire: 01-Apr-2020");
                    EnterText(txtPeoplesoftNumber_Xpath, "5034988");
                    EnterKeys(txtPeoplesoftNumber_Xpath, Keys.Tab);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Peoplesoft Number: 5034988");

                }

                else if (section.Text == "SECURITY SSE")
                {
                    EnterText(txtSecuritySSERemarks_Id, "Yes");
                    EnterKeys(txtSecuritySSERemarks_Id, Keys.Tab);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Remarks for Security SSE: Yes");
                }

                else if (section.Text == "HR Statement")
                {
                    string drpDwnQn = "//*[@id='tabs-1']//div[@id='configId']/h2[text()='dgSectionName']/parent::div/following-sibling::div//select";

                    drpDwnQn = drpDwnQn.Replace("dgSectionName", "HR Statement");
                    totalQuestions = GetElementCount(By.XPath(drpDwnQn));
                    drpDwnQn = drpDwnQn + "[@name= 'questionwithAnswer[0].templateAnswer']";
                    if (!IsDropdownSelectedByVisibleText((By.XPath(drpDwnQn)), "Yes"))
                    {
                        for (int j = 0; j < totalQuestions; j++)
                        {
                            SelectDropdownByVisibleText(By.XPath(drpDwnQn.Replace("0", j.ToString())), "Yes");
                            EnterKeys(By.XPath(drpDwnQn), Keys.Tab);
                            //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Yes for HR Statement");
                        }

                    }
                }


            }
            if (IsElementEnabled(btnOKCaptureChkSheet_Xpath))
            {
                Click(btnOKCaptureChkSheet_Xpath);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on OK button for Capture Checksheet");
            }

            SwitchToDefaultContent();
            Click(btnOKSuccessCheckSheet_Xpath);
            //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on OK button for Success Checksheet");
            SwitchToLTEContentFrame();
        }

        public void EnterCAODGDetails(string chargetyp, string unid, string propershipmntname, string pi, string noofpkg, string netqtyperpkg, string reportable)
        {
            this.chargeType = chargetyp;
            Click(btnSaveShipment_Name);
            //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Save button");
            ClickingYesOnPopupWarnings("");
            dgp.EnterDetailsForCAODGShipment(unid, propershipmntname, pi, noofpkg, netqtyperpkg, reportable);
            SwitchToLTEContentFrame();
        }

        public void SaveCAODGshipment()
        {
            ClickSave();
            ClickingYesOnPopupWarnings("");

        }

        public void ValidateWarningMessage(string expectedWarningMessage)
        {

            WaitForElementToBeVisible(lblWarningMessage_Css, TimeSpan.FromSeconds(10));
            string actualWarningMessage = GetText(lblWarningMessage_Css);
            if (!actualWarningMessage.Contains(expectedWarningMessage))
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Warning message is not as expected. Expected: " + expectedWarningMessage + " Actual: " + actualWarningMessage);
                Log.Error("Warning message is not as expected. Expected: " + expectedWarningMessage + " Actual: " + actualWarningMessage);
                Assert.Fail("Warning message is not as expected. Expected: " + expectedWarningMessage + " Actual: " + actualWarningMessage);

            }
            else
            {
                //Hooks.Hooks.UpdateTest(Status.Pass, "Warning message is as expected: " + actualWarningMessage);
                Log.Info("Warning message is as expected: " + actualWarningMessage);
            }

            awb_num = captureAWBNumber();
            //Hooks.Hooks.UpdateTest(Status.Info, "AWB Number: " + awb_num);
            ClickOnElementIfPresent(btnOrangePencilEditBooking_Css);
            WaitForElementToBeVisible(btnClear_Id, TimeSpan.FromSeconds(5));
            ClickElementUsingActions(btnClear_Id);
            WaitForElementToBeInvisible(lblShipmentDetails_Css, TimeSpan.FromSeconds(10));
            //Hooks.Hooks.UpdateTest(Status.Info, "Clicked on Clear button to refesh the AWB details");
        }

        public void ValidateEmbargoPopupErrorMessage(string expectedErrorMessage)
        {
            Click(btnSaveShipment_Name);
            WaitForElementToBeVisible(lblEmbargoDetails_Xpath, TimeSpan.FromSeconds(5));
            string actualErrorMessage = GetText(lblEmbargoErrorDescription_Xpath);
            if (!actualErrorMessage.Contains(expectedErrorMessage))
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Embargo Popup Error message is not as expected. Expected: " + expectedErrorMessage + " Actual: " + actualErrorMessage);
                Log.Error("Embargo Popup Error message is not as expected. Expected: " + expectedErrorMessage + " Actual: " + actualErrorMessage);
                Assert.Fail("Embargo Popup Error message is not as expected. Expected: " + expectedErrorMessage + " Actual: " + actualErrorMessage);

            }
            else
            {
                //Hooks.Hooks.UpdateTest(Status.Pass, "Embargo Popup Error message is as expected: " + actualErrorMessage);
                Log.Info("Embargo Popup Error message is as expected: " + actualErrorMessage);
            }

            Click(btnCloseEmbargo_Xpath);
            //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Close button for Embargo");
            ClickOnElementIfPresent(btnOrangePencilEditBooking_Css);
            WaitForElementToBeVisible(btnClear_Id, TimeSpan.FromSeconds(5));
            ClickElementUsingActions(btnClear_Id);
            WaitForElementToBeInvisible(lblShipmentDetails_Css, TimeSpan.FromSeconds(10));
            //Hooks.Hooks.UpdateTest(Status.Info, "Clicked on Clear button to refesh the AWB details");

        }

        public string ValidateAWBStatus(string expectedStatus)
        {
            string actualStatus = CaptureDataCaptureStatus();
            if (!actualStatus.Contains(expectedStatus))
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "AWB status is not as expected. Expected: " + expectedStatus + " Actual: " + actualStatus);
                Log.Error("AWB status is not as expected. Expected: " + expectedStatus + " Actual: " + actualStatus);
                Assert.Fail("AWB status is not as expected. Expected: " + expectedStatus + " Actual: " + actualStatus);

            }
            else
            {
                //Hooks.Hooks.UpdateTest(Status.Pass, "AWB status is as expected: " + actualStatus);
                Log.Info("AWB status is as expected: " + actualStatus);
                Log.Info("AWB number is: " + awb_num);
            }
            awb_num = captureAWBNumber();
            //Hooks.Hooks.UpdateTest(Status.Info, "AWB Number: " + awb_num);
            Log.Info("AWB number is: " + awb_num);
            ClickOnElementIfPresent(btnOrangePencilEditBooking_Css);
            WaitForElementToBeVisible(btnClear_Id, TimeSpan.FromSeconds(5));
            ClickElementUsingActions(btnClear_Id);
            WaitForElementToBeInvisible(lblShipmentDetails_Css, TimeSpan.FromSeconds(10));
            //Hooks.Hooks.UpdateTest(Status.Info, "Clicked on Clear button to refesh the AWB details");
            return awb_num;
        }

        public void ValidateCommodityChargeAmount(string totalPaybleAmount)
        {

            if (totalAmountCharged != totalPaybleAmount)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "Total amount charged is not equal to total payable amount. Total Amount Charged: " + totalAmountCharged + " Total Payable Amount: " + totalPaybleAmount);
                Log.Error("Total amount charged is not equal to total payable amount. Total Amount Charged: " + totalAmountCharged + " Total Payable Amount: " + totalPaybleAmount);
                Assert.Fail("Total amount charged is not equal to total payable amount. Total Amount Charged: " + totalAmountCharged + " Total Payable Amount: " + totalPaybleAmount);

            }
            else
            {
                //Hooks.Hooks.UpdateTest(Status.Pass, "Total amount charged is equal to total payable amount. Total Amount Charged: " + totalAmountCharged + " Total Payable Amount: " + totalPaybleAmount);
                Log.Info("Total amount charged is equal to total payable amount. Total Amount Charged: " + totalAmountCharged + " Total Payable Amount: " + totalPaybleAmount);
            }


        }

        public void ValidateRatedCustomerInPaymentPortal(string expectedCIDNum)
        {
            if (accountInfoCIDNum != expectedCIDNum)
            {
                //Hooks.Hooks.UpdateTest(Status.Fail, "CID number in Payment Portal is not as expected. Expected: " + expectedCIDNum + " Actual: " + accountInfoCIDNum);
                Log.Error("CID number in Payment Portal is not as expected. Expected: " + expectedCIDNum + " Actual: " + accountInfoCIDNum);
                Assert.Fail("CID number in Payment Portal is not as expected. Expected: " + expectedCIDNum + " Actual: " + accountInfoCIDNum);

            }
            else
            {
                //Hooks.Hooks.UpdateTest(Status.Pass, "CID number in Payment Portal is as expected: " + accountInfoCIDNum);
                Log.Info("CID number in Payment Portal is as expected: " + accountInfoCIDNum);
            }

        }

        public void CloseLTE001Screen()
        {
            while (!IsElementDisplayed(btnCloseShipment_Name))
            {
                try
                {
                    Click(btnCloseShipment_Name);
                    //Hooks.Hooks.UpdateTest(Status.Pass, "Clicked on Close button for LTE001 screen");
                    WaitForElementToBeInvisible(btnCloseShipment_Name, TimeSpan.FromSeconds(2));
                }
                catch (Exception)
                {
                    ClickingYesOnPopupWarnings("");
                    //Hooks.Hooks.UpdateTest(Status.Fail, "Error in closing LTE001 screen, retrying..");
                    Log.Error("Error in closing LTE001 screen, retrying..");
                }

            }


        }

        // Export Manifest Calling Functions //

        public void EnterFlightDateExportManifest()
        {
            try
            {
                emp.EnterFlightDate(shippingDatePST);
            }
            catch (Exception e)
            {
                Log.Error("Error in entering flight date in export manifest: " + e.ToString());
            }

        }

        public void EnterFlightinExportManifest(string fltnum)
        {
            try
            {
                if (fltnum.Equals(""))
                {
                    emp.EnterFlightNumber(flightNum);
                }
                else
                {
                    emp.EnterFlightNumber(fltnum);
                }

            }
            catch (Exception e)
            {
                Log.Error("Error in entering flight details in export manifest: " + e.ToString());
            }

        }

        public string CreateNewULDCartExportManifest(string carttype, string pou)
        {
            if (pou.Equals(""))
            {
                cartULDNum = emp.CreateULDOrCart(carttype, destination);
            }
            else
            {
                cartULDNum = emp.CreateULDOrCart(carttype, pou);
            }

            return cartULDNum;
        }

        public void CreateNewULDCartTypingAWBExportManifest(string carttype, string pou, string assignPieces)
        {
            awb_num = awb_num.Split("-")[1];
            emp.CreateNewULDCartByTypingAWB(carttype, pou, awb_num, assignPieces);
        }

        public void FilterOutAWBULDInExportManifest(string awbSectionName)
        {
            awb_num = awb_num.Split("-")[1];
            if (awbSectionName.Equals("PlannedShipment"))
            {
                emp.FilterOutPlannedAWBAndULD(awb_num, cartULDNum);
            }
            else if (awbSectionName.Equals("LyingList"))
            {
                emp.FilterOutLyingListAWBAndULD(cartULDNum, awb_num);
            }

        }

        public void FilterSplitAndAssignAWBToULDExportManifest(string awbSectionName, string splitPieces)
        {
            if (awbSectionName.Equals("PlannedShipment"))
            {
                awb_num = awb_num.Split("-")[1];
                emp.FilterOutPlannedAWBSplitAndAssign(awb_num, cartULDNum, splitPieces);
            }


        }

        public void AssignAWBToPreBuiltCartByAWBTypingExportManifest(string piecesToAssign)
        {
            awb_num = awb_num.Split("-")[1];
            emp.AssignAWBToPreBuiltCartByAWBTyping(awb_num, piecesToAssign);
        }

        public void EnterFlightinFogsQA()
        {
            try
            {
                fogsQAPage.EnterFlightNumber(flightNum);
            }
            catch (Exception e)
            {
                Log.Error("Error in entering flight details in export manifest: " + e.ToString());
            }

        }

        public void WhenUserEntersTheScreeningDetailsForJustSinglePieceAsWithScreeingMethodAsAndScreeningResultAs(string piece, string method, string result)
        {
            try
            {
                EnterText(txtScreeningAirport1_Xpath, origin);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Screening Airport: " + origin);
                SelectDropdownByVisibleText(drpdwnScreeningMethod1_Xpath, method);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Screening Method: " + method);
                EnterText(txtScreeningPieces1_Xpath, piece);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Entered Pieces: " + piece);
                SelectDropdownByVisibleText(drpdwnScreeningResult1_Xpath, result);
                //Hooks.Hooks.UpdateTest(Status.Pass, "Selected Screening Result: " + result);

                Console.WriteLine("Successfully entered screening details.");
            }
            catch (Exception ex)
            {

               // Hooks.Hooks.UpdateTest(Status.Fail, "Failed to enter screening details: " + ex.Message);
                Console.WriteLine("Error while entering screening details: " + ex.Message);
                throw;
            }
        }

        public void ValidateHandlingCodeinFogsQA(string handlingCode)
        {
            try
            {
                fogsQAPage.ValidateHandlingCodeForAWB(awb_num, handlingCode);
            }
            catch (Exception e)
            {
                Log.Error("Error in Validating Handling Code in Fogs QA: " + e.ToString());
            }

        }


    }
}
