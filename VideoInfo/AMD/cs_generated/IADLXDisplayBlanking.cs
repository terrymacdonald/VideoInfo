using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayBlanking : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayBlanking
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayBlanking*, int>)(lpVtbl[0]))((IADLXDisplayBlanking*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayBlanking*, int>)(lpVtbl[1]))((IADLXDisplayBlanking*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayBlanking*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayBlanking*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayBlanking*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayBlanking*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsCurrentBlanked([NativeTypeName("adlx_bool *")] bool* blanked)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayBlanking*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayBlanking*)Unsafe.AsPointer(ref this), blanked);
    }

    public ADLX_RESULT IsCurrentUnblanked([NativeTypeName("adlx_bool *")] bool* unBlanked)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayBlanking*, bool*, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayBlanking*)Unsafe.AsPointer(ref this), unBlanked);
    }

    public ADLX_RESULT SetBlanked()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayBlanking*, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplayBlanking*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT SetUnblanked()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayBlanking*, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayBlanking*)Unsafe.AsPointer(ref this));
    }
}
