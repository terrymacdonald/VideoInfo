using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXSystemMetricsList : adlx::IADLXList")]
public unsafe partial struct IADLXSystemMetricsList
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, int>)(lpVtbl[0]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, int>)(lpVtbl[1]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, uint>)(lpVtbl[3]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, byte>)(lpVtbl[4]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, uint>)(lpVtbl[5]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, uint>)(lpVtbl[6]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this), pItem);
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXSystemMetrics** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, uint, IADLXSystemMetrics**, ADLX_RESULT>)(lpVtbl[11]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXSystemMetrics *")] IADLXSystemMetrics* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsList*, IADLXSystemMetrics*, ADLX_RESULT>)(lpVtbl[12]))((IADLXSystemMetricsList*)Unsafe.AsPointer(ref this), pItem);
    }
}
