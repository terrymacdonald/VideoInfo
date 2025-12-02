using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DTessellation : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DTessellation
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DTessellation*, int>)(lpVtbl[0]))((IADLX3DTessellation*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DTessellation*, int>)(lpVtbl[1]))((IADLX3DTessellation*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DTessellation*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DTessellation*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DTessellation*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DTessellation*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetMode(ADLX_TESSELLATION_MODE* currentMode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DTessellation*, ADLX_TESSELLATION_MODE*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DTessellation*)Unsafe.AsPointer(ref this), currentMode);
    }

    public ADLX_RESULT GetLevel(ADLX_TESSELLATION_LEVEL* currentLevel)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DTessellation*, ADLX_TESSELLATION_LEVEL*, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DTessellation*)Unsafe.AsPointer(ref this), currentLevel);
    }

    public ADLX_RESULT SetMode(ADLX_TESSELLATION_MODE mode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DTessellation*, ADLX_TESSELLATION_MODE, ADLX_RESULT>)(lpVtbl[6]))((IADLX3DTessellation*)Unsafe.AsPointer(ref this), mode);
    }

    public ADLX_RESULT SetLevel(ADLX_TESSELLATION_LEVEL level)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DTessellation*, ADLX_TESSELLATION_LEVEL, ADLX_RESULT>)(lpVtbl[7]))((IADLX3DTessellation*)Unsafe.AsPointer(ref this), level);
    }
}
