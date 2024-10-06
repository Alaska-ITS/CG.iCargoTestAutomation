using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using iCargoUIAutomation.pages;
using iCargoUIAutomation.utilities;
using NUnit.Framework.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Firefox;
using System.Net.Mail;
using System.Configuration;


namespace iCargoUIAutomation.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private readonly IObjectContainer _container;
        public static ExtentReports? extent;
        public static ExtentTest? feature;
        public static ExtentTest? scenario;
        public static ExtentTest? step;
        public static string? testResultPath;
        public static string? reportPath;
        private static IWebDriver? driver;
        public static string? featureName;
        public static string? browser;
        public static string? appUrl = "https://asstg-icargo.ibsplc.aero/icargo/login.do";
        private const string containerName = "resources";
        private const string reportContainerName = "reports";
        private static AzureStorage? azureStorage;

        public Hooks(IObjectContainer container)
        {
            _container = container;

        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {

            // Set the report path (temporary local path, will be uploaded to Azure)           
            TimeZoneInfo pstZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            DateTime pstTime = TimeZoneInfo.ConvertTime(DateTime.Now, pstZone);
            string reportName = "TestResults_" + pstTime.ToString("yyyyMMdd_HHmmss");
            reportPath = Path.Combine(Path.GetTempPath(), reportName);
            testResultPath = reportPath + @"\index.html";

            var htmlReporter = new ExtentHtmlReporter(testResultPath);
            htmlReporter.Config.ReportName = "Automation Status Report";
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("Running after test run...");
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            feature = extent.CreateTest(featureContext.FeatureInfo.Title);
            feature.Log(Status.Info, featureContext.FeatureInfo.Description);

            browser = Environment.GetEnvironmentVariable("Browser", EnvironmentVariableTarget.Process); 
            //browser="chrome"; // Default browser is "chrome
           
            if (browser.Equals("chrome", StringComparison.OrdinalIgnoreCase))
            {
                driver = new ChromeDriver();
            }
            else if (browser.Equals("edge", StringComparison.OrdinalIgnoreCase))
            {
                driver = new EdgeDriver();
            }
            else if (browser.Equals("firefox", StringComparison.OrdinalIgnoreCase))
            {
                driver = new FirefoxDriver();
            }
            else if (browser.Equals("safari", StringComparison.OrdinalIgnoreCase))
            {
                driver = new SafariDriver();
            }
            else
            {
                throw new NotSupportedException($"Browser '{browser}' is not supported");
            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Manage().Window.Maximize();
            homePage hp = new homePage(driver);
            BasePage bp = new BasePage(driver);
            bp.DeleteAllCookies();
            bp.Open(appUrl);
            driver.FindElement(By.XPath("//a[@id='social-oidc']")).Click();
            bp.RefreshPage();
            if (bp.IsElementDisplayed(By.XPath("//body[@class='login']")))
            {
                hp.LoginICargo();
            }
            bp.SwitchToNewWindow();
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            homePage hp = new homePage(driver);
            hp.logoutiCargo();
            extent.Flush();
            azureStorage = new AzureStorage(reportContainerName);
            azureStorage.UploadFolderToAzure(reportPath);
            if (File.Exists(reportPath))
            {
                File.Delete(reportPath);
            }
            driver.Quit();
        }        

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario(ScenarioContext scenarioContext)
        {
            _container.RegisterInstanceAs<IWebDriver>(driver);
            scenario = feature.CreateNode(scenarioContext.ScenarioInfo.Title);
        }

        public static void createNode()
        {
            step = scenario.CreateNode(ScenarioStepContext.Current.StepInfo.Text);
        }

        public static void outOfStep()
        {
            step = scenario;
        }

        public static void UpdateTest(Status status, string stepMessaage)
        {
            step.Log(status, stepMessaage);
        }


        [AfterScenario]

        public void AfterScenario(FeatureContext featureContext)
        {

            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            DateTime time = DateTime.Now;
            featureName = featureContext.FeatureInfo.Title;
            string fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
            if (status == TestStatus.Failed)
            {
                scenario.Fail("Test Failed", captureScreenshot(fileName));
                scenario.Log(Status.Fail, "Test failed with log" + stackTrace);
            }
            if (MaintainBookingPage.awbNumber != "" || CreateShipmentPage.awb_num != "" && ScenarioContext.Current["Execute"] == "true")
            {


                azureStorage = new AzureStorage(containerName);
                string excelFileName = "AWBDetails.csv";
                string tempLocalPath = Path.Combine(Path.GetTempPath(), excelFileName);

                // Download the existing file if it exists
                tempLocalPath = azureStorage.DownloadFileFromBlob(excelFileName, tempLocalPath);
                ExcelFileConfig excelFileConfig = new ExcelFileConfig();

                if (excelFileConfig.IsSheetFilled(tempLocalPath, 1000)) // Set your desired max rows per sheet
                {
                    // If filled, generate a new unique file name
                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    excelFileName = $"AWBDetails_{timestamp}.xlsx";
                    tempLocalPath = Path.Combine(Path.GetTempPath(), excelFileName);
                }

                if (featureName.Contains("CAP018"))
                {
                    
                    // Append data to the downloaded or newly created Excel file
                    excelFileConfig.AppendDataToExcel(tempLocalPath, DateTime.Now.ToString("dd-MM-yyyy"), DateTime.Now.ToString("HH:mm:ss"), "CAP018", featureName, MaintainBookingPage.awbNumber, MaintainBookingPage.globalOrigin, MaintainBookingPage.globalDestination, MaintainBookingPage.globalProductCode, MaintainBookingPage.globalAgentCode, MaintainBookingPage.globalShipperCode, MaintainBookingPage.globalConsigneeCode, MaintainBookingPage.globalCommodityCode, MaintainBookingPage.globalPieces, MaintainBookingPage.globalWeight);
                    
                }
                else 
                {
                    
                    // Append data to the downloaded or newly created Excel file
                    excelFileConfig.AppendDataToExcel(tempLocalPath, DateTime.Now.ToString("dd-MM-yyyy"), DateTime.Now.ToString("HH:mm:ss"), "LTE001", featureName, CreateShipmentPage.awb_num, CreateShipmentPage.origin, CreateShipmentPage.destination, CreateShipmentPage.productCode, CreateShipmentPage.agentCode, CreateShipmentPage.shipperCode, CreateShipmentPage.consigneeCode, CreateShipmentPage.commodityCode, CreateShipmentPage.pieces, CreateShipmentPage.weight);

                }                

                // Upload the updated file back to Azure Blob Storage
                azureStorage.UploadFileToBlob(tempLocalPath, excelFileName);

                File.Delete(tempLocalPath);
            }

        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            string stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepName = scenarioContext.StepContext.StepInfo.Text;
        }

        public static MediaEntityModelProvider captureScreenshot(string fileName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            string screenshotPath = reportPath;
            string screenshotLocation = Path.Combine(screenshotPath, fileName);
            screenshot.SaveAsFile(screenshotLocation);
            return MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotLocation).Build();

        }
    }
}

