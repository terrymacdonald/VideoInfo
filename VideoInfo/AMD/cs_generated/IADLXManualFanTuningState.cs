using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualFanTuningState : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualFanTuningState
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningState*, int>)(lpVtbl[0]))((IADLXManualFanTuningState*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningState*, int>)(lpVtbl[1]))((IADLXManualFanTuningState*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningState*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualFanTuningState*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GetFanSpeed([NativeTypeName("adlx_int *")] int* value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningState*, int*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualFanTuningState*)Unsafe.AsPointer(ref this), value);
    }

    public ADLX_RESULT SetFanSpeed([NativeTypeName("adlx_int")] int value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningState*, int, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualFanTuningState*)Unsafe.AsPointer(ref this), value);
    }

    public ADLX_RESULT GetTemperature([NativeTypeName("adlx_int *")] int* value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningState*, int*, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualFanTuningState*)Unsafe.AsPointer(ref this), value);
    }

    public ADLX_RESULT SetTemperature([NativeTypeName("adlx_int")] int value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningState*, int, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualFanTuningState*)Unsafe.AsPointer(ref this), value);
    }
}
