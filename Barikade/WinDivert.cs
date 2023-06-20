using System;
using System.Runtime.InteropServices;

namespace Barikade
{
    /// <summary>
    /// The WinDivert class to import the WinDivert API functions.
    /// </summary>
    internal static class WinDivert
    {
        /// <summary>
        /// Function to open WinDivert.
        /// </summary>
        [DllImport("WinDivert.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr WinDivertOpen(string filter, uint layer, int priority, ulong flags);

        /// <summary>
        /// Function to receive packets.
        /// </summary>
        [DllImport("WinDivert.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool WinDivertRecv(IntPtr handle, IntPtr pPacket, uint packetLen, ref uint recvLen, IntPtr pAddr, ref IntPtr pRecvPacket);

        /// <summary>
        /// Function to close WinDivert.
        /// </summary>
        [DllImport("WinDivert.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool WinDivertClose(IntPtr handle);
    }
}
