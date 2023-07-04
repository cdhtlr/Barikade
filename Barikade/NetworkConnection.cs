using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Barikade
{
    /// <summary>
    /// The network connection class.
    /// </summary>
    internal static class NetworkConnection
    {
        /// <summary>
        /// Function to drops the network connection if file is exist.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        internal static void Drop(string filePath)
        {
            DateTime startTime = DateTime.Now;
            TimeSpan elapsedTime;

            IntPtr handle = WinDivert.WinDivertOpen("true", 0, 0, 0);

            if (handle == IntPtr.Zero)
                return;

            try
            {
                // Block if process path exist
                while (File.Exists(filePath))
                {
                    uint recvLen = 0;
                    IntPtr recvPacket = Marshal.AllocHGlobal(65535); // Allocate memory for the received packet

                    if (!WinDivert.WinDivertRecv(handle, recvPacket, 65535, ref recvLen, IntPtr.Zero, ref recvPacket))
                        break;

                    // Drop the received packet
                    // You can add custom logic here to filter and drop specific packets
                    // For example, you can examine the packet contents, source/destination IP addresses, etc.
                    // and decide whether to drop it or not

                    Marshal.FreeHGlobal(recvPacket);
                }

                // Additional blocking after process path deleted and previous blocking took < 5 seconds
                while (!File.Exists(filePath))
                {
                    // Check if the specified duration has elapsed
                    elapsedTime = DateTime.Now - startTime;
                    if (elapsedTime >= TimeSpan.FromSeconds(5))
                        break;

                    uint recvLen = 0;
                    IntPtr recvPacket = Marshal.AllocHGlobal(65535);

                    if (!WinDivert.WinDivertRecv(handle, recvPacket, 65535, ref recvLen, IntPtr.Zero, ref recvPacket))
                        break;

                    Marshal.FreeHGlobal(recvPacket);
                }
            }
            finally
            {
                WinDivert.WinDivertClose(handle);
            }
        }
    }
}
