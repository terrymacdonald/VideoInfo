using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayHDCP : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayHDCP
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayHDCP*, int>)(lpVtbl[0]))((IADLXDisplayHDCP*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayHDCP*, int>)(lpVtbl[1]))((IADLXDisplayHDCP*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayHDCP*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayHDCP*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayHDCP*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayHDCP*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayHDCP*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayHDCP*)Unsafe.AsPointer(ref this), enabled);
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayHDCP*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayHDCP*)Unsafe.AsPointer(ref this), enabled);
    }
}
