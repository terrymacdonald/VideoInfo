using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayIntegerScaling : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayIntegerScaling
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayIntegerScaling*, int>)(lpVtbl[0]))((IADLXDisplayIntegerScaling*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayIntegerScaling*, int>)(lpVtbl[1]))((IADLXDisplayIntegerScaling*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayIntegerScaling*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayIntegerScaling*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayIntegerScaling*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayIntegerScaling*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayIntegerScaling*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayIntegerScaling*)Unsafe.AsPointer(ref this), enabled);
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayIntegerScaling*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayIntegerScaling*)Unsafe.AsPointer(ref this), enabled);
    }
}
