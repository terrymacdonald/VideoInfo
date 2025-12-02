using System.Runtime.CompilerServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXDisplaySettingsChangedListener
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_bool")]
    public bool OnDisplaySettingsChanged([NativeTypeName("adlx::IADLXDisplaySettingsChangedEvent *")] IADLXDisplaySettingsChangedEvent* pDisplaySettingChangedEvent)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedListener*, IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[0]))((IADLXDisplaySettingsChangedListener*)Unsafe.AsPointer(ref this), pDisplaySettingChangedEvent) != 0;
    }
}
