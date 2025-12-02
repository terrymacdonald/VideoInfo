using System.Runtime.CompilerServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXGPUAutoTuningCompleteListener
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_bool")]
    public bool OnGPUAutoTuningComplete([NativeTypeName("adlx::IADLXGPUAutoTuningCompleteEvent *")] IADLXGPUAutoTuningCompleteEvent* pGPUAutoTuningCompleteEvent)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuningCompleteListener*, IADLXGPUAutoTuningCompleteEvent*, byte>)(lpVtbl[0]))((IADLXGPUAutoTuningCompleteListener*)Unsafe.AsPointer(ref this), pGPUAutoTuningCompleteEvent) != 0;
    }
}
