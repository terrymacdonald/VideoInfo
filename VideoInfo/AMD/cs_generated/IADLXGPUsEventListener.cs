using System.Runtime.CompilerServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXGPUsEventListener
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_bool")]
    public bool OnGPUListChanged([NativeTypeName("adlx::IADLXGPUList *")] IADLXGPUList* pNewGPUs)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUsEventListener*, IADLXGPUList*, byte>)(lpVtbl[0]))((IADLXGPUsEventListener*)Unsafe.AsPointer(ref this), pNewGPUs) != 0;
    }
}
