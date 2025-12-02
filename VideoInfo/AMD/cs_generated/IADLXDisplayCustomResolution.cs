using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayCustomResolution : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayCustomResolution
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomResolution*, int>)(lpVtbl[0]))((IADLXDisplayCustomResolution*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomResolution*, int>)(lpVtbl[1]))((IADLXDisplayCustomResolution*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomResolution*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayCustomResolution*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomResolution*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayCustomResolution*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT GetResolutionList(IADLXDisplayResolutionList** ppResolutionList)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomResolution*, IADLXDisplayResolutionList**, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayCustomResolution*)Unsafe.AsPointer(ref this), ppResolutionList);
    }

    public ADLX_RESULT GetCurrentAppliedResolution(IADLXDisplayResolution** ppResolution)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomResolution*, IADLXDisplayResolution**, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayCustomResolution*)Unsafe.AsPointer(ref this), ppResolution);
    }

    public ADLX_RESULT CreateNewResolution([NativeTypeName("adlx::IADLXDisplayResolution *")] IADLXDisplayResolution* pResolution)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomResolution*, IADLXDisplayResolution*, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplayCustomResolution*)Unsafe.AsPointer(ref this), pResolution);
    }

    public ADLX_RESULT DeleteResolution([NativeTypeName("adlx::IADLXDisplayResolution *")] IADLXDisplayResolution* pResolution)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayCustomResolution*, IADLXDisplayResolution*, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayCustomResolution*)Unsafe.AsPointer(ref this), pResolution);
    }
}
