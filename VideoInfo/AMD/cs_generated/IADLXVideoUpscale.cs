using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXVideoUpscale : adlx::IADLXInterface")]
public unsafe partial struct IADLXVideoUpscale
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, int>)(lpVtbl[0]))((IADLXVideoUpscale*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, int>)(lpVtbl[1]))((IADLXVideoUpscale*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXVideoUpscale*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXVideoUpscale*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXVideoUpscale*)Unsafe.AsPointer(ref this), isEnabled);
    }

    public ADLX_RESULT GetSharpnessRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[5]))((IADLXVideoUpscale*)Unsafe.AsPointer(ref this), range);
    }

    public ADLX_RESULT GetSharpness([NativeTypeName("adlx_int *")] int* currentMinRes)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLXVideoUpscale*)Unsafe.AsPointer(ref this), currentMinRes);
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, byte, ADLX_RESULT>)(lpVtbl[7]))((IADLXVideoUpscale*)Unsafe.AsPointer(ref this), enable);
    }

    public ADLX_RESULT SetSharpness([NativeTypeName("adlx_int")] int minSharp)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, int, ADLX_RESULT>)(lpVtbl[8]))((IADLXVideoUpscale*)Unsafe.AsPointer(ref this), minSharp);
    }
}
