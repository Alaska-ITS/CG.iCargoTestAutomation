using iCargoUIAutomation.pages;
using OpenQA.Selenium;

public class PageObjectManager : BasePage
{

    private IWebDriver driver;
    private homePage hp;
    private CreateShipmentPage csp;
    private MaintainBookingPage mbp;
    private ExportManifestPage emp;
    private PaymentPortalPage ppp;
    private DangerousGoodsPage dgp;
    private CaptureIrregularityPage cip;
    private FogsQAPage fogsQAPage;
    private ScreeningPage sp;
    private MarkFlightMovements mfm;
    private ImportManifestPage imp;

    // Add other page classes as needed

    public PageObjectManager(IWebDriver driver) : base(driver)
    {
        this.driver = driver;
    }

    public homePage GetHomePage()
    {
        return hp ?? (hp = new homePage(driver));
    }

    public CreateShipmentPage GetCreateShipmentPage()
    {
        return csp ?? (csp = new CreateShipmentPage(driver));
    }

    public MaintainBookingPage GetMaintainBookingPage()
    {
        return mbp ?? (mbp = new MaintainBookingPage(driver));
    }

    public ExportManifestPage GetExportManifestPage()
    {
        return emp ?? (emp = new ExportManifestPage(driver));
    }

    public PaymentPortalPage GetPaymentPortalPage()
    {
        return ppp ?? (ppp = new PaymentPortalPage(driver));
    }

    public DangerousGoodsPage GetDangerousGoodsPage()
    {
        return dgp ?? (dgp = new DangerousGoodsPage(driver));
    }

    public CaptureIrregularityPage GetCaptureIrregularityPage()
    {
        return cip ?? (cip = new CaptureIrregularityPage(driver));
    }

    public FogsQAPage GetFogsQAPage()
    {
        return fogsQAPage ?? (fogsQAPage = new FogsQAPage(driver));
    }

    public ScreeningPage GetScreeningPage()
    {
        return sp ?? (sp = new ScreeningPage(driver));
    }
    // Add other getter methods for other page classes as needed

    public MarkFlightMovements GetMarkFlightMovements()
    {
        return mfm ?? (mfm = new MarkFlightMovements(driver));
    }

    public ImportManifestPage GetImportManifestPage()
    {
        return imp ?? (imp = new ImportManifestPage(driver));
    }
}
