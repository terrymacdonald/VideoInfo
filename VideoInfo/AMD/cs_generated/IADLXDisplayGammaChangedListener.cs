using System.Runtime.CompilerServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXDisplayGammaChangedListener
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_bool")]
    public bool OnDisplayGammaChanged([NativeTypeName("adlx::IADLXDisplayGammaChangedEvent *")] IADLXDisplayGammaChangedEvent* pDisplayGammaChangedEvent)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGammaChangedListener*, IADLXDisplayGammaChangedEvent*, byte>)(lpVtbl[0]))((IADLXDisplayGammaChangedListener*)Unsafe.AsPointer(ref this), pDisplayGammaChangedEvent) != 0;
    }
}
