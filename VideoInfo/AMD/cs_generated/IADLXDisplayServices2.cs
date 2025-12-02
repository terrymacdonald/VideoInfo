using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayServices2 : adlx::IADLXDisplayServices1")]
public unsafe partial struct IADLXDisplayServices2
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, int>)(lpVtbl[0]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, int>)(lpVtbl[1]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GetNumberOfDisplays([NativeTypeName("adlx_uint *")] uint* numDisplays)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, uint*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), numDisplays);
    }

    public ADLX_RESULT GetDisplays(IADLXDisplayList** ppDisplay)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, IADLXDisplayList**, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), ppDisplay);
    }

    public ADLX_RESULT Get3DLUT([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplay3DLUT** ppDisp3DLUT)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, IADLXDisplay*, IADLXDisplay3DLUT**, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), pDisplay, ppDisp3DLUT);
    }

    public ADLX_RESULT GetGamut([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayGamut** ppDispGamut)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, IADLXDisplay*, IADLXDisplayGamut**, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), pDisplay, ppDispGamut);
    }

    public ADLX_RESULT GetGamma([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayGamma** ppDispGamma)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, IADLXDisplay*, IADLXDisplayGamma**, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), pDisplay, ppDispGamma);
    }

    public ADLX_RESULT GetDisplayChangedHandling(IADLXDisplayChangedHandling** ppDisplayChangedHandling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, IADLXDisplayChangedHandling**, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), ppDisplayChangedHandling);
    }

    public ADLX_RESULT GetFreeSync([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayFreeSync** ppFreeSync)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, IADLXDisplay*, IADLXDisplayFreeSync**, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), pDisplay, ppFreeSync);
    }

    public ADLX_RESULT GetVirtualSuperResolution([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayVSR** ppVSR)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, IADLXDisplay*, IADLXDisplayVSR**, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), pDisplay, ppVSR);
    }

    public ADLX_RESULT GetGPUScaling([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayGPUScaling** ppGPUScaling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, IADLXDisplay*, IADLXDisplayGPUScaling**, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), pDisplay, ppGPUScaling);
    }

    public ADLX_RESULT GetScalingMode([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayScalingMode** ppScalingMode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, IADLXDisplay*, IADLXDisplayScalingMode**, ADLX_RESULT>)(lpVtbl[12]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), pDisplay, ppScalingMode);
    }

    public ADLX_RESULT GetIntegerScaling([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayIntegerScaling** ppIntegerScaling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, IADLXDisplay*, IADLXDisplayIntegerScaling**, ADLX_RESULT>)(lpVtbl[13]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), pDisplay, ppIntegerScaling);
    }

    public ADLX_RESULT GetColorDepth([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayColorDepth** ppColorDepth)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, IADLXDisplay*, IADLXDisplayColorDepth**, ADLX_RESULT>)(lpVtbl[14]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), pDisplay, ppColorDepth);
    }

    public ADLX_RESULT GetPixelFormat([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayPixelFormat** ppPixelFormat)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, IADLXDisplay*, IADLXDisplayPixelFormat**, ADLX_RESULT>)(lpVtbl[15]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), pDisplay, ppPixelFormat);
    }

    public ADLX_RESULT GetCustomColor([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayCustomColor** ppCustomColor)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, IADLXDisplay*, IADLXDisplayCustomColor**, ADLX_RESULT>)(lpVtbl[16]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), pDisplay, ppCustomColor);
    }

    public ADLX_RESULT GetHDCP([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayHDCP** ppHDCP)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, IADLXDisplay*, IADLXDisplayHDCP**, ADLX_RESULT>)(lpVtbl[17]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), pDisplay, ppHDCP);
    }

    public ADLX_RESULT GetCustomResolution([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayCustomResolution** ppCustomResolution)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, IADLXDisplay*, IADLXDisplayCustomResolution**, ADLX_RESULT>)(lpVtbl[18]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), pDisplay, ppCustomResolution);
    }

    public ADLX_RESULT GetVariBright([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayVariBright** ppVariBright)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, IADLXDisplay*, IADLXDisplayVariBright**, ADLX_RESULT>)(lpVtbl[19]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), pDisplay, ppVariBright);
    }

    public ADLX_RESULT GetDisplayBlanking([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayBlanking** ppDisplayBlanking)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, IADLXDisplay*, IADLXDisplayBlanking**, ADLX_RESULT>)(lpVtbl[20]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), pDisplay, ppDisplayBlanking);
    }

    public ADLX_RESULT GetDisplayConnectivityExperience([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayConnectivityExperience** ppDisplayConnectivityExperience)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices2*, IADLXDisplay*, IADLXDisplayConnectivityExperience**, ADLX_RESULT>)(lpVtbl[21]))((IADLXDisplayServices2*)Unsafe.AsPointer(ref this), pDisplay, ppDisplayConnectivityExperience);
    }
}
