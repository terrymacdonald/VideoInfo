using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUAutoTuningCompleteEvent : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUAutoTuningCompleteEvent
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuningCompleteEvent*, int>)(lpVtbl[0]))((IADLXGPUAutoTuningCompleteEvent*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuningCompleteEvent*, int>)(lpVtbl[1]))((IADLXGPUAutoTuningCompleteEvent*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuningCompleteEvent*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUAutoTuningCompleteEvent*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsUndervoltGPUCompleted()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuningCompleteEvent*, byte>)(lpVtbl[3]))((IADLXGPUAutoTuningCompleteEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsOverclockGPUCompleted()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuningCompleteEvent*, byte>)(lpVtbl[4]))((IADLXGPUAutoTuningCompleteEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsOverclockVRAMCompleted()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuningCompleteEvent*, byte>)(lpVtbl[5]))((IADLXGPUAutoTuningCompleteEvent*)Unsafe.AsPointer(ref this)) != 0;
    }
}
