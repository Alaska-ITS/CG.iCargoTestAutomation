using iCargoUIAutomation.pages;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCargoUIAutomation.utilities
{
    public class SystemResourceInfo 
    {
        ILog Log = LogManager.GetLogger(typeof(SystemResourceInfo));
        public void GetMemoryUsage()
    {
        try
        {
            var ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            float availableMemory = ramCounter.NextValue();
            Console.WriteLine($"Available Memory (MB): {availableMemory}");
            Log.Info($"Available Memory (MB): {availableMemory}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving memory usage: {ex.Message}");
        }
    }

    // Method to get disk storage details
    public void GetDiskUsage()
    {
        try
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    Console.WriteLine($"Drive {drive.Name}");
                    Log.Info($"Drive {drive.Name}");
                    Console.WriteLine($"  Total Size: {drive.TotalSize / (1024 * 1024 * 1024)} GB");
                    Log.Info($"  Total Size: {drive.TotalSize / (1024 * 1024 * 1024)} GB");
                    Console.WriteLine($"  Available Space: {drive.AvailableFreeSpace / (1024 * 1024 * 1024)} GB");
                    Log.Info($"  Available Space: {drive.AvailableFreeSpace / (1024 * 1024 * 1024)} GB");
                    Console.WriteLine($"  Drive Type: {drive.DriveType}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving disk usage: {ex.Message}");
        }
    }

    // Method to display both memory and disk usage
    public void GetSystemResourceUsage()
    {
        Console.WriteLine("Fetching System Resources...");

        // Fetch memory usage
        GetMemoryUsage();

        // Fetch disk usage
        GetDiskUsage();
    }
}
}
