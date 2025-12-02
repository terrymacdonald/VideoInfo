using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUsChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUsChangedHandling
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUsChangedHandling*, int>)(lpVtbl[0]))((IADLXGPUsChangedHandling*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUsChangedHandling*, int>)(lpVtbl[1]))((IADLXGPUsChangedHandling*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUsChangedHandling*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUsChangedHandling*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT AddGPUsListEventListener([NativeTypeName("adlx::IADLXGPUsEventListener *")] IADLXGPUsEventListener* pListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUsChangedHandling*, IADLXGPUsEventListener*, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUsChangedHandling*)Unsafe.AsPointer(ref this), pListener);
    }

    public ADLX_RESULT RemoveGPUsListEventListener([NativeTypeName("adlx::IADLXGPUsEventListener *")] IADLXGPUsEventListener* pListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUsChangedHandling*, IADLXGPUsEventListener*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUsChangedHandling*)Unsafe.AsPointer(ref this), pListener);
    }
}
