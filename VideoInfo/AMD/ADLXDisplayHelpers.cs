using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Helper methods for ADLX Display operations.
    /// </summary>
    public static unsafe class ADLXDisplayHelpers
    {
        /// <summary>
        /// Gets the IADLXDisplayServices interface from the system services. Callers must dispose the returned pointer.
        /// </summary>
        public static IADLXDisplayServices* GetDisplayServices(IADLXSystem* pSystem)
        {
            if (pSystem == null) throw new ArgumentNullException(nameof(pSystem));

            IADLXDisplayServices* pDisplayServices = null;
            var result = pSystem->GetDisplaysServices(&pDisplayServices);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display services");
            }

            // Caller must wrap this in a ComPtr and dispose it.
            return (IADLXDisplayServices*)pDisplayServices;
        }

        /// <summary>
        /// Enumerate all displays from system display services
        /// </summary>
        public static IEnumerable<DisplayInfo> EnumerateAllDisplays(IADLXSystem* pSystem)
        {
            if (pSystem == null) return Array.Empty<DisplayInfo>();

            var results = new List<DisplayInfo>();

            using var displayServices = new ComPtr<IADLXDisplayServices>(GetDisplayServices(pSystem));
            if (displayServices.Get() == null) return Array.Empty<DisplayInfo>();

            IADLXDisplayList* pDisplayList = null;
            displayServices.Get()->GetDisplays(&pDisplayList);
            using var displayList = new ComPtr<IADLXDisplayList>(pDisplayList);

            for (uint i = 0; i < displayList.Get()->Size(); i++)
            {
                IADLXInterface* pItem = null;
                displayList.Get()->At(i, &pItem);
                using var display = new ComPtr<IADLXDisplay>((IADLXDisplay*)pItem);
                results.Add(new DisplayInfo(display.Get()));
            }

            return results;
        }

        /// <summary>
        /// Enumerate raw display handles for compatibility helpers.
        /// </summary>
        public static AdlxInterfaceHandle[] EnumerateAllDisplayHandles(IADLXSystem* pSystem)
        {
            if (pSystem == null) return Array.Empty<AdlxInterfaceHandle>();

            using var displayServices = new ComPtr<IADLXDisplayServices>(GetDisplayServices(pSystem));
            if (displayServices.Get() == null) return Array.Empty<AdlxInterfaceHandle>();

            IADLXDisplayList* pDisplayList = null;
            var result = displayServices.Get()->GetDisplays(&pDisplayList);
            if (result != ADLX_RESULT.ADLX_OK || pDisplayList == null)
                return Array.Empty<AdlxInterfaceHandle>();

            using var displayList = new ComPtr<IADLXDisplayList>(pDisplayList);
            var count = displayList.Get()->Size();
            var handles = new AdlxInterfaceHandle[count];

            for (uint i = 0; i < count; i++)
            {
                IADLXDisplay* pDisplay = null;
                displayList.Get()->At(i, &pDisplay);
                handles[i] = AdlxInterfaceHandle.From(pDisplay, addRef: false);
            }

            return handles;
        }

        public static unsafe string GetDisplayName(IntPtr pDisplay)
        {
            if (pDisplay == IntPtr.Zero) throw new ArgumentNullException(nameof(pDisplay));
            sbyte* name = null;
            ((IADLXDisplay*)pDisplay)->Name(&name);
            return ADLXHelpers.MarshalString(&name);
        }
    }

    /// <summary>
    /// Represents the collected information for a display.
    /// </summary>
    public readonly struct DisplayInfo
    {
        public string Name { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }
        public double RefreshRate { get; init; }
        public uint ManufacturerID { get; init; }
        public uint PixelClock { get; init; }
        public ADLX_DISPLAY_TYPE Type { get; init; }
        public ADLX_DISPLAY_CONNECTOR_TYPE ConnectorType { get; init; }
        public ADLX_DISPLAY_SCAN_TYPE ScanType { get; init; }
        public ulong UniqueId { get; init; }
        public string Edid { get; init; }
        public int GpuUniqueId { get; init; }

        [JsonConstructor]
        public DisplayInfo(string name, int width, int height, double refreshRate, uint manufacturerID, uint pixelClock, ADLX_DISPLAY_TYPE type, ADLX_DISPLAY_CONNECTOR_TYPE connectorType, ADLX_DISPLAY_SCAN_TYPE scanType, ulong uniqueId, string edid, int gpuUniqueId)
        {
            Name = name; Width = width; Height = height; RefreshRate = refreshRate;
            ManufacturerID = manufacturerID; PixelClock = pixelClock; Type = type; ConnectorType = connectorType;
            ScanType = scanType;
            UniqueId = uniqueId;
            Edid = edid;
            GpuUniqueId = gpuUniqueId;
        }

        internal unsafe DisplayInfo(IADLXDisplay* pDisplay)
        {
            sbyte* namePtr = null; pDisplay->Name(&namePtr); Name = ADLXHelpers.MarshalString(&namePtr);
            sbyte* edidPtr = null; pDisplay->EDID(&edidPtr); Edid = ADLXHelpers.MarshalString(&edidPtr);
            int w = 0, h = 0; pDisplay->NativeResolution(&w, &h); Width = w; Height = h;
            double rr = 0; pDisplay->RefreshRate(&rr); RefreshRate = rr;
            uint mid = 0; pDisplay->ManufacturerID(&mid); ManufacturerID = mid;
            uint pc = 0; pDisplay->PixelClock(&pc); PixelClock = pc;
            ADLX_DISPLAY_TYPE dt = default; pDisplay->DisplayType(&dt); Type = dt;
            ADLX_DISPLAY_CONNECTOR_TYPE ct = default; pDisplay->ConnectorType(&ct); ConnectorType = ct;
            ADLX_DISPLAY_SCAN_TYPE st = default; pDisplay->ScanType(&st); ScanType = st;
            nuint uid = 0; pDisplay->UniqueId(&uid); UniqueId = (ulong)uid;

            IADLXGPU* pGpu = null;
            pDisplay->GetGPU(&pGpu);
            using var gpu = new ComPtr<IADLXGPU>(pGpu);
            int gpuId = 0; gpu.Get()->UniqueId(&gpuId);
            GpuUniqueId = gpuId;
        }
    }
}