using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXChangedEvent : adlx::IADLXInterface")]
public unsafe partial struct IADLXChangedEvent
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXChangedEvent*, int>)(lpVtbl[0]))((IADLXChangedEvent*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXChangedEvent*, int>)(lpVtbl[1]))((IADLXChangedEvent*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXChangedEvent*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXChangedEvent*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXChangedEvent*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXChangedEvent*)Unsafe.AsPointer(ref this));
    }
}
