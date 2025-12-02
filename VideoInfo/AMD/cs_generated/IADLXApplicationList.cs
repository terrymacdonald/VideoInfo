using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXApplicationList : adlx::IADLXList")]
public unsafe partial struct IADLXApplicationList
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, int>)(lpVtbl[0]))((IADLXApplicationList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, int>)(lpVtbl[1]))((IADLXApplicationList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXApplicationList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, uint>)(lpVtbl[3]))((IADLXApplicationList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, byte>)(lpVtbl[4]))((IADLXApplicationList*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, uint>)(lpVtbl[5]))((IADLXApplicationList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, uint>)(lpVtbl[6]))((IADLXApplicationList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXApplicationList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXApplicationList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXApplicationList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXApplicationList*)Unsafe.AsPointer(ref this), pItem);
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXApplication** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, uint, IADLXApplication**, ADLX_RESULT>)(lpVtbl[11]))((IADLXApplicationList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXApplication *")] IADLXApplication* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplicationList*, IADLXApplication*, ADLX_RESULT>)(lpVtbl[12]))((IADLXApplicationList*)Unsafe.AsPointer(ref this), pItem);
    }
}
