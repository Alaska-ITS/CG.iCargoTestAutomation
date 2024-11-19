using Azure.Storage.Blobs.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCargoUIAutomation.pages
{
    public class MarkFlightMovements : BasePage
    {
        private CreateShipmentPage csp;
        private ExportManifestPage emp;
        private PageObjectManager pageObjectManager;
        public static string CurrentDatePST = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")).ToString("dd-MMM-yyyy");
        public static string CurrentTimePST = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")).ToString("HH:mm");
        public static string CurrentDateAKST = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Alaskan Standard Time")).ToString("dd-MMM-yyyy");
        public static string CurrentTimeAKST = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Alaskan Standard Time")).ToString("HH:mm");
        public MarkFlightMovements(IWebDriver driver) : base(driver)
        {
            pageObjectManager = new PageObjectManager(driver);
            emp = pageObjectManager.GetExportManifestPage();
            csp = pageObjectManager.GetCreateShipmentPage();
        }

        //Mark Flight Movement FLT006
        private By markFlightMovementFLT006Frame_Xpath = By.XPath("//iframe[@name='iCargoContentFrameFLT006']");
        private By txtflightNumber_id = By.Id("flightNumber_flightNumber");
        private By txtflightDate_id = By.Id("flightDate");
        private By btnlist_ID = By.Id("CMP_FLIGHT_OPERATION_MARKFLIGHTMOVEMENTS_DISPLAY_BUTTON");

        //Flight Movement Details
        private By txtActualDepartureDate_Xpath = By.XPath("//table[@id='listMovementTable']//input[@id='actualDateDeparture0']");
        private By txtActualDepartureTime_Xpath = By.XPath("//table[@id='listMovementTable']//input[@id='actualTimeDeparture0']"); 
        private By txtActualArrivalDate_Xpath = By.XPath("//table[@id='listMovementTable']//input[@id='actualDateArrival1']"); 
        private By txtActualArrivalTime_Xpath = By.XPath("//table[@id='listMovementTable']//input[@id='actualTimeArrival1']"); 

        //FLT006 footer buttons
        private By btnSave_ID = By.Id("CMP_FLIGHT_OPERATION_MARKFLIGHTMOVEMENTS_SAVE_BUTTON");
        private By btnClose_Id = By.Id("CMP_FLIGHT_OPERATION_MARKFLIGHTMOVEMENTS_CLOSE_BUTTON");    

        public void SwitchToFLT006Frame()
        {
            SwitchToFrame(markFlightMovementFLT006Frame_Xpath);
        }


        public void EnterFlightDetails()
        {
            Click(txtflightNumber_id);
            EnterText(txtflightNumber_id, CreateShipmentPage.flightNum);           
            EnterKeys(txtflightNumber_id, Keys.Tab);
            EnterText(txtflightDate_id, CreateShipmentPage.shippingDatePST);
            EnterKeys(txtflightDate_id, Keys.Tab);
        }

        public void ClickListButton()
        {
            DoubleClick(btnlist_ID);
            WaitForElementToBeVisible(txtActualDepartureDate_Xpath, TimeSpan.FromSeconds(3));
        }

        public void EnterActualArrivalDepartureDetails(string movementDirection, double adjustedTime=0)
        {
            // if the origin stations is SAN,SFO,LAX then use PST timezone, else use AKST timezone using dictionary

            var timeZoneMap = new Dictionary<string, (string Date, string Time)>
               {
                   { "SAN", (CurrentDatePST, CurrentTimePST) },
                   { "SFO", (CurrentDatePST, CurrentTimePST) },
                   { "LAX", (CurrentDatePST, CurrentTimePST) }
               };


            if (timeZoneMap.ContainsKey(CreateShipmentPage.origin))           
            {
                var timeZone = timeZoneMap[CreateShipmentPage.origin];                

                if (movementDirection.ToLower() == "departure")
                {
                    EnterText(txtActualDepartureDate_Xpath, timeZone.Date);
                    EnterKeys(txtActualDepartureDate_Xpath, Keys.Tab);                   
                    EnterText(txtActualDepartureTime_Xpath, DateTime.Parse(timeZone.Time).AddMinutes(0).ToString("HH:mm"));                    
                    EnterKeys(txtActualDepartureTime_Xpath, Keys.Tab);
                }
                else
                {
                    EnterText(txtActualArrivalDate_Xpath, TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")).ToString("dd-MMM-yyyy"));
                    EnterKeys(txtActualArrivalDate_Xpath, Keys.Tab);
                    EnterText(txtActualArrivalTime_Xpath, TimeZoneInfo.ConvertTime(DateTime.Now.AddMinutes(adjustedTime), TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")).ToString("HH:mm"));
                    EnterKeys(txtActualArrivalTime_Xpath, Keys.Tab);
                }
                

            }
            else
            {
                if (movementDirection.ToLower() == "departure")
                {
                    // Use AKST time zone for all other origins
                    EnterText(txtActualDepartureDate_Xpath, CurrentDateAKST);
                    EnterKeys(txtActualDepartureDate_Xpath, Keys.Tab);                    
                    EnterText(txtActualDepartureTime_Xpath, DateTime.Parse(CurrentTimeAKST).AddMinutes(0).ToString("HH:mm"));
                    //EnterText(txtActualDepartureTime_Xpath, CurrentTimeAKST);
                    EnterKeys(txtActualDepartureTime_Xpath, Keys.Tab);
                }
                else
                {
                    EnterText(txtActualArrivalDate_Xpath, TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Alaskan Standard Time")).ToString("dd-MMM-yyyy"));
                    EnterKeys(txtActualArrivalDate_Xpath, Keys.Tab);
                    EnterText(txtActualArrivalTime_Xpath, TimeZoneInfo.ConvertTime(DateTime.Now.AddMinutes(adjustedTime), TimeZoneInfo.FindSystemTimeZoneById("Alaskan Standard Time")).ToString("HH:mm"));
                    EnterKeys(txtActualArrivalTime_Xpath, Keys.Tab);
                }
                    

            }

        } 

        public void ClickSaveButton()
        {
            Click(btnSave_ID);
            WaitForElementToBeInvisible(txtActualDepartureDate_Xpath, TimeSpan.FromSeconds(3));
        }
        public void ClickCloseButton()
        {
            Click(btnClose_Id);
            WaitForElementToBeInvisible(btnClose_Id, TimeSpan.FromSeconds(3));
            SwitchToDefaultContent();
        }

    }
}
