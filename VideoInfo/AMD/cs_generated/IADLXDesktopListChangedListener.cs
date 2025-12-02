using System.Runtime.CompilerServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXDesktopListChangedListener
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_bool")]
    public bool OnDesktopListChanged([NativeTypeName("adlx::IADLXDesktopList *")] IADLXDesktopList* pNewDesktop)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopListChangedListener*, IADLXDesktopList*, byte>)(lpVtbl[0]))((IADLXDesktopListChangedListener*)Unsafe.AsPointer(ref this), pNewDesktop) != 0;
    }
}
