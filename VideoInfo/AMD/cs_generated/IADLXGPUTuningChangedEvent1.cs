using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUTuningChangedEvent1 : adlx::IADLXGPUTuningChangedEvent")]
public unsafe partial struct IADLXGPUTuningChangedEvent1
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent1*, int>)(lpVtbl[0]))((IADLXGPUTuningChangedEvent1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent1*, int>)(lpVtbl[1]))((IADLXGPUTuningChangedEvent1*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUTuningChangedEvent1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent1*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXGPUTuningChangedEvent1*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT GetGPU(IADLXGPU** ppGPU)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent1*, IADLXGPU**, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUTuningChangedEvent1*)Unsafe.AsPointer(ref this), ppGPU);
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsAutomaticTuningChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent1*, byte>)(lpVtbl[5]))((IADLXGPUTuningChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsPresetTuningChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent1*, byte>)(lpVtbl[6]))((IADLXGPUTuningChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsManualGPUCLKTuningChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent1*, byte>)(lpVtbl[7]))((IADLXGPUTuningChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsManualVRAMTuningChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent1*, byte>)(lpVtbl[8]))((IADLXGPUTuningChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsManualFanTuningChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent1*, byte>)(lpVtbl[9]))((IADLXGPUTuningChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsManualPowerTuningChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent1*, byte>)(lpVtbl[10]))((IADLXGPUTuningChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsSmartAccessMemoryChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent1*, byte>)(lpVtbl[11]))((IADLXGPUTuningChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    public ADLX_RESULT GetSmartAccessMemoryStatus([NativeTypeName("adlx_bool *")] bool* pEnabled, [NativeTypeName("adlx_bool *")] bool* pCompleted)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedEvent1*, bool*, bool*, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPUTuningChangedEvent1*)Unsafe.AsPointer(ref this), pEnabled, pCompleted);
    }
}
