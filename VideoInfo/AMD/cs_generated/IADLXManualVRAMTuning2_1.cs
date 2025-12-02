using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualVRAMTuning2_1 : adlx::IADLXManualVRAMTuning2")]
public unsafe partial struct IADLXManualVRAMTuning2_1
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2_1*, int>)(lpVtbl[0]))((IADLXManualVRAMTuning2_1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2_1*, int>)(lpVtbl[1]))((IADLXManualVRAMTuning2_1*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2_1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualVRAMTuning2_1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupportedMemoryTiming([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2_1*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualVRAMTuning2_1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetSupportedMemoryTimingDescriptionList(IADLXMemoryTimingDescriptionList** ppDescriptionList)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2_1*, IADLXMemoryTimingDescriptionList**, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualVRAMTuning2_1*)Unsafe.AsPointer(ref this), ppDescriptionList);
    }

    public ADLX_RESULT GetMemoryTimingDescription(ADLX_MEMORYTIMING_DESCRIPTION* description)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2_1*, ADLX_MEMORYTIMING_DESCRIPTION*, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualVRAMTuning2_1*)Unsafe.AsPointer(ref this), description);
    }

    public ADLX_RESULT SetMemoryTimingDescription(ADLX_MEMORYTIMING_DESCRIPTION description)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2_1*, ADLX_MEMORYTIMING_DESCRIPTION, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualVRAMTuning2_1*)Unsafe.AsPointer(ref this), description);
    }

    public ADLX_RESULT GetMaxVRAMFrequencyRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2_1*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualVRAMTuning2_1*)Unsafe.AsPointer(ref this), tuningRange);
    }

    public ADLX_RESULT GetMaxVRAMFrequency([NativeTypeName("adlx_int *")] int* freq)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2_1*, int*, ADLX_RESULT>)(lpVtbl[8]))((IADLXManualVRAMTuning2_1*)Unsafe.AsPointer(ref this), freq);
    }

    public ADLX_RESULT SetMaxVRAMFrequency([NativeTypeName("adlx_int")] int freq)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2_1*, int, ADLX_RESULT>)(lpVtbl[9]))((IADLXManualVRAMTuning2_1*)Unsafe.AsPointer(ref this), freq);
    }

    public ADLX_RESULT GetMaxVRAMFrequencyDefault([NativeTypeName("adlx_int *")] int* defaultVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2_1*, int*, ADLX_RESULT>)(lpVtbl[10]))((IADLXManualVRAMTuning2_1*)Unsafe.AsPointer(ref this), defaultVal);
    }
}
