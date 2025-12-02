using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXMultimediaServices : adlx::IADLXInterface")]
public unsafe partial struct IADLXMultimediaServices
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaServices*, int>)(lpVtbl[0]))((IADLXMultimediaServices*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaServices*, int>)(lpVtbl[1]))((IADLXMultimediaServices*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaServices*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXMultimediaServices*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GetMultimediaChangedHandling(IADLXMultimediaChangedHandling** ppMultimediaChangedHandling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaServices*, IADLXMultimediaChangedHandling**, ADLX_RESULT>)(lpVtbl[3]))((IADLXMultimediaServices*)Unsafe.AsPointer(ref this), ppMultimediaChangedHandling);
    }

    public ADLX_RESULT GetVideoUpscale([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXVideoUpscale** ppVideoupscale)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaServices*, IADLXGPU*, IADLXVideoUpscale**, ADLX_RESULT>)(lpVtbl[4]))((IADLXMultimediaServices*)Unsafe.AsPointer(ref this), pGPU, ppVideoupscale);
    }

    public ADLX_RESULT GetVideoSuperResolution([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXVideoSuperResolution** ppVideoSuperResolution)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaServices*, IADLXGPU*, IADLXVideoSuperResolution**, ADLX_RESULT>)(lpVtbl[5]))((IADLXMultimediaServices*)Unsafe.AsPointer(ref this), pGPU, ppVideoSuperResolution);
    }
}
