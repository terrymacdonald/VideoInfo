using System.Runtime.CompilerServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXInterface
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXInterface*, int>)(lpVtbl[0]))((IADLXInterface*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXInterface*, int>)(lpVtbl[1]))((IADLXInterface*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXInterface*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXInterface*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }
}
