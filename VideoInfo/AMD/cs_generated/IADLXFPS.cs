using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXFPS : adlx::IADLXInterface")]
public unsafe partial struct IADLXFPS
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPS*, int>)(lpVtbl[0]))((IADLXFPS*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPS*, int>)(lpVtbl[1]))((IADLXFPS*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPS*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXFPS*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT TimeStamp([NativeTypeName("adlx_int64 *")] long* ms)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPS*, long*, ADLX_RESULT>)(lpVtbl[3]))((IADLXFPS*)Unsafe.AsPointer(ref this), ms);
    }

    public ADLX_RESULT FPS([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXFPS*, int*, ADLX_RESULT>)(lpVtbl[4]))((IADLXFPS*)Unsafe.AsPointer(ref this), data);
    }
}
