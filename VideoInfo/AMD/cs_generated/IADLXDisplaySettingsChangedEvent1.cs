using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplaySettingsChangedEvent1 : adlx::IADLXDisplaySettingsChangedEvent")]
public unsafe partial struct IADLXDisplaySettingsChangedEvent1
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, int>)(lpVtbl[0]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, int>)(lpVtbl[1]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT GetDisplay(IADLXDisplay** ppDisplay)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, IADLXDisplay**, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this), ppDisplay);
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsFreeSyncChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, byte>)(lpVtbl[5]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsVSRChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, byte>)(lpVtbl[6]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsGPUScalingChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, byte>)(lpVtbl[7]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsScalingModeChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, byte>)(lpVtbl[8]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsIntegerScalingChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, byte>)(lpVtbl[9]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsColorDepthChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, byte>)(lpVtbl[10]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsPixelFormatChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, byte>)(lpVtbl[11]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsHDCPChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, byte>)(lpVtbl[12]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorHueChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, byte>)(lpVtbl[13]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorSaturationChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, byte>)(lpVtbl[14]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorBrightnessChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, byte>)(lpVtbl[15]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorTemperatureChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, byte>)(lpVtbl[16]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorContrastChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, byte>)(lpVtbl[17]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomResolutionChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, byte>)(lpVtbl[18]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsVariBrightChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, byte>)(lpVtbl[19]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsDisplayBlankingChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent1*, byte>)(lpVtbl[20]))((IADLXDisplaySettingsChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }
}
