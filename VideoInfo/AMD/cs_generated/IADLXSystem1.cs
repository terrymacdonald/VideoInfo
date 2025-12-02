using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXSystem1 : adlx::IADLXInterface")]
public unsafe partial struct IADLXSystem1
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem1*, int>)(lpVtbl[0]))((IADLXSystem1*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem1*, int>)(lpVtbl[1]))((IADLXSystem1*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSystem1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GetPowerTuningServices(IADLXPowerTuningServices** ppPowerTuningServices)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem1*, IADLXPowerTuningServices**, ADLX_RESULT>)(lpVtbl[3]))((IADLXSystem1*)Unsafe.AsPointer(ref this), ppPowerTuningServices);
    }
}
