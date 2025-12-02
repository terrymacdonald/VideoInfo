using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualGraphicsTuning2 : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualGraphicsTuning2
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, int>)(lpVtbl[0]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, int>)(lpVtbl[1]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GetGPUMinFrequencyRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), tuningRange);
    }

    public ADLX_RESULT GetGPUMinFrequency([NativeTypeName("adlx_int *")] int* minFreq)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, int*, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), minFreq);
    }

    public ADLX_RESULT SetGPUMinFrequency([NativeTypeName("adlx_int")] int minFreq)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, int, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), minFreq);
    }

    public ADLX_RESULT GetGPUMaxFrequencyRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), tuningRange);
    }

    public ADLX_RESULT GetGPUMaxFrequency([NativeTypeName("adlx_int *")] int* maxFreq)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, int*, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), maxFreq);
    }

    public ADLX_RESULT SetGPUMaxFrequency([NativeTypeName("adlx_int")] int maxFreq)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, int, ADLX_RESULT>)(lpVtbl[8]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), maxFreq);
    }

    public ADLX_RESULT GetGPUVoltageRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[9]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), tuningRange);
    }

    public ADLX_RESULT GetGPUVoltage([NativeTypeName("adlx_int *")] int* volt)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, int*, ADLX_RESULT>)(lpVtbl[10]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), volt);
    }

    public ADLX_RESULT SetGPUVoltage([NativeTypeName("adlx_int")] int volt)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, int, ADLX_RESULT>)(lpVtbl[11]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), volt);
    }
}
