using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayResolution : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayResolution
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolution*, int>)(lpVtbl[0]))((IADLXDisplayResolution*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolution*, int>)(lpVtbl[1]))((IADLXDisplayResolution*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolution*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayResolution*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GetValue(ADLX_CustomResolution* customResolution)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolution*, ADLX_CustomResolution*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayResolution*)Unsafe.AsPointer(ref this), customResolution);
    }

    public ADLX_RESULT SetValue(ADLX_CustomResolution customResolution)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolution*, ADLX_CustomResolution, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayResolution*)Unsafe.AsPointer(ref this), customResolution);
    }
}
