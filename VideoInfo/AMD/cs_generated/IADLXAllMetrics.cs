using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXAllMetrics : adlx::IADLXInterface")]
public unsafe partial struct IADLXAllMetrics
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetrics*, int>)(lpVtbl[0]))((IADLXAllMetrics*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetrics*, int>)(lpVtbl[1]))((IADLXAllMetrics*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetrics*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXAllMetrics*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT TimeStamp([NativeTypeName("adlx_int64 *")] long* ms)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetrics*, long*, ADLX_RESULT>)(lpVtbl[3]))((IADLXAllMetrics*)Unsafe.AsPointer(ref this), ms);
    }

    public ADLX_RESULT GetSystemMetrics(IADLXSystemMetrics** ppSystemMetrics)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetrics*, IADLXSystemMetrics**, ADLX_RESULT>)(lpVtbl[4]))((IADLXAllMetrics*)Unsafe.AsPointer(ref this), ppSystemMetrics);
    }

    public ADLX_RESULT GetFPS(IADLXFPS** ppFPS)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetrics*, IADLXFPS**, ADLX_RESULT>)(lpVtbl[5]))((IADLXAllMetrics*)Unsafe.AsPointer(ref this), ppFPS);
    }

    public ADLX_RESULT GetGPUMetrics([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXGPUMetrics** ppGPUMetrics)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetrics*, IADLXGPU*, IADLXGPUMetrics**, ADLX_RESULT>)(lpVtbl[6]))((IADLXAllMetrics*)Unsafe.AsPointer(ref this), pGPU, ppGPUMetrics);
    }
}
