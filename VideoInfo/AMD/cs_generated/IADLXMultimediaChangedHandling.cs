using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXMultimediaChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLXMultimediaChangedHandling
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedHandling*, int>)(lpVtbl[0]))((IADLXMultimediaChangedHandling*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedHandling*, int>)(lpVtbl[1]))((IADLXMultimediaChangedHandling*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedHandling*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXMultimediaChangedHandling*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT AddMultimediaEventListener([NativeTypeName("adlx::IADLXMultimediaChangedEventListener *")] IADLXMultimediaChangedEventListener* pMultimediaChangedEventListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedHandling*, IADLXMultimediaChangedEventListener*, ADLX_RESULT>)(lpVtbl[3]))((IADLXMultimediaChangedHandling*)Unsafe.AsPointer(ref this), pMultimediaChangedEventListener);
    }

    public ADLX_RESULT RemoveMultimediaEventListener([NativeTypeName("adlx::IADLXMultimediaChangedEventListener *")] IADLXMultimediaChangedEventListener* pMultimediaChangedEventListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedHandling*, IADLXMultimediaChangedEventListener*, ADLX_RESULT>)(lpVtbl[4]))((IADLXMultimediaChangedHandling*)Unsafe.AsPointer(ref this), pMultimediaChangedEventListener);
    }
}
