using System.Runtime.CompilerServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXPowerTuningChangedListener
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_bool")]
    public bool OnPowerTuningChanged([NativeTypeName("adlx::IADLXPowerTuningChangedEvent *")] IADLXPowerTuningChangedEvent* pPowerTuningChangedEvent)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedListener*, IADLXPowerTuningChangedEvent*, byte>)(lpVtbl[0]))((IADLXPowerTuningChangedListener*)Unsafe.AsPointer(ref this), pPowerTuningChangedEvent) != 0;
    }
}
