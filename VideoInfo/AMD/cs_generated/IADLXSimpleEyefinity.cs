using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXSimpleEyefinity : adlx::IADLXInterface")]
public unsafe partial struct IADLXSimpleEyefinity
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSimpleEyefinity*, int>)(lpVtbl[0]))((IADLXSimpleEyefinity*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSimpleEyefinity*, int>)(lpVtbl[1]))((IADLXSimpleEyefinity*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSimpleEyefinity*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSimpleEyefinity*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSimpleEyefinity*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXSimpleEyefinity*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT Create(IADLXEyefinityDesktop** ppEyefinityDesktop)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSimpleEyefinity*, IADLXEyefinityDesktop**, ADLX_RESULT>)(lpVtbl[4]))((IADLXSimpleEyefinity*)Unsafe.AsPointer(ref this), ppEyefinityDesktop);
    }

    public ADLX_RESULT DestroyAll()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSimpleEyefinity*, ADLX_RESULT>)(lpVtbl[5]))((IADLXSimpleEyefinity*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Destroy([NativeTypeName("adlx::IADLXEyefinityDesktop *")] IADLXEyefinityDesktop* pDesktop)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSimpleEyefinity*, IADLXEyefinityDesktop*, ADLX_RESULT>)(lpVtbl[6]))((IADLXSimpleEyefinity*)Unsafe.AsPointer(ref this), pDesktop);
    }
}
