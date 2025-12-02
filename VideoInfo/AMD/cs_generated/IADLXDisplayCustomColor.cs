using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayCustomColor : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayCustomColor
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, int>)(lpVtbl[0]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, int>)(lpVtbl[1]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsHueSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetHueRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), range);
    }

    public ADLX_RESULT GetHue([NativeTypeName("adlx_int *")] int* currentHue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, int*, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), currentHue);
    }

    public ADLX_RESULT SetHue([NativeTypeName("adlx_int")] int hue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, int, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), hue);
    }

    public ADLX_RESULT IsSaturationSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, bool*, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetSaturationRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), range);
    }

    public ADLX_RESULT GetSaturation([NativeTypeName("adlx_int *")] int* currentSaturation)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, int*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), currentSaturation);
    }

    public ADLX_RESULT SetSaturation([NativeTypeName("adlx_int")] int saturation)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, int, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), saturation);
    }

    public ADLX_RESULT IsBrightnessSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, bool*, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetBrightnessRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[12]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), range);
    }

    public ADLX_RESULT GetBrightness([NativeTypeName("adlx_int *")] int* currentBrightness)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, int*, ADLX_RESULT>)(lpVtbl[13]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), currentBrightness);
    }

    public ADLX_RESULT SetBrightness([NativeTypeName("adlx_int")] int brightness)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, int, ADLX_RESULT>)(lpVtbl[14]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), brightness);
    }

    public ADLX_RESULT IsContrastSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, bool*, ADLX_RESULT>)(lpVtbl[15]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetContrastRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[16]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), range);
    }

    public ADLX_RESULT GetContrast([NativeTypeName("adlx_int *")] int* currentContrast)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, int*, ADLX_RESULT>)(lpVtbl[17]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), currentContrast);
    }

    public ADLX_RESULT SetContrast([NativeTypeName("adlx_int")] int contrast)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, int, ADLX_RESULT>)(lpVtbl[18]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), contrast);
    }

    public ADLX_RESULT IsTemperatureSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, bool*, ADLX_RESULT>)(lpVtbl[19]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetTemperatureRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[20]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), range);
    }

    public ADLX_RESULT GetTemperature([NativeTypeName("adlx_int *")] int* currentTemperature)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, int*, ADLX_RESULT>)(lpVtbl[21]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), currentTemperature);
    }

    public ADLX_RESULT SetTemperature([NativeTypeName("adlx_int")] int temperature)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomColor*, int, ADLX_RESULT>)(lpVtbl[22]))((IADLXDisplayCustomColor*)Unsafe.AsPointer(ref this), temperature);
    }
}
