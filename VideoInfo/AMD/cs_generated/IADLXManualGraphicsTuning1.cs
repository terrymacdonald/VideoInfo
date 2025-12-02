using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualGraphicsTuning1 : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualGraphicsTuning1
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning1*, int>)(lpVtbl[0]))((IADLXManualGraphicsTuning1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning1*, int>)(lpVtbl[1]))((IADLXManualGraphicsTuning1*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualGraphicsTuning1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GetGPUTuningRanges(ADLX_IntRange* frequencyRange, ADLX_IntRange* voltageRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning1*, ADLX_IntRange*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualGraphicsTuning1*)Unsafe.AsPointer(ref this), frequencyRange, voltageRange);
    }

    public ADLX_RESULT GetGPUTuningStates(IADLXManualTuningStateList** ppGFXStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning1*, IADLXManualTuningStateList**, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualGraphicsTuning1*)Unsafe.AsPointer(ref this), ppGFXStates);
    }

    public ADLX_RESULT GetEmptyGPUTuningStates(IADLXManualTuningStateList** ppGFXStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning1*, IADLXManualTuningStateList**, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualGraphicsTuning1*)Unsafe.AsPointer(ref this), ppGFXStates);
    }

    public ADLX_RESULT IsValidGPUTuningStates([NativeTypeName("adlx::IADLXManualTuningStateList *")] IADLXManualTuningStateList* pGFXStates, [NativeTypeName("adlx_int *")] int* errorIndex)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning1*, IADLXManualTuningStateList*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualGraphicsTuning1*)Unsafe.AsPointer(ref this), pGFXStates, errorIndex);
    }

    public ADLX_RESULT SetGPUTuningStates([NativeTypeName("adlx::IADLXManualTuningStateList *")] IADLXManualTuningStateList* pGFXStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning1*, IADLXManualTuningStateList*, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualGraphicsTuning1*)Unsafe.AsPointer(ref this), pGFXStates);
    }
}
