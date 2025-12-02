using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualPowerTuning1 : adlx::IADLXManualPowerTuning")]
public unsafe partial struct IADLXManualPowerTuning1
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, int>)(lpVtbl[0]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, int>)(lpVtbl[1]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GetPowerLimitRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), tuningRange);
    }

    public ADLX_RESULT GetPowerLimit([NativeTypeName("adlx_int *")] int* curVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, int*, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), curVal);
    }

    public ADLX_RESULT SetPowerLimit([NativeTypeName("adlx_int")] int curVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, int, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), curVal);
    }

    public ADLX_RESULT IsSupportedTDCLimit([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetTDCLimitRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), tuningRange);
    }

    public ADLX_RESULT GetTDCLimit([NativeTypeName("adlx_int *")] int* curVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, int*, ADLX_RESULT>)(lpVtbl[8]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), curVal);
    }

    public ADLX_RESULT SetTDCLimit([NativeTypeName("adlx_int")] int curVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, int, ADLX_RESULT>)(lpVtbl[9]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), curVal);
    }

    public ADLX_RESULT GetPowerLimitDefault([NativeTypeName("adlx_int *")] int* defaultVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, int*, ADLX_RESULT>)(lpVtbl[10]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), defaultVal);
    }

    public ADLX_RESULT GetTDCLimitDefault([NativeTypeName("adlx_int *")] int* defaultVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, int*, ADLX_RESULT>)(lpVtbl[11]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), defaultVal);
    }
}
