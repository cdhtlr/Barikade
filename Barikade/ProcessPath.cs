using System;
using System.IO;
using System.Linq;
using System.Management;

namespace Barikade
{
    /// <summary>
    /// The process path class.
    /// </summary>
    internal static class ProcessPath
    {
        /// <summary>
        /// Function to get the process path.
        /// </summary>
        /// <param name="processId">The process id.</param>
        /// <returns>A string.</returns>
        internal static string Get(int processId)
        {
            using (var searcher = new ManagementObjectSearcher($"SELECT ExecutablePath FROM Win32_Process WHERE ProcessId = {processId}"))
            {
                var obj = searcher.Get().OfType<ManagementObject>().FirstOrDefault();
                return obj?["ExecutablePath"]?.ToString() ?? string.Empty;
            }
        }

        /// <summary>
        /// Function to determine the path is allowed path.
        /// </summary>
        /// <param name="processPath">The process path.</param>
        /// <returns>A bool.</returns>
        internal static bool IsAllowedPath(string processPath)
        {
            return Program.allowedPaths.Any(allowedPath =>
            {
                string normalAllowedPath = allowedPath.Substring(1);
                return (allowedPath.StartsWith("+") && processPath.StartsWith(normalAllowedPath, StringComparison.OrdinalIgnoreCase)) ||
                    (allowedPath.StartsWith("-") && processPath.Equals(normalAllowedPath + Path.GetFileName(processPath), StringComparison.OrdinalIgnoreCase));
            });
        }
    }
}
