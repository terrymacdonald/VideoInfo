using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayScalingMode : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayScalingMode
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayScalingMode*, int>)(lpVtbl[0]))((IADLXDisplayScalingMode*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayScalingMode*, int>)(lpVtbl[1]))((IADLXDisplayScalingMode*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayScalingMode*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayScalingMode*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayScalingMode*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayScalingMode*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetMode(ADLX_SCALE_MODE* currentMode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayScalingMode*, ADLX_SCALE_MODE*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayScalingMode*)Unsafe.AsPointer(ref this), currentMode);
    }

    public ADLX_RESULT SetMode(ADLX_SCALE_MODE mode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayScalingMode*, ADLX_SCALE_MODE, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayScalingMode*)Unsafe.AsPointer(ref this), mode);
    }
}
