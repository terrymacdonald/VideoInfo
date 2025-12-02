using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUTuningServices : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUTuningServices
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, int>)(lpVtbl[0]))((IADLXGPUTuningServices*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, int>)(lpVtbl[1]))((IADLXGPUTuningServices*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUTuningServices*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GetGPUTuningChangedHandling(IADLXGPUTuningChangedHandling** ppGPUTuningChangedHandling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPUTuningChangedHandling**, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUTuningServices*)Unsafe.AsPointer(ref this), ppGPUTuningChangedHandling);
    }

    public ADLX_RESULT IsAtFactory([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* isFactory)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUTuningServices*)Unsafe.AsPointer(ref this), pGPU, isFactory);
    }

    public ADLX_RESULT ResetToFactory([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPUTuningServices*)Unsafe.AsPointer(ref this), pGPU);
    }

    public ADLX_RESULT IsSupportedAutoTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPUTuningServices*)Unsafe.AsPointer(ref this), pGPU, supported);
    }

    public ADLX_RESULT IsSupportedPresetTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPUTuningServices*)Unsafe.AsPointer(ref this), pGPU, supported);
    }

    public ADLX_RESULT IsSupportedManualGFXTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPUTuningServices*)Unsafe.AsPointer(ref this), pGPU, supported);
    }

    public ADLX_RESULT IsSupportedManualVRAMTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPUTuningServices*)Unsafe.AsPointer(ref this), pGPU, supported);
    }

    public ADLX_RESULT IsSupportedManualFanTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPUTuningServices*)Unsafe.AsPointer(ref this), pGPU, supported);
    }

    public ADLX_RESULT IsSupportedManualPowerTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPUTuningServices*)Unsafe.AsPointer(ref this), pGPU, supported);
    }

    public ADLX_RESULT GetAutoTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppAutoTuning)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, IADLXInterface**, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPUTuningServices*)Unsafe.AsPointer(ref this), pGPU, ppAutoTuning);
    }

    public ADLX_RESULT GetPresetTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppPresetTuning)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, IADLXInterface**, ADLX_RESULT>)(lpVtbl[13]))((IADLXGPUTuningServices*)Unsafe.AsPointer(ref this), pGPU, ppPresetTuning);
    }

    public ADLX_RESULT GetManualGFXTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualGFXTuning)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, IADLXInterface**, ADLX_RESULT>)(lpVtbl[14]))((IADLXGPUTuningServices*)Unsafe.AsPointer(ref this), pGPU, ppManualGFXTuning);
    }

    public ADLX_RESULT GetManualVRAMTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualVRAMTuning)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, IADLXInterface**, ADLX_RESULT>)(lpVtbl[15]))((IADLXGPUTuningServices*)Unsafe.AsPointer(ref this), pGPU, ppManualVRAMTuning);
    }

    public ADLX_RESULT GetManualFanTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualFanTuning)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, IADLXInterface**, ADLX_RESULT>)(lpVtbl[16]))((IADLXGPUTuningServices*)Unsafe.AsPointer(ref this), pGPU, ppManualFanTuning);
    }

    public ADLX_RESULT GetManualPowerTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualPowerTuning)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, IADLXInterface**, ADLX_RESULT>)(lpVtbl[17]))((IADLXGPUTuningServices*)Unsafe.AsPointer(ref this), pGPU, ppManualPowerTuning);
    }
}
