using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DRadeonSuperResolution : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DRadeonSuperResolution
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DRadeonSuperResolution*, int>)(lpVtbl[0]))((IADLX3DRadeonSuperResolution*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DRadeonSuperResolution*, int>)(lpVtbl[1]))((IADLX3DRadeonSuperResolution*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DRadeonSuperResolution*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DRadeonSuperResolution*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DRadeonSuperResolution*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DRadeonSuperResolution*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DRadeonSuperResolution*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DRadeonSuperResolution*)Unsafe.AsPointer(ref this), enabled);
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DRadeonSuperResolution*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DRadeonSuperResolution*)Unsafe.AsPointer(ref this), enable);
    }

    public ADLX_RESULT GetSharpnessRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DRadeonSuperResolution*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[6]))((IADLX3DRadeonSuperResolution*)Unsafe.AsPointer(ref this), range);
    }

    public ADLX_RESULT GetSharpness([NativeTypeName("adlx_int *")] int* currentSharpness)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DRadeonSuperResolution*, int*, ADLX_RESULT>)(lpVtbl[7]))((IADLX3DRadeonSuperResolution*)Unsafe.AsPointer(ref this), currentSharpness);
    }

    public ADLX_RESULT SetSharpness([NativeTypeName("adlx_int")] int sharpness)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DRadeonSuperResolution*, int, ADLX_RESULT>)(lpVtbl[8]))((IADLX3DRadeonSuperResolution*)Unsafe.AsPointer(ref this), sharpness);
    }
}
