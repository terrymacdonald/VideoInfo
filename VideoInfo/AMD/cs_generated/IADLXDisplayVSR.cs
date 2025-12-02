using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayVSR : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayVSR
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVSR*, int>)(lpVtbl[0]))((IADLXDisplayVSR*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVSR*, int>)(lpVtbl[1]))((IADLXDisplayVSR*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVSR*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayVSR*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVSR*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayVSR*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVSR*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayVSR*)Unsafe.AsPointer(ref this), enabled);
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVSR*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayVSR*)Unsafe.AsPointer(ref this), enabled);
    }
}
