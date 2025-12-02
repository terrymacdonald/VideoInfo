using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDesktop : adlx::IADLXInterface")]
public unsafe partial struct IADLXDesktop
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktop*, int>)(lpVtbl[0]))((IADLXDesktop*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktop*, int>)(lpVtbl[1]))((IADLXDesktop*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktop*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDesktop*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT Orientation(ADLX_ORIENTATION* orientation)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktop*, ADLX_ORIENTATION*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDesktop*)Unsafe.AsPointer(ref this), orientation);
    }

    public ADLX_RESULT Size([NativeTypeName("adlx_int *")] int* width, [NativeTypeName("adlx_int *")] int* height)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktop*, int*, int*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDesktop*)Unsafe.AsPointer(ref this), width, height);
    }

    public ADLX_RESULT TopLeft(ADLX_Point* locationTopLeft)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktop*, ADLX_Point*, ADLX_RESULT>)(lpVtbl[5]))((IADLXDesktop*)Unsafe.AsPointer(ref this), locationTopLeft);
    }

    public ADLX_RESULT Type(ADLX_DESKTOP_TYPE* desktopType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktop*, ADLX_DESKTOP_TYPE*, ADLX_RESULT>)(lpVtbl[6]))((IADLXDesktop*)Unsafe.AsPointer(ref this), desktopType);
    }

    public ADLX_RESULT GetNumberOfDisplays([NativeTypeName("adlx_uint *")] uint* numDisplays)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktop*, uint*, ADLX_RESULT>)(lpVtbl[7]))((IADLXDesktop*)Unsafe.AsPointer(ref this), numDisplays);
    }

    public ADLX_RESULT GetDisplays(IADLXDisplayList** ppDisplays)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktop*, IADLXDisplayList**, ADLX_RESULT>)(lpVtbl[8]))((IADLXDesktop*)Unsafe.AsPointer(ref this), ppDisplays);
    }
}
