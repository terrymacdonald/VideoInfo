using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXMemoryTimingDescription : adlx::IADLXInterface")]
public unsafe partial struct IADLXMemoryTimingDescription
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescription*, int>)(lpVtbl[0]))((IADLXMemoryTimingDescription*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescription*, int>)(lpVtbl[1]))((IADLXMemoryTimingDescription*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescription*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXMemoryTimingDescription*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GetDescription(ADLX_MEMORYTIMING_DESCRIPTION* description)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMemoryTimingDescription*, ADLX_MEMORYTIMING_DESCRIPTION*, ADLX_RESULT>)(lpVtbl[3]))((IADLXMemoryTimingDescription*)Unsafe.AsPointer(ref this), description);
    }
}
