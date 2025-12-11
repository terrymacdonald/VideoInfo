using System;
using System.Management;
using System.Runtime.Versioning;

namespace IGCLWrapper
{
	/// <summary>
	/// Hardware detection utilities for Intel GPU presence
	/// </summary>
	public static class IGCLHardwareDetection
	{
		private const string INTEL_PCI_VENDOR_ID = "8086";

		/// <summary>
		/// Checks if an Intel GPU is present in the system via PCI Vendor ID detection
		/// </summary>
		/// <param name="errorMessage">Details about why Intel GPU was not detected</param>
		/// <returns>True if Intel GPU hardware is detected, false otherwise</returns>
		[SupportedOSPlatform("windows")]
		public static bool HasIntelGPU(out string errorMessage)
		{
			try
			{
				// Query for video controllers with Intel's PCI Vendor ID (8086)
				using var searcher = new ManagementObjectSearcher(
					$@"SELECT * FROM Win32_VideoController WHERE PNPDeviceID LIKE 'PCI\\VEN_{INTEL_PCI_VENDOR_ID}%'");

				var devices = searcher.Get();
				if (devices.Count > 0)
				{
					errorMessage = string.Empty;
					return true;
				}

				errorMessage = $"No Intel GPU hardware detected (PCI Vendor ID {INTEL_PCI_VENDOR_ID} not found)";
				return false;
			}
			catch (Exception ex)
			{
				errorMessage = $"Failed to query PCI devices: {ex.Message}";
				return false;
			}
		}

		/// <summary>
		/// Gets detailed information about detected Intel GPUs
		/// </summary>
		/// <returns>Array of GPU names, or empty array if none found</returns>
		[SupportedOSPlatform("windows")]
		public static string[] GetIntelGPUNames()
		{
			try
			{
				using var searcher = new ManagementObjectSearcher(
					$@"SELECT Name FROM Win32_VideoController WHERE PNPDeviceID LIKE 'PCI\\VEN_{INTEL_PCI_VENDOR_ID}%'");

				var devices = searcher.Get();
				var names = new string[devices.Count];
				int index = 0;

				foreach (ManagementObject device in devices)
				{
					names[index++] = device["Name"]?.ToString() ?? "Unknown Intel GPU";
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