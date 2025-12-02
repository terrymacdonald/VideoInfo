using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplay3DLUT : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplay3DLUT
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, int>)(lpVtbl[0]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, int>)(lpVtbl[1]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupportedSCE([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedSCEVividGaming([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsCurrentSCEDisabled([NativeTypeName("adlx_bool *")] bool* sceDisabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, bool*, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this), sceDisabled);
    }

    public ADLX_RESULT IsCurrentSCEVividGaming([NativeTypeName("adlx_bool *")] bool* vividGaming)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this), vividGaming);
    }

    public ADLX_RESULT SetSCEDisabled()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT SetSCEVividGaming()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT IsSupportedSCEDynamicContrast([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsCurrentSCEDynamicContrast([NativeTypeName("adlx_bool *")] bool* dynamicContrast)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this), dynamicContrast);
    }

    public ADLX_RESULT GetSCEDynamicContrastRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this), range);
    }

    public ADLX_RESULT GetSCEDynamicContrast([NativeTypeName("adlx_int *")] int* contrast)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, int*, ADLX_RESULT>)(lpVtbl[12]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this), contrast);
    }

    public ADLX_RESULT SetSCEDynamicContrast([NativeTypeName("adlx_int")] int contrast)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, int, ADLX_RESULT>)(lpVtbl[13]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this), contrast);
    }

    public ADLX_RESULT IsSupportedUser3DLUT([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, bool*, ADLX_RESULT>)(lpVtbl[14]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT ClearUser3DLUT()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, ADLX_RESULT>)(lpVtbl[15]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT GetSDRUser3DLUT(ADLX_3DLUT_TRANSFER_FUNCTION* transferFunction, ADLX_3DLUT_COLORSPACE* colorSpace, [NativeTypeName("adlx_int *")] int* pointsNumber, ADLX_3DLUT_Data* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, ADLX_3DLUT_TRANSFER_FUNCTION*, ADLX_3DLUT_COLORSPACE*, int*, ADLX_3DLUT_Data*, ADLX_RESULT>)(lpVtbl[16]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this), transferFunction, colorSpace, pointsNumber, data);
    }

    public ADLX_RESULT SetSDRUser3DLUT(ADLX_3DLUT_TRANSFER_FUNCTION transferFunction, ADLX_3DLUT_COLORSPACE colorSpace, [NativeTypeName("adlx_int")] int pointsNumber, [NativeTypeName("const ADLX_3DLUT_Data *")] ADLX_3DLUT_Data* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, ADLX_3DLUT_TRANSFER_FUNCTION, ADLX_3DLUT_COLORSPACE, int, ADLX_3DLUT_Data*, ADLX_RESULT>)(lpVtbl[17]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this), transferFunction, colorSpace, pointsNumber, data);
    }

    public ADLX_RESULT GetHDRUser3DLUT(ADLX_3DLUT_TRANSFER_FUNCTION* transferFunction, ADLX_3DLUT_COLORSPACE* colorSpace, [NativeTypeName("adlx_int *")] int* pointsNumber, ADLX_3DLUT_Data* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, ADLX_3DLUT_TRANSFER_FUNCTION*, ADLX_3DLUT_COLORSPACE*, int*, ADLX_3DLUT_Data*, ADLX_RESULT>)(lpVtbl[18]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this), transferFunction, colorSpace, pointsNumber, data);
    }

    public ADLX_RESULT SetHDRUser3DLUT(ADLX_3DLUT_TRANSFER_FUNCTION transferFunction, ADLX_3DLUT_COLORSPACE colorSpace, [NativeTypeName("adlx_int")] int pointsNumber, [NativeTypeName("const ADLX_3DLUT_Data *")] ADLX_3DLUT_Data* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, ADLX_3DLUT_TRANSFER_FUNCTION, ADLX_3DLUT_COLORSPACE, int, ADLX_3DLUT_Data*, ADLX_RESULT>)(lpVtbl[19]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this), transferFunction, colorSpace, pointsNumber, data);
    }

    public ADLX_RESULT GetAllUser3DLUT(ADLX_3DLUT_TRANSFER_FUNCTION* transferFunction, ADLX_3DLUT_COLORSPACE* colorSpace, [NativeTypeName("adlx_int *")] int* pointsNumber, ADLX_3DLUT_Data* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, ADLX_3DLUT_TRANSFER_FUNCTION*, ADLX_3DLUT_COLORSPACE*, int*, ADLX_3DLUT_Data*, ADLX_RESULT>)(lpVtbl[20]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this), transferFunction, colorSpace, pointsNumber, data);
    }

    public ADLX_RESULT SetAllUser3DLUT(ADLX_3DLUT_TRANSFER_FUNCTION transferFunction, ADLX_3DLUT_COLORSPACE colorSpace, [NativeTypeName("adlx_int")] int pointsNumber, [NativeTypeName("const ADLX_3DLUT_Data *")] ADLX_3DLUT_Data* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, ADLX_3DLUT_TRANSFER_FUNCTION, ADLX_3DLUT_COLORSPACE, int, ADLX_3DLUT_Data*, ADLX_RESULT>)(lpVtbl[21]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this), transferFunction, colorSpace, pointsNumber, data);
    }

    public ADLX_RESULT GetUser3DLUTIndex([NativeTypeName("adlx_int")] int lutSize, [NativeTypeName("const ADLX_UINT16_RGB *")] ADLX_UINT16_RGB* rgbCoordinate, [NativeTypeName("adlx_int *")] int* index)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, int, ADLX_UINT16_RGB*, int*, ADLX_RESULT>)(lpVtbl[22]))((IADLXDisplay3DLUT*)Unsafe.AsPointer(ref this), lutSize, rgbCoordinate, index);
    }
}
