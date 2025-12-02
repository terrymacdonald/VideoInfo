using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUTuningChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUTuningChangedHandling
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedHandling*, int>)(lpVtbl[0]))((IADLXGPUTuningChangedHandling*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedHandling*, int>)(lpVtbl[1]))((IADLXGPUTuningChangedHandling*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedHandling*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUTuningChangedHandling*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT AddGPUTuningEventListener([NativeTypeName("adlx::IADLXGPUTuningChangedListener *")] IADLXGPUTuningChangedListener* pGPUTuningChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedHandling*, IADLXGPUTuningChangedListener*, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUTuningChangedHandling*)Unsafe.AsPointer(ref this), pGPUTuningChangedListener);
    }

    public ADLX_RESULT RemoveGPUTuningEventListener([NativeTypeName("adlx::IADLXGPUTuningChangedListener *")] IADLXGPUTuningChangedListener* pGPUTuningChangedListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedHandling*, IADLXGPUTuningChangedListener*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUTuningChangedHandling*)Unsafe.AsPointer(ref this), pGPUTuningChangedListener);
    }
}
