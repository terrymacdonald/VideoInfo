using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplaySettingsChangedEvent3 : adlx::IADLXDisplaySettingsChangedEvent2")]
public unsafe partial struct IADLXDisplaySettingsChangedEvent3
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, int>)(lpVtbl[0]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, int>)(lpVtbl[1]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT GetDisplay(IADLXDisplay** ppDisplay)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, IADLXDisplay**, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this), ppDisplay);
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsFreeSyncChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, byte>)(lpVtbl[5]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsVSRChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, byte>)(lpVtbl[6]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsGPUScalingChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, byte>)(lpVtbl[7]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsScalingModeChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, byte>)(lpVtbl[8]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsIntegerScalingChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, byte>)(lpVtbl[9]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsColorDepthChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, byte>)(lpVtbl[10]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsPixelFormatChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, byte>)(lpVtbl[11]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsHDCPChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, byte>)(lpVtbl[12]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorHueChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, byte>)(lpVtbl[13]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorSaturationChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, byte>)(lpVtbl[14]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorBrightnessChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, byte>)(lpVtbl[15]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorTemperatureChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, byte>)(lpVtbl[16]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorContrastChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, byte>)(lpVtbl[17]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomResolutionChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, byte>)(lpVtbl[18]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsVariBrightChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, byte>)(lpVtbl[19]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsDisplayBlankingChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, byte>)(lpVtbl[20]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsDisplayConnectivityExperienceChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, byte>)(lpVtbl[21]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsDisplayDynamicRefreshRateControlChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, byte>)(lpVtbl[22]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsFreeSyncColorAccuracyChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent3*, byte>)(lpVtbl[23]))((IADLXDisplaySettingsChangedEvent3*)Unsafe.AsPointer(ref this)) != 0;
    }
}
