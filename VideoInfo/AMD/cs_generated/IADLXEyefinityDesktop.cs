using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXEyefinityDesktop : adlx::IADLXInterface")]
public unsafe partial struct IADLXEyefinityDesktop
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXEyefinityDesktop*, int>)(lpVtbl[0]))((IADLXEyefinityDesktop*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXEyefinityDesktop*, int>)(lpVtbl[1]))((IADLXEyefinityDesktop*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXEyefinityDesktop*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXEyefinityDesktop*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT GridSize([NativeTypeName("adlx_uint *")] uint* rows, [NativeTypeName("adlx_uint *")] uint* cols)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXEyefinityDesktop*, uint*, uint*, ADLX_RESULT>)(lpVtbl[3]))((IADLXEyefinityDesktop*)Unsafe.AsPointer(ref this), rows, cols);
    }

    public ADLX_RESULT GetDisplay([NativeTypeName("adlx_uint")] uint row, [NativeTypeName("adlx_uint")] uint col, IADLXDisplay** ppDisplay)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXEyefinityDesktop*, uint, uint, IADLXDisplay**, ADLX_RESULT>)(lpVtbl[4]))((IADLXEyefinityDesktop*)Unsafe.AsPointer(ref this), row, col, ppDisplay);
    }

    public ADLX_RESULT DisplayOrientation([NativeTypeName("adlx_uint")] uint row, [NativeTypeName("adlx_uint")] uint col, ADLX_ORIENTATION* displayOrientation)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXEyefinityDesktop*, uint, uint, ADLX_ORIENTATION*, ADLX_RESULT>)(lpVtbl[5]))((IADLXEyefinityDesktop*)Unsafe.AsPointer(ref this), row, col, displayOrientation);
    }

    public ADLX_RESULT DisplaySize([NativeTypeName("adlx_uint")] uint row, [NativeTypeName("adlx_uint")] uint col, [NativeTypeName("adlx_int *")] int* displayWidth, [NativeTypeName("adlx_int *")] int* displayHeight)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXEyefinityDesktop*, uint, uint, int*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLXEyefinityDesktop*)Unsafe.AsPointer(ref this), row, col, displayWidth, displayHeight);
    }

    public ADLX_RESULT DisplayTopLeft([NativeTypeName("adlx_uint")] uint row, [NativeTypeName("adlx_uint")] uint col, ADLX_Point* displayLocationTopLeft)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXEyefinityDesktop*, uint, uint, ADLX_Point*, ADLX_RESULT>)(lpVtbl[7]))((IADLXEyefinityDesktop*)Unsafe.AsPointer(ref this), row, col, displayLocationTopLeft);
    }
}
