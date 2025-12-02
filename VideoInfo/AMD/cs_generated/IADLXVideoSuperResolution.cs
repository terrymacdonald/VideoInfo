using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXVideoSuperResolution : adlx::IADLXInterface")]
public unsafe partial struct IADLXVideoSuperResolution
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoSuperResolution*, int>)(lpVtbl[0]))((IADLXVideoSuperResolution*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoSuperResolution*, int>)(lpVtbl[1]))((IADLXVideoSuperResolution*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoSuperResolution*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXVideoSuperResolution*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoSuperResolution*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXVideoSuperResolution*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoSuperResolution*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXVideoSuperResolution*)Unsafe.AsPointer(ref this), isEnabled);
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoSuperResolution*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLXVideoSuperResolution*)Unsafe.AsPointer(ref this), enable);
    }
}
