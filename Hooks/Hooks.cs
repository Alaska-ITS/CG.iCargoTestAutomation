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
        public static string? scenarioName;
        public static string? browser;
        public static string? appUrl = "https://asstg-icargo.ibsplc.aero/icargo/login.do";
        private const string containerName = "resources";
        private const string reportContainerName = "reports";
        private const string logContainerName = "logs";
        private const string testDataContainername = "testdata";
        private static AzureStorage? azureStorage;
        public static List<string> uploadedBlobPaths = new List<string>();        

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
            string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string testDataFolderPath = Path.Combine(solutionDirectory, "TestData");

            // Ensure the TestData folder exists, or create it
            if (!Directory.Exists(testDataFolderPath))
            {
                Directory.CreateDirectory(testDataFolderPath);
            }

            // Delete all files in the TestData folder
            Console.WriteLine($"Clearing existing files in folder: {testDataFolderPath}");
            var existingFiles = Directory.GetFiles(testDataFolderPath);
            foreach (var file in existingFiles)
            {
                try
                {
                    File.Delete(file);
                    Console.WriteLine($"Deleted file: {file}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to delete file {file}. Error: {ex.Message}");
                }
            }

            // Get the system's temp folder
            string tempFolderPath = Path.GetTempPath();

            // Fetch the list of Excel files from the blob
            var azureStorage = new AzureStorage(testDataContainername);
            var excelFiles = azureStorage.GetBlobFileNames()
                            .Where(fileName => fileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                            .ToList();

            // Download new files to the temp folder
            foreach (var fileName in excelFiles)
            {
                try
                {
                    string tempFilePath = Path.Combine(tempFolderPath, fileName);
                    var downloadedFilePath = azureStorage.DownloadFileFromBlob(fileName, tempFilePath);
                    Console.WriteLine($"Downloaded test data file: {fileName} to {downloadedFilePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to download file {fileName}. Error: {ex.Message}");
                }
            }

            // Move files from the temp folder to the TestData folder
            foreach (var fileName in excelFiles)
            {
                try
                {
                    string tempFilePath = Path.Combine(tempFolderPath, fileName);
                    string destinationFilePath = Path.Combine(testDataFolderPath, fileName);

                    if (File.Exists(tempFilePath))
                    {
                        File.Move(tempFilePath, destinationFilePath);
                        Console.WriteLine($"Moved file: {fileName} to {destinationFilePath}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to move file {fileName}. Error: {ex.Message}");
                }
            }

            // Clean up the temp folder by deleting the files
            foreach (var fileName in excelFiles)
            {
                try
                {
                    string tempFilePath = Path.Combine(tempFolderPath, fileName);
                    if (File.Exists(tempFilePath))
                    {
                        File.Delete(tempFilePath);
                        Console.WriteLine($"Deleted temp file: {tempFilePath}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to delete temp file {fileName}. Error: {ex.Message}");
                }
            }

            Console.WriteLine("File processing completed.");


        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("Running after test run...");
           SystemResourceInfo systemResourceInfo = new SystemResourceInfo();
            systemResourceInfo.GetSystemResourceUsage();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            
            //browser = Environment.GetEnvironmentVariable("Browser", EnvironmentVariableTarget.Process);            
            browser = "chrome";
          
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--incognito");

            EdgeOptions edgeOptions = new EdgeOptions();
            edgeOptions.AddArgument("InPrivate");

            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.AddArgument("-private");

            SafariOptions safariOptions = new SafariOptions();
            safariOptions.AddAdditionalOption("InPrivate", true);

            if (browser.Equals("chrome", StringComparison.OrdinalIgnoreCase))
            {
                driver = new ChromeDriver(options);
            }
            else if (browser.Equals("edge", StringComparison.OrdinalIgnoreCase))
            {
                driver = new EdgeDriver(edgeOptions);
            }
            else if (browser.Equals("firefox", StringComparison.OrdinalIgnoreCase))
            {
                driver = new FirefoxDriver(firefoxOptions);
            }
            else if (browser.Equals("safari", StringComparison.OrdinalIgnoreCase))
            {
                driver = new SafariDriver(safariOptions);
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
            foreach (string blobPath in uploadedBlobPaths)
            {                
               TestContext.WriteLine($"Blob file path: {blobPath}");
            }
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
            scenario = extent.CreateTest(scenarioContext.ScenarioInfo.Title);
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
            scenarioName = ScenarioContext.Current.ScenarioInfo.Title;
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
                //extent.Flush();
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

