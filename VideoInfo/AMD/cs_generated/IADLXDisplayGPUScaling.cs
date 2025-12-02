using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayGPUScaling : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayGPUScaling
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGPUScaling*, int>)(lpVtbl[0]))((IADLXDisplayGPUScaling*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGPUScaling*, int>)(lpVtbl[1]))((IADLXDisplayGPUScaling*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGPUScaling*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayGPUScaling*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGPUScaling*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayGPUScaling*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGPUScaling*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayGPUScaling*)Unsafe.AsPointer(ref this), enabled);
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGPUScaling*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayGPUScaling*)Unsafe.AsPointer(ref this), enabled);
    }
}
