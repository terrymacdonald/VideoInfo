using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUPresetTuning : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUPresetTuning
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, int>)(lpVtbl[0]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, int>)(lpVtbl[1]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupportedPowerSaver([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedQuiet([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedBalanced([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedTurbo([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedRage([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsCurrentPowerSaver([NativeTypeName("adlx_bool *")] bool* isPowerSaver)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), isPowerSaver);
    }

    public ADLX_RESULT IsCurrentQuiet([NativeTypeName("adlx_bool *")] bool* isQuiet)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), isQuiet);
    }

    public ADLX_RESULT IsCurrentBalanced([NativeTypeName("adlx_bool *")] bool* isBalance)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), isBalance);
    }

    public ADLX_RESULT IsCurrentTurbo([NativeTypeName("adlx_bool *")] bool* isTurbo)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), isTurbo);
    }

    public ADLX_RESULT IsCurrentRage([NativeTypeName("adlx_bool *")] bool* isRage)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), isRage);
    }

    public ADLX_RESULT SetPowerSaver()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, ADLX_RESULT>)(lpVtbl[13]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT SetQuiet()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, ADLX_RESULT>)(lpVtbl[14]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT SetBalanced()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, ADLX_RESULT>)(lpVtbl[15]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT SetTurbo()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, ADLX_RESULT>)(lpVtbl[16]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT SetRage()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, ADLX_RESULT>)(lpVtbl[17]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this));
    }
}
