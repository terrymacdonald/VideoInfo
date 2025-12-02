using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDesktopChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLXDesktopChangedHandling
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopChangedHandling*, int>)(lpVtbl[0]))((IADLXDesktopChangedHandling*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopChangedHandling*, int>)(lpVtbl[1]))((IADLXDesktopChangedHandling*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopChangedHandling*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDesktopChangedHandling*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT AddDesktopListEventListener([NativeTypeName("adlx::IADLXDesktopListChangedListener *")] IADLXDesktopListChangedListener* pDesktopListChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopChangedHandling*, IADLXDesktopListChangedListener*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDesktopChangedHandling*)Unsafe.AsPointer(ref this), pDesktopListChangedListener);
    }

    public ADLX_RESULT RemoveDesktopListEventListener([NativeTypeName("adlx::IADLXDesktopListChangedListener *")] IADLXDesktopListChangedListener* pDesktopListChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopChangedHandling*, IADLXDesktopListChangedListener*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDesktopChangedHandling*)Unsafe.AsPointer(ref this), pDesktopListChangedListener);
    }
}
