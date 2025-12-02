using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXAllMetricsList : adlx::IADLXList")]
public unsafe partial struct IADLXAllMetricsList
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetricsList*, int>)(lpVtbl[0]))((IADLXAllMetricsList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetricsList*, int>)(lpVtbl[1]))((IADLXAllMetricsList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetricsList*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXAllMetricsList*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetricsList*, uint>)(lpVtbl[3]))((IADLXAllMetricsList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetricsList*, byte>)(lpVtbl[4]))((IADLXAllMetricsList*)Unsafe.AsPointer(ref this)) != 0;
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetricsList*, uint>)(lpVtbl[5]))((IADLXAllMetricsList*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetricsList*, uint>)(lpVtbl[6]))((IADLXAllMetricsList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetricsList*, uint, IADLXInterface**, ADLX_RESULT>)(lpVtbl[7]))((IADLXAllMetricsList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    public ADLX_RESULT Clear()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetricsList*, ADLX_RESULT>)(lpVtbl[8]))((IADLXAllMetricsList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Remove_Back()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetricsList*, ADLX_RESULT>)(lpVtbl[9]))((IADLXAllMetricsList*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetricsList*, IADLXInterface*, ADLX_RESULT>)(lpVtbl[10]))((IADLXAllMetricsList*)Unsafe.AsPointer(ref this), pItem);
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXAllMetrics** ppItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetricsList*, uint, IADLXAllMetrics**, ADLX_RESULT>)(lpVtbl[11]))((IADLXAllMetricsList*)Unsafe.AsPointer(ref this), location, ppItem);
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXAllMetrics *")] IADLXAllMetrics* pItem)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXAllMetricsList*, IADLXAllMetrics*, ADLX_RESULT>)(lpVtbl[12]))((IADLXAllMetricsList*)Unsafe.AsPointer(ref this), pItem);
    }
}
