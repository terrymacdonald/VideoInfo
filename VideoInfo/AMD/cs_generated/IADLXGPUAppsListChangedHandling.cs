using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUAppsListChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUAppsListChangedHandling
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAppsListChangedHandling*, int>)(lpVtbl[0]))((IADLXGPUAppsListChangedHandling*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAppsListChangedHandling*, int>)(lpVtbl[1]))((IADLXGPUAppsListChangedHandling*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAppsListChangedHandling*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUAppsListChangedHandling*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT AddGPUAppsListEventListener([NativeTypeName("adlx::IADLXGPUAppsListEventListener *")] IADLXGPUAppsListEventListener* pGPUAppsListEventListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAppsListChangedHandling*, IADLXGPUAppsListEventListener*, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUAppsListChangedHandling*)Unsafe.AsPointer(ref this), pGPUAppsListEventListener);
    }

    public ADLX_RESULT RemoveGPUAppsListEventListener([NativeTypeName("adlx::IADLXGPUAppsListEventListener *")] IADLXGPUAppsListEventListener* pGPUAppsListEventListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAppsListChangedHandling*, IADLXGPUAppsListEventListener*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUAppsListChangedHandling*)Unsafe.AsPointer(ref this), pGPUAppsListEventListener);
    }
}
