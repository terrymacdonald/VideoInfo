using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplaySettingsChangedEvent : adlx::IADLXChangedEvent")]
public unsafe partial struct IADLXDisplaySettingsChangedEvent
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, int>)(lpVtbl[0]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, int>)(lpVtbl[1]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT GetDisplay(IADLXDisplay** ppDisplay)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, IADLXDisplay**, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this), ppDisplay);
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsFreeSyncChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[5]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsVSRChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[6]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsGPUScalingChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[7]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsScalingModeChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[8]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsIntegerScalingChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[9]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsColorDepthChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[10]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsPixelFormatChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[11]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsHDCPChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[12]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorHueChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[13]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorSaturationChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[14]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorBrightnessChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[15]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorTemperatureChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[16]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorContrastChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[17]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomResolutionChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[18]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsVariBrightChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[19]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }
}
