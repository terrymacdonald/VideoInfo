using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXPowerTuningChangedEvent1 : adlx::IADLXPowerTuningChangedEvent")]
public unsafe partial struct IADLXPowerTuningChangedEvent1
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent1*, int>)(lpVtbl[0]))((IADLXPowerTuningChangedEvent1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent1*, int>)(lpVtbl[1]))((IADLXPowerTuningChangedEvent1*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXPowerTuningChangedEvent1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent1*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXPowerTuningChangedEvent1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsSmartShiftMaxChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent1*, byte>)(lpVtbl[4]))((IADLXPowerTuningChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsSmartShiftEcoChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent1*, byte>)(lpVtbl[5]))((IADLXPowerTuningChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }
}
