using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXPowerTuningChangedEvent : adlx::IADLXChangedEvent")]
public unsafe partial struct IADLXPowerTuningChangedEvent
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent*, int>)(lpVtbl[0]))((IADLXPowerTuningChangedEvent*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent*, int>)(lpVtbl[1]))((IADLXPowerTuningChangedEvent*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXPowerTuningChangedEvent*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXPowerTuningChangedEvent*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsSmartShiftMaxChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent*, byte>)(lpVtbl[4]))((IADLXPowerTuningChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }
}
