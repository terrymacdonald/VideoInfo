using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXPowerTuningServices1 : adlx::IADLXPowerTuningServices")]
public unsafe partial struct IADLXPowerTuningServices1
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices1*, int>)(lpVtbl[0]))((IADLXPowerTuningServices1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices1*, int>)(lpVtbl[1]))((IADLXPowerTuningServices1*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXPowerTuningServices1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GetPowerTuningChangedHandling(IADLXPowerTuningChangedHandling** ppPowerTuningChangedHandling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices1*, IADLXPowerTuningChangedHandling**, ADLX_RESULT>)(lpVtbl[3]))((IADLXPowerTuningServices1*)Unsafe.AsPointer(ref this), ppPowerTuningChangedHandling);
    }

    public ADLX_RESULT GetSmartShiftMax(IADLXSmartShiftMax** ppSmartShiftMax)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices1*, IADLXSmartShiftMax**, ADLX_RESULT>)(lpVtbl[4]))((IADLXPowerTuningServices1*)Unsafe.AsPointer(ref this), ppSmartShiftMax);
    }

    public ADLX_RESULT GetSmartShiftEco(IADLXSmartShiftEco** ppSmartShiftEco)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices1*, IADLXSmartShiftEco**, ADLX_RESULT>)(lpVtbl[5]))((IADLXPowerTuningServices1*)Unsafe.AsPointer(ref this), ppSmartShiftEco);
    }

    public ADLX_RESULT IsGPUConnectSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices1*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXPowerTuningServices1*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetGPUConnectGPUs(IADLXGPU2List** ppGPUs)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices1*, IADLXGPU2List**, ADLX_RESULT>)(lpVtbl[7]))((IADLXPowerTuningServices1*)Unsafe.AsPointer(ref this), ppGPUs);
    }
}
