using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXSmartShiftMax : adlx::IADLXInterface")]
public unsafe partial struct IADLXSmartShiftMax
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, int>)(lpVtbl[0]))((IADLXSmartShiftMax*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, int>)(lpVtbl[1]))((IADLXSmartShiftMax*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSmartShiftMax*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXSmartShiftMax*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetBiasMode(ADLX_SSM_BIAS_MODE* mode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, ADLX_SSM_BIAS_MODE*, ADLX_RESULT>)(lpVtbl[4]))((IADLXSmartShiftMax*)Unsafe.AsPointer(ref this), mode);
    }

    public ADLX_RESULT SetBiasMode(ADLX_SSM_BIAS_MODE mode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, ADLX_SSM_BIAS_MODE, ADLX_RESULT>)(lpVtbl[5]))((IADLXSmartShiftMax*)Unsafe.AsPointer(ref this), mode);
    }

    public ADLX_RESULT GetBiasRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[6]))((IADLXSmartShiftMax*)Unsafe.AsPointer(ref this), range);
    }

    public ADLX_RESULT GetBias([NativeTypeName("adlx_int *")] int* bias)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, int*, ADLX_RESULT>)(lpVtbl[7]))((IADLXSmartShiftMax*)Unsafe.AsPointer(ref this), bias);
    }

    public ADLX_RESULT SetBias([NativeTypeName("adlx_int")] int bias)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, int, ADLX_RESULT>)(lpVtbl[8]))((IADLXSmartShiftMax*)Unsafe.AsPointer(ref this), bias);
    }
}
