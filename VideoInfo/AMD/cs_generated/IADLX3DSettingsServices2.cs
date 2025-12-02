using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DSettingsServices2 : adlx::IADLX3DSettingsServices1")]
public unsafe partial struct IADLX3DSettingsServices2
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices2*, int>)(lpVtbl[0]))((IADLX3DSettingsServices2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices2*, int>)(lpVtbl[1]))((IADLX3DSettingsServices2*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices2*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DSettingsServices2*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GetAntiLag([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DAntiLag** pp3DAntiLag)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices2*, IADLXGPU*, IADLX3DAntiLag**, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DSettingsServices2*)Unsafe.AsPointer(ref this), pGPU, pp3DAntiLag);
    }

    public ADLX_RESULT GetChill([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DChill** pp3DChill)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices2*, IADLXGPU*, IADLX3DChill**, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DSettingsServices2*)Unsafe.AsPointer(ref this), pGPU, pp3DChill);
    }

    public ADLX_RESULT GetBoost([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DBoost** pp3DBoost)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices2*, IADLXGPU*, IADLX3DBoost**, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DSettingsServices2*)Unsafe.AsPointer(ref this), pGPU, pp3DBoost);
    }

    public ADLX_RESULT GetImageSharpening([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DImageSharpening** pp3DImageSharpening)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices2*, IADLXGPU*, IADLX3DImageSharpening**, ADLX_RESULT>)(lpVtbl[6]))((IADLX3DSettingsServices2*)Unsafe.AsPointer(ref this), pGPU, pp3DImageSharpening);
    }

    public ADLX_RESULT GetEnhancedSync([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DEnhancedSync** pp3DEnhancedSync)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices2*, IADLXGPU*, IADLX3DEnhancedSync**, ADLX_RESULT>)(lpVtbl[7]))((IADLX3DSettingsServices2*)Unsafe.AsPointer(ref this), pGPU, pp3DEnhancedSync);
    }

    public ADLX_RESULT GetWaitForVerticalRefresh([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DWaitForVerticalRefresh** pp3DWaitForVerticalRefresh)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices2*, IADLXGPU*, IADLX3DWaitForVerticalRefresh**, ADLX_RESULT>)(lpVtbl[8]))((IADLX3DSettingsServices2*)Unsafe.AsPointer(ref this), pGPU, pp3DWaitForVerticalRefresh);
    }

    public ADLX_RESULT GetFrameRateTargetControl([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DFrameRateTargetControl** pp3DFrameRateTargetControl)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices2*, IADLXGPU*, IADLX3DFrameRateTargetControl**, ADLX_RESULT>)(lpVtbl[9]))((IADLX3DSettingsServices2*)Unsafe.AsPointer(ref this), pGPU, pp3DFrameRateTargetControl);
    }

    public ADLX_RESULT GetAntiAliasing([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DAntiAliasing** pp3DAntiAliasing)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices2*, IADLXGPU*, IADLX3DAntiAliasing**, ADLX_RESULT>)(lpVtbl[10]))((IADLX3DSettingsServices2*)Unsafe.AsPointer(ref this), pGPU, pp3DAntiAliasing);
    }

    public ADLX_RESULT GetMorphologicalAntiAliasing([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DMorphologicalAntiAliasing** pp3DMorphologicalAntiAliasing)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices2*, IADLXGPU*, IADLX3DMorphologicalAntiAliasing**, ADLX_RESULT>)(lpVtbl[11]))((IADLX3DSettingsServices2*)Unsafe.AsPointer(ref this), pGPU, pp3DMorphologicalAntiAliasing);
    }

    public ADLX_RESULT GetAnisotropicFiltering([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DAnisotropicFiltering** pp3DAnisotropicFiltering)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices2*, IADLXGPU*, IADLX3DAnisotropicFiltering**, ADLX_RESULT>)(lpVtbl[12]))((IADLX3DSettingsServices2*)Unsafe.AsPointer(ref this), pGPU, pp3DAnisotropicFiltering);
    }

    public ADLX_RESULT GetTessellation([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DTessellation** pp3DTessellation)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices2*, IADLXGPU*, IADLX3DTessellation**, ADLX_RESULT>)(lpVtbl[13]))((IADLX3DSettingsServices2*)Unsafe.AsPointer(ref this), pGPU, pp3DTessellation);
    }

    public ADLX_RESULT GetRadeonSuperResolution(IADLX3DRadeonSuperResolution** pp3DRadeonSuperResolution)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices2*, IADLX3DRadeonSuperResolution**, ADLX_RESULT>)(lpVtbl[14]))((IADLX3DSettingsServices2*)Unsafe.AsPointer(ref this), pp3DRadeonSuperResolution);
    }

    public ADLX_RESULT GetResetShaderCache([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DResetShaderCache** pp3DResetShaderCache)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices2*, IADLXGPU*, IADLX3DResetShaderCache**, ADLX_RESULT>)(lpVtbl[15]))((IADLX3DSettingsServices2*)Unsafe.AsPointer(ref this), pGPU, pp3DResetShaderCache);
    }

    public ADLX_RESULT Get3DSettingsChangedHandling(IADLX3DSettingsChangedHandling** pp3DSettingsChangedHandling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices2*, IADLX3DSettingsChangedHandling**, ADLX_RESULT>)(lpVtbl[16]))((IADLX3DSettingsServices2*)Unsafe.AsPointer(ref this), pp3DSettingsChangedHandling);
    }

    public ADLX_RESULT GetAMDFluidMotionFrames(IADLX3DAMDFluidMotionFrames** pp3DAMDFluidMotionFrames)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices2*, IADLX3DAMDFluidMotionFrames**, ADLX_RESULT>)(lpVtbl[17]))((IADLX3DSettingsServices2*)Unsafe.AsPointer(ref this), pp3DAMDFluidMotionFrames);
    }

    public ADLX_RESULT GetImageSharpenDesktop([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DImageSharpenDesktop** pp3DImageSharpenDesktop)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices2*, IADLXGPU*, IADLX3DImageSharpenDesktop**, ADLX_RESULT>)(lpVtbl[18]))((IADLX3DSettingsServices2*)Unsafe.AsPointer(ref this), pGPU, pp3DImageSharpenDesktop);
    }
}
