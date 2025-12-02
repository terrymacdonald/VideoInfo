using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXSystemMetrics : adlx::IADLXInterface")]
public unsafe partial struct IADLXSystemMetrics
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetrics*, int>)(lpVtbl[0]))((IADLXSystemMetrics*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetrics*, int>)(lpVtbl[1]))((IADLXSystemMetrics*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetrics*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSystemMetrics*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT TimeStamp([NativeTypeName("adlx_int64 *")] long* ms)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetrics*, long*, ADLX_RESULT>)(lpVtbl[3]))((IADLXSystemMetrics*)Unsafe.AsPointer(ref this), ms);
    }

    public ADLX_RESULT CPUUsage([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetrics*, double*, ADLX_RESULT>)(lpVtbl[4]))((IADLXSystemMetrics*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT SystemRAM([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetrics*, int*, ADLX_RESULT>)(lpVtbl[5]))((IADLXSystemMetrics*)Unsafe.AsPointer(ref this), data);
    }

    public ADLX_RESULT SmartShift([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetrics*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLXSystemMetrics*)Unsafe.AsPointer(ref this), data);
    }
}
