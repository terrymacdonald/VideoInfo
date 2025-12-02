using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualPowerTuning : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualPowerTuning
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, int>)(lpVtbl[0]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, int>)(lpVtbl[1]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GetPowerLimitRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this), tuningRange);
    }

    public ADLX_RESULT GetPowerLimit([NativeTypeName("adlx_int *")] int* curVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, int*, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this), curVal);
    }

    public ADLX_RESULT SetPowerLimit([NativeTypeName("adlx_int")] int curVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, int, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this), curVal);
    }

    public ADLX_RESULT IsSupportedTDCLimit([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetTDCLimitRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this), tuningRange);
    }

    public ADLX_RESULT GetTDCLimit([NativeTypeName("adlx_int *")] int* curVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, int*, ADLX_RESULT>)(lpVtbl[8]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this), curVal);
    }

    public ADLX_RESULT SetTDCLimit([NativeTypeName("adlx_int")] int curVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, int, ADLX_RESULT>)(lpVtbl[9]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this), curVal);
    }
}
