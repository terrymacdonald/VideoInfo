using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUList : adlx::IADLXList")]
public unsafe partial struct IADLXGPUList
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, int>)(lpVtbl[0]))((IADLXGPUList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, int>)(lpVtbl[1]))((IADLXGPUList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, uint>)(lpVtbl[3]))((IADLXGPUList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, byte>)(lpVtbl[4]))((IADLXGPUList*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, uint>)(lpVtbl[5]))((IADLXGPUList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, uint>)(lpVtbl[6]))((IADLXGPUList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPUList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPUList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPUList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPUList*)Unsafe.AsPointer(ref this), pItem);
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXGPU** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, uint, IADLXGPU**, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPUList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUList*, IADLXGPU*, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPUList*)Unsafe.AsPointer(ref this), pItem);
    }
}
