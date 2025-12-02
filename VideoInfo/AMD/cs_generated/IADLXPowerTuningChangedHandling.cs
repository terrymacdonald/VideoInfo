using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXPowerTuningChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLXPowerTuningChangedHandling
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedHandling*, int>)(lpVtbl[0]))((IADLXPowerTuningChangedHandling*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedHandling*, int>)(lpVtbl[1]))((IADLXPowerTuningChangedHandling*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedHandling*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXPowerTuningChangedHandling*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT AddPowerTuningEventListener([NativeTypeName("adlx::IADLXPowerTuningChangedListener *")] IADLXPowerTuningChangedListener* pPowerTuningChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedHandling*, IADLXPowerTuningChangedListener*, ADLX_RESULT>)(lpVtbl[3]))((IADLXPowerTuningChangedHandling*)Unsafe.AsPointer(ref this), pPowerTuningChangedListener);
    }

    public ADLX_RESULT RemovePowerTuningEventListener([NativeTypeName("adlx::IADLXPowerTuningChangedListener *")] IADLXPowerTuningChangedListener* pPowerTuningChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedHandling*, IADLXPowerTuningChangedListener*, ADLX_RESULT>)(lpVtbl[4]))((IADLXPowerTuningChangedHandling*)Unsafe.AsPointer(ref this), pPowerTuningChangedListener);
    }
}
