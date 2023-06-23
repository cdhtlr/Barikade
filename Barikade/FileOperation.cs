using System;
using System.IO;
using System.Security.AccessControl;
using System.Threading;

namespace Barikade
{
    /// <summary>
    /// The file operation class.
    /// </summary>
    internal static class FileOperation
    {
        /// <summary>
        /// Function to delete file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        internal static void Delete(string filePath)
        {
            while (File.Exists(filePath))
            {
                try
                {
                    // Remove the write and delete permissions from the file
                    FileSecurity fileSecurity = File.GetAccessControl(filePath);
                    fileSecurity.AddAccessRule(new FileSystemAccessRule(Environment.UserName,
                        FileSystemRights.Delete | FileSystemRights.Write, AccessControlType.Deny));
                    File.SetAccessControl(filePath, fileSecurity);

                    // Delete the file without confirmation
                    File.Delete(filePath);
                }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
