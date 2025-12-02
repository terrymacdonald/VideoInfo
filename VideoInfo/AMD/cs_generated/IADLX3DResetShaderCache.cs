using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DResetShaderCache : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DResetShaderCache
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DResetShaderCache*, int>)(lpVtbl[0]))((IADLX3DResetShaderCache*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DResetShaderCache*, int>)(lpVtbl[1]))((IADLX3DResetShaderCache*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DResetShaderCache*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DResetShaderCache*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DResetShaderCache*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DResetShaderCache*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT ResetShaderCache()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DResetShaderCache*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DResetShaderCache*)Unsafe.AsPointer(ref this));
    }
}
