using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary> 
    /// Helper methods for ADLX GPU operations.
    /// </summary>
    public static unsafe class ADLXGpuHelpers
    {
        /// <summary>
        /// Enumerates all available GPUs.
        /// </summary>
        public static IEnumerable<GpuInfo> EnumerateAllGpus(IADLXSystem* pSystem)
        {
            if (pSystem == null) return Array.Empty<GpuInfo>();

            var results = new List<GpuInfo>();

            IADLXGPUList* pGpuList = null;
            pSystem->GetGPUs(&pGpuList);
            using var gpuList = new ComPtr<IADLXGPUList>(pGpuList);

            for (uint i = 0; i < gpuList.Get()->Size(); i++)
            {
                IADLXInterface* pItem = null;
                gpuList.Get()->At(i, &pItem);
                var pGpu = (IADLXGPU*)pItem;
                using var gpu = new ComPtr<IADLXGPU>(pGpu);
                results.Add(new GpuInfo(gpu.Get()));
            }

            return results;
        }
    }

    /// <summary>
    /// Represents the collected information for a GPU.
    /// </summary>
    public readonly struct GpuInfo
    {
        public string Name { get; init; }
        public string VendorId { get; init; }
        public int UniqueId { get; init; }
        public uint TotalVRAM { get; init; }
        public string VRAMType { get; init; }
        public bool IsExternal { get; init; }
        public bool HasDesktops { get; init; }
        public string DeviceId { get; init; }
        public string PNPString { get; init; }
        public string DriverPath { get; init; }

        [JsonConstructor]
        public GpuInfo(string name, string vendorId, int uniqueId, uint totalVRAM, string vramType, bool isExternal, bool hasDesktops, string deviceId, string pnpString, string driverPath)
        {
            Name = name;
            VendorId = vendorId;
            UniqueId = uniqueId;
            TotalVRAM = totalVRAM;
            VRAMType = vramType;
            IsExternal = isExternal;
            HasDesktops = hasDesktops;
            DeviceId = deviceId;
            PNPString = pnpString;
            DriverPath = driverPath;
        }

        internal unsafe GpuInfo(IADLXGPU* pGpu)
        {
            sbyte* namePtr = null;
            pGpu->Name(&namePtr);
            Name = ADLXHelpers.MarshalString(&namePtr);

            sbyte* vendorIdPtr = null;
            pGpu->VendorId(&vendorIdPtr);
            VendorId = ADLXHelpers.MarshalString(&vendorIdPtr);

            int uid = 0;
            pGpu->UniqueId(&uid);
            UniqueId = uid;

            uint vram = 0;
            pGpu->TotalVRAM(&vram);
            TotalVRAM = vram;

            sbyte* vramTypePtr = null;
            pGpu->VRAMType(&vramTypePtr);
            VRAMType = ADLXHelpers.MarshalString(&vramTypePtr);

            bool isExt = false;
            pGpu->IsExternal(&isExt);
            IsExternal = isExt;

            bool hasDesk = false;
            pGpu->HasDesktops(&hasDesk);
            HasDesktops = hasDesk;

            sbyte* devIdPtr = null;
            pGpu->DeviceId(&devIdPtr);
            DeviceId = ADLXHelpers.MarshalString(&devIdPtr);

            sbyte* pnpPtr = null;
            pGpu->PNPString(&pnpPtr);
            PNPString = ADLXHelpers.MarshalString(&pnpPtr);

            sbyte* driverPathPtr = null;
            pGpu->DriverPath(&driverPathPtr);
            DriverPath = ADLXHelpers.MarshalString(&driverPathPtr);
        }
    }
}