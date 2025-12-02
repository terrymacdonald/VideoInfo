using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayPixelFormat : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayPixelFormat
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, int>)(lpVtbl[0]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, int>)(lpVtbl[1]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetValue(ADLX_PIXEL_FORMAT* pixelFormat)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, ADLX_PIXEL_FORMAT*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), pixelFormat);
    }

    public ADLX_RESULT SetValue(ADLX_PIXEL_FORMAT pixelFormat)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, ADLX_PIXEL_FORMAT, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), pixelFormat);
    }

    public ADLX_RESULT IsSupportedPixelFormat(ADLX_PIXEL_FORMAT pixelFormat, [NativeTypeName("adlx_bool *")] bool* supportd)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, ADLX_PIXEL_FORMAT, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), pixelFormat, supportd);
    }

    public ADLX_RESULT IsSupportedRGB444Full([NativeTypeName("adlx_bool *")] bool* supportd)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, bool*, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), supportd);
    }

    public ADLX_RESULT IsSupportedYCbCr444([NativeTypeName("adlx_bool *")] bool* supportd)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), supportd);
    }

    public ADLX_RESULT IsSupportedYCbCr422([NativeTypeName("adlx_bool *")] bool* supportd)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), supportd);
    }

    public ADLX_RESULT IsSupportedRGB444Limited([NativeTypeName("adlx_bool *")] bool* supportd)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), supportd);
    }

    public ADLX_RESULT IsSupportedYCbCr420([NativeTypeName("adlx_bool *")] bool* supportd)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, bool*, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), supportd);
    }
}
