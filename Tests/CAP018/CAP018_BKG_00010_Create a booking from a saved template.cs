using iCargoUIAutomation.Fixtures;
using iCargoUIAutomation.pages;
using iCargoUIAutomation.utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCargoUIAutomation.Tests.CAP018
{
    public class CAP018_BKG_00010_Create_a_booking_from_a_saved_template : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly PageObjectManager pageObjectManager;
        private readonly homePage hp;
        private readonly MaintainBookingPage mbp;
        
        public CAP018_BKG_00010_Create_a_booking_from_a_saved_template(TestFixture fixture)
        {
            driver = fixture.Driver;
            pageObjectManager = new PageObjectManager(driver);
            hp = pageObjectManager.GetHomePage();
            mbp = pageObjectManager.GetMaintainBookingPage();
        }
        [Fact]
        [Trait("Category", "CAP018")]
        [Trait("Category", "CAP018_BKG_00010")]
        public void CAP018_BKG_00010_Create_a_Booking_From_A_Saved_Template()
        {
            try
            {
                //Navigate to CAP018 Maintain Booking Page
                hp.enterScreenName("CAP018");
                mbp.SwitchToCAP018Frame();
                //Clicking on list button in Maintain Booking screen
                mbp.ClickNewListButton();

                //Select the saved template
                mbp.SelectTemplate();

                //Clicking on select button
                mbp.ClickSaveButton();
                mbp.CaptureAwbNumber();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test Failed: {ex.Message}");
            }
        }
    }    
}
