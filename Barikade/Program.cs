using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceProcess;

namespace Barikade
{
    /// <summary>
    /// The program class.
    /// </summary>
    internal static class Program
    {
        internal static string[] allowedPaths;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            LoadAllowedPaths();
            ServiceBase.Run(new ServiceBase[] { new Service() });
        }

        /// <summary>
        /// Function to load the allowed paths.
        /// </summary>
        private static void LoadAllowedPaths()
        {
            // Initialize the allowedPaths array with current directory of Barikade (non-recursive)
            // This will automatically allows the Barikade program and other files that are in the same folder as it to run.
            List<string> paths = new List<string> { "-"+Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory) };

            // Create a new path.ini file with default directories if AllowedPaths.ini not exists
            string pathConfigFile = Path.Combine(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory), "AllowedPaths.ini");
            if (!File.Exists(pathConfigFile))
                File.WriteAllLines(pathConfigFile, new string[] { "+C:\\ProgramData\\Microsoft\\Windows Defender\\Platform\\", "+C:\\Program Files\\", "+C:\\Program Files (x86)\\", "+C:\\Windows\\" });

            paths.AddRange(File.ReadAllLines(pathConfigFile));

            allowedPaths = paths.ToArray();
        }

        /// <summary>
        /// Function to write message to log file if Log.txt exists.
        /// </summary>
        /// <param name="message">The message.</param>
        internal static void Log(string message)
        {
            string logFile = Path.Combine(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory), "Log.txt");

            if (File.Exists(logFile))
                File.AppendAllLines(logFile, new string[] { message });
        }
    }
}
