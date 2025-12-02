using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayChangedHandling
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, int>)(lpVtbl[0]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, int>)(lpVtbl[1]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT AddDisplayListEventListener([NativeTypeName("adlx::IADLXDisplayListChangedListener *")] IADLXDisplayListChangedListener* pDisplayListChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplayListChangedListener*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplayListChangedListener);
    }

    public ADLX_RESULT RemoveDisplayListEventListener([NativeTypeName("adlx::IADLXDisplayListChangedListener *")] IADLXDisplayListChangedListener* pDisplayListChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplayListChangedListener*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplayListChangedListener);
    }

    public ADLX_RESULT AddDisplayGamutEventListener([NativeTypeName("adlx::IADLXDisplayGamutChangedListener *")] IADLXDisplayGamutChangedListener* pDisplayGamutChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplayGamutChangedListener*, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplayGamutChangedListener);
    }

    public ADLX_RESULT RemoveDisplayGamutEventListener([NativeTypeName("adlx::IADLXDisplayGamutChangedListener *")] IADLXDisplayGamutChangedListener* pDisplayGamutChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplayGamutChangedListener*, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplayGamutChangedListener);
    }

    public ADLX_RESULT AddDisplayGammaEventListener([NativeTypeName("adlx::IADLXDisplayGammaChangedListener *")] IADLXDisplayGammaChangedListener* pDisplayGammaChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplayGammaChangedListener*, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplayGammaChangedListener);
    }

    public ADLX_RESULT RemoveDisplayGammaEventListener([NativeTypeName("adlx::IADLXDisplayGammaChangedListener *")] IADLXDisplayGammaChangedListener* pDisplayGammaChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplayGammaChangedListener*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplayGammaChangedListener);
    }

    public ADLX_RESULT AddDisplay3DLUTEventListener([NativeTypeName("adlx::IADLXDisplay3DLUTChangedListener *")] IADLXDisplay3DLUTChangedListener* pDisplay3DLUTChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplay3DLUTChangedListener*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplay3DLUTChangedListener);
    }

    public ADLX_RESULT RemoveDisplay3DLUTEventListener([NativeTypeName("adlx::IADLXDisplay3DLUTChangedListener *")] IADLXDisplay3DLUTChangedListener* pDisplay3DLUTChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplay3DLUTChangedListener*, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplay3DLUTChangedListener);
    }

    public ADLX_RESULT AddDisplaySettingsEventListener([NativeTypeName("adlx::IADLXDisplaySettingsChangedListener *")] IADLXDisplaySettingsChangedListener* pDisplaySettingsChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplaySettingsChangedListener*, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplaySettingsChangedListener);
    }

    public ADLX_RESULT RemoveDisplaySettingsEventListener([NativeTypeName("adlx::IADLXDisplaySettingsChangedListener *")] IADLXDisplaySettingsChangedListener* pDisplaySettingsChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayChangedHandling*, IADLXDisplaySettingsChangedListener*, ADLX_RESULT>)(lpVtbl[12]))((IADLXDisplayChangedHandling*)Unsafe.AsPointer(ref this), pDisplaySettingsChangedListener);
    }
}
