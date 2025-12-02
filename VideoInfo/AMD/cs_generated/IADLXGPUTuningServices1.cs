using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUTuningServices1 : adlx::IADLXGPUTuningServices")]
public unsafe partial struct IADLXGPUTuningServices1
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, int>)(lpVtbl[0]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, int>)(lpVtbl[1]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GetGPUTuningChangedHandling(IADLXGPUTuningChangedHandling** ppGPUTuningChangedHandling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPUTuningChangedHandling**, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), ppGPUTuningChangedHandling);
    }

    public ADLX_RESULT IsAtFactory([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* isFactory)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, isFactory);
    }

    public ADLX_RESULT ResetToFactory([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU);
    }

    public ADLX_RESULT IsSupportedAutoTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, supported);
    }

    public ADLX_RESULT IsSupportedPresetTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, supported);
    }

    public ADLX_RESULT IsSupportedManualGFXTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, supported);
    }

    public ADLX_RESULT IsSupportedManualVRAMTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, supported);
    }

    public ADLX_RESULT IsSupportedManualFanTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, supported);
    }

    public ADLX_RESULT IsSupportedManualPowerTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, supported);
    }

    public ADLX_RESULT GetAutoTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppAutoTuning)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, IADLXInterface**, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, ppAutoTuning);
    }

    public ADLX_RESULT GetPresetTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppPresetTuning)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, IADLXInterface**, ADLX_RESULT>)(lpVtbl[13]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, ppPresetTuning);
    }

    public ADLX_RESULT GetManualGFXTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualGFXTuning)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, IADLXInterface**, ADLX_RESULT>)(lpVtbl[14]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, ppManualGFXTuning);
    }

    public ADLX_RESULT GetManualVRAMTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualVRAMTuning)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, IADLXInterface**, ADLX_RESULT>)(lpVtbl[15]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, ppManualVRAMTuning);
    }

    public ADLX_RESULT GetManualFanTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualFanTuning)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, IADLXInterface**, ADLX_RESULT>)(lpVtbl[16]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, ppManualFanTuning);
    }

    public ADLX_RESULT GetManualPowerTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualPowerTuning)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, IADLXInterface**, ADLX_RESULT>)(lpVtbl[17]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, ppManualPowerTuning);
    }

    public ADLX_RESULT GetSmartAccessMemory([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXSmartAccessMemory** ppSmartAccessMemory)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, IADLXSmartAccessMemory**, ADLX_RESULT>)(lpVtbl[18]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, ppSmartAccessMemory);
    }
}
