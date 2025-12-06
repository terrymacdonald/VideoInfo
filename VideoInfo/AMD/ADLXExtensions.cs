using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Microsoft.Win32.SafeHandles;

namespace ADLXWrapper
{
    /// <summary>
    /// Helper methods for ADLX GPU operations
    /// Provides convenient access to GPU properties via VTable
    /// </summary>
    public static unsafe class ADLXHelpers
    {
        /// <summary>
        /// Try QueryInterface on an ADLX interface given an IID string (wide string).
        /// </summary>
        public static unsafe bool TryQueryInterface(IntPtr pInterface, string iid, out IntPtr resultPtr)
        {
            if (pInterface == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pInterface));
            resultPtr = IntPtr.Zero;
            var vtbl = *(ADLXVTables.IADLXInterfaceVtbl**)pInterface;
            var qiFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.QueryInterfaceFn>(vtbl->QueryInterface);
            var pChars = Marshal.StringToHGlobalUni(iid);
            try
            {
                IntPtr* pOut = stackalloc IntPtr[1];
                *pOut = IntPtr.Zero;
                var r = qiFn(pInterface, (char*)pChars, pOut);
                resultPtr = *pOut;
                return r == ADLX_RESULT.ADLX_OK && resultPtr != IntPtr.Zero;
            }
            finally
            {
                Marshal.FreeHGlobal(pChars);
            }
        }

        /// <summary>
        /// Get GPU name
        /// </summary>
        public static string GetGPUName(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var nameFn = (ADLXVTables.NameFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->Name, typeof(ADLXVTables.NameFn));

            byte* pName;
            var result = nameFn(pGPU, &pName);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU name");
            }

