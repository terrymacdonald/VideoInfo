using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualVRAMTuning1 : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualVRAMTuning1
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, int>)(lpVtbl[0]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, int>)(lpVtbl[1]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupportedMemoryTiming([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetSupportedMemoryTimingDescriptionList(IADLXMemoryTimingDescriptionList** ppDescriptionList)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, IADLXMemoryTimingDescriptionList**, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), ppDescriptionList);
    }

    public ADLX_RESULT GetMemoryTimingDescription(ADLX_MEMORYTIMING_DESCRIPTION* description)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, ADLX_MEMORYTIMING_DESCRIPTION*, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), description);
    }

    public ADLX_RESULT SetMemoryTimingDescription(ADLX_MEMORYTIMING_DESCRIPTION description)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, ADLX_MEMORYTIMING_DESCRIPTION, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), description);
    }

    public ADLX_RESULT GetVRAMTuningRanges(ADLX_IntRange* frequencyRange, ADLX_IntRange* voltageRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, ADLX_IntRange*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), frequencyRange, voltageRange);
    }

    public ADLX_RESULT GetVRAMTuningStates(IADLXManualTuningStateList** ppVRAMStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, IADLXManualTuningStateList**, ADLX_RESULT>)(lpVtbl[8]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), ppVRAMStates);
    }

    public ADLX_RESULT GetEmptyVRAMTuningStates(IADLXManualTuningStateList** ppVRAMStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, IADLXManualTuningStateList**, ADLX_RESULT>)(lpVtbl[9]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), ppVRAMStates);
    }

    public ADLX_RESULT IsValidVRAMTuningStates([NativeTypeName("adlx::IADLXManualTuningStateList *")] IADLXManualTuningStateList* pVRAMStates, [NativeTypeName("adlx_int *")] int* errorIndex)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, IADLXManualTuningStateList*, int*, ADLX_RESULT>)(lpVtbl[10]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), pVRAMStates, errorIndex);
    }

    public ADLX_RESULT SetVRAMTuningStates([NativeTypeName("adlx::IADLXManualTuningStateList *")] IADLXManualTuningStateList* pVRAMStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, IADLXManualTuningStateList*, ADLX_RESULT>)(lpVtbl[11]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), pVRAMStates);
    }
}
