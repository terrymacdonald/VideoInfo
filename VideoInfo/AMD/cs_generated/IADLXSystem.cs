using System.Runtime.CompilerServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXSystem
{
    public void** lpVtbl;

    public ADLX_RESULT HybridGraphicsType(ADLX_HG_TYPE* hgType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, ADLX_HG_TYPE*, ADLX_RESULT>)(lpVtbl[0]))((IADLXSystem*)Unsafe.AsPointer(ref this), hgType);
    }

    public ADLX_RESULT GetGPUs(IADLXGPUList** ppGPUs)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, IADLXGPUList**, ADLX_RESULT>)(lpVtbl[1]))((IADLXSystem*)Unsafe.AsPointer(ref this), ppGPUs);
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSystem*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GetDisplaysServices(IADLXDisplayServices** ppDispServices)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, IADLXDisplayServices**, ADLX_RESULT>)(lpVtbl[3]))((IADLXSystem*)Unsafe.AsPointer(ref this), ppDispServices);
    }

    public ADLX_RESULT GetDesktopsServices(IADLXDesktopServices** ppDeskServices)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, IADLXDesktopServices**, ADLX_RESULT>)(lpVtbl[4]))((IADLXSystem*)Unsafe.AsPointer(ref this), ppDeskServices);
    }

    public ADLX_RESULT GetGPUsChangedHandling(IADLXGPUsChangedHandling** ppGPUsChangedHandling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, IADLXGPUsChangedHandling**, ADLX_RESULT>)(lpVtbl[5]))((IADLXSystem*)Unsafe.AsPointer(ref this), ppGPUsChangedHandling);
    }

    public ADLX_RESULT EnableLog(ADLX_LOG_DESTINATION mode, ADLX_LOG_SEVERITY severity, [NativeTypeName("adlx::IADLXLog *")] IADLXLog* pLogger, [NativeTypeName("const wchar_t *")] ushort* fileName)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, ADLX_LOG_DESTINATION, ADLX_LOG_SEVERITY, IADLXLog*, ushort*, ADLX_RESULT>)(lpVtbl[6]))((IADLXSystem*)Unsafe.AsPointer(ref this), mode, severity, pLogger, fileName);
    }

    public ADLX_RESULT Get3DSettingsServices(IADLX3DSettingsServices** pp3DSettingsServices)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, IADLX3DSettingsServices**, ADLX_RESULT>)(lpVtbl[7]))((IADLXSystem*)Unsafe.AsPointer(ref this), pp3DSettingsServices);
    }

    public ADLX_RESULT GetGPUTuningServices(IADLXGPUTuningServices** ppGPUTuningServices)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, IADLXGPUTuningServices**, ADLX_RESULT>)(lpVtbl[8]))((IADLXSystem*)Unsafe.AsPointer(ref this), ppGPUTuningServices);
    }

    public ADLX_RESULT GetPerformanceMonitoringServices(IADLXPerformanceMonitoringServices** ppPerformanceMonitoringServices)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, IADLXPerformanceMonitoringServices**, ADLX_RESULT>)(lpVtbl[9]))((IADLXSystem*)Unsafe.AsPointer(ref this), ppPerformanceMonitoringServices);
    }

    public ADLX_RESULT TotalSystemRAM([NativeTypeName("adlx_uint *")] uint* ramMB)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, uint*, ADLX_RESULT>)(lpVtbl[10]))((IADLXSystem*)Unsafe.AsPointer(ref this), ramMB);
    }

    public ADLX_RESULT GetI2C([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXI2C** ppI2C)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, IADLXGPU*, IADLXI2C**, ADLX_RESULT>)(lpVtbl[11]))((IADLXSystem*)Unsafe.AsPointer(ref this), pGPU, ppI2C);
    }
}
