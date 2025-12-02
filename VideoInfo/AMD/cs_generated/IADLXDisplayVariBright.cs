using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayVariBright : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayVariBright
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright*, int>)(lpVtbl[0]))((IADLXDisplayVariBright*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright*, int>)(lpVtbl[1]))((IADLXDisplayVariBright*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayVariBright*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayVariBright*)Unsafe.AsPointer(ref this), supported);
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayVariBright*)Unsafe.AsPointer(ref this), enabled);
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayVariBright*)Unsafe.AsPointer(ref this), enabled);
    }

    public ADLX_RESULT IsCurrentMaximizeBrightness([NativeTypeName("adlx_bool *")] bool* maximizeBrightness)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplayVariBright*)Unsafe.AsPointer(ref this), maximizeBrightness);
    }

    public ADLX_RESULT IsCurrentOptimizeBrightness([NativeTypeName("adlx_bool *")] bool* optimizeBrightness)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright*, bool*, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayVariBright*)Unsafe.AsPointer(ref this), optimizeBrightness);
    }

    public ADLX_RESULT IsCurrentBalanced([NativeTypeName("adlx_bool *")] bool* balanced)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplayVariBright*)Unsafe.AsPointer(ref this), balanced);
    }

    public ADLX_RESULT IsCurrentOptimizeBattery([NativeTypeName("adlx_bool *")] bool* optimizeBattery)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplayVariBright*)Unsafe.AsPointer(ref this), optimizeBattery);
    }

    public ADLX_RESULT IsCurrentMaximizeBattery([NativeTypeName("adlx_bool *")] bool* maximizeBattery)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplayVariBright*)Unsafe.AsPointer(ref this), maximizeBattery);
    }

    public ADLX_RESULT SetMaximizeBrightness()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright*, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplayVariBright*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT SetOptimizeBrightness()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright*, ADLX_RESULT>)(lpVtbl[12]))((IADLXDisplayVariBright*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT SetBalanced()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright*, ADLX_RESULT>)(lpVtbl[13]))((IADLXDisplayVariBright*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT SetOptimizeBattery()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright*, ADLX_RESULT>)(lpVtbl[14]))((IADLXDisplayVariBright*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT SetMaximizeBattery()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright*, ADLX_RESULT>)(lpVtbl[15]))((IADLXDisplayVariBright*)Unsafe.AsPointer(ref this));
    }
}
