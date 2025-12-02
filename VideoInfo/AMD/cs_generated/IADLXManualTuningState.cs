using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualTuningState : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualTuningState
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningState*, int>)(lpVtbl[0]))((IADLXManualTuningState*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningState*, int>)(lpVtbl[1]))((IADLXManualTuningState*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningState*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualTuningState*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GetFrequency([NativeTypeName("adlx_int *")] int* value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningState*, int*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualTuningState*)Unsafe.AsPointer(ref this), value);
    }

    public ADLX_RESULT SetFrequency([NativeTypeName("adlx_int")] int value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningState*, int, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualTuningState*)Unsafe.AsPointer(ref this), value);
    }

    public ADLX_RESULT GetVoltage([NativeTypeName("adlx_int *")] int* value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningState*, int*, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualTuningState*)Unsafe.AsPointer(ref this), value);
    }

    public ADLX_RESULT SetVoltage([NativeTypeName("adlx_int")] int value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningState*, int, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualTuningState*)Unsafe.AsPointer(ref this), value);
    }
}
