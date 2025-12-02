using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplay : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplay
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, int>)(lpVtbl[0]))((IADLXDisplay*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, int>)(lpVtbl[1]))((IADLXDisplay*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplay*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public readonly ADLX_RESULT ManufacturerID([NativeTypeName("adlx_uint *")] uint* manufacturerID)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, uint*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplay*)Unsafe.AsPointer(in this), manufacturerID);
    }

    public readonly ADLX_RESULT DisplayType(ADLX_DISPLAY_TYPE* displayType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, ADLX_DISPLAY_TYPE*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplay*)Unsafe.AsPointer(in this), displayType);
    }

    public readonly ADLX_RESULT ConnectorType(ADLX_DISPLAY_CONNECTOR_TYPE* connectType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, ADLX_DISPLAY_CONNECTOR_TYPE*, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplay*)Unsafe.AsPointer(in this), connectType);
    }

    public readonly ADLX_RESULT Name([NativeTypeName("const char **")] sbyte** displayName)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, sbyte**, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplay*)Unsafe.AsPointer(in this), displayName);
    }

    public readonly ADLX_RESULT EDID([NativeTypeName("const char **")] sbyte** edid)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, sbyte**, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplay*)Unsafe.AsPointer(in this), edid);
    }

    public readonly ADLX_RESULT NativeResolution([NativeTypeName("adlx_int *")] int* maxHResolution, [NativeTypeName("adlx_int *")] int* maxVResolution)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, int*, int*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplay*)Unsafe.AsPointer(in this), maxHResolution, maxVResolution);
    }

    public readonly ADLX_RESULT RefreshRate([NativeTypeName("adlx_double *")] double* refreshRate)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, double*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplay*)Unsafe.AsPointer(in this), refreshRate);
    }

    public readonly ADLX_RESULT PixelClock([NativeTypeName("adlx_uint *")] uint* pixelClock)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, uint*, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplay*)Unsafe.AsPointer(in this), pixelClock);
    }

    public readonly ADLX_RESULT ScanType(ADLX_DISPLAY_SCAN_TYPE* scanType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, ADLX_DISPLAY_SCAN_TYPE*, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplay*)Unsafe.AsPointer(in this), scanType);
    }

    public ADLX_RESULT GetGPU(IADLXGPU** ppGPU)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, IADLXGPU**, ADLX_RESULT>)(lpVtbl[12]))((IADLXDisplay*)Unsafe.AsPointer(ref this), ppGPU);
    }

    public ADLX_RESULT UniqueId([NativeTypeName("adlx_size *")] nuint* uniqueId)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay*, nuint*, ADLX_RESULT>)(lpVtbl[13]))((IADLXDisplay*)Unsafe.AsPointer(ref this), uniqueId);
    }
}
