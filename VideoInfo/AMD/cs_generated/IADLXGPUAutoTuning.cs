using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUAutoTuning : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUAutoTuning
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, int>)(lpVtbl[0]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, int>)(lpVtbl[1]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupportedUndervoltGPU([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedOverclockGPU([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedOverclockVRAM([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, bool*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsCurrentUndervoltGPU([NativeTypeName("adlx_bool *")] bool* isUndervoltGPU)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), isUndervoltGPU);
    }

    public ADLX_RESULT IsCurrentOverclockGPU([NativeTypeName("adlx_bool *")] bool* isOverclockGPU)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, bool*, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), isOverclockGPU);
    }

    public ADLX_RESULT IsCurrentOverclockVRAM([NativeTypeName("adlx_bool *")] bool* isOverclockVRAM)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), isOverclockVRAM);
    }

    public ADLX_RESULT StartUndervoltGPU([NativeTypeName("adlx::IADLXGPUAutoTuningCompleteListener *")] IADLXGPUAutoTuningCompleteListener* pCompleteListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, IADLXGPUAutoTuningCompleteListener*, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), pCompleteListener);
    }

    public ADLX_RESULT StartOverclockGPU([NativeTypeName("adlx::IADLXGPUAutoTuningCompleteListener *")] IADLXGPUAutoTuningCompleteListener* pCompleteListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, IADLXGPUAutoTuningCompleteListener*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), pCompleteListener);
    }

    public ADLX_RESULT StartOverclockVRAM([NativeTypeName("adlx::IADLXGPUAutoTuningCompleteListener *")] IADLXGPUAutoTuningCompleteListener* pCompleteListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, IADLXGPUAutoTuningCompleteListener*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), pCompleteListener);
    }
}
