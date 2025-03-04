using iCargoXunit.Fixtures;
using iCargoXunit.pages;
using iCargoXunit.utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCargoXunit.Tests.CAP018
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
        public void CAP018_BKG_00010_Create_a_Booking_From_A_Saved_Template()
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
    }    
}
