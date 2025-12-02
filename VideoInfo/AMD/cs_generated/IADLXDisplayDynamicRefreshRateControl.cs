using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayDynamicRefreshRateControl : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayDynamicRefreshRateControl
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayDynamicRefreshRateControl*, int>)(lpVtbl[0]))((IADLXDisplayDynamicRefreshRateControl*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayDynamicRefreshRateControl*, int>)(lpVtbl[1]))((IADLXDisplayDynamicRefreshRateControl*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayDynamicRefreshRateControl*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayDynamicRefreshRateControl*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayDynamicRefreshRateControl*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayDynamicRefreshRateControl*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayDynamicRefreshRateControl*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayDynamicRefreshRateControl*)Unsafe.AsPointer(ref this), enabled);
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayDynamicRefreshRateControl*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayDynamicRefreshRateControl*)Unsafe.AsPointer(ref this), enabled);
    }
}
