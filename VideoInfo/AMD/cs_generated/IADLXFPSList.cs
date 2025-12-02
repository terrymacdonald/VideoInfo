using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXFPSList : adlx::IADLXList")]
public unsafe partial struct IADLXFPSList
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPSList*, int>)(lpVtbl[0]))((IADLXFPSList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPSList*, int>)(lpVtbl[1]))((IADLXFPSList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPSList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXFPSList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPSList*, uint>)(lpVtbl[3]))((IADLXFPSList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPSList*, byte>)(lpVtbl[4]))((IADLXFPSList*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPSList*, uint>)(lpVtbl[5]))((IADLXFPSList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPSList*, uint>)(lpVtbl[6]))((IADLXFPSList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPSList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXFPSList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPSList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXFPSList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPSList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXFPSList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPSList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXFPSList*)Unsafe.AsPointer(ref this), pItem);
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXFPS** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPSList*, uint, IADLXFPS**, ADLX_RESULT>)(lpVtbl[11]))((IADLXFPSList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXFPS *")] IADLXFPS* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPSList*, IADLXFPS*, ADLX_RESULT>)(lpVtbl[12]))((IADLXFPSList*)Unsafe.AsPointer(ref this), pItem);
    }
}
