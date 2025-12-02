using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUMetrics2 : adlx::IADLXGPUMetrics1")]
public unsafe partial struct IADLXGPUMetrics2
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics2*, int>)(lpVtbl[0]))((IADLXGPUMetrics2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics2*, int>)(lpVtbl[1]))((IADLXGPUMetrics2*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics2*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUMetrics2*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT TimeStamp([NativeTypeName("adlx_int64 *")] long* ms)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics2*, long*, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUMetrics2*)Unsafe.AsPointer(ref this), ms);
    }

    public ADLX_RESULT GPUUsage([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics2*, double*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUMetrics2*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUClockSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics2*, int*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPUMetrics2*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUVRAMClockSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics2*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPUMetrics2*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics2*, double*, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPUMetrics2*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUHotspotTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics2*, double*, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPUMetrics2*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUPower([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics2*, double*, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPUMetrics2*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUTotalBoardPower([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics2*, double*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPUMetrics2*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUFanSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics2*, int*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPUMetrics2*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUVRAM([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics2*, int*, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPUMetrics2*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUVoltage([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics2*, int*, ADLX_RESULT>)(lpVtbl[13]))((IADLXGPUMetrics2*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUIntakeTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics2*, double*, ADLX_RESULT>)(lpVtbl[14]))((IADLXGPUMetrics2*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUMemoryTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics2*, double*, ADLX_RESULT>)(lpVtbl[15]))((IADLXGPUMetrics2*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT NPUFrequency([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics2*, int*, ADLX_RESULT>)(lpVtbl[16]))((IADLXGPUMetrics2*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT NPUActivityLevel([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics2*, int*, ADLX_RESULT>)(lpVtbl[17]))((IADLXGPUMetrics2*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUSharedMemory([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics2*, int*, ADLX_RESULT>)(lpVtbl[18]))((IADLXGPUMetrics2*)Unsafe.AsPointer(ref this), data);
    }
}
