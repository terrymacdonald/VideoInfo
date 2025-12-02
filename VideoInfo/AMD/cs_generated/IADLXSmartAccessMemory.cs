using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXSmartAccessMemory : adlx::IADLXInterface")]
public unsafe partial struct IADLXSmartAccessMemory
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartAccessMemory*, int>)(lpVtbl[0]))((IADLXSmartAccessMemory*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartAccessMemory*, int>)(lpVtbl[1]))((IADLXSmartAccessMemory*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartAccessMemory*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSmartAccessMemory*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartAccessMemory*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXSmartAccessMemory*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartAccessMemory*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXSmartAccessMemory*)Unsafe.AsPointer(ref this), enabled);
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartAccessMemory*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLXSmartAccessMemory*)Unsafe.AsPointer(ref this), enabled);
    }
}
