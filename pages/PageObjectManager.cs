using iCargoUIAutomation.pages;
using OpenQA.Selenium;

public class PageObjectManager: BasePage
{

    private IWebDriver driver;
    private homePage hp;
    private createShipmentPage csp;
   
    // Add other page classes as needed

    public PageObjectManager(IWebDriver driver): base(driver)
    {
        this.driver = driver;
    }

    public homePage GetHomePage()
    {
        return hp ?? (hp = new homePage(driver));
    }

    public createShipmentPage GetCreateShipmentPage()
    {
        return csp ?? (csp = new createShipmentPage(driver));
    }

  

    // Add other getter methods for other page classes as needed
}
