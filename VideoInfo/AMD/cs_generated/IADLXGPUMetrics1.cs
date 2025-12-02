using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUMetrics1 : adlx::IADLXGPUMetrics")]
public unsafe partial struct IADLXGPUMetrics1
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, int>)(lpVtbl[0]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, int>)(lpVtbl[1]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT TimeStamp([NativeTypeName("adlx_int64 *")] long* ms)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, long*, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), ms);
    }

    public ADLX_RESULT GPUUsage([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, double*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUClockSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, int*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUVRAMClockSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, double*, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUHotspotTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, double*, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUPower([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, double*, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUTotalBoardPower([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, double*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUFanSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, int*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUVRAM([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, int*, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUVoltage([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, int*, ADLX_RESULT>)(lpVtbl[13]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUIntakeTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, double*, ADLX_RESULT>)(lpVtbl[14]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT GPUMemoryTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, double*, ADLX_RESULT>)(lpVtbl[15]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT NPUFrequency([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, int*, ADLX_RESULT>)(lpVtbl[16]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT NPUActivityLevel([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, int*, ADLX_RESULT>)(lpVtbl[17]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }
}
