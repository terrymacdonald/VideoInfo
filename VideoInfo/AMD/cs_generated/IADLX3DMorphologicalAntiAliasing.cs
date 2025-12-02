using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DMorphologicalAntiAliasing : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DMorphologicalAntiAliasing
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DMorphologicalAntiAliasing*, int>)(lpVtbl[0]))((IADLX3DMorphologicalAntiAliasing*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DMorphologicalAntiAliasing*, int>)(lpVtbl[1]))((IADLX3DMorphologicalAntiAliasing*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DMorphologicalAntiAliasing*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DMorphologicalAntiAliasing*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DMorphologicalAntiAliasing*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DMorphologicalAntiAliasing*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DMorphologicalAntiAliasing*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DMorphologicalAntiAliasing*)Unsafe.AsPointer(ref this), isEnabled);
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DMorphologicalAntiAliasing*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DMorphologicalAntiAliasing*)Unsafe.AsPointer(ref this), enable);
    }
}
