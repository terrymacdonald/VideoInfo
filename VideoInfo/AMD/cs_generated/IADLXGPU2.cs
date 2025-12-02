using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPU2 : adlx::IADLXGPU1")]
public unsafe partial struct IADLXGPU2
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, int>)(lpVtbl[0]))((IADLXGPU2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, int>)(lpVtbl[1]))((IADLXGPU2*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPU2*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT VendorId([NativeTypeName("const char **")] sbyte** vendorId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPU2*)Unsafe.AsPointer(ref this), vendorId);
    }

    public readonly ADLX_RESULT ASICFamilyType(ADLX_ASIC_FAMILY_TYPE* asicFamilyType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, ADLX_ASIC_FAMILY_TYPE*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPU2*)Unsafe.AsPointer(in this), asicFamilyType);
    }

    public readonly ADLX_RESULT Type(ADLX_GPU_TYPE* gpuType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, ADLX_GPU_TYPE*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPU2*)Unsafe.AsPointer(in this), gpuType);
    }

    public readonly ADLX_RESULT IsExternal([NativeTypeName("adlx_bool *")] bool* isExternal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPU2*)Unsafe.AsPointer(in this), isExternal);
    }

    public readonly ADLX_RESULT Name([NativeTypeName("const char **")] sbyte** name)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPU2*)Unsafe.AsPointer(in this), name);
    }

    public readonly ADLX_RESULT DriverPath([NativeTypeName("const char **")] sbyte** driverPath)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPU2*)Unsafe.AsPointer(in this), driverPath);
    }

    public readonly ADLX_RESULT PNPString([NativeTypeName("const char **")] sbyte** pnpString)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPU2*)Unsafe.AsPointer(in this), pnpString);
    }

    public readonly ADLX_RESULT HasDesktops([NativeTypeName("adlx_bool *")] bool* hasDesktops)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPU2*)Unsafe.AsPointer(in this), hasDesktops);
    }

    public ADLX_RESULT TotalVRAM([NativeTypeName("adlx_uint *")] uint* vramMB)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, uint*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPU2*)Unsafe.AsPointer(ref this), vramMB);
    }

    public ADLX_RESULT VRAMType([NativeTypeName("const char **")] sbyte** type)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPU2*)Unsafe.AsPointer(ref this), type);
    }

    public ADLX_RESULT BIOSInfo([NativeTypeName("const char **")] sbyte** partNumber, [NativeTypeName("const char **")] sbyte** version, [NativeTypeName("const char **")] sbyte** date)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, sbyte**, sbyte**, ADLX_RESULT>)(lpVtbl[13]))((IADLXGPU2*)Unsafe.AsPointer(ref this), partNumber, version, date);
    }

    public ADLX_RESULT DeviceId([NativeTypeName("const char **")] sbyte** deviceId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[14]))((IADLXGPU2*)Unsafe.AsPointer(ref this), deviceId);
    }

    public ADLX_RESULT RevisionId([NativeTypeName("const char **")] sbyte** revisionId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[15]))((IADLXGPU2*)Unsafe.AsPointer(ref this), revisionId);
    }

    public ADLX_RESULT SubSystemId([NativeTypeName("const char **")] sbyte** subSystemId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[16]))((IADLXGPU2*)Unsafe.AsPointer(ref this), subSystemId);
    }

    public ADLX_RESULT SubSystemVendorId([NativeTypeName("const char **")] sbyte** subSystemVendorId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[17]))((IADLXGPU2*)Unsafe.AsPointer(ref this), subSystemVendorId);
    }

    public ADLX_RESULT UniqueId([NativeTypeName("adlx_int *")] int* uniqueId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, int*, ADLX_RESULT>)(lpVtbl[18]))((IADLXGPU2*)Unsafe.AsPointer(ref this), uniqueId);
    }

    public readonly ADLX_RESULT PCIBusType(ADLX_PCI_BUS_TYPE* busType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, ADLX_PCI_BUS_TYPE*, ADLX_RESULT>)(lpVtbl[19]))((IADLXGPU2*)Unsafe.AsPointer(in this), busType);
    }

    public readonly ADLX_RESULT PCIBusLaneWidth([NativeTypeName("adlx_uint *")] uint* laneWidth)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, uint*, ADLX_RESULT>)(lpVtbl[20]))((IADLXGPU2*)Unsafe.AsPointer(in this), laneWidth);
    }

    public ADLX_RESULT MultiGPUMode(ADLX_MGPU_MODE* mode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, ADLX_MGPU_MODE*, ADLX_RESULT>)(lpVtbl[21]))((IADLXGPU2*)Unsafe.AsPointer(ref this), mode);
    }

    public readonly ADLX_RESULT ProductName([NativeTypeName("const char **")] sbyte** productName)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[22]))((IADLXGPU2*)Unsafe.AsPointer(in this), productName);
    }

    public ADLX_RESULT IsPowerOff([NativeTypeName("adlx_bool *")] bool* state)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, bool*, ADLX_RESULT>)(lpVtbl[23]))((IADLXGPU2*)Unsafe.AsPointer(ref this), state);
    }

    public ADLX_RESULT PowerOn()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, ADLX_RESULT>)(lpVtbl[24]))((IADLXGPU2*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT StartPowerOff([NativeTypeName("adlx::IADLXGPUConnectChangedListener *")] IADLXGPUConnectChangedListener* pGPUConnectChangedListener, [NativeTypeName("adlx_int")] int timeout)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, IADLXGPUConnectChangedListener*, int, ADLX_RESULT>)(lpVtbl[25]))((IADLXGPU2*)Unsafe.AsPointer(ref this), pGPUConnectChangedListener, timeout);
    }

    public ADLX_RESULT AbortPowerOff()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, ADLX_RESULT>)(lpVtbl[26]))((IADLXGPU2*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT IsSupportedApplicationList([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, bool*, ADLX_RESULT>)(lpVtbl[27]))((IADLXGPU2*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetApplications(IADLXApplicationList** ppApplications)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, IADLXApplicationList**, ADLX_RESULT>)(lpVtbl[28]))((IADLXGPU2*)Unsafe.AsPointer(ref this), ppApplications);
    }

    public ADLX_RESULT AMDSoftwareReleaseDate([NativeTypeName("adlx_uint *")] uint* year, [NativeTypeName("adlx_uint *")] uint* month, [NativeTypeName("adlx_uint *")] uint* day)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, uint*, uint*, uint*, ADLX_RESULT>)(lpVtbl[29]))((IADLXGPU2*)Unsafe.AsPointer(ref this), year, month, day);
    }

    public ADLX_RESULT AMDSoftwareEdition([NativeTypeName("const char **")] sbyte** edition)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[30]))((IADLXGPU2*)Unsafe.AsPointer(ref this), edition);
    }

    public ADLX_RESULT AMDSoftwareVersion([NativeTypeName("const char **")] sbyte** version)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[31]))((IADLXGPU2*)Unsafe.AsPointer(ref this), version);
    }

    public ADLX_RESULT DriverVersion([NativeTypeName("const char **")] sbyte** version)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[32]))((IADLXGPU2*)Unsafe.AsPointer(ref this), version);
    }

    public ADLX_RESULT AMDWindowsDriverVersion([NativeTypeName("const char **")] sbyte** version)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, sbyte**, ADLX_RESULT>)(lpVtbl[33]))((IADLXGPU2*)Unsafe.AsPointer(ref this), version);
    }

    public ADLX_RESULT LUID(ADLX_LUID* luid)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2*, ADLX_LUID*, ADLX_RESULT>)(lpVtbl[34]))((IADLXGPU2*)Unsafe.AsPointer(ref this), luid);
    }
}
