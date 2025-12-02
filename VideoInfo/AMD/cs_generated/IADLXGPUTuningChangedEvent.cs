using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUTuningChangedEvent : adlx::IADLXChangedEvent")]
public unsafe partial struct IADLXGPUTuningChangedEvent
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent*, int>)(lpVtbl[0]))((IADLXGPUTuningChangedEvent*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent*, int>)(lpVtbl[1]))((IADLXGPUTuningChangedEvent*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUTuningChangedEvent*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXGPUTuningChangedEvent*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT GetGPU(IADLXGPU** ppGPU)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent*, IADLXGPU**, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUTuningChangedEvent*)Unsafe.AsPointer(ref this), ppGPU);
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsAutomaticTuningChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent*, byte>)(lpVtbl[5]))((IADLXGPUTuningChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsPresetTuningChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent*, byte>)(lpVtbl[6]))((IADLXGPUTuningChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsManualGPUCLKTuningChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent*, byte>)(lpVtbl[7]))((IADLXGPUTuningChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsManualVRAMTuningChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent*, byte>)(lpVtbl[8]))((IADLXGPUTuningChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsManualFanTuningChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent*, byte>)(lpVtbl[9]))((IADLXGPUTuningChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsManualPowerTuningChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent*, byte>)(lpVtbl[10]))((IADLXGPUTuningChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }
}
