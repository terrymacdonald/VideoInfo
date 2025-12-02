using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualFanTuningStateList : adlx::IADLXList")]
public unsafe partial struct IADLXManualFanTuningStateList
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, int>)(lpVtbl[0]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, int>)(lpVtbl[1]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, uint>)(lpVtbl[3]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, byte>)(lpVtbl[4]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, uint>)(lpVtbl[5]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, uint>)(lpVtbl[6]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this), pItem);
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXManualFanTuningState** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, uint, IADLXManualFanTuningState**, ADLX_RESULT>)(lpVtbl[11]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXManualFanTuningState *")] IADLXManualFanTuningState* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningStateList*, IADLXManualFanTuningState*, ADLX_RESULT>)(lpVtbl[12]))((IADLXManualFanTuningStateList*)Unsafe.AsPointer(ref this), pItem);
    }
}
