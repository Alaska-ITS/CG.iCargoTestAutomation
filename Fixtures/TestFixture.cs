using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using System;
using System.IO;
using System.Threading.Tasks;
using iCargoXunit.pages;
using AventStack.ExtentReports;
using iCargoXunit.utilities;

namespace iCargoXunit.Fixtures
{
    public class TestFixture : IAsyncLifetime
    {
        public static ExtentReports? extent;
        public static string? reportPath;
        public static string? testResultPath;
        private const string testDataContainerName = "testdata";
        private static AzureStorage? azureStorage;

        public IWebDriver Driver { get; private set; }
        private readonly homePage hp;
        private readonly BasePage bp;

        public TestFixture()
        {
            //SetupReport();
            azureStorage = new AzureStorage(testDataContainerName);
            DownloadTestDataFromAzure().Wait();            
            InitializeWebDriver();
            SetupICargo();  
        }

        private async Task DownloadTestDataFromAzure()
        {
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

            // Fetch the list of Excel files from the blob
            azureStorage = new AzureStorage(testDataContainerName);
            var excelFiles = azureStorage.GetBlobFileNames()
                            .Where(fileName => fileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                            .ToList();

            // Download new files from the blob
            foreach (var fileName in excelFiles)
            {
                try
                {
                    string localFilePath = Path.Combine(testDataFolderPath, fileName);
                    var downloadedFilePath = azureStorage.DownloadFileFromBlob(fileName, localFilePath);
                    Console.WriteLine($"Downloaded test data file: {fileName} to {downloadedFilePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to download file {fileName}. Error: {ex.Message}");
                }
            }

            Console.WriteLine("File download completed.");

        }
        private void InitializeWebDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--incognito");

            Driver = new ChromeDriver(options);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Driver.Manage().Window.Maximize();
        }

        private void SetupICargo()
        {
           homePage hp = new homePage(Driver);
          BasePage  bp = new BasePage(Driver);

            bp.DeleteAllCookies();
            bp.Open("https://asstg-icargo.ibsplc.aero/icargo/login.do");
            Driver.FindElement(By.XPath("//a[@id='social-oidc']")).Click();
            bp.RefreshPage();

            if (bp.IsElementDisplayed(By.XPath("//body[@class='login']")))
            {
                hp.LoginICargo();
            }
            bp.SwitchToNewWindow();
        }

        public Task InitializeAsync() => Task.CompletedTask;

        public async Task DisposeAsync()
        {
            extent?.Flush();

            if (!string.IsNullOrEmpty(reportPath) && Directory.Exists(reportPath))
            {
                Directory.Delete(reportPath, true);
            }

            Console.WriteLine("Test execution completed. Reports uploaded.");

            // Logout before quitting WebDriver
            try
            {
                hp.logoutiCargo();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logout failed: {ex.Message}");
            }

            Driver?.Quit();
        }
    }

    [CollectionDefinition("Test Collection")]
    public class TestCollection : ICollectionFixture<TestFixture> { }
}
