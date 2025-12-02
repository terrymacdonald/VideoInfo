using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayColorDepth : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayColorDepth
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, int>)(lpVtbl[0]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, int>)(lpVtbl[1]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetValue(ADLX_COLOR_DEPTH* currentColorDepth)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, ADLX_COLOR_DEPTH*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), currentColorDepth);
    }

    public ADLX_RESULT SetValue(ADLX_COLOR_DEPTH colorDepth)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, ADLX_COLOR_DEPTH, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), colorDepth);
    }

    public ADLX_RESULT IsSupportedColorDepth(ADLX_COLOR_DEPTH colorDepth, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, ADLX_COLOR_DEPTH, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), colorDepth, supported);
    }

    public ADLX_RESULT IsSupportedBPC_6([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, bool*, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedBPC_8([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedBPC_10([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedBPC_12([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedBPC_14([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, bool*, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedBPC_16([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, bool*, ADLX_RESULT>)(lpVtbl[12]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), supported);
    }
}
