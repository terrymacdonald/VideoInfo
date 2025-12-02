using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DChill : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DChill
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, int>)(lpVtbl[0]))((IADLX3DChill*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, int>)(lpVtbl[1]))((IADLX3DChill*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DChill*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DChill*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DChill*)Unsafe.AsPointer(ref this), isEnabled);
    }

    public ADLX_RESULT GetFPSRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DChill*)Unsafe.AsPointer(ref this), range);
    }

    public ADLX_RESULT GetMinFPS([NativeTypeName("adlx_int *")] int* currentMinFPS)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLX3DChill*)Unsafe.AsPointer(ref this), currentMinFPS);
    }

    public ADLX_RESULT GetMaxFPS([NativeTypeName("adlx_int *")] int* currentMaxFPS)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, int*, ADLX_RESULT>)(lpVtbl[7]))((IADLX3DChill*)Unsafe.AsPointer(ref this), currentMaxFPS);
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, byte, ADLX_RESULT>)(lpVtbl[8]))((IADLX3DChill*)Unsafe.AsPointer(ref this), enable);
    }

    public ADLX_RESULT SetMinFPS([NativeTypeName("adlx_int")] int minFPS)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, int, ADLX_RESULT>)(lpVtbl[9]))((IADLX3DChill*)Unsafe.AsPointer(ref this), minFPS);
    }

    public ADLX_RESULT SetMaxFPS([NativeTypeName("adlx_int")] int maxFPS)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, int, ADLX_RESULT>)(lpVtbl[10]))((IADLX3DChill*)Unsafe.AsPointer(ref this), maxFPS);
    }
}
