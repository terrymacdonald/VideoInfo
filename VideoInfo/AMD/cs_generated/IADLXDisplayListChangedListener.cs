using System.Runtime.CompilerServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXDisplayListChangedListener
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_bool")]
    public bool OnDisplayListChanged([NativeTypeName("adlx::IADLXDisplayList *")] IADLXDisplayList* pNewDisplay)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayListChangedListener*, IADLXDisplayList*, byte>)(lpVtbl[0]))((IADLXDisplayListChangedListener*)Unsafe.AsPointer(ref this), pNewDisplay) != 0;
    }
}
