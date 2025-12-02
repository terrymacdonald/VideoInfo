using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DSettingsChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DSettingsChangedHandling
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedHandling*, int>)(lpVtbl[0]))((IADLX3DSettingsChangedHandling*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedHandling*, int>)(lpVtbl[1]))((IADLX3DSettingsChangedHandling*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedHandling*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DSettingsChangedHandling*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT Add3DSettingsEventListener([NativeTypeName("adlx::IADLX3DSettingsChangedListener *")] IADLX3DSettingsChangedListener* p3DSettingsChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedHandling*, IADLX3DSettingsChangedListener*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DSettingsChangedHandling*)Unsafe.AsPointer(ref this), p3DSettingsChangedListener);
    }

    public ADLX_RESULT Remove3DSettingsEventListener([NativeTypeName("adlx::IADLX3DSettingsChangedListener *")] IADLX3DSettingsChangedListener* p3DSettingsChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedHandling*, IADLX3DSettingsChangedListener*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DSettingsChangedHandling*)Unsafe.AsPointer(ref this), p3DSettingsChangedListener);
    }
}
