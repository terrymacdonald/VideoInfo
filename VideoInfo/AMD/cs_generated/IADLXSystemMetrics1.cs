using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXSystemMetrics1 : adlx::IADLXSystemMetrics")]
public unsafe partial struct IADLXSystemMetrics1
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetrics1*, int>)(lpVtbl[0]))((IADLXSystemMetrics1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetrics1*, int>)(lpVtbl[1]))((IADLXSystemMetrics1*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetrics1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSystemMetrics1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT TimeStamp([NativeTypeName("adlx_int64 *")] long* ms)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetrics1*, long*, ADLX_RESULT>)(lpVtbl[3]))((IADLXSystemMetrics1*)Unsafe.AsPointer(ref this), ms);
    }

    public ADLX_RESULT CPUUsage([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetrics1*, double*, ADLX_RESULT>)(lpVtbl[4]))((IADLXSystemMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT SystemRAM([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetrics1*, int*, ADLX_RESULT>)(lpVtbl[5]))((IADLXSystemMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT SmartShift([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetrics1*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLXSystemMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT PowerDistribution([NativeTypeName("adlx_int *")] int* apuShiftValue, [NativeTypeName("adlx_int *")] int* gpuShiftValue, [NativeTypeName("adlx_int *")] int* apuShiftLimit, [NativeTypeName("adlx_int *")] int* gpuShiftLimit, [NativeTypeName("adlx_int *")] int* totalShiftLimit)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetrics1*, int*, int*, int*, int*, int*, ADLX_RESULT>)(lpVtbl[7]))((IADLXSystemMetrics1*)Unsafe.AsPointer(ref this), apuShiftValue, gpuShiftValue, apuShiftLimit, gpuShiftLimit, totalShiftLimit);
    }
}
