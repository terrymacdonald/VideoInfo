using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXList : adlx::IADLXInterface")]
public unsafe partial struct IADLXList
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXList*, int>)(lpVtbl[0]))((IADLXList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXList*, int>)(lpVtbl[1]))((IADLXList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXList*, uint>)(lpVtbl[3]))((IADLXList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXList*, byte>)(lpVtbl[4]))((IADLXList*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXList*, uint>)(lpVtbl[5]))((IADLXList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXList*, uint>)(lpVtbl[6]))((IADLXList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXList*)Unsafe.AsPointer(ref this), pItem);
    }
}
