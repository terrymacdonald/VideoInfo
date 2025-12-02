using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DFrameRateTargetControl : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DFrameRateTargetControl
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFrameRateTargetControl*, int>)(lpVtbl[0]))((IADLX3DFrameRateTargetControl*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFrameRateTargetControl*, int>)(lpVtbl[1]))((IADLX3DFrameRateTargetControl*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFrameRateTargetControl*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DFrameRateTargetControl*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFrameRateTargetControl*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DFrameRateTargetControl*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFrameRateTargetControl*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DFrameRateTargetControl*)Unsafe.AsPointer(ref this), isEnabled);
    }

    public ADLX_RESULT GetFPSRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFrameRateTargetControl*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DFrameRateTargetControl*)Unsafe.AsPointer(ref this), range);
    }

    public ADLX_RESULT GetFPS([NativeTypeName("adlx_int *")] int* currentFPS)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFrameRateTargetControl*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLX3DFrameRateTargetControl*)Unsafe.AsPointer(ref this), currentFPS);
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFrameRateTargetControl*, byte, ADLX_RESULT>)(lpVtbl[7]))((IADLX3DFrameRateTargetControl*)Unsafe.AsPointer(ref this), enable);
    }

    public ADLX_RESULT SetFPS([NativeTypeName("adlx_int")] int maxFPS)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFrameRateTargetControl*, int, ADLX_RESULT>)(lpVtbl[8]))((IADLX3DFrameRateTargetControl*)Unsafe.AsPointer(ref this), maxFPS);
    }
}
