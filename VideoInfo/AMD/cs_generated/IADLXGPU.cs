using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPU : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPU
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, int>)(lpVtbl[0]))((IADLXGPU*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, int>)(lpVtbl[1]))((IADLXGPU*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPU*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT VendorId([NativeTypeName("const char **")] sbyte** vendorId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPU*)Unsafe.AsPointer(ref this), vendorId);
    }

    public readonly ADLX_RESULT ASICFamilyType(ADLX_ASIC_FAMILY_TYPE* asicFamilyType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, ADLX_ASIC_FAMILY_TYPE*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPU*)Unsafe.AsPointer(in this), asicFamilyType);
    }

    public readonly ADLX_RESULT Type(ADLX_GPU_TYPE* gpuType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, ADLX_GPU_TYPE*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPU*)Unsafe.AsPointer(in this), gpuType);
    }

    public readonly ADLX_RESULT IsExternal([NativeTypeName("adlx_bool *")] bool* isExternal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPU*)Unsafe.AsPointer(in this), isExternal);
    }

    public readonly ADLX_RESULT Name([NativeTypeName("const char **")] sbyte** name)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPU*)Unsafe.AsPointer(in this), name);
    }

    public readonly ADLX_RESULT DriverPath([NativeTypeName("const char **")] sbyte** driverPath)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPU*)Unsafe.AsPointer(in this), driverPath);
    }

    public readonly ADLX_RESULT PNPString([NativeTypeName("const char **")] sbyte** pnpString)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPU*)Unsafe.AsPointer(in this), pnpString);
    }

    public readonly ADLX_RESULT HasDesktops([NativeTypeName("adlx_bool *")] bool* hasDesktops)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPU*)Unsafe.AsPointer(in this), hasDesktops);
    }

    public ADLX_RESULT TotalVRAM([NativeTypeName("adlx_uint *")] uint* vramMB)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, uint*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPU*)Unsafe.AsPointer(ref this), vramMB);
    }

    public ADLX_RESULT VRAMType([NativeTypeName("const char **")] sbyte** type)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPU*)Unsafe.AsPointer(ref this), type);
    }

    public ADLX_RESULT BIOSInfo([NativeTypeName("const char **")] sbyte** partNumber, [NativeTypeName("const char **")] sbyte** version, [NativeTypeName("const char **")] sbyte** date)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, sbyte**, sbyte**, ADLX_RESULT>)(lpVtbl[13]))((IADLXGPU*)Unsafe.AsPointer(ref this), partNumber, version, date);
    }

    public ADLX_RESULT DeviceId([NativeTypeName("const char **")] sbyte** deviceId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, ADLX_RESULT>)(lpVtbl[14]))((IADLXGPU*)Unsafe.AsPointer(ref this), deviceId);
    }

    public ADLX_RESULT RevisionId([NativeTypeName("const char **")] sbyte** revisionId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, ADLX_RESULT>)(lpVtbl[15]))((IADLXGPU*)Unsafe.AsPointer(ref this), revisionId);
    }

    public ADLX_RESULT SubSystemId([NativeTypeName("const char **")] sbyte** subSystemId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, ADLX_RESULT>)(lpVtbl[16]))((IADLXGPU*)Unsafe.AsPointer(ref this), subSystemId);
    }

    public ADLX_RESULT SubSystemVendorId([NativeTypeName("const char **")] sbyte** subSystemVendorId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, sbyte**, ADLX_RESULT>)(lpVtbl[17]))((IADLXGPU*)Unsafe.AsPointer(ref this), subSystemVendorId);
    }

    public ADLX_RESULT UniqueId([NativeTypeName("adlx_int *")] int* uniqueId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU*, int*, ADLX_RESULT>)(lpVtbl[18]))((IADLXGPU*)Unsafe.AsPointer(ref this), uniqueId);
    }
}
