using System;
using System.IO;
using System.ServiceProcess;

namespace Barikade
{
    /// <summary>
    /// The program class.
    /// </summary>
    internal static class Program
    {
        internal static string[] monitoredPaths;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            LoadMonitoredPaths();
            ServiceBase.Run(new ServiceBase[] { new Service() });
        }

        /// <summary>
        /// Loads the monitored paths.
        /// </summary>
        private static void LoadMonitoredPaths()
        {
            string pathConfigFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @".\AllowedPaths.ini");
            if (!File.Exists(pathConfigFile))
                // Create a new path.ini file with default directories
                File.WriteAllLines(pathConfigFile, new string[] { "+C:\\Program Files\\", "+C:\\Program Files (x86)\\", "+C:\\Windows\\" });

            monitoredPaths = File.ReadAllLines(pathConfigFile);
        }
    }
}
