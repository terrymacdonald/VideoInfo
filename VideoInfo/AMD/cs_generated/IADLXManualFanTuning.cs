using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualFanTuning : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualFanTuning
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, int>)(lpVtbl[0]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, int>)(lpVtbl[1]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GetFanTuningRanges(ADLX_IntRange* speedRange, ADLX_IntRange* temperatureRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, ADLX_IntRange*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), speedRange, temperatureRange);
    }

    public ADLX_RESULT GetFanTuningStates(IADLXManualFanTuningStateList** ppStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, IADLXManualFanTuningStateList**, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), ppStates);
    }

    public ADLX_RESULT GetEmptyFanTuningStates(IADLXManualFanTuningStateList** ppStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, IADLXManualFanTuningStateList**, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), ppStates);
    }

    public ADLX_RESULT IsValidFanTuningStates([NativeTypeName("adlx::IADLXManualFanTuningStateList *")] IADLXManualFanTuningStateList* pStates, [NativeTypeName("adlx_int *")] int* errorIndex)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, IADLXManualFanTuningStateList*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), pStates, errorIndex);
    }

    public ADLX_RESULT SetFanTuningStates([NativeTypeName("adlx::IADLXManualFanTuningStateList *")] IADLXManualFanTuningStateList* pStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, IADLXManualFanTuningStateList*, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), pStates);
    }

    public ADLX_RESULT IsSupportedZeroRPM([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetZeroRPMState([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), isSet);
    }

    public ADLX_RESULT SetZeroRPMState([NativeTypeName("adlx_bool")] byte set)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, byte, ADLX_RESULT>)(lpVtbl[10]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), set);
    }

    public ADLX_RESULT IsSupportedMinAcousticLimit([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, bool*, ADLX_RESULT>)(lpVtbl[11]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetMinAcousticLimitRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[12]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), tuningRange);
    }

    public ADLX_RESULT GetMinAcousticLimit([NativeTypeName("adlx_int *")] int* value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, int*, ADLX_RESULT>)(lpVtbl[13]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), value);
    }

    public ADLX_RESULT SetMinAcousticLimit([NativeTypeName("adlx_int")] int value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, int, ADLX_RESULT>)(lpVtbl[14]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), value);
    }

    public ADLX_RESULT IsSupportedMinFanSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, bool*, ADLX_RESULT>)(lpVtbl[15]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetMinFanSpeedRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[16]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), tuningRange);
    }

    public ADLX_RESULT GetMinFanSpeed([NativeTypeName("adlx_int *")] int* value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, int*, ADLX_RESULT>)(lpVtbl[17]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), value);
    }

    public ADLX_RESULT SetMinFanSpeed([NativeTypeName("adlx_int")] int value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, int, ADLX_RESULT>)(lpVtbl[18]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), value);
    }

    public ADLX_RESULT IsSupportedTargetFanSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, bool*, ADLX_RESULT>)(lpVtbl[19]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetTargetFanSpeedRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[20]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), tuningRange);
    }

    public ADLX_RESULT GetTargetFanSpeed([NativeTypeName("adlx_int *")] int* value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, int*, ADLX_RESULT>)(lpVtbl[21]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), value);
    }

    public ADLX_RESULT SetTargetFanSpeed([NativeTypeName("adlx_int")] int value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, int, ADLX_RESULT>)(lpVtbl[22]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), value);
    }
}
