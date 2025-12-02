using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXSystemMetricsSupport1 : adlx::IADLXSystemMetricsSupport")]
public unsafe partial struct IADLXSystemMetricsSupport1
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, int>)(lpVtbl[0]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, int>)(lpVtbl[1]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupportedCPUUsage([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedSystemRAM([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedSmartShift([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[5]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetCPUUsageRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    public ADLX_RESULT GetSystemRAMRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[7]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    public ADLX_RESULT GetSmartShiftRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[8]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    public ADLX_RESULT IsSupportedPowerDistribution([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }
}
