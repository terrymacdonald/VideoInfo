using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPU2List : adlx::IADLXList")]
public unsafe partial struct IADLXGPU2List
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2List*, int>)(lpVtbl[0]))((IADLXGPU2List*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2List*, int>)(lpVtbl[1]))((IADLXGPU2List*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2List*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPU2List*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2List*, uint>)(lpVtbl[3]))((IADLXGPU2List*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2List*, byte>)(lpVtbl[4]))((IADLXGPU2List*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2List*, uint>)(lpVtbl[5]))((IADLXGPU2List*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2List*, uint>)(lpVtbl[6]))((IADLXGPU2List*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2List*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPU2List*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2List*, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPU2List*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2List*, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPU2List*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2List*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPU2List*)Unsafe.AsPointer(ref this), pItem);
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXGPU2** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2List*, uint, IADLXGPU2**, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPU2List*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXGPU2 *")] IADLXGPU2* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPU2List*, IADLXGPU2*, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPU2List*)Unsafe.AsPointer(ref this), pItem);
    }
}