            return MarshalString(pName);
        }

        /// <summary>
        /// Get GPU vendor ID
        /// </summary>
        public static string GetGPUVendorId(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var vendorIdFn = (ADLXVTables.VendorIdFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->VendorId, typeof(ADLXVTables.VendorIdFn));

            byte* pVendorId;
            var result = vendorIdFn(pGPU, &pVendorId);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU vendor ID");
            }

            return MarshalString(pVendorId);
        }

        /// <summary>
        /// Get GPU driver path
        /// </summary>
        public static string GetGPUDriverPath(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var driverPathFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.NameFn>(vtbl->DriverPath);

            byte* pDriverPath;
            var result = driverPathFn(pGPU, &pDriverPath);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU driver path");
            }

            return MarshalString(pDriverPath);
        }

        /// <summary>
        /// Get GPU PNP string
        /// </summary>
        public static string GetGPUPNPString(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var pnpStringFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.NameFn>(vtbl->PNPString);

            byte* pPNPString;
            var result = pnpStringFn(pGPU, &pPNPString);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU PNP string");
            }

            return MarshalString(pPNPString);
        }

        /// <summary>
        /// Get GPU total VRAM in MB
        /// </summary>
        public static uint GetGPUTotalVRAM(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var totalVRAMFn = (ADLXVTables.TotalVRAMFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->TotalVRAM, typeof(ADLXVTables.TotalVRAMFn));

            uint vramMB;
            var result = totalVRAMFn(pGPU, &vramMB);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU total VRAM");
            }

            return vramMB;
        }

        /// <summary>
        /// Get GPU VRAM type
        /// </summary>
        public static string GetGPUVRAMType(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var vramTypeFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.NameFn>(vtbl->VRAMType);

            byte* pVRAMType;
            var result = vramTypeFn(pGPU, &pVRAMType);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU VRAM type");
            }

            return MarshalString(pVRAMType);
        }

        /// <summary>
        /// Get GPU unique ID
        /// </summary>
        public static int GetGPUUniqueId(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var uniqueIdFn = (ADLXVTables.UniqueIdFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->UniqueId, typeof(ADLXVTables.UniqueIdFn));

            int uniqueId;
            var result = uniqueIdFn(pGPU, &uniqueId);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU unique ID");
            }

            return uniqueId;
        }

        /// <summary>
        /// Get GPU device ID
        /// </summary>
        public static string GetGPUDeviceId(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var deviceIdFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.NameFn>(vtbl->DeviceId);

            byte* pDeviceId;
            var result = deviceIdFn(pGPU, &pDeviceId);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU device ID");
            }

            return MarshalString(pDeviceId);
        }

        /// <summary>
        /// Check if GPU is external
        /// </summary>
        public static bool IsGPUExternal(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var isExternalFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.IsExternalFn>(vtbl->IsExternal);

            byte isExternal;
            var result = isExternalFn(pGPU, &isExternal);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check if GPU is external");
            }

            return isExternal != 0;
        }

        /// <summary>
        /// Check if GPU has desktops
        /// </summary>
        public static bool HasGPUDesktops(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var hasDesktopsFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.HasDesktopsFn>(vtbl->HasDesktops);

            byte hasDesktops;
            var result = hasDesktopsFn(pGPU, &hasDesktops);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check if GPU has desktops");
            }

            return hasDesktops != 0;
        }

        /// <summary>
        /// Release an ADLX interface
        /// Decrements the reference count
        /// </summary>
        public static void ReleaseInterface(IntPtr pInterface)
        {
            if (pInterface == IntPtr.Zero)
                return;

            var vtbl = *(ADLXVTables.IADLXInterfaceVtbl**)pInterface;
            var releaseFn = (ADLXVTables.ReleaseFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->Release, typeof(ADLXVTables.ReleaseFn));

            releaseFn(pInterface);
        }

        /// <summary>
        /// Add reference to an ADLX interface
        /// Increments the reference count
        /// </summary>
        public static void AddRefInterface(IntPtr pInterface)
        {
            if (pInterface == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pInterface));

            var vtbl = *(ADLXVTables.IADLXInterfaceVtbl**)pInterface;
            var addRefFn = (ADLXVTables.AddRefFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->AddRef, typeof(ADLXVTables.AddRefFn));

            addRefFn(pInterface);
        }

        /// <summary>
        /// Helper to marshal ANSI string pointer to managed string
        /// </summary>
        private static string MarshalString(byte* pStr)
        {
            if (pStr == null)
                return string.Empty;

            return Marshal.PtrToStringAnsi((IntPtr)pStr) ?? string.Empty;
        }
    }

    /// <summary>
    /// Helper methods for working with ADLX display services
    /// </summary>
    public static unsafe class ADLXDisplayHelpers
    {
        /// <summary>
        /// Acquire display services and wrap in a SafeHandle.
        /// </summary>
        public static AdlxInterfaceHandle GetDisplayServicesHandle(IntPtr pSystem)
        {
            if (pSystem == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSystem));

            var systemVtbl = *(ADLXVTables.IADLXSystemVtbl**)pSystem;
            var getDisplayServicesFn = Marshal.GetDelegateForFunctionPointer<GetDisplayServicesFn>(
                systemVtbl->GetDisplaysServices);

            IntPtr pDisplayServices;
            var result = getDisplayServicesFn(pSystem, &pDisplayServices);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display services");
            }

            return AdlxInterfaceHandle.From(pDisplayServices);
        }

        public static AdlxInterfaceHandle GetDisplayChangedHandlingHandle(IntPtr pDisplayServices)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));

            var svcVtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetDisplayChangedHandlingFn>(svcVtbl->GetDisplayChangedHandling);

            IntPtr pHandling;
            var result = getFn(pDisplayServices, &pHandling);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get display changed handling");

            return AdlxInterfaceHandle.From(pHandling);
        }

        public delegate bool DisplaySettingsChangedCallback(IntPtr pEvent);

        public static DisplaySettingsListenerHandle CreateDisplaySettingsChangedListener(DisplaySettingsChangedCallback cb)
        {
            return DisplaySettingsListenerHandle.Create(cb);
        }

        public static void AddDisplaySettingsChangedListener(IntPtr pDisplayChangedHandling, DisplaySettingsListenerHandle listener)
        {
            if (pDisplayChangedHandling == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayChangedHandling));
            if (listener is null)
                throw new ArgumentNullException(nameof(listener));

            var vtbl = *(ADLXVTables.IADLXDisplayChangedHandlingVtbl**)pDisplayChangedHandling;
            var addFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.AddDisplaySettingsEventListenerFn>(vtbl->AddDisplaySettingsEventListener);
            var result = addFn(pDisplayChangedHandling, listener.DangerousGetHandle());
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to add display settings listener");
        }

        public static void RemoveDisplaySettingsChangedListener(IntPtr pDisplayChangedHandling, DisplaySettingsListenerHandle listener)
        {
            if (pDisplayChangedHandling == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayChangedHandling));
            if (listener is null)
                throw new ArgumentNullException(nameof(listener));

            var vtbl = *(ADLXVTables.IADLXDisplayChangedHandlingVtbl**)pDisplayChangedHandling;
            var removeFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.RemoveDisplaySettingsEventListenerFn>(vtbl->RemoveDisplaySettingsEventListener);
            var result = removeFn(pDisplayChangedHandling, listener.DangerousGetHandle());
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to remove display settings listener");
        }

        /// <summary>
        /// Enumerate displays for a GPU
        /// Returns array of display interface pointers
        /// </summary>
        public static IntPtr[] EnumerateDisplays(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            // We need the system services to get display services
            // This will be called from tests that have access to system services
            // For now, return empty array - will be implemented when we have display services access
            return Array.Empty<IntPtr>();
        }

        /// <summary>
        /// Enumerate all displays from system display services
        /// Returns array of display interface pointers
        /// </summary>
        public static IntPtr[] EnumerateAllDisplays(IntPtr pSystem)
        {
            if (pSystem == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSystem));

            // Get display services from system
            var systemVtbl = *(ADLXVTables.IADLXSystemVtbl**)pSystem;
            var getDisplayServicesFn = Marshal.GetDelegateForFunctionPointer<GetDisplayServicesFn>(
                systemVtbl->GetDisplaysServices);

            IntPtr pDisplayServices;
            var result = getDisplayServicesFn(pSystem, &pDisplayServices);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display services");
            }

            if (pDisplayServices == IntPtr.Zero)
            {
                return Array.Empty<IntPtr>();
            }

            try
            {
                // Get displays from display services
                var displayServicesVtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
                var getDisplaysFn = (ADLXVTables.GetDisplaysFn)Marshal.GetDelegateForFunctionPointer(
                    displayServicesVtbl->GetDisplays, typeof(ADLXVTables.GetDisplaysFn));

                IntPtr pDisplayList;
                result = getDisplaysFn(pDisplayServices, &pDisplayList);

                if (result != ADLX_RESULT.ADLX_OK)
                {
                    throw new ADLXException(result, "Failed to get display list");
                }

                if (pDisplayList == IntPtr.Zero)
                {
                    return Array.Empty<IntPtr>();
                }

                try
                {
                    // Get list size
                    var listVtbl = *(ADLXVTables.IADLXDisplayListVtbl**)pDisplayList;
                    var sizeFn = (ADLXVTables.SizeFn)Marshal.GetDelegateForFunctionPointer(
                        listVtbl->Size, typeof(ADLXVTables.SizeFn));
                    var atFn = (ADLXVTables.AtFn)Marshal.GetDelegateForFunctionPointer(
                        listVtbl->At, typeof(ADLXVTables.AtFn));

                    uint count = sizeFn(pDisplayList);

                    if (count == 0)
                    {
                        return Array.Empty<IntPtr>();
                    }

                    // Get each display from the list
                    var displays = new IntPtr[count];
                    for (uint i = 0; i < count; i++)
                    {
                        IntPtr pDisplay;
                        result = atFn(pDisplayList, i, &pDisplay);

                        if (result != ADLX_RESULT.ADLX_OK)
                        {
                            throw new ADLXException(result, $"Failed to get display at index {i}");
                        }

                        displays[i] = pDisplay;
                    }

                    return displays;
                }
                finally
                {
                    // Release the display list interface
                    ADLXHelpers.ReleaseInterface(pDisplayList);
                }
            }
            finally
            {
                // Release the display services interface
                ADLXHelpers.ReleaseInterface(pDisplayServices);
            }
        }

        /// <summary>
        /// Enumerate all displays from system display services, returning SafeHandles for automatic release.
        /// </summary>
        public static AdlxInterfaceHandle[] EnumerateAllDisplayHandles(IntPtr pSystem)
        {
            var raw = EnumerateAllDisplays(pSystem);
            var handles = new AdlxInterfaceHandle[raw.Length];
            for (int i = 0; i < raw.Length; i++)
            {
                handles[i] = AdlxInterfaceHandle.From(raw[i]);
            }
            return handles;
        }

        // Delegate for GetDisplaysServices
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate ADLX_RESULT GetDisplayServicesFn(IntPtr pThis, IntPtr* ppDisplayServices);

        /// <summary>
        /// Get display name
        /// </summary>
        public static string GetDisplayName(IntPtr pDisplay)
        {
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayVtbl**)pDisplay;
            var nameFn = (ADLXVTables.DisplayNameFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->Name, typeof(ADLXVTables.DisplayNameFn));

            byte* pName;
            var result = nameFn(pDisplay, &pName);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display name");
            }

            return MarshalString(pName);
        }

        /// <summary>
        /// Get display native resolution
        /// </summary>
        public static (int width, int height) GetDisplayNativeResolution(IntPtr pDisplay)
        {
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayVtbl**)pDisplay;
            var resolutionFn = (ADLXVTables.NativeResolutionFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->NativeResolution, typeof(ADLXVTables.NativeResolutionFn));

            int width, height;
            var result = resolutionFn(pDisplay, &width, &height);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display resolution");
            }

            return (width, height);
        }

        /// <summary>
        /// Get display refresh rate
        /// </summary>
        public static double GetDisplayRefreshRate(IntPtr pDisplay)
        {
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayVtbl**)pDisplay;
            var refreshRateFn = (ADLXVTables.RefreshRateFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->RefreshRate, typeof(ADLXVTables.RefreshRateFn));

            double refreshRate;
            var result = refreshRateFn(pDisplay, &refreshRate);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display refresh rate");
            }

            return refreshRate;
        }

        /// <summary>
        /// Get display manufacturer ID
        /// </summary>
        public static uint GetDisplayManufacturerID(IntPtr pDisplay)
        {
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayVtbl**)pDisplay;
            var manufacturerIDFn = (ADLXVTables.ManufacturerIDFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->ManufacturerID, typeof(ADLXVTables.ManufacturerIDFn));

            uint manufacturerID;
            var result = manufacturerIDFn(pDisplay, &manufacturerID);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display manufacturer ID");
            }

            return manufacturerID;
        }

        /// <summary>
        /// Get display pixel clock
        /// </summary>
        public static uint GetDisplayPixelClock(IntPtr pDisplay)
        {
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayVtbl**)pDisplay;
            var pixelClockFn = (ADLXVTables.PixelClockFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->PixelClock, typeof(ADLXVTables.PixelClockFn));

            uint pixelClock;
            var result = pixelClockFn(pDisplay, &pixelClock);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display pixel clock");
            }

            return pixelClock;
        }

        /// <summary>
        /// Get display connector type
        /// </summary>
        public static ADLX_DISPLAY_CONNECTOR_TYPE GetDisplayConnectorType(IntPtr pDisplay)
        {
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayVtbl**)pDisplay;
            var connectorFn = Marshal.GetDelegateForFunctionPointer<DisplayConnectorTypeFn>(vtbl->ConnectorType);

            ADLX_DISPLAY_CONNECTOR_TYPE connectorType;
            var result = connectorFn(pDisplay, &connectorType);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display connector type");
            }

            return connectorType;
        }

        /// <summary>
        /// Get display type
        /// </summary>
        public static ADLX_DISPLAY_TYPE GetDisplayType(IntPtr pDisplay)
        {
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayVtbl**)pDisplay;
            var typeFn = Marshal.GetDelegateForFunctionPointer<DisplayTypeFn>(vtbl->DisplayType);

            ADLX_DISPLAY_TYPE displayType;
            var result = typeFn(pDisplay, &displayType);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display type");
            }

            return displayType;
        }

        /// <summary>
        /// Get display scan type
        /// </summary>
        public static ADLX_DISPLAY_SCAN_TYPE GetDisplayScanType(IntPtr pDisplay)
        {
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayVtbl**)pDisplay;
            var scanFn = Marshal.GetDelegateForFunctionPointer<DisplayScanTypeFn>(vtbl->ScanType);

            ADLX_DISPLAY_SCAN_TYPE scanType;
            var result = scanFn(pDisplay, &scanType);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display scan type");
            }

            return scanType;
        }

        /// <summary>
        /// Get display unique identifier
        /// </summary>
        public static long GetDisplayUniqueId(IntPtr pDisplay)
        {
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayVtbl**)pDisplay;
            var uniqueIdFn = Marshal.GetDelegateForFunctionPointer<DisplayUniqueIdFn>(vtbl->UniqueId);

            UIntPtr uniqueId = UIntPtr.Zero;
            var result = uniqueIdFn(pDisplay, &uniqueId);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display unique id");
            }

            return (long)uniqueId.ToUInt64();
        }

        /// <summary>
        /// Get display EDID string
        /// </summary>
        public static string GetDisplayEdid(IntPtr pDisplay)
        {
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayVtbl**)pDisplay;
            var edidFn = Marshal.GetDelegateForFunctionPointer<DisplayEdidFn>(vtbl->EDID);

            byte* edid;
            var result = edidFn(pDisplay, &edid);

            if (result != ADLX_RESULT.ADLX_OK || edid == null)
            {
                return string.Empty;
            }

            return MarshalString(edid);
        }

        /// <summary>
        /// Get the GPU backing this display (raw pointer; caller owns release)
        /// </summary>
        public static IntPtr GetDisplayGPU(IntPtr pDisplay)
        {
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayVtbl**)pDisplay;
            if (vtbl->GetGPU == IntPtr.Zero)
                return IntPtr.Zero;

            var getGpuFn = Marshal.GetDelegateForFunctionPointer<DisplayGetGpuFn>(vtbl->GetGPU);

            IntPtr pGpu;
            var result = getGpuFn(pDisplay, &pGpu);
            if (result != ADLX_RESULT.ADLX_OK)
                return IntPtr.Zero;

            return pGpu;
        }

        /// <summary>
        /// Helper to marshal ANSI string pointer to managed string
        /// </summary>
        private static string MarshalString(byte* pStr)
        {
            if (pStr == null)
                return string.Empty;

            return Marshal.PtrToStringAnsi((IntPtr)pStr) ?? string.Empty;
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private unsafe delegate ADLX_RESULT DisplayConnectorTypeFn(IntPtr pThis, ADLX_DISPLAY_CONNECTOR_TYPE* connectType);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private unsafe delegate ADLX_RESULT DisplayTypeFn(IntPtr pThis, ADLX_DISPLAY_TYPE* displayType);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private unsafe delegate ADLX_RESULT DisplayScanTypeFn(IntPtr pThis, ADLX_DISPLAY_SCAN_TYPE* scanType);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private unsafe delegate ADLX_RESULT DisplayUniqueIdFn(IntPtr pThis, UIntPtr* uniqueId);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private unsafe delegate ADLX_RESULT DisplayEdidFn(IntPtr pThis, byte** edid);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private unsafe delegate ADLX_RESULT DisplayGetGpuFn(IntPtr pThis, IntPtr* pGpu);
    }

    /// <summary>
    /// Helper methods for ADLX list operations
    /// </summary>
    public static unsafe class ADLXListHelpers
    {
        /// <summary>
        /// Get the size of a list
        /// </summary>
        public static uint GetListSize(IntPtr pList)
        {
            if (pList == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pList));

            var vtbl = *(ADLXVTables.IADLXListVtbl**)pList;
            var sizeFn = (ADLXVTables.SizeFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->Size, typeof(ADLXVTables.SizeFn));

            return sizeFn(pList);
        }

        /// <summary>
        /// Check if a list is empty
        /// </summary>
        public static bool IsListEmpty(IntPtr pList)
        {
            if (pList == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pList));

            var vtbl = *(ADLXVTables.IADLXListVtbl**)pList;
            var emptyFn = (ADLXVTables.EmptyFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->Empty, typeof(ADLXVTables.EmptyFn));

            return emptyFn(pList) != 0;
        }

        /// <summary>
        /// Get item at specific index from list
        /// </summary>
        public static IntPtr GetListItemAt(IntPtr pList, uint index)
        {
            if (pList == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pList));

            var vtbl = *(ADLXVTables.IADLXListVtbl**)pList;
            var atFn = (ADLXVTables.AtFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->At, typeof(ADLXVTables.AtFn));

            IntPtr pItem;
            var result = atFn(pList, index, &pItem);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, $"Failed to get item at index {index}");
            }

            return pItem;
        }

        /// <summary>
        /// Convert a list to an array of interface pointers
        /// </summary>
        public static IntPtr[] ListToArray(IntPtr pList)
        {
            if (pList == IntPtr.Zero)
                return Array.Empty<IntPtr>();

            uint count = GetListSize(pList);
            if (count == 0)
                return Array.Empty<IntPtr>();

            var items = new IntPtr[count];
            for (uint i = 0; i < count; i++)
            {
                items[i] = GetListItemAt(pList, i);
            }

            return items;
        }
    }

    /// <summary>
    /// Helper methods for GPU information retrieval
    /// Combines multiple property calls into convenient structs
    /// </summary>
    public static class ADLXGPUInfo
    {
        /// <summary>
        /// Basic GPU information
        /// </summary>
        public struct GPUBasicInfo
        {
            public string Name;
            public string VendorId;
            public int UniqueId;
            public uint TotalVRAM;
            public string VRAMType;
            public bool IsExternal;
            public bool HasDesktops;
        }

        /// <summary>
        /// Get comprehensive basic information about a GPU
        /// </summary>
        public static GPUBasicInfo GetBasicInfo(IntPtr pGPU)
        {
            return new GPUBasicInfo
            {
                Name = ADLXHelpers.GetGPUName(pGPU),
                VendorId = ADLXHelpers.GetGPUVendorId(pGPU),
                UniqueId = ADLXHelpers.GetGPUUniqueId(pGPU),
                TotalVRAM = ADLXHelpers.GetGPUTotalVRAM(pGPU),
                VRAMType = ADLXHelpers.GetGPUVRAMType(pGPU),
                IsExternal = ADLXHelpers.IsGPUExternal(pGPU),
                HasDesktops = ADLXHelpers.HasGPUDesktops(pGPU)
            };
        }

        /// <summary>
        /// GPU identification information
        /// </summary>
        public struct GPUIdentification
        {
            public string DeviceId;
            public string PNPString;
            public string DriverPath;
            public int UniqueId;
        }

        /// <summary>
        /// Get GPU identification information
        /// </summary>
        public static GPUIdentification GetIdentification(IntPtr pGPU)
        {
            return new GPUIdentification
            {
                DeviceId = ADLXHelpers.GetGPUDeviceId(pGPU),
                PNPString = ADLXHelpers.GetGPUPNPString(pGPU),
                DriverPath = ADLXHelpers.GetGPUDriverPath(pGPU),
                UniqueId = ADLXHelpers.GetGPUUniqueId(pGPU)
            };
        }
    }

    /// <summary>
    /// Helper methods for GPU tuning services
    /// </summary>
    public static unsafe class ADLXGPUTuningHelpers
    {
        /// <summary>
        /// Check if auto tuning is supported for a GPU
        /// </summary>
        public static bool IsSupportedAutoTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUTuningServicesVtbl**)pGPUTuningServices;
            var isSupportedFn = (ADLXVTables.IsSupportedTuningFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedAutoTuning, typeof(ADLXVTables.IsSupportedTuningFn));

            byte supported;
            var result = isSupportedFn(pGPUTuningServices, pGPU, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check auto tuning support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if preset tuning is supported for a GPU
        /// </summary>
        public static bool IsSupportedPresetTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUTuningServicesVtbl**)pGPUTuningServices;
            var isSupportedFn = (ADLXVTables.IsSupportedTuningFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedPresetTuning, typeof(ADLXVTables.IsSupportedTuningFn));

            byte supported;
            var result = isSupportedFn(pGPUTuningServices, pGPU, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check preset tuning support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if manual GFX tuning is supported for a GPU
        /// </summary>
        public static bool IsSupportedManualGFXTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUTuningServicesVtbl**)pGPUTuningServices;
            var isSupportedFn = (ADLXVTables.IsSupportedTuningFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedManualGFXTuning, typeof(ADLXVTables.IsSupportedTuningFn));

            byte supported;
            var result = isSupportedFn(pGPUTuningServices, pGPU, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check manual GFX tuning support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if manual VRAM tuning is supported for a GPU
        /// </summary>
        public static bool IsSupportedManualVRAMTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUTuningServicesVtbl**)pGPUTuningServices;
            var isSupportedFn = (ADLXVTables.IsSupportedTuningFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedManualVRAMTuning, typeof(ADLXVTables.IsSupportedTuningFn));

            byte supported;
            var result = isSupportedFn(pGPUTuningServices, pGPU, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check manual VRAM tuning support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if manual fan tuning is supported for a GPU
        /// </summary>
        public static bool IsSupportedManualFanTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUTuningServicesVtbl**)pGPUTuningServices;
            var isSupportedFn = (ADLXVTables.IsSupportedTuningFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedManualFanTuning, typeof(ADLXVTables.IsSupportedTuningFn));

            byte supported;
            var result = isSupportedFn(pGPUTuningServices, pGPU, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check manual fan tuning support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if manual power tuning is supported for a GPU
        /// </summary>
        public static bool IsSupportedManualPowerTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUTuningServicesVtbl**)pGPUTuningServices;
            var isSupportedFn = (ADLXVTables.IsSupportedTuningFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedManualPowerTuning, typeof(ADLXVTables.IsSupportedTuningFn));

            byte supported;
            var result = isSupportedFn(pGPUTuningServices, pGPU, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check manual power tuning support");
            }

            return supported != 0;
        }
    }

    /// <summary>
    /// Helper methods for GPU performance monitoring information retrieval
    /// </summary>
    public static class ADLXPerformanceMonitoringInfo
    {
        /// <summary>
        /// GPU metrics support capabilities
        /// </summary>
        public struct GPUMetricsSupport
        {
            public bool UsageSupported;
            public bool ClockSpeedSupported;
            public bool TemperatureSupported;
            public bool PowerSupported;
            public bool FanSpeedSupported;
            public bool VRAMSupported;
        }

        /// <summary>
        /// Current GPU metrics values
        /// </summary>
        public struct GPUMetricsSnapshot
        {
            public double Temperature;
            public double Usage;
            public int ClockSpeed;
            public int VRAMClockSpeed;
            public int VRAMUsage;
            public int FanSpeed;
            public double Power;
            public double TotalBoardPower;
            public int Voltage;
            public long TimestampMs;
        }

        /// <summary>
        /// System-level metrics values.
        /// </summary>
        public struct SystemMetricsSnapshot
        {
            public long TimestampMs;
            public double CpuUsage;
            public int SystemRam;
            public int SmartShift;
            public PowerDistributionSnapshot? PowerDistribution;
        }

        /// <summary>
        /// SmartShift power distribution values (when available via IADLXSystemMetrics1).
        /// </summary>
        public struct PowerDistributionSnapshot
        {
            public int ApuShiftValue;
            public int GpuShiftValue;
            public int ApuShiftLimit;
            public int GpuShiftLimit;
            public int TotalShiftLimit;
        }

        /// <summary>
        /// GPU metrics paired with a GPU identifier.
        /// </summary>
        public struct GPUMetricsEntry
        {
            public int GPUUniqueId;
            public GPUMetricsSnapshot Metrics;
        }

        /// <summary>
        /// Aggregate snapshot for system + GPU metrics.
        /// </summary>
        public struct AllMetricsSnapshot
        {
            public long TimestampMs;
            public SystemMetricsSnapshot? System;
            public int? FPS;
            public GPUMetricsEntry[] GPU;
        }

        /// <summary>
        /// Get metrics support for a GPU
        /// </summary>
        public static GPUMetricsSupport GetMetricsSupport(IntPtr pPerfMonServices, IntPtr pGPU)
        {
            var pMetricsSupport = ADLXPerformanceMonitoringHelpers.GetSupportedGPUMetrics(pPerfMonServices, pGPU);
            try
            {
                var support = new GPUMetricsSupport();

                try { support.UsageSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUUsage(pMetricsSupport); }
                catch { support.UsageSupported = false; }

                try { support.ClockSpeedSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUClockSpeed(pMetricsSupport); }
                catch { support.ClockSpeedSupported = false; }

                try { support.TemperatureSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUTemperature(pMetricsSupport); }
                catch { support.TemperatureSupported = false; }

                try { support.PowerSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUPower(pMetricsSupport); }
                catch { support.PowerSupported = false; }

                try { support.FanSpeedSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUFanSpeed(pMetricsSupport); }
                catch { support.FanSpeedSupported = false; }

                try { support.VRAMSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUVRAM(pMetricsSupport); }
                catch { support.VRAMSupported = false; }

                return support;
            }
            finally
            {
                ADLXHelpers.ReleaseInterface(pMetricsSupport);
            }
        }

        /// <summary>
        /// Get current metrics snapshot for a GPU
        /// </summary>
        public static GPUMetricsSnapshot GetCurrentMetrics(IntPtr pPerfMonServices, IntPtr pGPU)
        {
            var pMetrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(pPerfMonServices, pGPU);
            try
            {
                var snapshot = new GPUMetricsSnapshot();

                try { snapshot.Temperature = ADLXPerformanceMonitoringHelpers.GetGPUTemperature(pMetrics); }
                catch { snapshot.Temperature = 0; }

                try { snapshot.Usage = ADLXPerformanceMonitoringHelpers.GetGPUUsage(pMetrics); }
                catch { snapshot.Usage = 0; }

                try { snapshot.ClockSpeed = ADLXPerformanceMonitoringHelpers.GetGPUClockSpeed(pMetrics); }
                catch { snapshot.ClockSpeed = 0; }

                try { snapshot.VRAMClockSpeed = ADLXPerformanceMonitoringHelpers.GetGPUVRAMClockSpeed(pMetrics); }
                catch { snapshot.VRAMClockSpeed = 0; }

                try { snapshot.VRAMUsage = ADLXPerformanceMonitoringHelpers.GetGPUVRAM(pMetrics); }
                catch { snapshot.VRAMUsage = 0; }

                try { snapshot.FanSpeed = ADLXPerformanceMonitoringHelpers.GetGPUFanSpeed(pMetrics); }
                catch { snapshot.FanSpeed = 0; }

                try { snapshot.Power = ADLXPerformanceMonitoringHelpers.GetGPUPower(pMetrics); }
                catch { snapshot.Power = 0; }

                return snapshot;
            }
            finally
            {
                ADLXHelpers.ReleaseInterface(pMetrics);
            }
        }
    }

    /// <summary>
    /// Helper methods for GPU tuning information retrieval
    /// </summary>
    public static class ADLXGPUTuningInfo
    {
        /// <summary>
        /// GPU tuning capabilities
        /// </summary>
        public struct GPUTuningCapabilities
        {
            public bool AutoTuningSupported;
            public bool PresetTuningSupported;
            public bool ManualGFXTuningSupported;
            public bool ManualVRAMTuningSupported;
            public bool ManualFanTuningSupported;
            public bool ManualPowerTuningSupported;
        }

        /// <summary>
        /// Get comprehensive tuning capabilities for a GPU
        /// </summary>
        public static GPUTuningCapabilities GetTuningCapabilities(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            var capabilities = new GPUTuningCapabilities();

            try
            {
                capabilities.AutoTuningSupported = ADLXGPUTuningHelpers.IsSupportedAutoTuning(pGPUTuningServices, pGPU);
            }
            catch
            {
                capabilities.AutoTuningSupported = false;
            }

            try
            {
                capabilities.PresetTuningSupported = ADLXGPUTuningHelpers.IsSupportedPresetTuning(pGPUTuningServices, pGPU);
            }
            catch
            {
                capabilities.PresetTuningSupported = false;
            }

            try
            {
                capabilities.ManualGFXTuningSupported = ADLXGPUTuningHelpers.IsSupportedManualGFXTuning(pGPUTuningServices, pGPU);
            }
            catch
            {
                capabilities.ManualGFXTuningSupported = false;
            }

            try
            {
                capabilities.ManualVRAMTuningSupported = ADLXGPUTuningHelpers.IsSupportedManualVRAMTuning(pGPUTuningServices, pGPU);
            }
            catch
            {
                capabilities.ManualVRAMTuningSupported = false;
            }

            try
            {
                capabilities.ManualFanTuningSupported = ADLXGPUTuningHelpers.IsSupportedManualFanTuning(pGPUTuningServices, pGPU);
            }
            catch
            {
                capabilities.ManualFanTuningSupported = false;
            }

            try
            {
                capabilities.ManualPowerTuningSupported = ADLXGPUTuningHelpers.IsSupportedManualPowerTuning(pGPUTuningServices, pGPU);
            }
            catch
            {
                capabilities.ManualPowerTuningSupported = false;
            }

            return capabilities;
        }
    }

    /// <summary>
    /// Helper methods for display information retrieval
    /// Combines multiple property calls into convenient structs
    /// </summary>
    public static class ADLXDisplayInfo
    {
        /// <summary>
        /// Basic display information
        /// </summary>
        public struct DisplayBasicInfo
        {
            public string Name;
            public int Width;
            public int Height;
            public double RefreshRate;
            public uint ManufacturerID;
            public uint PixelClock;
        }

        /// <summary>
        /// Get comprehensive basic information about a display
        /// </summary>
        public static DisplayBasicInfo GetBasicInfo(IntPtr pDisplay)
        {
            var resolution = ADLXDisplayHelpers.GetDisplayNativeResolution(pDisplay);

            return new DisplayBasicInfo
            {
                Name = ADLXDisplayHelpers.GetDisplayName(pDisplay),
                Width = resolution.width,
                Height = resolution.height,
                RefreshRate = ADLXDisplayHelpers.GetDisplayRefreshRate(pDisplay),
                ManufacturerID = ADLXDisplayHelpers.GetDisplayManufacturerID(pDisplay),
                PixelClock = ADLXDisplayHelpers.GetDisplayPixelClock(pDisplay)
            };
        }
    }

    /// <summary>
    /// Helpers for multimedia settings (video upscale and video super resolution).
    /// </summary>
    public static unsafe class ADLXMultimediaHelpers
    {
        public static AdlxInterfaceHandle GetMultimediaServices(IntPtr pSystem)
        {
            if (pSystem == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSystem));

            var vtbl = *(ADLXVTables.IADLXSystemVtbl**)pSystem;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetMultiMediaServicesFn>(vtbl->GetMultiMediaServices);
            IntPtr pServices;
            var result = getFn(pSystem, &pServices);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get multimedia services");
            return AdlxInterfaceHandle.From(pServices);
        }

        public static AdlxInterfaceHandle GetVideoUpscale(IntPtr pMultimediaServices, IntPtr pGPU)
        {
            if (pMultimediaServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMultimediaServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXMultimediaServicesVtbl**)pMultimediaServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetVideoUpscaleFn>(vtbl->GetVideoUpscale);
            IntPtr pUpscale;
            var result = getFn(pMultimediaServices, pGPU, &pUpscale);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get video upscale interface");
            return AdlxInterfaceHandle.From(pUpscale);
        }

        public static AdlxInterfaceHandle GetVideoSuperResolution(IntPtr pMultimediaServices, IntPtr pGPU)
        {
            if (pMultimediaServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMultimediaServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXMultimediaServicesVtbl**)pMultimediaServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetVideoSuperResolutionFn>(vtbl->GetVideoSuperResolution);
            IntPtr pVsr;
            var result = getFn(pMultimediaServices, pGPU, &pVsr);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get video super resolution interface");
            return AdlxInterfaceHandle.From(pVsr);
        }

        public static (bool supported, bool enabled, ADLX_IntRange scaleFactorRange, int minInputResolution) GetVideoUpscaleState(IntPtr pVideoUpscale)
        {
            if (pVideoUpscale == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVideoUpscale));

            var vtbl = *(ADLXVTables.IADLXVideoUpscaleVtbl**)pVideoUpscale;
            var boolFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>;
            var getRangeFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetIntRangeFn>(vtbl->GetScaleFactorRange);
            var getMinResFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetIntValueFn>(vtbl->GetMinInputResolution);

            byte sup = 0, en = 0;
            boolFn(vtbl->IsSupported)(pVideoUpscale, &sup);
            boolFn(vtbl->IsEnabled)(pVideoUpscale, &en);

            ADLX_IntRange range = default;
            getRangeFn(pVideoUpscale, &range);

            int minRes = 0;
            getMinResFn(pVideoUpscale, &minRes);

            return (sup != 0, en != 0, range, minRes);
        }

        public static void SetVideoUpscaleEnabled(IntPtr pVideoUpscale, bool enable)
        {
            if (pVideoUpscale == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVideoUpscale));

            var vtbl = *(ADLXVTables.IADLXVideoUpscaleVtbl**)pVideoUpscale;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetEnabled);
            var result = setFn(pVideoUpscale, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set video upscale enabled");
        }

        public static void SetVideoUpscaleMinInputResolution(IntPtr pVideoUpscale, int minResolution)
        {
            if (pVideoUpscale == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVideoUpscale));

            var vtbl = *(ADLXVTables.IADLXVideoUpscaleVtbl**)pVideoUpscale;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetIntValueFn>(vtbl->SetMinInputResolution);
            var result = setFn(pVideoUpscale, minResolution);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set video upscale minimum input resolution");
        }

        public static (bool supported, bool enabled) GetVideoSuperResolutionState(IntPtr pVsr)
        {
            if (pVsr == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVsr));

            var vtbl = *(ADLXVTables.IADLXVideoSuperResolutionVtbl**)pVsr;
            var boolFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>;
            byte sup = 0, en = 0;
            boolFn(vtbl->IsSupported)(pVsr, &sup);
            boolFn(vtbl->IsEnabled)(pVsr, &en);
            return (sup != 0, en != 0);
        }

        public static void SetVideoSuperResolutionEnabled(IntPtr pVsr, bool enable)
        {
            if (pVsr == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVsr));

            var vtbl = *(ADLXVTables.IADLXVideoSuperResolutionVtbl**)pVsr;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetEnabled);
            var result = setFn(pVsr, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set video super resolution enabled");
        }
    }

    /// <summary>
    /// Helper methods for power tuning (manual power limit, SmartShift Max/Eco).
    /// </summary>
    public static unsafe class ADLXPowerTuningHelpers
    {
        public static AdlxInterfaceHandle GetPowerTuningServices(IntPtr pSystem)
        {
            if (pSystem == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSystem));

            var vtbl = *(ADLXVTables.IADLXSystemVtbl**)pSystem;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetPowerTuningServicesFn>(vtbl->GetPowerTuningServices);
            IntPtr pServices;
            var result = getFn(pSystem, &pServices);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get power tuning services");
            return AdlxInterfaceHandle.From(pServices);
        }

        public static AdlxInterfaceHandle GetSmartShiftMax(IntPtr pPowerServices)
        {
            if (pPowerServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPowerServices));

            var vtbl = *(ADLXVTables.IADLXPowerTuningServicesVtbl**)pPowerServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetGPUsFn>(vtbl->GetSmartShiftMax);
            IntPtr pMax;
            var result = getFn(pPowerServices, &pMax);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get SmartShift Max interface");
            return AdlxInterfaceHandle.From(pMax);
        }

        public static AdlxInterfaceHandle GetSmartShiftEco(IntPtr pPowerServices)
        {
            if (pPowerServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPowerServices));

            var vtbl = *(ADLXVTables.IADLXPowerTuningServices1Vtbl**)pPowerServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetGPUsFn>(vtbl->GetSmartShiftEco);
            IntPtr pEco;
            var result = getFn(pPowerServices, &pEco);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get SmartShift Eco interface");
            return AdlxInterfaceHandle.From(pEco);
        }

        public static (bool supported, ADLX_SSM_BIAS_MODE biasMode, ADLX_IntRange biasRange, int biasValue) GetSmartShiftMaxState(IntPtr pSmartShiftMax)
        {
            if (pSmartShiftMax == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSmartShiftMax));

            var vtbl = *(ADLXVTables.IADLXSmartShiftMaxVtbl**)pSmartShiftMax;
            var boolFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            byte sup = 0;
            boolFn(pSmartShiftMax, &sup);

            ADLX_SSM_BIAS_MODE mode = default;
            ADLX_IntRange range = default;
            int bias = 0;

            var getModeFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetSSMBiasModeFn>(vtbl->GetBiasMode);
            var getRangeFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetIntRangeFn>(vtbl->GetBiasRange);
            var getBiasFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetIntValueFn>(vtbl->GetBias);

            getModeFn(pSmartShiftMax, &mode);
            getRangeFn(pSmartShiftMax, &range);
            getBiasFn(pSmartShiftMax, &bias);

            return (sup != 0, mode, range, bias);
        }

        public static void SetSmartShiftMaxBias(IntPtr pSmartShiftMax, ADLX_SSM_BIAS_MODE mode, int bias)
        {
            if (pSmartShiftMax == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSmartShiftMax));

            var vtbl = *(ADLXVTables.IADLXSmartShiftMaxVtbl**)pSmartShiftMax;
            var setModeFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetSSMBiasModeFn>(vtbl->SetBiasMode);
            var setBiasFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetIntValueFn>(vtbl->SetBias);

            var modeResult = setModeFn(pSmartShiftMax, mode);
            if (modeResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(modeResult, "Failed to set SmartShift Max bias mode");

            var biasResult = setBiasFn(pSmartShiftMax, bias);
            if (biasResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(biasResult, "Failed to set SmartShift Max bias");
        }

        public static (bool supported, bool enabled) GetSmartShiftEcoState(IntPtr pSmartShiftEco)
        {
            if (pSmartShiftEco == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSmartShiftEco));

            var vtbl = *(ADLXVTables.IADLXSmartShiftEcoVtbl**)pSmartShiftEco;
            var boolFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>;
            byte sup = 0, en = 0;
            boolFn(vtbl->IsSupported)(pSmartShiftEco, &sup);
            boolFn(vtbl->IsEnabled)(pSmartShiftEco, &en);
            return (sup != 0, en != 0);
        }

        public static void SetSmartShiftEcoEnabled(IntPtr pSmartShiftEco, bool enable)
        {
            if (pSmartShiftEco == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSmartShiftEco));

            var vtbl = *(ADLXVTables.IADLXSmartShiftEcoVtbl**)pSmartShiftEco;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetEnabled);
            var result = setFn(pSmartShiftEco, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set SmartShift Eco");
        }

        public static AdlxInterfaceHandle GetManualPowerTuning(IntPtr pGpuTuningServices, IntPtr pGPU)
        {
            if (pGpuTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGpuTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUTuningServicesVtbl**)pGpuTuningServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetManualPowerTuningFn>(vtbl->GetManualPowerTuning);
            IntPtr pManual;
            var result = getFn(pGpuTuningServices, pGPU, &pManual);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get manual power tuning interface");
            return AdlxInterfaceHandle.From(pManual);
        }

        public static (bool supported, ADLX_IntRange range, int value) GetManualPowerLimit(IntPtr pManualPower)
        {
            if (pManualPower == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pManualPower));

            var vtbl = *(ADLXVTables.IADLXManualPowerTuningVtbl**)pManualPower;
            var rangeFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetIntRangeFn>(vtbl->GetPowerLimitRange);
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetIntValueFn>(vtbl->GetPowerLimit);
            ADLX_IntRange range = default;
            var r1 = rangeFn(pManualPower, &range);
            int value = 0;
            var r2 = getFn(pManualPower, &value);
            var supported = r1 == ADLX_RESULT.ADLX_OK && r2 == ADLX_RESULT.ADLX_OK;
            return (supported, range, value);
        }

        public static void SetManualPowerLimit(IntPtr pManualPower, int value)
        {
            if (pManualPower == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pManualPower));

            var vtbl = *(ADLXVTables.IADLXManualPowerTuningVtbl**)pManualPower;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetIntValueFn>(vtbl->SetPowerLimit);
            var result = setFn(pManualPower, value);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set manual power limit");
        }

        public static (bool supported, ADLX_IntRange range, int value, int defaultValue) GetManualTDCLimit(IntPtr pManualPowerV1)
        {
            if (pManualPowerV1 == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pManualPowerV1));

            var vtbl = *(ADLXVTables.IADLXManualPowerTuning1Vtbl**)pManualPowerV1;
            var boolFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupportedTDCLimit);
            byte sup = 0;
            boolFn(pManualPowerV1, &sup);

            ADLX_IntRange range = default;
            int value = 0, defVal = 0;
            var getRangeFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetIntRangeFn>(vtbl->GetTDCLimitRange);
            var getValFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetIntValueFn>(vtbl->GetTDCLimit);
            var getDefFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetIntValueFn>(vtbl->GetTDCLimitDefault);

            getRangeFn(pManualPowerV1, &range);
            getValFn(pManualPowerV1, &value);
            getDefFn(pManualPowerV1, &defVal);

            return (sup != 0, range, value, defVal);
        }

        public static void SetManualTDCLimit(IntPtr pManualPowerV1, int value)
        {
            if (pManualPowerV1 == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pManualPowerV1));

            var vtbl = *(ADLXVTables.IADLXManualPowerTuning1Vtbl**)pManualPowerV1;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetIntValueFn>(vtbl->SetTDCLimit);
            var result = setFn(pManualPowerV1, value);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set TDC limit");
        }
    }

    /// <summary>
    /// Helper methods for performance monitoring services
    /// </summary>
    public static unsafe class ADLXPerformanceMonitoringHelpers
    {
        public static ADLXPerformanceMonitoringInfo.GPUMetricsSnapshot[] EnumerateGPUMetricsList(IntPtr pMetricsList)
        {
            if (pMetricsList == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetricsList));

            var vtbl = *(ADLXVTables.IADLXListVtbl**)pMetricsList;
            var sizeFn = (ADLXVTables.SizeFn)Marshal.GetDelegateForFunctionPointer(vtbl->Size, typeof(ADLXVTables.SizeFn));
            var atFn = (ADLXVTables.AtFn)Marshal.GetDelegateForFunctionPointer(vtbl->At, typeof(ADLXVTables.AtFn));
            var count = sizeFn(pMetricsList);
            var snapshots = new ADLXPerformanceMonitoringInfo.GPUMetricsSnapshot[count];
            for (uint i = 0; i < count; i++)
            {
                IntPtr pItem = IntPtr.Zero;
                if (atFn(pMetricsList, i, &pItem) == ADLX_RESULT.ADLX_OK && pItem != IntPtr.Zero)
                {
                    try
                    {
                        snapshots[i] = GetCurrentMetricsFromHandle(pItem);
                    }
                    finally
                    {
                        ADLXHelpers.ReleaseInterface(pItem);
                    }
                }
            }
            return snapshots;
        }

        public static ADLXPerformanceMonitoringInfo.GPUMetricsSnapshot GetCurrentMetricsFromHandle(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var snapshot = new ADLXPerformanceMonitoringInfo.GPUMetricsSnapshot();
            try { snapshot.Temperature = GetGPUTemperature(pMetrics); } catch { }
            try { snapshot.Usage = GetGPUUsage(pMetrics); } catch { }
            try { snapshot.ClockSpeed = GetGPUClockSpeed(pMetrics); } catch { }
            try { snapshot.VRAMClockSpeed = GetGPUVRAMClockSpeed(pMetrics); } catch { }
            try { snapshot.VRAMUsage = GetGPUVRAM(pMetrics); } catch { }
            try { snapshot.FanSpeed = GetGPUFanSpeed(pMetrics); } catch { }
            try { snapshot.Power = GetGPUPower(pMetrics); } catch { }
            try { snapshot.TimestampMs = GetMetricTimestamp(pMetrics); } catch { }
            try { snapshot.TotalBoardPower = GetGPUTotalBoardPower(pMetrics); } catch { }
            try { snapshot.Voltage = GetGPUVoltage(pMetrics); } catch { }
            return snapshot;
        }

        public static ADLXPerformanceMonitoringInfo.SystemMetricsSnapshot[] EnumerateSystemMetricsList(IntPtr pMetricsList)
        {
            if (pMetricsList == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetricsList));

            var vtbl = *(ADLXVTables.IADLXListVtbl**)pMetricsList;
            var sizeFn = (ADLXVTables.SizeFn)Marshal.GetDelegateForFunctionPointer(vtbl->Size, typeof(ADLXVTables.SizeFn));
            var atFn = (ADLXVTables.AtFn)Marshal.GetDelegateForFunctionPointer(vtbl->At, typeof(ADLXVTables.AtFn));
            var count = sizeFn(pMetricsList);
            var snapshots = new ADLXPerformanceMonitoringInfo.SystemMetricsSnapshot[count];
            for (uint i = 0; i < count; i++)
            {
                IntPtr pItem = IntPtr.Zero;
                if (atFn(pMetricsList, i, &pItem) == ADLX_RESULT.ADLX_OK && pItem != IntPtr.Zero)
                {
                    try
                    {
                        snapshots[i] = GetCurrentSystemMetricsFromHandle(pItem);
                    }
                    finally
                    {
                        ADLXHelpers.ReleaseInterface(pItem);
                    }
                }
            }
            return snapshots;
        }

        public static ADLXPerformanceMonitoringInfo.SystemMetricsSnapshot GetCurrentSystemMetricsFromHandle(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var snapshot = new ADLXPerformanceMonitoringInfo.SystemMetricsSnapshot();
            var pSys = (IADLXSystemMetrics*)pMetrics;

            try { long ts = 0; pSys->TimeStamp(&ts); snapshot.TimestampMs = ts; } catch { }
            try { double cpu = 0; pSys->CPUUsage(&cpu); snapshot.CpuUsage = cpu; } catch { }
            try { int ram = 0; pSys->SystemRAM(&ram); snapshot.SystemRam = ram; } catch { }
            try { int ss = 0; pSys->SmartShift(&ss); snapshot.SmartShift = ss; } catch { }

            try
            {
                if (ADLXHelpers.TryQueryInterface(pMetrics, "IADLXSystemMetrics1", out var sys1Ptr) && sys1Ptr != IntPtr.Zero)
                {
                    try
                    {
                        var pSys1 = (IADLXSystemMetrics1*)sys1Ptr;
                        int apu = 0, gpu = 0, apuLimit = 0, gpuLimit = 0, total = 0;
                        if (pSys1->PowerDistribution(&apu, &gpu, &apuLimit, &gpuLimit, &total) == ADLX_RESULT.ADLX_OK)
                        {
                            snapshot.PowerDistribution = new ADLXPerformanceMonitoringInfo.PowerDistributionSnapshot
                            {
                                ApuShiftValue = apu,
                                GpuShiftValue = gpu,
                                ApuShiftLimit = apuLimit,
                                GpuShiftLimit = gpuLimit,
                                TotalShiftLimit = total
                            };
                        }
                    }
                    finally
                    {
                        ADLXHelpers.ReleaseInterface(sys1Ptr);
                    }
                }
            }
            catch
            {
                // ignore, optional
            }

            return snapshot;
        }

        public static ADLXPerformanceMonitoringInfo.AllMetricsSnapshot[] EnumerateAllMetricsList(IntPtr pMetricsList, IntPtr[]? gpuHandles = null)
        {
            if (pMetricsList == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetricsList));

            var vtbl = *(ADLXVTables.IADLXListVtbl**)pMetricsList;
            var sizeFn = (ADLXVTables.SizeFn)Marshal.GetDelegateForFunctionPointer(vtbl->Size, typeof(ADLXVTables.SizeFn));
            var atFn = (ADLXVTables.AtFn)Marshal.GetDelegateForFunctionPointer(vtbl->At, typeof(ADLXVTables.AtFn));
            var count = sizeFn(pMetricsList);
            var snapshots = new ADLXPerformanceMonitoringInfo.AllMetricsSnapshot[count];
            for (uint i = 0; i < count; i++)
            {
                IntPtr pItem = IntPtr.Zero;
                if (atFn(pMetricsList, i, &pItem) == ADLX_RESULT.ADLX_OK && pItem != IntPtr.Zero)
                {
                    try
                    {
                        snapshots[i] = GetCurrentAllMetricsFromHandle(pItem, gpuHandles);
                    }
                    finally
                    {
                        ADLXHelpers.ReleaseInterface(pItem);
                    }
                }
            }
            return snapshots;
        }

        public static ADLXPerformanceMonitoringInfo.AllMetricsSnapshot GetCurrentAllMetricsFromHandle(IntPtr pMetrics, IntPtr[]? gpuHandles = null)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var snapshot = new ADLXPerformanceMonitoringInfo.AllMetricsSnapshot
            {
                GPU = Array.Empty<ADLXPerformanceMonitoringInfo.GPUMetricsEntry>()
            };

            var pAll = (IADLXAllMetrics*)pMetrics;
            try { long ts = 0; pAll->TimeStamp(&ts); snapshot.TimestampMs = ts; } catch { }

            // System metrics
            try
            {
                IADLXSystemMetrics* pSys = null;
                if (pAll->GetSystemMetrics(&pSys) == ADLX_RESULT.ADLX_OK && pSys != null)
                {
                    try
                    {
                        snapshot.System = GetCurrentSystemMetricsFromHandle((IntPtr)pSys);
                    }
                    finally
                    {
                        ADLXHelpers.ReleaseInterface((IntPtr)pSys);
                    }
                }
            }
            catch { snapshot.System = null; }

            // FPS
            try
            {
                IADLXFPS* pFps = null;
                if (pAll->GetFPS(&pFps) == ADLX_RESULT.ADLX_OK && pFps != null)
                {
                    try
                    {
                        snapshot.FPS = TryReadFps((IntPtr)pFps);
                    }
                    finally
                    {
                        ADLXHelpers.ReleaseInterface((IntPtr)pFps);
                    }
                }
            }
            catch { snapshot.FPS = null; }

            // GPU metrics
            if (gpuHandles != null && gpuHandles.Length > 0)
            {
                var gpuSnapshots = new List<ADLXPerformanceMonitoringInfo.GPUMetricsEntry>(gpuHandles.Length);
                foreach (var gpu in gpuHandles)
                {
                    if (gpu == IntPtr.Zero)
                        continue;

                    IADLXGPUMetrics* pGpuMetrics = null;
                    if (pAll->GetGPUMetrics((IADLXGPU*)gpu, &pGpuMetrics) == ADLX_RESULT.ADLX_OK && pGpuMetrics != null)
                    {
                        try
                        {
                            var metrics = GetCurrentMetricsFromHandle((IntPtr)pGpuMetrics);
                            int gpuId = 0;
                            try { gpuId = ADLXHelpers.GetGPUUniqueId(gpu); } catch { }
                            gpuSnapshots.Add(new ADLXPerformanceMonitoringInfo.GPUMetricsEntry
                            {
                                GPUUniqueId = gpuId,
                                Metrics = metrics
                            });
                        }
                        finally
                        {
                            ADLXHelpers.ReleaseInterface((IntPtr)pGpuMetrics);
                        }
                    }
                }

                snapshot.GPU = gpuSnapshots.ToArray();
            }

            return snapshot;
        }

        private static int? TryReadFps(IntPtr pFps)
        {
            var pFPS = (IADLXFPS*)pFps;
            try
            {
                int fps = 0;
                if (pFPS->FPS(&fps) == ADLX_RESULT.ADLX_OK)
                    return fps;
            }
            catch
            {
            }
            return null;
        }

        public static long GetMetricTimestamp(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsVtbl**)pMetrics;
            var tsFn = (delegate* unmanaged[Stdcall]<IntPtr, long*, ADLX_RESULT>)vtbl->TimeStamp;
            long ts = 0;
            var result = tsFn(pMetrics, &ts);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get metric timestamp");
            return ts;
        }
        public static ADLX_IntRange GetSamplingIntervalRange(IntPtr pPerfMonServices)
        {
            if (pPerfMonServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPerfMonServices));

            var vtbl = *(ADLXVTables.IADLXPerformanceMonitoringServicesVtbl**)pPerfMonServices;
            var getRangeFn = (ADLXVTables.GetIntRangeFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GetSamplingIntervalRange, typeof(ADLXVTables.GetIntRangeFn));
            ADLX_IntRange range = default;
            var result = getRangeFn(pPerfMonServices, &range);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get sampling interval range");
            return range;
        }

        public static int GetSamplingInterval(IntPtr pPerfMonServices)
        {
            if (pPerfMonServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPerfMonServices));

            var vtbl = *(ADLXVTables.IADLXPerformanceMonitoringServicesVtbl**)pPerfMonServices;
            var getFn = (ADLXVTables.GetIntValueFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GetSamplingInterval, typeof(ADLXVTables.GetIntValueFn));
            int interval = 0;
            var result = getFn(pPerfMonServices, &interval);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get sampling interval");
            return interval;
        }

        public static void SetSamplingInterval(IntPtr pPerfMonServices, int intervalMs)
        {
            if (pPerfMonServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPerfMonServices));

            var vtbl = *(ADLXVTables.IADLXPerformanceMonitoringServicesVtbl**)pPerfMonServices;
            var setFn = (ADLXVTables.SetIntValueFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->SetSamplingInterval, typeof(ADLXVTables.SetIntValueFn));
            var result = setFn(pPerfMonServices, intervalMs);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set sampling interval");
        }

        public static ADLX_IntRange GetMaxHistorySizeRange(IntPtr pPerfMonServices)
        {
            if (pPerfMonServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPerfMonServices));

            // Max history size range isn't exposed directly; use max getter as upper bound and 0 as min
            int max = GetMaxPerformanceMetricsHistorySize(pPerfMonServices);
            return new ADLX_IntRange { minValue = 0, maxValue = max, step = 1 };
        }

        public static int GetMaxPerformanceMetricsHistorySize(IntPtr pPerfMonServices)
        {
            if (pPerfMonServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPerfMonServices));

            var vtbl = *(ADLXVTables.IADLXPerformanceMonitoringServicesVtbl**)pPerfMonServices;
            var getFn = (ADLXVTables.GetIntValueFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GetMaxPerformanceMetricsHistorySize, typeof(ADLXVTables.GetIntValueFn));
            int size = 0;
            var result = getFn(pPerfMonServices, &size);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get max performance metrics history size");
            return size;
        }

        public static int GetCurrentPerformanceMetricsHistorySize(IntPtr pPerfMonServices)
        {
            if (pPerfMonServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPerfMonServices));

            var vtbl = *(ADLXVTables.IADLXPerformanceMonitoringServicesVtbl**)pPerfMonServices;
            var getFn = (ADLXVTables.GetIntValueFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GetCurrentPerformanceMetricsHistorySize, typeof(ADLXVTables.GetIntValueFn));
            int size = 0;
            var result = getFn(pPerfMonServices, &size);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get current performance metrics history size");
            return size;
        }

        public static void SetMaxPerformanceMetricsHistorySize(IntPtr pPerfMonServices, int sizeSec)
        {
            if (pPerfMonServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPerfMonServices));

            var vtbl = *(ADLXVTables.IADLXPerformanceMonitoringServicesVtbl**)pPerfMonServices;
            var setFn = (ADLXVTables.SetIntValueFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->SetMaxPerformanceMetricsHistorySize, typeof(ADLXVTables.SetIntValueFn));
            var result = setFn(pPerfMonServices, sizeSec);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set max performance metrics history size");
        }

        public static void ClearPerformanceMetricsHistory(IntPtr pPerfMonServices)
        {
            if (pPerfMonServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPerfMonServices));

            var vtbl = *(ADLXVTables.IADLXPerformanceMonitoringServicesVtbl**)pPerfMonServices;
            var clearFn = (ADLXVTables.InvokeFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->ClearPerformanceMetricsHistory, typeof(ADLXVTables.InvokeFn));
            var result = clearFn(pPerfMonServices);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to clear performance metrics history");
        }

        public static AdlxInterfaceHandle GetGPUMetricsHistory(IntPtr pPerfMonServices, IntPtr pGPU, int startMs, int stopMs)
        {
            if (pPerfMonServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPerfMonServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXPerformanceMonitoringServicesVtbl**)pPerfMonServices;
            var getFn = (ADLXVTables.GetGPUMetricsHistoryFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GetGPUMetricsHistory, typeof(ADLXVTables.GetGPUMetricsHistoryFn));
            IntPtr list;
            var result = getFn(pPerfMonServices, pGPU, startMs, stopMs, &list);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get GPU metrics history");
            return AdlxInterfaceHandle.From(list);
        }

        public static void StartPerformanceMetricsTracking(IntPtr pPerfMonServices)
        {
            if (pPerfMonServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPerfMonServices));
            var vtbl = *(ADLXVTables.IADLXPerformanceMonitoringServicesVtbl**)pPerfMonServices;
            var startFn = (ADLXVTables.InvokeFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->StartPerformanceMetricsTracking, typeof(ADLXVTables.InvokeFn));
            var result = startFn(pPerfMonServices);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to start performance metrics tracking");
        }

        public static void StopPerformanceMetricsTracking(IntPtr pPerfMonServices)
        {
            if (pPerfMonServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPerfMonServices));
            var vtbl = *(ADLXVTables.IADLXPerformanceMonitoringServicesVtbl**)pPerfMonServices;
            var stopFn = (ADLXVTables.InvokeFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->StopPerformanceMetricsTracking, typeof(ADLXVTables.InvokeFn));
            var result = stopFn(pPerfMonServices);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to stop performance metrics tracking");
        }

        public static AdlxInterfaceHandle GetAllMetricsHistory(IntPtr pPerfMonServices, int startMs, int stopMs)
        {
            if (pPerfMonServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPerfMonServices));

            var vtbl = *(ADLXVTables.IADLXPerformanceMonitoringServicesVtbl**)pPerfMonServices;
            var getFn = (ADLXVTables.GetAllMetricsHistoryFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GetAllMetricsHistory, typeof(ADLXVTables.GetAllMetricsHistoryFn));
            IntPtr list;
            var result = getFn(pPerfMonServices, startMs, stopMs, &list);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get all metrics history");
            return AdlxInterfaceHandle.From(list);
        }

        public static AdlxInterfaceHandle GetSystemMetricsHistory(IntPtr pPerfMonServices, int startMs, int stopMs)
        {
            if (pPerfMonServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPerfMonServices));

            var vtbl = *(ADLXVTables.IADLXPerformanceMonitoringServicesVtbl**)pPerfMonServices;
            var getFn = (ADLXVTables.GetSystemMetricsHistoryFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GetSystemMetricsHistory, typeof(ADLXVTables.GetSystemMetricsHistoryFn));
            IntPtr list;
            var result = getFn(pPerfMonServices, startMs, stopMs, &list);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get system metrics history");
            return AdlxInterfaceHandle.From(list);
        }

        public static AdlxInterfaceHandle GetCurrentAllMetrics(IntPtr pPerfMonServices)
        {
            if (pPerfMonServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPerfMonServices));

            var vtbl = *(ADLXVTables.IADLXPerformanceMonitoringServicesVtbl**)pPerfMonServices;
            var getFn = (ADLXVTables.GetAllMetricsHistoryFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GetCurrentAllMetrics, typeof(ADLXVTables.GetAllMetricsHistoryFn));
            IntPtr pAll;
            var result = getFn(pPerfMonServices, 0, 0, &pAll);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get current all metrics");
            return AdlxInterfaceHandle.From(pAll);
        }

        /// <summary>
        /// Get supported GPU metrics for a GPU
        /// </summary>
        public static IntPtr GetSupportedGPUMetrics(IntPtr pPerfMonServices, IntPtr pGPU)
        {
            if (pPerfMonServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPerfMonServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXPerformanceMonitoringServicesVtbl**)pPerfMonServices;
            var getSupportedMetricsFn = (ADLXVTables.GetSupportedGPUMetricsFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GetSupportedGPUMetrics, typeof(ADLXVTables.GetSupportedGPUMetricsFn));

            IntPtr pMetricsSupport;
            var result = getSupportedMetricsFn(pPerfMonServices, pGPU, &pMetricsSupport);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get supported GPU metrics");
            }

            return pMetricsSupport;
        }

        /// <summary>
        /// Get current GPU metrics for a GPU
        /// </summary>
        public static IntPtr GetCurrentGPUMetrics(IntPtr pPerfMonServices, IntPtr pGPU)
        {
            if (pPerfMonServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPerfMonServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXPerformanceMonitoringServicesVtbl**)pPerfMonServices;
            var getCurrentMetricsFn = (ADLXVTables.GetCurrentGPUMetricsFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GetCurrentGPUMetrics, typeof(ADLXVTables.GetCurrentGPUMetricsFn));

            IntPtr pMetrics;
            var result = getCurrentMetricsFn(pPerfMonServices, pGPU, &pMetrics);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get current GPU metrics");
            }

            return pMetrics;
        }

        /// <summary>
        /// Check if GPU usage metric is supported
        /// </summary>
        public static bool IsSupportedGPUUsage(IntPtr pMetricsSupport)
        {
            if (pMetricsSupport == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetricsSupport));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsSupportVtbl**)pMetricsSupport;
            var isSupportedFn = (ADLXVTables.IsSupportedMetricFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedGPUUsage, typeof(ADLXVTables.IsSupportedMetricFn));

            byte supported;
            var result = isSupportedFn(pMetricsSupport, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check GPU usage support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if GPU clock speed metric is supported
        /// </summary>
        public static bool IsSupportedGPUClockSpeed(IntPtr pMetricsSupport)
        {
            if (pMetricsSupport == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetricsSupport));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsSupportVtbl**)pMetricsSupport;
            var isSupportedFn = (ADLXVTables.IsSupportedMetricFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedGPUClockSpeed, typeof(ADLXVTables.IsSupportedMetricFn));

            byte supported;
            var result = isSupportedFn(pMetricsSupport, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check GPU clock speed support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if GPU temperature metric is supported
        /// </summary>
        public static bool IsSupportedGPUTemperature(IntPtr pMetricsSupport)
        {
            if (pMetricsSupport == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetricsSupport));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsSupportVtbl**)pMetricsSupport;
            var isSupportedFn = (ADLXVTables.IsSupportedMetricFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedGPUTemperature, typeof(ADLXVTables.IsSupportedMetricFn));

            byte supported;
            var result = isSupportedFn(pMetricsSupport, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check GPU temperature support");
            }

            return supported != 0;
        }

        public static bool IsSupportedGPUHotspotTemperature(IntPtr pMetricsSupport)
        {
            if (pMetricsSupport == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetricsSupport));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsSupportVtbl**)pMetricsSupport;
            var isSupportedFn = (ADLXVTables.IsSupportedMetricFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedGPUHotspotTemperature, typeof(ADLXVTables.IsSupportedMetricFn));

            byte supported;
            var result = isSupportedFn(pMetricsSupport, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to check GPU hotspot temperature support");

            return supported != 0;
        }

        /// <summary>
        /// Check if GPU power metric is supported
        /// </summary>
        public static bool IsSupportedGPUPower(IntPtr pMetricsSupport)
        {
            if (pMetricsSupport == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetricsSupport));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsSupportVtbl**)pMetricsSupport;
            var isSupportedFn = (ADLXVTables.IsSupportedMetricFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedGPUPower, typeof(ADLXVTables.IsSupportedMetricFn));

            byte supported;
            var result = isSupportedFn(pMetricsSupport, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check GPU power support");
            }

            return supported != 0;
        }

        public static bool IsSupportedGPUVoltage(IntPtr pMetricsSupport)
        {
            if (pMetricsSupport == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetricsSupport));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsSupportVtbl**)pMetricsSupport;
            var isSupportedFn = (ADLXVTables.IsSupportedMetricFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedGPUVoltage, typeof(ADLXVTables.IsSupportedMetricFn));

            byte supported;
            var result = isSupportedFn(pMetricsSupport, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to check GPU voltage support");

            return supported != 0;
        }

        public static bool IsSupportedGPUTotalBoardPower(IntPtr pMetricsSupport)
        {
            if (pMetricsSupport == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetricsSupport));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsSupportVtbl**)pMetricsSupport;
            var isSupportedFn = (ADLXVTables.IsSupportedMetricFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedGPUTotalBoardPower, typeof(ADLXVTables.IsSupportedMetricFn));

            byte supported;
            var result = isSupportedFn(pMetricsSupport, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to check GPU total board power support");

            return supported != 0;
        }

        public static bool IsSupportedGPUVRAMClockSpeed(IntPtr pMetricsSupport)
        {
            if (pMetricsSupport == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetricsSupport));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsSupportVtbl**)pMetricsSupport;
            var isSupportedFn = (ADLXVTables.IsSupportedMetricFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedGPUVRAMClockSpeed, typeof(ADLXVTables.IsSupportedMetricFn));

            byte supported;
            var result = isSupportedFn(pMetricsSupport, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to check GPU VRAM clock support");

            return supported != 0;
        }

        /// <summary>
        /// Check if GPU fan speed metric is supported
        /// </summary>
        public static bool IsSupportedGPUFanSpeed(IntPtr pMetricsSupport)
        {
            if (pMetricsSupport == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetricsSupport));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsSupportVtbl**)pMetricsSupport;
            var isSupportedFn = (ADLXVTables.IsSupportedMetricFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedGPUFanSpeed, typeof(ADLXVTables.IsSupportedMetricFn));

            byte supported;
            var result = isSupportedFn(pMetricsSupport, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check GPU fan speed support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if GPU VRAM metric is supported
        /// </summary>
        public static bool IsSupportedGPUVRAM(IntPtr pMetricsSupport)
        {
            if (pMetricsSupport == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetricsSupport));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsSupportVtbl**)pMetricsSupport;
            var isSupportedFn = (ADLXVTables.IsSupportedMetricFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedGPUVRAM, typeof(ADLXVTables.IsSupportedMetricFn));

            byte supported;
            var result = isSupportedFn(pMetricsSupport, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check GPU VRAM support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Get GPU temperature from metrics
        /// </summary>
        public static double GetGPUTemperature(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsVtbl**)pMetrics;
            var temperatureFn = (ADLXVTables.GPUTemperatureFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GPUTemperature, typeof(ADLXVTables.GPUTemperatureFn));

            double temperature;
            var result = temperatureFn(pMetrics, &temperature);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU temperature");
            }

            return temperature;
        }

        /// <summary>
        /// Get GPU usage from metrics
        /// </summary>
        public static double GetGPUUsage(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsVtbl**)pMetrics;
            var usageFn = (ADLXVTables.GPUUsageFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GPUUsage, typeof(ADLXVTables.GPUUsageFn));

            double usage;
            var result = usageFn(pMetrics, &usage);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU usage");
            }

            return usage;
        }

        /// <summary>
        /// Get GPU clock speed from metrics
        /// </summary>
        public static int GetGPUClockSpeed(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsVtbl**)pMetrics;
            var clockSpeedFn = (ADLXVTables.GPUClockSpeedFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GPUClockSpeed, typeof(ADLXVTables.GPUClockSpeedFn));

            int clockSpeed;
            var result = clockSpeedFn(pMetrics, &clockSpeed);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU clock speed");
            }

            return clockSpeed;
        }

        /// <summary>
        /// Get GPU VRAM clock speed from metrics
        /// </summary>
        public static int GetGPUVRAMClockSpeed(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsVtbl**)pMetrics;
            var vramClockSpeedFn = (ADLXVTables.GPUClockSpeedFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GPUVRAMClockSpeed, typeof(ADLXVTables.GPUClockSpeedFn));

            int vramClockSpeed;
            var result = vramClockSpeedFn(pMetrics, &vramClockSpeed);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU VRAM clock speed");
            }

            return vramClockSpeed;
        }

        /// <summary>
        /// Get GPU VRAM usage from metrics
        /// </summary>
        public static int GetGPUVRAM(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsVtbl**)pMetrics;
            var vramFn = (ADLXVTables.GPUVRAMFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GPUVRAM, typeof(ADLXVTables.GPUVRAMFn));

            int vram;
            var result = vramFn(pMetrics, &vram);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU VRAM");
            }

            return vram;
        }

        /// <summary>
        /// Get GPU fan speed from metrics
        /// </summary>
        public static int GetGPUFanSpeed(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsVtbl**)pMetrics;
            var fanSpeedFn = (ADLXVTables.GPUClockSpeedFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GPUFanSpeed, typeof(ADLXVTables.GPUClockSpeedFn));

            int fanSpeed;
            var result = fanSpeedFn(pMetrics, &fanSpeed);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU fan speed");
            }

            return fanSpeed;
        }

        /// <summary>
        /// Get GPU power from metrics
        /// </summary>
        public static double GetGPUPower(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsVtbl**)pMetrics;
            var powerFn = (ADLXVTables.GPUPowerFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GPUPower, typeof(ADLXVTables.GPUPowerFn));

            double power;
            var result = powerFn(pMetrics, &power);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU power");
            }

            return power;
        }

        /// <summary>
        /// Get GPU hotspot temperature (if supported).
        /// </summary>
        public static double GetGPUHotspotTemperature(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsVtbl**)pMetrics;
            var tempFn = (ADLXVTables.GPUTemperatureFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GPUHotspotTemperature, typeof(ADLXVTables.GPUTemperatureFn));

            double temp;
            var result = tempFn(pMetrics, &temp);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get GPU hotspot temperature");
            return temp;
        }

        /// <summary>
        /// Get GPU voltage (if supported).
        /// </summary>
        public static int GetGPUVoltage(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsVtbl**)pMetrics;
            var voltFn = (ADLXVTables.GetIntValueFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GPUVoltage, typeof(ADLXVTables.GetIntValueFn));

            int voltage = 0;
            var result = voltFn(pMetrics, &voltage);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get GPU voltage");
            return voltage;
        }

        /// <summary>
        /// Get GPU total board power (if supported).
        /// </summary>
        public static double GetGPUTotalBoardPower(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsVtbl**)pMetrics;
            var powerFn = (ADLXVTables.GPUPowerFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GPUTotalBoardPower, typeof(ADLXVTables.GPUPowerFn));

            double value = 0;
            var result = powerFn(pMetrics, &value);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get GPU total board power");
            return value;
        }

    }

    /// <summary>
    /// Helper methods for desktop and Eyefinity services
    /// </summary>
    public static unsafe class ADLXDesktopHelpers
    {
        public static AdlxInterfaceHandle GetDesktopServicesHandle(IntPtr pSystem)
        {
            if (pSystem == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSystem));

            var systemVtbl = *(ADLXVTables.IADLXSystemVtbl**)pSystem;
            var getDesktopServicesFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetDesktopsServicesFn>(
                systemVtbl->GetDesktopsServices);

            IntPtr pDesktopServices;
            var result = getDesktopServicesFn(pSystem, &pDesktopServices);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get desktop services");
            }

            return AdlxInterfaceHandle.From(pDesktopServices);
        }

        public static IntPtr[] EnumerateAllDesktops(IntPtr pDesktopServices)
        {
            if (pDesktopServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDesktopServices));

            var svcVtbl = *(ADLXVTables.IADLXDesktopServicesVtbl**)pDesktopServices;
            var getDesktopsFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetDesktopsFn>(
                svcVtbl->GetDesktops);

            IntPtr pDesktopList;
            var result = getDesktopsFn(pDesktopServices, &pDesktopList);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get desktop list");
            }

            if (pDesktopList == IntPtr.Zero)
            {
                return Array.Empty<IntPtr>();
            }

            try
            {
                var listVtbl = *(ADLXVTables.IADLXDesktopListVtbl**)pDesktopList;
                var sizeFn = (ADLXVTables.SizeFn)Marshal.GetDelegateForFunctionPointer(listVtbl->Size, typeof(ADLXVTables.SizeFn));
                var atFn = (ADLXVTables.AtFn)Marshal.GetDelegateForFunctionPointer(listVtbl->At, typeof(ADLXVTables.AtFn));

                uint count = sizeFn(pDesktopList);
                if (count == 0)
                {
                    return Array.Empty<IntPtr>();
                }

                var desktops = new IntPtr[count];
                for (uint i = 0; i < count; i++)
                {
                    IntPtr pDesktop;
                    result = atFn(pDesktopList, i, &pDesktop);
                    if (result != ADLX_RESULT.ADLX_OK)
                    {
                        throw new ADLXException(result, $"Failed to get desktop at index {i}");
                    }
                    desktops[i] = pDesktop;
                }

                return desktops;
            }
            finally
            {
                ADLXHelpers.ReleaseInterface(pDesktopList);
            }
        }

        public static AdlxInterfaceHandle[] EnumerateAllDesktopHandles(IntPtr pDesktopServices)
        {
            var raw = EnumerateAllDesktops(pDesktopServices);
            var handles = new AdlxInterfaceHandle[raw.Length];
            for (int i = 0; i < raw.Length; i++)
            {
                handles[i] = AdlxInterfaceHandle.From(raw[i]);
            }
            return handles;
        }

        public static ADLX_DESKTOP_TYPE GetDesktopType(IntPtr pDesktop)
        {
            if (pDesktop == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDesktop));

            var vtbl = *(ADLXVTables.IADLXDesktopVtbl**)pDesktop;
            var typeFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.DesktopTypeFn>(vtbl->Type);

            ADLX_DESKTOP_TYPE type;
            var result = typeFn(pDesktop, &type);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get desktop type");
            }

            return type;
        }

        public static (int Width, int Height) GetDesktopSize(IntPtr pDesktop)
        {
            if (pDesktop == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDesktop));

            var vtbl = *(ADLXVTables.IADLXDesktopVtbl**)pDesktop;
            var sizeFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.DesktopSizeFn>(vtbl->Size);

            int w, h;
            var result = sizeFn(pDesktop, &w, &h);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get desktop size");
            }

            return (w, h);
        }

        public static (int X, int Y) GetDesktopTopLeft(IntPtr pDesktop)
        {
            if (pDesktop == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDesktop));

            var vtbl = *(ADLXVTables.IADLXDesktopVtbl**)pDesktop;
            var topLeftFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.DesktopTopLeftFn>(vtbl->TopLeft);

            ADLX_Point pt;
            var result = topLeftFn(pDesktop, &pt);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get desktop top-left");
            }

            return (pt.x, pt.y);
        }

        public static ADLX_ORIENTATION GetDesktopOrientation(IntPtr pDesktop)
        {
            if (pDesktop == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDesktop));

            var vtbl = *(ADLXVTables.IADLXDesktopVtbl**)pDesktop;
            var orientFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.DesktopOrientationFn>(vtbl->Orientation);

            ADLX_ORIENTATION orientation;
            var result = orientFn(pDesktop, &orientation);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get desktop orientation");
            }

            return orientation;
        }

        public static AdlxInterfaceHandle GetSimpleEyefinityHandle(IntPtr pDesktopServices)
        {
            if (pDesktopServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDesktopServices));

            var vtbl = *(ADLXVTables.IADLXDesktopServicesVtbl**)pDesktopServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetSimpleEyefinityFn>(vtbl->GetSimpleEyefinity);

            IntPtr pSimple;
            var result = getFn(pDesktopServices, &pSimple);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get simple Eyefinity interface");
            }

            return AdlxInterfaceHandle.From(pSimple);
        }

        public static bool IsSimpleEyefinitySupported(IntPtr pSimpleEyefinity)
        {
            if (pSimpleEyefinity == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSimpleEyefinity));

            var vtbl = *(ADLXVTables.IADLXSimpleEyefinityVtbl**)pSimpleEyefinity;
            var supportedFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.EyefinityIsSupportedFn>(vtbl->IsSupported);

            byte supported;
            var result = supportedFn(pSimpleEyefinity, &supported);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to query Eyefinity support");
            }

            return supported != 0;
        }

        public static AdlxInterfaceHandle CreateEyefinityDesktop(IntPtr pSimpleEyefinity)
        {
            if (pSimpleEyefinity == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSimpleEyefinity));

            var vtbl = *(ADLXVTables.IADLXSimpleEyefinityVtbl**)pSimpleEyefinity;
            var createFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.EyefinityCreateFn>(vtbl->Create);

            IntPtr pDesktop;
            var result = createFn(pSimpleEyefinity, &pDesktop);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to create Eyefinity desktop");
            }

            return AdlxInterfaceHandle.From(pDesktop);
        }

        public static void DestroyEyefinityDesktop(IntPtr pSimpleEyefinity, IntPtr pEyefinityDesktop)
        {
            if (pSimpleEyefinity == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSimpleEyefinity));
            if (pEyefinityDesktop == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pEyefinityDesktop));

            var vtbl = *(ADLXVTables.IADLXSimpleEyefinityVtbl**)pSimpleEyefinity;
            var destroyFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.EyefinityDestroyFn>(vtbl->Destroy);

            var result = destroyFn(pSimpleEyefinity, pEyefinityDesktop);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to destroy Eyefinity desktop");
            }
        }

        public static (uint rows, uint cols) GetEyefinityGridSize(IntPtr pEyefinityDesktop)
        {
            if (pEyefinityDesktop == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pEyefinityDesktop));

            var vtbl = *(ADLXVTables.IADLXEyefinityDesktopVtbl**)pEyefinityDesktop;
            var gridFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.EyefinityGridSizeFn>(vtbl->GridSize);
            uint rows = 0, cols = 0;
            var result = gridFn(pEyefinityDesktop, &rows, &cols);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query Eyefinity grid size");
            return (rows, cols);
        }

        public static AdlxInterfaceHandle GetEyefinityDisplay(IntPtr pEyefinityDesktop, uint row, uint col)
        {
            if (pEyefinityDesktop == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pEyefinityDesktop));

            var vtbl = *(ADLXVTables.IADLXEyefinityDesktopVtbl**)pEyefinityDesktop;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.EyefinityGetDisplayFn>(vtbl->GetDisplay);
            IntPtr pDisplay;
            var result = getFn(pEyefinityDesktop, row, col, &pDisplay);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, $"Failed to get Eyefinity display at {row},{col}");
            return AdlxInterfaceHandle.From(pDisplay);
        }

        public static (ADLX_ORIENTATION orientation, int width, int height) GetEyefinityDisplayInfo(IntPtr pEyefinityDesktop, uint row, uint col)
        {
            if (pEyefinityDesktop == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pEyefinityDesktop));

            var vtbl = *(ADLXVTables.IADLXEyefinityDesktopVtbl**)pEyefinityDesktop;
            var orientFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.EyefinityDisplayOrientationFn>(vtbl->DisplayOrientation);
            var sizeFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.EyefinityDisplaySizeFn>(vtbl->DisplaySize);

            ADLX_ORIENTATION orientation = default;
            int w = 0, h = 0;
            var r1 = orientFn(pEyefinityDesktop, row, col, &orientation);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, $"Failed to get Eyefinity orientation at {row},{col}");

            var r2 = sizeFn(pEyefinityDesktop, row, col, &w, &h);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, $"Failed to get Eyefinity display size at {row},{col}");

            return (orientation, w, h);
        }

        public static void DestroyAllEyefinityDesktops(IntPtr pSimpleEyefinity)
        {
            if (pSimpleEyefinity == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSimpleEyefinity));

            var vtbl = *(ADLXVTables.IADLXSimpleEyefinityVtbl**)pSimpleEyefinity;
            var destroyAllFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.EyefinityDestroyAllFn>(vtbl->DestroyAll);

            var result = destroyAllFn(pSimpleEyefinity);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to destroy all Eyefinity desktops");
            }
        }
    }

    /// <summary>
    /// Display settings helpers (FreeSync, GPU scaling, scaling mode, color depth, pixel format, VSR, integer scaling, HDCP, VariBright, display blanking, custom color).
    /// </summary>
    public static unsafe class ADLXDisplaySettingsHelpers
    {
        public static AdlxInterfaceHandle GetGammaHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetGammaFn>(vtbl->GetGamma);
            IntPtr pGamma;
            var result = getFn(pDisplayServices, pDisplay, &pGamma);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get Gamma interface");
            return AdlxInterfaceHandle.From(pGamma);
        }

        public static (bool reGammaRamp, bool deGammaRamp, bool reGammaCoeff, bool currentSRGB, bool currentBT709, bool currentPQ, bool currentPQ2084Interim, bool current36) GetGammaState(IntPtr pGamma)
        {
            if (pGamma == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGamma));

            var vtbl = *(ADLXVTables.IADLXDisplayGammaVtbl**)pGamma;
            var boolFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>;

            byte a = 0, b = 0, c = 0, d = 0, e = 0, f = 0, g = 0;
            boolFn(vtbl->IsCurrentReGammaRamp)(pGamma, &a);
            boolFn(vtbl->IsCurrentDeGammaRamp)(pGamma, &b);
            boolFn(vtbl->IsCurrentRegammaCoefficient)(pGamma, &c);
            boolFn(vtbl->IsCurrentReGammaSRGB)(pGamma, &d);
            boolFn(vtbl->IsCurrentReGammaBT709)(pGamma, &e);
            boolFn(vtbl->IsCurrentReGammaPQ)(pGamma, &f);
            boolFn(vtbl->IsCurrentReGammaPQ2084Interim)(pGamma, &g);
            byte h = 0;
            boolFn(vtbl->IsCurrentReGamma36)(pGamma, &h);

            return (a != 0, b != 0, c != 0, d != 0, e != 0, f != 0, g != 0, h != 0);
        }

        public static void ReapplyGamma(IntPtr pGamma)
        {
            if (pGamma == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGamma));

            var vtbl = *(ADLXVTables.IADLXDisplayGammaVtbl**)pGamma;
            var boolFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>;
            var invokeFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.InvokeFn>;

            byte srgb = 0, bt709 = 0, pq = 0, pq2084 = 0, g36 = 0, coeff = 0, reRamp = 0, deRamp = 0;
            boolFn(vtbl->IsCurrentReGammaSRGB)(pGamma, &srgb);
            boolFn(vtbl->IsCurrentReGammaBT709)(pGamma, &bt709);
            boolFn(vtbl->IsCurrentReGammaPQ)(pGamma, &pq);
            boolFn(vtbl->IsCurrentReGammaPQ2084Interim)(pGamma, &pq2084);
            boolFn(vtbl->IsCurrentReGamma36)(pGamma, &g36);
            boolFn(vtbl->IsCurrentRegammaCoefficient)(pGamma, &coeff);
            boolFn(vtbl->IsCurrentReGammaRamp)(pGamma, &reRamp);
            boolFn(vtbl->IsCurrentDeGammaRamp)(pGamma, &deRamp);

            if (srgb != 0 || bt709 != 0 || pq != 0 || pq2084 != 0 || g36 != 0)
            {
                var target = srgb != 0 ? vtbl->SetReGammaSRGB :
                             bt709 != 0 ? vtbl->SetReGammaBT709 :
                             pq != 0 ? vtbl->SetReGammaPQ :
                             pq2084 != 0 ? vtbl->SetReGammaPQ2084Interim :
                             vtbl->SetReGamma36;
                var r = invokeFn(target)(pGamma);
                if (r != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r, "Failed to reapply preset gamma");
                return;
            }

            if (coeff != 0)
            {
                ADLX_RegammaCoeff current = default;
                var getCoeffFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetGammaCoeffFn>(vtbl->GetGammaCoefficient);
                var setCoeffFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetGammaCoeffFn>(vtbl->SetReGammaCoefficient);
                var r = getCoeffFn(pGamma, &current);
                if (r != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r, "Failed to read current gamma coefficient");
                r = setCoeffFn(pGamma, current);
                if (r != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r, "Failed to reapply gamma coefficient");
                return;
            }

            var getRampFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetGammaRampFn>(vtbl->GetGammaRamp);
            if (reRamp != 0)
            {
                ADLX_GammaRamp ramp = default;
                var r = getRampFn(pGamma, &ramp);
                if (r != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r, "Failed to read current re-gamma ramp");
                var setRampFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetGammaRampFn>(vtbl->SetReGammaRamp);
                r = setRampFn(pGamma, ramp);
                if (r != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r, "Failed to reapply re-gamma ramp");
                return;
            }

            if (deRamp != 0)
            {
                ADLX_GammaRamp ramp = default;
                var r = getRampFn(pGamma, &ramp);
                if (r != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r, "Failed to read current de-gamma ramp");
                var setRampFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetGammaRampFn>(vtbl->SetDeGammaRamp);
                r = setRampFn(pGamma, ramp);
                if (r != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r, "Failed to reapply de-gamma ramp");
                return;
            }

            var resetFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.InvokeFn>(vtbl->ResetGammaRamp);
            var reset = resetFn(pGamma);
            if (reset != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(reset, "Failed to reset gamma ramp");
        }

        public static AdlxInterfaceHandle GetGamutHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetGamutFn>(vtbl->GetGamut);
            IntPtr pGamut;
            var result = getFn(pDisplayServices, pDisplay, &pGamut);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get Gamut interface");
            return AdlxInterfaceHandle.From(pGamut);
        }

        public static (ADLX_GamutColorSpace gamut, bool whitePoint5000K, bool whitePoint6500K, bool whitePoint7500K, bool whitePoint9300K, bool customWhitePoint, bool bt2020Supported, bool adobeSupported) GetGamutState(IntPtr pGamut)
        {
            if (pGamut == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGamut));

            var vtbl = *(ADLXVTables.IADLXDisplayGamutVtbl**)pGamut;
            var boolFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>;
            ADLX_GamutColorSpace gamut = default;
            var getSpaceFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetGamutColorSpaceFn>(vtbl->GetGamutColorSpace);
            var r = getSpaceFn(pGamut, &gamut);
            if (r != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r, "Failed to get gamut color space");

            byte w5000 = 0, w6500 = 0, w7500 = 0, w9300 = 0, wCustom = 0, bt2020 = 0, adobe = 0;
            boolFn(vtbl->IsCurrent5000kWhitePoint)(pGamut, &w5000);
            boolFn(vtbl->IsCurrent6500kWhitePoint)(pGamut, &w6500);
            boolFn(vtbl->IsCurrent7500kWhitePoint)(pGamut, &w7500);
            boolFn(vtbl->IsCurrent9300kWhitePoint)(pGamut, &w9300);
            boolFn(vtbl->IsCurrentCustomWhitePoint)(pGamut, &wCustom);
            boolFn(vtbl->IsSupportedCCIR2020ColorSpace)(pGamut, &bt2020);
            boolFn(vtbl->IsSupportedAdobeRgbColorSpace)(pGamut, &adobe);

            return (gamut, w5000 != 0, w6500 != 0, w7500 != 0, w9300 != 0, wCustom != 0, bt2020 != 0, adobe != 0);
        }

        public static void ReapplyGamut(IntPtr pGamut)
        {
            if (pGamut == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGamut));

            var vtbl = *(ADLXVTables.IADLXDisplayGamutVtbl**)pGamut;
            var boolFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>;

            byte cur5000 = 0, cur6500 = 0, cur7500 = 0, cur9300 = 0, curCustomWhite = 0;
            boolFn(vtbl->IsCurrent5000kWhitePoint)(pGamut, &cur5000);
            boolFn(vtbl->IsCurrent6500kWhitePoint)(pGamut, &cur6500);
            boolFn(vtbl->IsCurrent7500kWhitePoint)(pGamut, &cur7500);
            boolFn(vtbl->IsCurrent9300kWhitePoint)(pGamut, &cur9300);
            boolFn(vtbl->IsCurrentCustomWhitePoint)(pGamut, &curCustomWhite);

            byte cur709 = 0, cur601 = 0, curAdobe = 0, curCIERgb = 0, cur2020 = 0, curCustomSpace = 0;
            boolFn(vtbl->IsCurrentCCIR709ColorSpace)(pGamut, &cur709);
            boolFn(vtbl->IsCurrentCCIR601ColorSpace)(pGamut, &cur601);
            boolFn(vtbl->IsCurrentAdobeRgbColorSpace)(pGamut, &curAdobe);
            boolFn(vtbl->IsCurrentCIERgbColorSpace)(pGamut, &curCIERgb);
            boolFn(vtbl->IsCurrentCCIR2020ColorSpace)(pGamut, &cur2020);
            boolFn(vtbl->IsCurrentCustomColorSpace)(pGamut, &curCustomSpace);

            ADLX_GamutColorSpace currentSpace = default;
            var getSpaceFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetGamutColorSpaceFn>(vtbl->GetGamutColorSpace);
            var r = getSpaceFn(pGamut, &currentSpace);
            if (r != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r, "Failed to read current gamut color space");

            ADLX_Point whitePoint = default;
            var whiteResult = ADLX_RESULT.ADLX_FAIL;
            if (vtbl->GetWhitePoint != IntPtr.Zero)
            {
                var getWpFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetWhitePointFn>(vtbl->GetWhitePoint);
                whiteResult = getWpFn(pGamut, &whitePoint);
            }
            var hasCustomWhitePoint = whiteResult == ADLX_RESULT.ADLX_OK;

            ADLX_WHITE_POINT? wp = cur5000 != 0 ? ADLX_WHITE_POINT.WHITE_POINT_5000K :
                                   cur6500 != 0 ? ADLX_WHITE_POINT.WHITE_POINT_6500K :
                                   cur7500 != 0 ? ADLX_WHITE_POINT.WHITE_POINT_7500K :
                                   cur9300 != 0 ? ADLX_WHITE_POINT.WHITE_POINT_9300K :
                                   (ADLX_WHITE_POINT?)null;

            ADLX_GAMUT_SPACE? gamutSpace = cur709 != 0 ? ADLX_GAMUT_SPACE.GAMUT_SPACE_CCIR_709 :
                                         cur601 != 0 ? ADLX_GAMUT_SPACE.GAMUT_SPACE_CCIR_601 :
                                         curAdobe != 0 ? ADLX_GAMUT_SPACE.GAMUT_SPACE_ADOBE_RGB :
                                         curCIERgb != 0 ? ADLX_GAMUT_SPACE.GAMUT_SPACE_CIE_RGB :
                                         cur2020 != 0 ? ADLX_GAMUT_SPACE.GAMUT_SPACE_CCIR_2020 :
                                         (ADLX_GAMUT_SPACE?)null;

            if (gamutSpace.HasValue)
            {
                if (wp.HasValue)
                {
                    var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetGamutPredefinedWhitePredefinedFn>(vtbl->SetGamut_PredefinedWhite_PredefinedGamut);
                    var setResult = setFn(pGamut, wp.Value, gamutSpace.Value);
                    if (setResult != ADLX_RESULT.ADLX_OK)
                        throw new ADLXException(setResult, "Failed to reapply predefined gamut");
                    return;
                }

                if (hasCustomWhitePoint || curCustomWhite != 0)
                {
                    ADLX_RGB white = new ADLX_RGB { gamutR = whitePoint.x, gamutG = whitePoint.y, gamutB = 0 };
                    var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetGamutCustomWhitePredefinedFn>(vtbl->SetGamut_CustomWhite_PredefinedGamut);
                    var setResult = setFn(pGamut, white, gamutSpace.Value);
                    if (setResult != ADLX_RESULT.ADLX_OK)
                        throw new ADLXException(setResult, "Failed to reapply custom white point with predefined gamut");
                    return;
                }
            }

            if (wp.HasValue)
            {
                var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetGamutPredefinedWhiteCustomFn>(vtbl->SetGamut_PredefinedWhite_CustomGamut);
                var setResult = setFn(pGamut, wp.Value, currentSpace);
                if (setResult != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(setResult, "Failed to reapply custom gamut with predefined white point");
                return;
            }

            if (hasCustomWhitePoint || curCustomWhite != 0 || curCustomSpace != 0)
            {
                ADLX_RGB white = new ADLX_RGB { gamutR = whitePoint.x, gamutG = whitePoint.y, gamutB = 0 };
                var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetGamutCustomWhiteCustomFn>(vtbl->SetGamut_CustomWhite_CustomGamut);
                var setResult = setFn(pGamut, white, currentSpace);
                if (setResult != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(setResult, "Failed to reapply custom gamut");
                return;
            }

            throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Unable to determine current gamut configuration to reapply");
        }

        public static AdlxInterfaceHandle Get3DLUTHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.Get3DLUTFn>(vtbl->Get3DLUT);
            IntPtr pLut;
            var result = getFn(pDisplayServices, pDisplay, &pLut);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get 3DLUT interface");
            return AdlxInterfaceHandle.From(pLut);
        }

        public static (bool sceSupported, bool vividGamingSupported, bool currentDisabled, bool currentVividGaming) Get3DLUTState(IntPtr p3dLut)
        {
            if (p3dLut == IntPtr.Zero)
                throw new ArgumentNullException(nameof(p3dLut));

            var vtbl = *(ADLXVTables.IADLXDisplay3DLUTVtbl**)p3dLut;
            var boolFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>;
            byte supSce = 0, supVivid = 0, curDis = 0, curVivid = 0;
            boolFn(vtbl->IsSupportedSCE)(p3dLut, &supSce);
            boolFn(vtbl->IsSupportedSCEVividGaming)(p3dLut, &supVivid);
            boolFn(vtbl->IsCurrentSCEDisabled)(p3dLut, &curDis);
            boolFn(vtbl->IsCurrentSCEVividGaming)(p3dLut, &curVivid);

            return (supSce != 0, supVivid != 0, curDis != 0, curVivid != 0);
        }

        public static void Reapply3DLUT(IntPtr p3dLut)
        {
            if (p3dLut == IntPtr.Zero)
                throw new ArgumentNullException(nameof(p3dLut));

            var vtbl = *(ADLXVTables.IADLXDisplay3DLUTVtbl**)p3dLut;
            var boolFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>;
            var invokeFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.InvokeFn>;

            byte supSce = 0, supVivid = 0, curDis = 0, curVivid = 0;
            boolFn(vtbl->IsSupportedSCE)(p3dLut, &supSce);
            boolFn(vtbl->IsSupportedSCEVividGaming)(p3dLut, &supVivid);
            boolFn(vtbl->IsCurrentSCEDisabled)(p3dLut, &curDis);
            boolFn(vtbl->IsCurrentSCEVividGaming)(p3dLut, &curVivid);

            if (supSce != 0 || supVivid != 0)
            {
                var target = curVivid != 0 && supVivid != 0 ? vtbl->SetSCEVividGaming : vtbl->SetSCEDisabled;
                var r = invokeFn(target)(p3dLut);
                if (r != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r, "Failed to reapply SCE state");
            }

            byte supDynamic = 0;
            boolFn(vtbl->IsSupportedSCEDynamicContrast)(p3dLut, &supDynamic);
            if (supDynamic != 0)
            {
                ADLX_IntRange range = default;
                var getRangeFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetIntRangeFn>(vtbl->GetSCEDynamicContrastRange);
                var rangeResult = getRangeFn(p3dLut, &range);
                if (rangeResult != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(rangeResult, "Failed to query SCE dynamic contrast range");

                int currentContrast = 0;
                var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetIntValueFn>(vtbl->GetSCEDynamicContrast);
                var getResult = getFn(p3dLut, &currentContrast);
                if (getResult != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(getResult, "Failed to read SCE dynamic contrast");

                var clamped = Math.Clamp(currentContrast, range.minValue, range.maxValue);
                var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetIntValueFn>(vtbl->SetSCEDynamicContrast);
                var setResult = setFn(p3dLut, clamped);
                if (setResult != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(setResult, "Failed to reapply SCE dynamic contrast");
            }
        }

        public static AdlxInterfaceHandle GetDisplayConnectivityExperienceHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetDisplayConnectivityExperienceFn>(vtbl->GetDisplayConnectivityExperience);

            IntPtr pConn;
            var result = getFn(pDisplayServices, pDisplay, &pConn);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get Display Connectivity Experience interface");

            return AdlxInterfaceHandle.From(pConn);
        }

        public static (bool hdmiQdSupported, bool hdmiQdEnabled, bool dpLinkSupported, ADLX_DP_LINK_RATE dpLinkRate, uint activeLanes, uint totalLanes, int preEmphasis, int voltageSwing, bool linkProtectionEnabled) GetDisplayConnectivityExperienceState(IntPtr pConnectivity)
        {
            if (pConnectivity == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pConnectivity));

            var vtbl = *(ADLXVTables.IADLXDisplayConnectivityExperienceVtbl**)pConnectivity;
            var supHdmiFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupportedHDMIQualityDetection);
            var supDpFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupportedDPLink);
            var enHdmiFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsEnabledHDMIQualityDetection);
            var getLinkRateFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetDPLinkRateFn>(vtbl->GetDPLinkRate);
            var getActiveFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetUIntFn>(vtbl->GetNumberOfActiveLanes);
            var getTotalFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetUIntFn>(vtbl->GetNumberOfTotalLanes);
            var getPreFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetIntValueFn>(vtbl->GetRelativePreEmphasis);
            var getVoltFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetIntValueFn>(vtbl->GetRelativeVoltageSwing);
            var linkProtectFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsEnabledLinkProtection);

            byte supHdmi = 0, supDp = 0, enHdmi = 0, linkProtect = 0;
            var r1 = supHdmiFn(pConnectivity, &supHdmi);
            var r2 = supDpFn(pConnectivity, &supDp);
            var r3 = enHdmiFn(pConnectivity, &enHdmi);
            if (r1 != ADLX_RESULT.ADLX_OK) throw new ADLXException(r1, "Failed to query HDMI quality detection support");
            if (r2 != ADLX_RESULT.ADLX_OK) throw new ADLXException(r2, "Failed to query DP link support");
            if (r3 != ADLX_RESULT.ADLX_OK) throw new ADLXException(r3, "Failed to query HDMI quality detection enabled");

            ADLX_DP_LINK_RATE linkRate = default;
            getLinkRateFn(pConnectivity, &linkRate);

            uint active = 0, total = 0;
            getActiveFn(pConnectivity, &active);
            getTotalFn(pConnectivity, &total);

            int pre = 0, volt = 0;
            getPreFn(pConnectivity, &pre);
            getVoltFn(pConnectivity, &volt);

            linkProtectFn(pConnectivity, &linkProtect);

            return (supHdmi != 0, enHdmi != 0, supDp != 0, linkRate, active, total, pre, volt, linkProtect != 0);
        }

        public static void SetDisplayConnectivityHDMIQualityDetectionEnabled(IntPtr pConnectivity, bool enable)
        {
            if (pConnectivity == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pConnectivity));

            var vtbl = *(ADLXVTables.IADLXDisplayConnectivityExperienceVtbl**)pConnectivity;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetEnabledHDMIQualityDetection);
            var result = setFn(pConnectivity, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set HDMI quality detection");
        }

        public static void SetDisplayConnectivityRelativePreEmphasis(IntPtr pConnectivity, int value)
        {
            if (pConnectivity == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pConnectivity));
            var vtbl = *(ADLXVTables.IADLXDisplayConnectivityExperienceVtbl**)pConnectivity;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetIntValueFn>(vtbl->SetRelativePreEmphasis);
            var result = setFn(pConnectivity, value);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set relative pre-emphasis");
        }

        public static void SetDisplayConnectivityRelativeVoltageSwing(IntPtr pConnectivity, int value)
        {
            if (pConnectivity == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pConnectivity));
            var vtbl = *(ADLXVTables.IADLXDisplayConnectivityExperienceVtbl**)pConnectivity;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetIntValueFn>(vtbl->SetRelativeVoltageSwing);
            var result = setFn(pConnectivity, value);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set relative voltage swing");
        }

        public static AdlxInterfaceHandle GetCustomResolutionHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetCustomResolutionFn>(vtbl->GetCustomResolution);

            IntPtr pCustomRes;
            var result = getFn(pDisplayServices, pDisplay, &pCustomRes);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Custom Resolution interface");
            }

            return AdlxInterfaceHandle.From(pCustomRes);
        }

        public static (bool supported, ADLX_CustomResolution current) GetCustomResolutionState(IntPtr pCustomResolution)
        {
            if (pCustomResolution == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pCustomResolution));

            var vtbl = *(ADLXVTables.IADLXDisplayCustomResolutionVtbl**)pCustomResolution;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            byte supported = 0;
            var r1 = supFn(pCustomResolution, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query Custom Resolution support");

            ADLX_CustomResolution current = default;
            if (supported != 0)
            {
                IntPtr pRes;
                var getCurr = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetCurrentResolutionFn>(vtbl->GetCurrentAppliedResolution);
                var r2 = getCurr(pCustomResolution, &pRes);
                if (r2 == ADLX_RESULT.ADLX_OK && pRes != IntPtr.Zero)
                {
                    var resVtbl = *(ADLXVTables.IADLXDisplayResolutionVtbl**)pRes;
                    var getValue = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetCustomResolutionValueFn>(resVtbl->GetValue);
                    var r3 = getValue(pRes, &current);
                    Marshal.GetDelegateForFunctionPointer<ADLXVTables.ReleaseFn>(resVtbl->Release)(pRes);
                    if (r3 != ADLX_RESULT.ADLX_OK)
                        throw new ADLXException(r3, "Failed to read current custom resolution");
                }
            }

            return (supported != 0, current);
        }

        public static (bool supported, bool enabled, bool backlightAdaptiveSupported, bool backlightAdaptiveEnabled, bool batteryLifeSupported, bool batteryLifeEnabled) GetVariBright1State(IntPtr pVariBright)
        {
            if (pVariBright == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVariBright));

            var vtbl = *(ADLXVTables.IADLXDisplayVariBright1Vtbl**)pVariBright;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var enFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsEnabled);
            var supBAFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsBacklightAdaptiveSupported);
            var enBAFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsBacklightAdaptiveEnabled);
            var supBLFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsBatteryLifeSupported);
            var enBLFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsBatteryLifeEnabled);

            byte sup = 0, en = 0, supBA = 0, enBA = 0, supBL = 0, enBL = 0;
            supFn(pVariBright, &sup);
            enFn(pVariBright, &en);
            supBAFn(pVariBright, &supBA);
            enBAFn(pVariBright, &enBA);
            supBLFn(pVariBright, &supBL);
            enBLFn(pVariBright, &enBL);

            return (sup != 0, en != 0, supBA != 0, enBA != 0, supBL != 0, enBL != 0);
        }

        public static void SetVariBrightBacklightAdaptiveEnabled(IntPtr pVariBright, bool enable)
        {
            if (pVariBright == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVariBright));
            var vtbl = *(ADLXVTables.IADLXDisplayVariBright1Vtbl**)pVariBright;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetBacklightAdaptiveEnabled);
            var result = setFn(pVariBright, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set backlight adaptive mode");
        }

        public static void SetVariBrightBatteryLifeEnabled(IntPtr pVariBright, bool enable)
        {
            if (pVariBright == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVariBright));
            var vtbl = *(ADLXVTables.IADLXDisplayVariBright1Vtbl**)pVariBright;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetBatteryLifeEnabled);
            var result = setFn(pVariBright, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set battery life mode");
        }

        public static AdlxInterfaceHandle GetCustomColorHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetCustomColorFn>(vtbl->GetCustomColor);

            IntPtr pCustomColor;
            var result = getFn(pDisplayServices, pDisplay, &pCustomColor);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Custom Color interface");
            }

            return AdlxInterfaceHandle.From(pCustomColor);
        }

        public static (bool supported, int value, ADLX_IntRange range) GetCustomColorHue(IntPtr pCustomColor)
        {
            if (pCustomColor == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pCustomColor));

            var vtbl = *(ADLXVTables.IADLXDisplayCustomColorVtbl**)pCustomColor;
            return GetCustomColorIntProperty(pCustomColor, vtbl->IsHueSupported, vtbl->GetHueRange, vtbl->GetHue, "Hue");
        }

        public static void SetCustomColorHue(IntPtr pCustomColor, int hue)
        {
            if (pCustomColor == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pCustomColor));
            var vtbl = *(ADLXVTables.IADLXDisplayCustomColorVtbl**)pCustomColor;
            SetCustomColorIntProperty(pCustomColor, vtbl->SetHue, hue);
        }

        public static (bool supported, int value, ADLX_IntRange range) GetCustomColorSaturation(IntPtr pCustomColor)
        {
            if (pCustomColor == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pCustomColor));

            var vtbl = *(ADLXVTables.IADLXDisplayCustomColorVtbl**)pCustomColor;
            return GetCustomColorIntProperty(pCustomColor, vtbl->IsSaturationSupported, vtbl->GetSaturationRange, vtbl->GetSaturation, "Saturation");
        }

        public static void SetCustomColorSaturation(IntPtr pCustomColor, int saturation)
        {
            if (pCustomColor == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pCustomColor));
            var vtbl = *(ADLXVTables.IADLXDisplayCustomColorVtbl**)pCustomColor;
            SetCustomColorIntProperty(pCustomColor, vtbl->SetSaturation, saturation);
        }

        public static (bool supported, int value, ADLX_IntRange range) GetCustomColorBrightness(IntPtr pCustomColor)
        {
            if (pCustomColor == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pCustomColor));

            var vtbl = *(ADLXVTables.IADLXDisplayCustomColorVtbl**)pCustomColor;
            return GetCustomColorIntProperty(pCustomColor, vtbl->IsBrightnessSupported, vtbl->GetBrightnessRange, vtbl->GetBrightness, "Brightness");
        }

        public static void SetCustomColorBrightness(IntPtr pCustomColor, int brightness)
        {
            if (pCustomColor == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pCustomColor));
            var vtbl = *(ADLXVTables.IADLXDisplayCustomColorVtbl**)pCustomColor;
            SetCustomColorIntProperty(pCustomColor, vtbl->SetBrightness, brightness);
        }

        public static (bool supported, int value, ADLX_IntRange range) GetCustomColorContrast(IntPtr pCustomColor)
        {
            if (pCustomColor == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pCustomColor));

            var vtbl = *(ADLXVTables.IADLXDisplayCustomColorVtbl**)pCustomColor;
            return GetCustomColorIntProperty(pCustomColor, vtbl->IsContrastSupported, vtbl->GetContrastRange, vtbl->GetContrast, "Contrast");
        }

        public static void SetCustomColorContrast(IntPtr pCustomColor, int contrast)
        {
            if (pCustomColor == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pCustomColor));
            var vtbl = *(ADLXVTables.IADLXDisplayCustomColorVtbl**)pCustomColor;
            SetCustomColorIntProperty(pCustomColor, vtbl->SetContrast, contrast);
        }

        public static (bool supported, int value, ADLX_IntRange range) GetCustomColorTemperature(IntPtr pCustomColor)
        {
            if (pCustomColor == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pCustomColor));

            var vtbl = *(ADLXVTables.IADLXDisplayCustomColorVtbl**)pCustomColor;
            return GetCustomColorIntProperty(pCustomColor, vtbl->IsTemperatureSupported, vtbl->GetTemperatureRange, vtbl->GetTemperature, "Temperature");
        }

        public static void SetCustomColorTemperature(IntPtr pCustomColor, int temperature)
        {
            if (pCustomColor == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pCustomColor));
            var vtbl = *(ADLXVTables.IADLXDisplayCustomColorVtbl**)pCustomColor;
            SetCustomColorIntProperty(pCustomColor, vtbl->SetTemperature, temperature);
        }

        private static (bool supported, int value, ADLX_IntRange range) GetCustomColorIntProperty(IntPtr pCustomColor, IntPtr supportPtr, IntPtr rangePtr, IntPtr valuePtr, string name)
        {
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(supportPtr);
            byte supported = 0;
            var r1 = supFn(pCustomColor, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, $"Failed to query Custom Color support for {name}");

            ADLX_IntRange range = default;
            var rangeFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetIntRangeFn>(rangePtr);
            var r2 = rangeFn(pCustomColor, &range);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, $"Failed to query Custom Color range for {name}");

            int value = 0;
            var valFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetIntValueFn>(valuePtr);
            var r3 = valFn(pCustomColor, &value);
            if (r3 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r3, $"Failed to query Custom Color value for {name}");

            return (supported != 0, value, range);
        }

        private static void SetCustomColorIntProperty(IntPtr pCustomColor, IntPtr setterPtr, int value)
        {
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetIntValueFn>(setterPtr);
            var result = setFn(pCustomColor, value);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set Custom Color value");
        }

        public static AdlxInterfaceHandle GetDisplayBlankingHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetDisplayBlankingFn>(vtbl->GetDisplayBlanking);

            IntPtr pBlanking;
            var result = getFn(pDisplayServices, pDisplay, &pBlanking);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Display Blanking interface");
            }

            return AdlxInterfaceHandle.From(pBlanking);
        }

        public static (bool supported, bool blanked) GetDisplayBlankingState(IntPtr pBlanking)
        {
            if (pBlanking == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pBlanking));

            var vtbl = *(ADLXVTables.IADLXDisplayBlankingVtbl**)pBlanking;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var blankedFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsCurrentBlanked);

            byte supported = 0;
            byte blanked = 0;
            var r1 = supFn(pBlanking, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query Display Blanking support");

            var r2 = blankedFn(pBlanking, &blanked);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query current blanking state");

            return (supported != 0, blanked != 0);
        }

        public static void SetDisplayBlanked(IntPtr pBlanking, bool blank)
        {
            if (pBlanking == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pBlanking));

            var vtbl = *(ADLXVTables.IADLXDisplayBlankingVtbl**)pBlanking;
            if (blank)
            {
                var fn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.InvokeFn>(vtbl->SetBlanked);
                var result = fn(pBlanking);
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to set display blanked");
            }
            else
            {
                var fn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.InvokeFn>(vtbl->SetUnblanked);
                var result = fn(pBlanking);
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to set display unblanked");
            }
        }

        public static AdlxInterfaceHandle GetVirtualSuperResolutionHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetVirtualSuperResolutionFn>(vtbl->GetVirtualSuperResolution);

            IntPtr pVsr;
            var result = getFn(pDisplayServices, pDisplay, &pVsr);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Virtual Super Resolution interface");
            }

            return AdlxInterfaceHandle.From(pVsr);
        }

        public static (bool supported, bool enabled) GetVirtualSuperResolutionState(IntPtr pVsr)
        {
            if (pVsr == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVsr));

            var vtbl = *(ADLXVTables.IADLXDisplayVSRVtbl**)pVsr;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var enFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsEnabled);

            byte supported = 0;
            byte enabled = 0;

            var r1 = supFn(pVsr, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query VSR support");

            var r2 = enFn(pVsr, &enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query VSR enabled");

            return (supported != 0, enabled != 0);
        }

        public static void SetVirtualSuperResolutionEnabled(IntPtr pVsr, bool enable)
        {
            if (pVsr == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVsr));

            var vtbl = *(ADLXVTables.IADLXDisplayVSRVtbl**)pVsr;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetEnabled);
            var result = setFn(pVsr, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set VSR state");
        }

        public static AdlxInterfaceHandle GetIntegerScalingHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetIntegerScalingFn>(vtbl->GetIntegerScaling);

            IntPtr pIntegerScaling;
            var result = getFn(pDisplayServices, pDisplay, &pIntegerScaling);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Integer Scaling interface");
            }

            return AdlxInterfaceHandle.From(pIntegerScaling);
        }

        public static (bool supported, bool enabled) GetIntegerScalingState(IntPtr pIntegerScaling)
        {
            if (pIntegerScaling == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pIntegerScaling));

            var vtbl = *(ADLXVTables.IADLXDisplayIntegerScalingVtbl**)pIntegerScaling;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var enFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsEnabled);

            byte supported = 0;
            byte enabled = 0;

            var r1 = supFn(pIntegerScaling, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query Integer Scaling support");

            var r2 = enFn(pIntegerScaling, &enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query Integer Scaling enabled");

            return (supported != 0, enabled != 0);
        }

        public static void SetIntegerScalingEnabled(IntPtr pIntegerScaling, bool enable)
        {
            if (pIntegerScaling == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pIntegerScaling));

            var vtbl = *(ADLXVTables.IADLXDisplayIntegerScalingVtbl**)pIntegerScaling;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetEnabled);
            var result = setFn(pIntegerScaling, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set Integer Scaling state");
        }

        public static AdlxInterfaceHandle GetHDCPHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetHDCPFn>(vtbl->GetHDCP);

            IntPtr pHdcp;
            var result = getFn(pDisplayServices, pDisplay, &pHdcp);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get HDCP interface");
            }

            return AdlxInterfaceHandle.From(pHdcp);
        }

        public static (bool supported, bool enabled) GetHDCPState(IntPtr pHdcp)
        {
            if (pHdcp == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pHdcp));

            var vtbl = *(ADLXVTables.IADLXDisplayHDCPVtbl**)pHdcp;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var enFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsEnabled);

            byte supported = 0;
            byte enabled = 0;

            var r1 = supFn(pHdcp, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query HDCP support");

            var r2 = enFn(pHdcp, &enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query HDCP enabled");

            return (supported != 0, enabled != 0);
        }

        public static void SetHDCPEnabled(IntPtr pHdcp, bool enable)
        {
            if (pHdcp == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pHdcp));

            var vtbl = *(ADLXVTables.IADLXDisplayHDCPVtbl**)pHdcp;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetEnabled);
            var result = setFn(pHdcp, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set HDCP state");
        }

        public enum VariBrightMode
        {
            Unknown = 0,
            MaximizeBrightness,
            OptimizeBrightness,
            Balanced,
            OptimizeBattery,
            MaximizeBattery
        }

        public static AdlxInterfaceHandle GetVariBrightHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetVariBrightFn>(vtbl->GetVariBright);

            IntPtr pVariBright;
            var result = getFn(pDisplayServices, pDisplay, &pVariBright);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get VariBright interface");
            }

            return AdlxInterfaceHandle.From(pVariBright);
        }

        public static (bool supported, bool enabled, VariBrightMode mode) GetVariBrightState(IntPtr pVariBright)
        {
            if (pVariBright == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVariBright));

            var vtbl = *(ADLXVTables.IADLXDisplayVariBrightVtbl**)pVariBright;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var enFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsEnabled);

            byte supported = 0;
            byte enabled = 0;
            var r1 = supFn(pVariBright, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query VariBright support");

            var r2 = enFn(pVariBright, &enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query VariBright enabled");

            VariBrightMode mode = VariBrightMode.Unknown;
            if (supported != 0)
            {
                mode = DetectVariBrightMode(pVariBright, vtbl);
            }

            return (supported != 0, enabled != 0, mode);
        }

        private static VariBrightMode DetectVariBrightMode(IntPtr pVariBright, ADLXVTables.IADLXDisplayVariBrightVtbl* vtbl)
        {
            byte flag = 0;

            var maxBrightFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsCurrentMaximizeBrightness);
            var maxBright = maxBrightFn(pVariBright, &flag);
            if (maxBright == ADLX_RESULT.ADLX_OK && flag != 0) return VariBrightMode.MaximizeBrightness;

            flag = 0;
            var optBrightFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsCurrentOptimizeBrightness);
            var optBright = optBrightFn(pVariBright, &flag);
            if (optBright == ADLX_RESULT.ADLX_OK && flag != 0) return VariBrightMode.OptimizeBrightness;

            flag = 0;
            var balancedFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsCurrentBalanced);
            var balanced = balancedFn(pVariBright, &flag);
            if (balanced == ADLX_RESULT.ADLX_OK && flag != 0) return VariBrightMode.Balanced;

            flag = 0;
            var optBatteryFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsCurrentOptimizeBattery);
            var optBattery = optBatteryFn(pVariBright, &flag);
            if (optBattery == ADLX_RESULT.ADLX_OK && flag != 0) return VariBrightMode.OptimizeBattery;

            flag = 0;
            var maxBatteryFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsCurrentMaximizeBattery);
            var maxBattery = maxBatteryFn(pVariBright, &flag);
            if (maxBattery == ADLX_RESULT.ADLX_OK && flag != 0) return VariBrightMode.MaximizeBattery;

            return VariBrightMode.Unknown;
        }

        public static void SetVariBright(IntPtr pVariBright, bool enable, VariBrightMode mode)
        {
            if (pVariBright == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVariBright));

            var vtbl = *(ADLXVTables.IADLXDisplayVariBrightVtbl**)pVariBright;
            var setEnabledFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetEnabled);
            var result = setEnabledFn(pVariBright, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set VariBright enabled state");

            if (!enable)
                return;

            ADLX_RESULT modeResult = ADLX_RESULT.ADLX_OK;
            switch (mode)
            {
                case VariBrightMode.MaximizeBrightness:
                    modeResult = Marshal.GetDelegateForFunctionPointer<ADLXVTables.InvokeFn>(vtbl->SetMaximizeBrightness)(pVariBright);
                    break;
                case VariBrightMode.OptimizeBrightness:
                    modeResult = Marshal.GetDelegateForFunctionPointer<ADLXVTables.InvokeFn>(vtbl->SetOptimizeBrightness)(pVariBright);
                    break;
                case VariBrightMode.Balanced:
                    modeResult = Marshal.GetDelegateForFunctionPointer<ADLXVTables.InvokeFn>(vtbl->SetBalanced)(pVariBright);
                    break;
                case VariBrightMode.OptimizeBattery:
                    modeResult = Marshal.GetDelegateForFunctionPointer<ADLXVTables.InvokeFn>(vtbl->SetOptimizeBattery)(pVariBright);
                    break;
                case VariBrightMode.MaximizeBattery:
                    modeResult = Marshal.GetDelegateForFunctionPointer<ADLXVTables.InvokeFn>(vtbl->SetMaximizeBattery)(pVariBright);
                    break;
                default:
                    modeResult = ADLX_RESULT.ADLX_OK;
                    break;
            }

            if (modeResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(modeResult, "Failed to set VariBright mode");
        }

        public static AdlxInterfaceHandle GetColorDepthHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetColorDepthFn>(vtbl->GetColorDepth);

            IntPtr pColorDepth;
            var result = getFn(pDisplayServices, pDisplay, &pColorDepth);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Color Depth interface");
            }

            return AdlxInterfaceHandle.From(pColorDepth);
        }

        public static (bool supported, ADLX_COLOR_DEPTH current) GetColorDepthState(IntPtr pColorDepth)
        {
            if (pColorDepth == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pColorDepth));

            var vtbl = *(ADLXVTables.IADLXDisplayColorDepthVtbl**)pColorDepth;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetColorDepthValueFn>(vtbl->GetValue);

            byte supported = 0;
            ADLX_COLOR_DEPTH depth = default;

            var r1 = supFn(pColorDepth, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query Color Depth support");

            var r2 = getFn(pColorDepth, &depth);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query Color Depth value");

            return (supported != 0, depth);
        }

        public static void SetColorDepth(IntPtr pColorDepth, ADLX_COLOR_DEPTH depth)
        {
            if (pColorDepth == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pColorDepth));

            var vtbl = *(ADLXVTables.IADLXDisplayColorDepthVtbl**)pColorDepth;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetColorDepthValueFn>(vtbl->SetValue);
            var result = setFn(pColorDepth, depth);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set Color Depth");
        }

        public static AdlxInterfaceHandle GetPixelFormatHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetPixelFormatFn>(vtbl->GetPixelFormat);

            IntPtr pPixelFormat;
            var result = getFn(pDisplayServices, pDisplay, &pPixelFormat);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Pixel Format interface");
            }

            return AdlxInterfaceHandle.From(pPixelFormat);
        }

        public static (bool supported, ADLX_PIXEL_FORMAT current) GetPixelFormatState(IntPtr pPixelFormat)
        {
            if (pPixelFormat == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPixelFormat));

            var vtbl = *(ADLXVTables.IADLXDisplayPixelFormatVtbl**)pPixelFormat;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetPixelFormatValueFn>(vtbl->GetValue);

            byte supported = 0;
            ADLX_PIXEL_FORMAT format = default;

            var r1 = supFn(pPixelFormat, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query Pixel Format support");

            var r2 = getFn(pPixelFormat, &format);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query Pixel Format value");

            return (supported != 0, format);
        }

        public static void SetPixelFormat(IntPtr pPixelFormat, ADLX_PIXEL_FORMAT format)
        {
            if (pPixelFormat == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPixelFormat));

            var vtbl = *(ADLXVTables.IADLXDisplayPixelFormatVtbl**)pPixelFormat;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetPixelFormatValueFn>(vtbl->SetValue);
            var result = setFn(pPixelFormat, format);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set Pixel Format");
        }

        public static AdlxInterfaceHandle GetFreeSyncHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetFreeSyncFn>(vtbl->GetFreeSync);

            IntPtr pFS;
            var result = getFn(pDisplayServices, pDisplay, &pFS);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get FreeSync interface");
            }

            return AdlxInterfaceHandle.From(pFS);
        }

        public static AdlxInterfaceHandle GetGPUScalingHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetGPUScalingFn>(vtbl->GetGPUScaling);

            IntPtr pScaling;
            var result = getFn(pDisplayServices, pDisplay, &pScaling);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU scaling interface");
            }

            return AdlxInterfaceHandle.From(pScaling);
        }

        public static AdlxInterfaceHandle GetScalingModeHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetScalingModeFn>(vtbl->GetScalingMode);

            IntPtr pMode;
            var result = getFn(pDisplayServices, pDisplay, &pMode);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get scaling mode interface");
            }

            return AdlxInterfaceHandle.From(pMode);
        }

        public static (bool supported, bool enabled) GetFreeSyncState(IntPtr pFreeSync)
        {
            if (pFreeSync == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pFreeSync));

            var vtbl = *(ADLXVTables.IADLXDisplayFreeSyncVtbl**)pFreeSync;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var enFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsEnabled);

            byte supported;
            byte enabled = 0;
            var r1 = supFn(pFreeSync, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query FreeSync support");

            var r2 = enFn(pFreeSync, &enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query FreeSync enabled");

            return (supported != 0, enabled != 0);
        }

        public static void SetFreeSyncEnabled(IntPtr pFreeSync, bool enable)
        {
            if (pFreeSync == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pFreeSync));

            var vtbl = *(ADLXVTables.IADLXDisplayFreeSyncVtbl**)pFreeSync;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetEnabled);
            var result = setFn(pFreeSync, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to set FreeSync enabled state");
            }
        }

        public static (bool supported, bool enabled) GetGPUScalingState(IntPtr pGPUScaling)
        {
            if (pGPUScaling == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUScaling));

            var vtbl = *(ADLXVTables.IADLXDisplayGPUScalingVtbl**)pGPUScaling;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var enFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsEnabled);

            byte supported;
            byte enabled = 0;
            var r1 = supFn(pGPUScaling, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query GPU scaling support");

            var r2 = enFn(pGPUScaling, &enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query GPU scaling enabled");

            return (supported != 0, enabled != 0);
        }

        public static void SetGPUScalingEnabled(IntPtr pGPUScaling, bool enable)
        {
            if (pGPUScaling == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUScaling));

            var vtbl = *(ADLXVTables.IADLXDisplayGPUScalingVtbl**)pGPUScaling;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetEnabled);
            var result = setFn(pGPUScaling, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to set GPU scaling enabled state");
            }
        }

        public static (bool supported, ADLX_SCALE_MODE mode) GetScalingMode(IntPtr pScalingMode)
        {
            if (pScalingMode == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pScalingMode));

            var vtbl = *(ADLXVTables.IADLXDisplayScalingModeVtbl**)pScalingMode;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetScaleModeFn>(vtbl->GetMode);

            byte supported;
            var r1 = supFn(pScalingMode, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query scaling mode support");

            ADLX_SCALE_MODE mode;
            var r2 = getFn(pScalingMode, &mode);
            if (r2 != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(r2, "Failed to get scaling mode");
            }

            return (supported != 0, mode);
        }

        public static void SetScalingMode(IntPtr pScalingMode, ADLX_SCALE_MODE mode)
        {
            if (pScalingMode == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pScalingMode));

            var vtbl = *(ADLXVTables.IADLXDisplayScalingModeVtbl**)pScalingMode;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetScaleModeFn>(vtbl->SetMode);
            var result = setFn(pScalingMode, mode);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to set scaling mode");
            }
        }

        public static AdlxInterfaceHandle GetFreeSyncColorAccuracyHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetFreeSyncColorAccuracyFn>(vtbl->GetFreeSyncColorAccuracy);

            IntPtr pFSCA;
            var result = getFn(pDisplayServices, pDisplay, &pFSCA);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get FreeSync Color Accuracy interface");
            }

            return AdlxInterfaceHandle.From(pFSCA);
        }

        public static (bool supported, bool enabled) GetFreeSyncColorAccuracyState(IntPtr pFSCA)
        {
            if (pFSCA == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pFSCA));

            var vtbl = *(ADLXVTables.IADLXDisplayFreeSyncColorAccuracyVtbl**)pFSCA;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var enFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsEnabled);

            byte supported;
            byte enabled = 0;
            var r1 = supFn(pFSCA, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query FreeSync Color Accuracy support");

            var r2 = enFn(pFSCA, &enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query FreeSync Color Accuracy enabled");

            return (supported != 0, enabled != 0);
        }

        public static void SetFreeSyncColorAccuracyEnabled(IntPtr pFSCA, bool enable)
        {
            if (pFSCA == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pFSCA));

            var vtbl = *(ADLXVTables.IADLXDisplayFreeSyncColorAccuracyVtbl**)pFSCA;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetEnabled);
            var result = setFn(pFSCA, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to set FreeSync Color Accuracy state");
            }
        }

        public static AdlxInterfaceHandle GetDynamicRefreshRateControlHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetDynamicRefreshRateControlFn>(vtbl->GetDynamicRefreshRateControl);

            IntPtr pDRR;
            var result = getFn(pDisplayServices, pDisplay, &pDRR);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Dynamic Refresh Rate Control interface");
            }

            return AdlxInterfaceHandle.From(pDRR);
        }

        public static (bool supported, bool enabled) GetDynamicRefreshRateControlState(IntPtr pDRR)
        {
            if (pDRR == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDRR));

            var vtbl = *(ADLXVTables.IADLXDisplayDynamicRefreshRateControlVtbl**)pDRR;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var enFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsEnabled);

            byte supported;
            byte enabled = 0;
            var r1 = supFn(pDRR, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query DRR support");

            var r2 = enFn(pDRR, &enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query DRR enabled");

            return (supported != 0, enabled != 0);
        }

        public static void SetDynamicRefreshRateControlEnabled(IntPtr pDRR, bool enable)
        {
            if (pDRR == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDRR));

            var vtbl = *(ADLXVTables.IADLXDisplayDynamicRefreshRateControlVtbl**)pDRR;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetFn>(vtbl->SetEnabled);
            var result = setFn(pDRR, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to set DRR state");
            }
        }
    }

    /// <summary>
    /// Safe handle for an unmanaged IADLXDisplaySettingsChangedListener backed by a managed delegate.
    /// </summary>
    public sealed unsafe class DisplaySettingsListenerHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private static readonly ConcurrentDictionary<IntPtr, ADLXDisplayHelpers.DisplaySettingsChangedCallback> _map = new();
        private static readonly IntPtr _thunkPtr = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, byte>)&OnDisplaySettingsChanged;
        private readonly GCHandle _gcHandle;
        private readonly IntPtr _vtbl;

        private DisplaySettingsListenerHandle(ADLXDisplayHelpers.DisplaySettingsChangedCallback cb)
            : base(true)
        {
            _gcHandle = GCHandle.Alloc(cb);
            _vtbl = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(_vtbl, _thunkPtr);

            var inst = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(inst, _vtbl);
            handle = inst;
            _map[inst] = cb;
        }

        public static DisplaySettingsListenerHandle Create(ADLXDisplayHelpers.DisplaySettingsChangedCallback cb)
        {
            if (cb == null) throw new ArgumentNullException(nameof(cb));
            return new DisplaySettingsListenerHandle(cb);
        }

        protected override bool ReleaseHandle()
        {
            _map.TryRemove(handle, out _);
            if (_gcHandle.IsAllocated) _gcHandle.Free();
            if (_vtbl != IntPtr.Zero) Marshal.FreeHGlobal(_vtbl);
            if (handle != IntPtr.Zero) Marshal.FreeHGlobal(handle);
            return true;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static byte OnDisplaySettingsChanged(IntPtr pThis, IntPtr pEvent)
        {
            if (_map.TryGetValue(pThis, out var cb))
            {
                return cb(pEvent) ? (byte)1 : (byte)0;
            }
            return 0;
        }
    }
}
