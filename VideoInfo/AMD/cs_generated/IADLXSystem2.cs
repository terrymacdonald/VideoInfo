using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXSystem2 : adlx::IADLXSystem1")]
public unsafe partial struct IADLXSystem2
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem2*, int>)(lpVtbl[0]))((IADLXSystem2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem2*, int>)(lpVtbl[1]))((IADLXSystem2*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem2*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSystem2*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GetPowerTuningServices(IADLXPowerTuningServices** ppPowerTuningServices)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem2*, IADLXPowerTuningServices**, ADLX_RESULT>)(lpVtbl[3]))((IADLXSystem2*)Unsafe.AsPointer(ref this), ppPowerTuningServices);
    }

    public ADLX_RESULT GetMultimediaServices(IADLXMultimediaServices** ppMultiMediaServices)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem2*, IADLXMultimediaServices**, ADLX_RESULT>)(lpVtbl[4]))((IADLXSystem2*)Unsafe.AsPointer(ref this), ppMultiMediaServices);
    }

    public ADLX_RESULT GetGPUAppsListChangedHandling(IADLXGPUAppsListChangedHandling** ppGPUAppsListChangedHandling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem2*, IADLXGPUAppsListChangedHandling**, ADLX_RESULT>)(lpVtbl[5]))((IADLXSystem2*)Unsafe.AsPointer(ref this), ppGPUAppsListChangedHandling);
    }
}
