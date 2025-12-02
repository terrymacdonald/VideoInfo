using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUMetricsSupport1 : adlx::IADLXGPUMetricsSupport")]
public unsafe partial struct IADLXGPUMetricsSupport1
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, int>)(lpVtbl[0]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, int>)(lpVtbl[1]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupportedGPUUsage([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedGPUClockSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedGPUVRAMClockSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedGPUTemperature([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedGPUHotspotTemperature([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedGPUPower([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedGPUTotalBoardPower([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedGPUFanSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedGPUVRAM([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedGPUVoltage([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetGPUUsageRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[13]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    public ADLX_RESULT GetGPUClockSpeedRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[14]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    public ADLX_RESULT GetGPUVRAMClockSpeedRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[15]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    public ADLX_RESULT GetGPUTemperatureRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[16]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    public ADLX_RESULT GetGPUHotspotTemperatureRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[17]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    public ADLX_RESULT GetGPUPowerRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[18]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    public ADLX_RESULT GetGPUFanSpeedRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[19]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    public ADLX_RESULT GetGPUVRAMRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[20]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    public ADLX_RESULT GetGPUVoltageRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[21]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    public ADLX_RESULT GetGPUTotalBoardPowerRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[22]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    public ADLX_RESULT GetGPUIntakeTemperatureRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[23]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    public ADLX_RESULT IsSupportedGPUIntakeTemperature([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[24]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedGPUMemoryTemperature([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[25]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetGPUMemoryTemperatureRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[26]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    public ADLX_RESULT IsSupportedNPUFrequency([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[27]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetNPUFrequencyRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[28]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    public ADLX_RESULT IsSupportedNPUActivityLevel([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[29]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetNPUActivityLevelRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[30]))((IADLXGPUMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }
}
