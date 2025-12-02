using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayResolutionList : adlx::IADLXList")]
public unsafe partial struct IADLXDisplayResolutionList
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, int>)(lpVtbl[0]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, int>)(lpVtbl[1]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, uint>)(lpVtbl[3]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, byte>)(lpVtbl[4]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, uint>)(lpVtbl[5]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, uint>)(lpVtbl[6]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this), pItem);
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXDisplayResolution** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, uint, IADLXDisplayResolution**, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXDisplayResolution *")] IADLXDisplayResolution* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayResolutionList*, IADLXDisplayResolution*, ADLX_RESULT>)(lpVtbl[12]))((IADLXDisplayResolutionList*)Unsafe.AsPointer(ref this), pItem);
    }
}
