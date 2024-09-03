using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using log4net;
using System;
using System.IO;

namespace iCargoUIAutomation.utilities
{
    public class LogFileConfiguration
    {
        public void ConfigureLog4Net()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            patternLayout.ActivateOptions();

            // Log file path
            string logDirectory = @"\\seavvfile1\projectmgmt_pmo\iCargoAutomationReports\Logs";
            string logFileName = "Log_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".log";
            string logFilePath = Path.Combine(logDirectory, logFileName);

            // Ensure the directory exists
            if (!Directory.Exists(logDirectory))
            {
                try
                {
                    Directory.CreateDirectory(logDirectory);
                }
                catch (Exception)
                {
                    // Fallback to the project directory's resource folder if unable to create the specified path
                    string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    logDirectory = Path.Combine(projectDirectory, "Resource", "Logs");
                    logFilePath = Path.Combine(logDirectory, logFileName);

                    Directory.CreateDirectory(logDirectory);
                }
            }

            // Configure the FileAppender
            FileAppender appender = new FileAppender
            {
                AppendToFile = true,
                File = logFilePath,
                Layout = patternLayout
            };
            appender.ActivateOptions();
            hierarchy.Root.AddAppender(appender);

            hierarchy.Root.Level = log4net.Core.Level.All;
            hierarchy.Configured = true;

            Console.WriteLine("Log file path: " + logFilePath);
        }
    }
}
