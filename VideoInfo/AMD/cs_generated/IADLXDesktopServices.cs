using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDesktopServices : adlx::IADLXInterface")]
public unsafe partial struct IADLXDesktopServices
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopServices*, int>)(lpVtbl[0]))((IADLXDesktopServices*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopServices*, int>)(lpVtbl[1]))((IADLXDesktopServices*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopServices*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDesktopServices*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GetNumberOfDesktops([NativeTypeName("adlx_uint *")] uint* numDesktops)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopServices*, uint*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDesktopServices*)Unsafe.AsPointer(ref this), numDesktops);
    }

    public ADLX_RESULT GetDesktops(IADLXDesktopList** ppDesktops)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopServices*, IADLXDesktopList**, ADLX_RESULT>)(lpVtbl[4]))((IADLXDesktopServices*)Unsafe.AsPointer(ref this), ppDesktops);
    }

    public ADLX_RESULT GetDesktopChangedHandling(IADLXDesktopChangedHandling** ppDesktopChangedHandling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopServices*, IADLXDesktopChangedHandling**, ADLX_RESULT>)(lpVtbl[5]))((IADLXDesktopServices*)Unsafe.AsPointer(ref this), ppDesktopChangedHandling);
    }

    public ADLX_RESULT GetSimpleEyefinity(IADLXSimpleEyefinity** ppSimpleEyefinity)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopServices*, IADLXSimpleEyefinity**, ADLX_RESULT>)(lpVtbl[6]))((IADLXDesktopServices*)Unsafe.AsPointer(ref this), ppSimpleEyefinity);
    }
}
