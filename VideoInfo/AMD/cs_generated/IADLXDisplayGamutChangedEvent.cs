using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayGamutChangedEvent : adlx::IADLXChangedEvent")]
public unsafe partial struct IADLXDisplayGamutChangedEvent
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamutChangedEvent*, int>)(lpVtbl[0]))((IADLXDisplayGamutChangedEvent*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamutChangedEvent*, int>)(lpVtbl[1]))((IADLXDisplayGamutChangedEvent*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamutChangedEvent*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayGamutChangedEvent*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamutChangedEvent*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXDisplayGamutChangedEvent*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT GetDisplay(IADLXDisplay** ppDisplay)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamutChangedEvent*, IADLXDisplay**, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayGamutChangedEvent*)Unsafe.AsPointer(ref this), ppDisplay);
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsWhitePointChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamutChangedEvent*, byte>)(lpVtbl[5]))((IADLXDisplayGamutChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsColorSpaceChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamutChangedEvent*, byte>)(lpVtbl[6]))((IADLXDisplayGamutChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }
}
