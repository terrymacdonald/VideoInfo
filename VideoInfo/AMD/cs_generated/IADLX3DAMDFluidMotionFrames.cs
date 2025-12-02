using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DAMDFluidMotionFrames : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DAMDFluidMotionFrames
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames*, int>)(lpVtbl[0]))((IADLX3DAMDFluidMotionFrames*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames*, int>)(lpVtbl[1]))((IADLX3DAMDFluidMotionFrames*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DAMDFluidMotionFrames*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DAMDFluidMotionFrames*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DAMDFluidMotionFrames*)Unsafe.AsPointer(ref this), enabled);
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DAMDFluidMotionFrames*)Unsafe.AsPointer(ref this), enable);
    }
}
