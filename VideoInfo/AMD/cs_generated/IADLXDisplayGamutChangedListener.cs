using System.Runtime.CompilerServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXDisplayGamutChangedListener
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_bool")]
    public bool OnDisplayGamutChanged([NativeTypeName("adlx::IADLXDisplayGamutChangedEvent *")] IADLXDisplayGamutChangedEvent* pDisplayGamutChangedEvent)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamutChangedListener*, IADLXDisplayGamutChangedEvent*, byte>)(lpVtbl[0]))((IADLXDisplayGamutChangedListener*)Unsafe.AsPointer(ref this), pDisplayGamutChangedEvent) != 0;
    }
}
