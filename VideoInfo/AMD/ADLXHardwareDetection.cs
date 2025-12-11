using System;
using System.Management;
using System.Runtime.Versioning;

namespace ADLXWrapper
{
    /// <summary>
    /// Hardware detection utilities for AMD GPU presence.
    /// </summary>
    public static class ADLXHardwareDetection
    {
        private const string AMD_PCI_VENDOR_ID = "1002";

        /// <summary>
        /// Checks if an AMD GPU is present in the system via PCI Vendor ID detection.
        /// </summary>
        /// <param name="errorMessage">Details about why AMD GPU was not detected.</param>
        /// <returns>True if AMD GPU hardware is detected, false otherwise.</returns>
        [SupportedOSPlatform("windows")]
        public static bool HasAMDGPU(out string errorMessage)
        {
            try
            {
                using var searcher = new ManagementObjectSearcher(
                    $"SELECT * FROM Win32_VideoController WHERE PNPDeviceID LIKE 'PCI\\\\VEN_{AMD_PCI_VENDOR_ID}%'");

                var devices = searcher.Get();
                if (devices.Count > 0)
                {
                    errorMessage = string.Empty;
                    return true;
                }

                errorMessage = $"No AMD GPU hardware detected (PCI Vendor ID {AMD_PCI_VENDOR_ID} not found)";
                return false;
            }
            catch (Exception ex)
            {
                errorMessage = $"Failed to query PCI devices: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Gets detailed information about detected AMD GPUs.
        /// </summary>
        /// <returns>Array of GPU names, or empty array if none found.</returns>
        [SupportedOSPlatform("windows")]
        public static string[] GetAMDGPUNames()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher(
                    $"SELECT Name FROM Win32_VideoController WHERE PNPDeviceID LIKE 'PCI\\\\VEN_{AMD_PCI_VENDOR_ID}%'");

                var devices = searcher.Get();
                var names = new string[devices.Count];
                int index = 0;

                foreach (ManagementObject device in devices)
                {
                    names[index++] = device["Name"]?.ToString() ?? "Unknown AMD GPU";
                }

                return names;
            }
            catch
            {
                return Array.Empty<string>();
            }
        }
    }
}
