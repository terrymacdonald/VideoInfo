using System.Runtime.CompilerServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXLog
{
    public void** lpVtbl;

    public ADLX_RESULT WriteLog([NativeTypeName("const wchar_t *")] ushort* msg)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXLog*, ushort*, ADLX_RESULT>)(lpVtbl[0]))((IADLXLog*)Unsafe.AsPointer(ref this), msg);
    }
}
