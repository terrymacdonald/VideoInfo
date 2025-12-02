using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayFreeSyncColorAccuracy : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayFreeSyncColorAccuracy
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayFreeSyncColorAccuracy*, int>)(lpVtbl[0]))((IADLXDisplayFreeSyncColorAccuracy*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayFreeSyncColorAccuracy*, int>)(lpVtbl[1]))((IADLXDisplayFreeSyncColorAccuracy*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayFreeSyncColorAccuracy*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayFreeSyncColorAccuracy*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayFreeSyncColorAccuracy*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayFreeSyncColorAccuracy*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayFreeSyncColorAccuracy*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayFreeSyncColorAccuracy*)Unsafe.AsPointer(ref this), enabled);
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayFreeSyncColorAccuracy*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayFreeSyncColorAccuracy*)Unsafe.AsPointer(ref this), enabled);
    }
}
