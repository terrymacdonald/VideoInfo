using System.Runtime.CompilerServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXDisplay3DLUTChangedListener
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_bool")]
    public bool OnDisplay3DLUTChanged([NativeTypeName("adlx::IADLXDisplay3DLUTChangedEvent *")] IADLXDisplay3DLUTChangedEvent* pDisplay3DLUTChangedEvent)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUTChangedListener*, IADLXDisplay3DLUTChangedEvent*, byte>)(lpVtbl[0]))((IADLXDisplay3DLUTChangedListener*)Unsafe.AsPointer(ref this), pDisplay3DLUTChangedEvent) != 0;
    }
}
