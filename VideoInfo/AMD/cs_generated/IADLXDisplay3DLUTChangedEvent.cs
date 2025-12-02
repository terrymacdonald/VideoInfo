using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplay3DLUTChangedEvent : adlx::IADLXChangedEvent")]
public unsafe partial struct IADLXDisplay3DLUTChangedEvent
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUTChangedEvent*, int>)(lpVtbl[0]))((IADLXDisplay3DLUTChangedEvent*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUTChangedEvent*, int>)(lpVtbl[1]))((IADLXDisplay3DLUTChangedEvent*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUTChangedEvent*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplay3DLUTChangedEvent*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUTChangedEvent*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXDisplay3DLUTChangedEvent*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT GetDisplay(IADLXDisplay** ppDisplay)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUTChangedEvent*, IADLXDisplay**, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplay3DLUTChangedEvent*)Unsafe.AsPointer(ref this), ppDisplay);
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsSCEChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUTChangedEvent*, byte>)(lpVtbl[5]))((IADLXDisplay3DLUTChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustom3DLUTChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUTChangedEvent*, byte>)(lpVtbl[6]))((IADLXDisplay3DLUTChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }
}
