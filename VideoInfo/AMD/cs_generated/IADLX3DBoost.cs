using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DBoost : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DBoost
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DBoost*, int>)(lpVtbl[0]))((IADLX3DBoost*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DBoost*, int>)(lpVtbl[1]))((IADLX3DBoost*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DBoost*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DBoost*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DBoost*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DBoost*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DBoost*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DBoost*)Unsafe.AsPointer(ref this), isEnabled);
    }

    public ADLX_RESULT GetResolutionRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DBoost*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DBoost*)Unsafe.AsPointer(ref this), range);
    }

    public ADLX_RESULT GetResolution([NativeTypeName("adlx_int *")] int* currentMinRes)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DBoost*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLX3DBoost*)Unsafe.AsPointer(ref this), currentMinRes);
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DBoost*, byte, ADLX_RESULT>)(lpVtbl[7]))((IADLX3DBoost*)Unsafe.AsPointer(ref this), enable);
    }

    public ADLX_RESULT SetResolution([NativeTypeName("adlx_int")] int minRes)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DBoost*, int, ADLX_RESULT>)(lpVtbl[8]))((IADLX3DBoost*)Unsafe.AsPointer(ref this), minRes);
    }
}
