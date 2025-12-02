using System.Runtime.CompilerServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXMultimediaChangedEventListener
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_bool")]
    public bool OnMultimediaChanged([NativeTypeName("adlx::IADLXMultimediaChangedEvent *")] IADLXMultimediaChangedEvent* pMultimediaChangedEvent)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedEventListener*, IADLXMultimediaChangedEvent*, byte>)(lpVtbl[0]))((IADLXMultimediaChangedEventListener*)Unsafe.AsPointer(ref this), pMultimediaChangedEvent) != 0;
    }
}
