using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXMultimediaChangedEvent : adlx::IADLXChangedEvent")]
public unsafe partial struct IADLXMultimediaChangedEvent
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedEvent*, int>)(lpVtbl[0]))((IADLXMultimediaChangedEvent*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedEvent*, int>)(lpVtbl[1]))((IADLXMultimediaChangedEvent*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedEvent*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXMultimediaChangedEvent*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedEvent*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXMultimediaChangedEvent*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT GetGPU(IADLXGPU** ppGPU)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedEvent*, IADLXGPU**, ADLX_RESULT>)(lpVtbl[4]))((IADLXMultimediaChangedEvent*)Unsafe.AsPointer(ref this), ppGPU);
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsVideoUpscaleChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedEvent*, byte>)(lpVtbl[5]))((IADLXMultimediaChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsVideoSuperResolutionChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedEvent*, byte>)(lpVtbl[6]))((IADLXMultimediaChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }
}
