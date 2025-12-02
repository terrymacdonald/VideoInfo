using System.Runtime.CompilerServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXI2C : adlx::IADLXInterface")]
public unsafe partial struct IADLXI2C
{
    public void** lpVtbl;

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXI2C*, int>)(lpVtbl[0]))((IADLXI2C*)Unsafe.AsPointer(ref this));
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXI2C*, int>)(lpVtbl[1]))((IADLXI2C*)Unsafe.AsPointer(ref this));
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXI2C*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXI2C*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    public ADLX_RESULT Version([NativeTypeName("adlx_int *")] int* major, [NativeTypeName("adlx_int *")] int* minor)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXI2C*, int*, int*, ADLX_RESULT>)(lpVtbl[3]))((IADLXI2C*)Unsafe.AsPointer(ref this), major, minor);
    }

    public ADLX_RESULT IsSupported(ADLX_I2C_LINE line, [NativeTypeName("adlx_int")] int address, [NativeTypeName("adlx_bool *")] bool* isSupported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXI2C*, ADLX_I2C_LINE, int, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXI2C*)Unsafe.AsPointer(ref this), line, address, isSupported);
    }

    public ADLX_RESULT Read(ADLX_I2C_LINE line, [NativeTypeName("adlx_int")] int speed, [NativeTypeName("adlx_int")] int address, [NativeTypeName("adlx_int")] int offset, [NativeTypeName("adlx_int")] int dataSize, [NativeTypeName("adlx_byte *")] byte* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXI2C*, ADLX_I2C_LINE, int, int, int, int, byte*, ADLX_RESULT>)(lpVtbl[5]))((IADLXI2C*)Unsafe.AsPointer(ref this), line, speed, address, offset, dataSize, data);
    }

    public ADLX_RESULT RepeatedStartRead(ADLX_I2C_LINE line, [NativeTypeName("adlx_int")] int speed, [NativeTypeName("adlx_int")] int address, [NativeTypeName("adlx_int")] int offset, [NativeTypeName("adlx_int")] int dataSize, [NativeTypeName("adlx_byte *")] byte* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXI2C*, ADLX_I2C_LINE, int, int, int, int, byte*, ADLX_RESULT>)(lpVtbl[6]))((IADLXI2C*)Unsafe.AsPointer(ref this), line, speed, address, offset, dataSize, data);
    }

    public ADLX_RESULT Write(ADLX_I2C_LINE line, [NativeTypeName("adlx_int")] int speed, [NativeTypeName("adlx_int")] int address, [NativeTypeName("adlx_int")] int offset, [NativeTypeName("adlx_int")] int dataSize, [NativeTypeName("adlx_byte *")] byte* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXI2C*, ADLX_I2C_LINE, int, int, int, int, byte*, ADLX_RESULT>)(lpVtbl[7]))((IADLXI2C*)Unsafe.AsPointer(ref this), line, speed, address, offset, dataSize, data);
    }
}
