using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPU1 : adlx::IADLXGPU")]
public unsafe partial struct IADLXGPU1
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, int>)(lpVtbl[0]))((IADLXGPU1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, int>)(lpVtbl[1]))((IADLXGPU1*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPU1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT VendorId([NativeTypeName("const char **")] sbyte** vendorId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPU1*)Unsafe.AsPointer(ref this), vendorId);
    }

    public readonly ADLX_RESULT ASICFamilyType(ADLX_ASIC_FAMILY_TYPE* asicFamilyType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, ADLX_ASIC_FAMILY_TYPE*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPU1*)Unsafe.AsPointer(in this), asicFamilyType);
    }

    public readonly ADLX_RESULT Type(ADLX_GPU_TYPE* gpuType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, ADLX_GPU_TYPE*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPU1*)Unsafe.AsPointer(in this), gpuType);
    }

    public readonly ADLX_RESULT IsExternal([NativeTypeName("adlx_bool *")] bool* isExternal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPU1*)Unsafe.AsPointer(in this), isExternal);
    }

    public readonly ADLX_RESULT Name([NativeTypeName("const char **")] sbyte** name)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPU1*)Unsafe.AsPointer(in this), name);
    }

    public readonly ADLX_RESULT DriverPath([NativeTypeName("const char **")] sbyte** driverPath)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPU1*)Unsafe.AsPointer(in this), driverPath);
    }

    public readonly ADLX_RESULT PNPString([NativeTypeName("const char **")] sbyte** pnpString)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPU1*)Unsafe.AsPointer(in this), pnpString);
    }

    public readonly ADLX_RESULT HasDesktops([NativeTypeName("adlx_bool *")] bool* hasDesktops)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPU1*)Unsafe.AsPointer(in this), hasDesktops);
    }

    public ADLX_RESULT TotalVRAM([NativeTypeName("adlx_uint *")] uint* vramMB)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, uint*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPU1*)Unsafe.AsPointer(ref this), vramMB);
    }

    public ADLX_RESULT VRAMType([NativeTypeName("const char **")] sbyte** type)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPU1*)Unsafe.AsPointer(ref this), type);
    }

    public ADLX_RESULT BIOSInfo([NativeTypeName("const char **")] sbyte** partNumber, [NativeTypeName("const char **")] sbyte** version, [NativeTypeName("const char **")] sbyte** date)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, sbyte**, sbyte**, ADLX_RESULT>)(lpVtbl[13]))((IADLXGPU1*)Unsafe.AsPointer(ref this), partNumber, version, date);
    }

    public ADLX_RESULT DeviceId([NativeTypeName("const char **")] sbyte** deviceId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[14]))((IADLXGPU1*)Unsafe.AsPointer(ref this), deviceId);
    }

    public ADLX_RESULT RevisionId([NativeTypeName("const char **")] sbyte** revisionId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[15]))((IADLXGPU1*)Unsafe.AsPointer(ref this), revisionId);
    }

    public ADLX_RESULT SubSystemId([NativeTypeName("const char **")] sbyte** subSystemId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[16]))((IADLXGPU1*)Unsafe.AsPointer(ref this), subSystemId);
    }

    public ADLX_RESULT SubSystemVendorId([NativeTypeName("const char **")] sbyte** subSystemVendorId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[17]))((IADLXGPU1*)Unsafe.AsPointer(ref this), subSystemVendorId);
    }

    public ADLX_RESULT UniqueId([NativeTypeName("adlx_int *")] int* uniqueId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, int*, ADLX_RESULT>)(lpVtbl[18]))((IADLXGPU1*)Unsafe.AsPointer(ref this), uniqueId);
    }

    public readonly ADLX_RESULT PCIBusType(ADLX_PCI_BUS_TYPE* busType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, ADLX_PCI_BUS_TYPE*, ADLX_RESULT>)(lpVtbl[19]))((IADLXGPU1*)Unsafe.AsPointer(in this), busType);
    }

    public readonly ADLX_RESULT PCIBusLaneWidth([NativeTypeName("adlx_uint *")] uint* laneWidth)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, uint*, ADLX_RESULT>)(lpVtbl[20]))((IADLXGPU1*)Unsafe.AsPointer(in this), laneWidth);
    }

    public ADLX_RESULT MultiGPUMode(ADLX_MGPU_MODE* mode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, ADLX_MGPU_MODE*, ADLX_RESULT>)(lpVtbl[21]))((IADLXGPU1*)Unsafe.AsPointer(ref this), mode);
    }

    public readonly ADLX_RESULT ProductName([NativeTypeName("const char **")] sbyte** productName)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU1*, sbyte**, ADLX_RESULT>)(lpVtbl[22]))((IADLXGPU1*)Unsafe.AsPointer(in this), productName);
    }
}
