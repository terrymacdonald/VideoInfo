using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayConnectivityExperience : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayConnectivityExperience
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, int>)(lpVtbl[0]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, int>)(lpVtbl[1]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupportedHDMIQualityDetection([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsSupportedDPLink([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsEnabledHDMIQualityDetection([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, bool*, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), enabled);
    }

    public ADLX_RESULT SetEnabledHDMIQualityDetection([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, byte, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), enabled);
    }

    public ADLX_RESULT GetDPLinkRate(ADLX_DP_LINK_RATE* linkRate)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, ADLX_DP_LINK_RATE*, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), linkRate);
    }

    public ADLX_RESULT GetNumberOfActiveLanes([NativeTypeName("adlx_uint *")] uint* numActiveLanes)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, uint*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), numActiveLanes);
    }

    public ADLX_RESULT GetNumberOfTotalLanes([NativeTypeName("adlx_uint *")] uint* numTotalLanes)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, uint*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), numTotalLanes);
    }

    public ADLX_RESULT GetRelativePreEmphasis([NativeTypeName("adlx_int *")] int* relativePreEmphasis)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, int*, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), relativePreEmphasis);
    }

    public ADLX_RESULT SetRelativePreEmphasis([NativeTypeName("adlx_int")] int relativePreEmphasis)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, int, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), relativePreEmphasis);
    }

    public ADLX_RESULT GetRelativeVoltageSwing([NativeTypeName("adlx_int *")] int* relativeVoltageSwing)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, int*, ADLX_RESULT>)(lpVtbl[12]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), relativeVoltageSwing);
    }

    public ADLX_RESULT SetRelativeVoltageSwing([NativeTypeName("adlx_int")] int relativeVoltageSwing)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, int, ADLX_RESULT>)(lpVtbl[13]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), relativeVoltageSwing);
    }

    public ADLX_RESULT IsEnabledLinkProtection([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, bool*, ADLX_RESULT>)(lpVtbl[14]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), enabled);
    }
}
