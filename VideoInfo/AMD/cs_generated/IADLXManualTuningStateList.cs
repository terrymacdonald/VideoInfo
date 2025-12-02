using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualTuningStateList : adlx::IADLXList")]
public unsafe partial struct IADLXManualTuningStateList
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, int>)(lpVtbl[0]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, int>)(lpVtbl[1]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, uint>)(lpVtbl[3]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, byte>)(lpVtbl[4]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, uint>)(lpVtbl[5]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, uint>)(lpVtbl[6]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this), pItem);
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXManualTuningState** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, uint, IADLXManualTuningState**, ADLX_RESULT>)(lpVtbl[11]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXManualTuningState *")] IADLXManualTuningState* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningStateList*, IADLXManualTuningState*, ADLX_RESULT>)(lpVtbl[12]))((IADLXManualTuningStateList*)Unsafe.AsPointer(ref this), pItem);
    }
}
