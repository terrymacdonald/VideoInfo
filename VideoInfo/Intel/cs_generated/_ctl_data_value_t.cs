using System.Runtime.InteropServices;

namespace IGCLWrapper
{
    [StructLayout(LayoutKind.Explicit)]
    public partial struct _ctl_data_value_t
    {
        [FieldOffset(0)]
        [NativeTypeName("int8_t")]
        public sbyte data8;

        [FieldOffset(0)]
        [NativeTypeName("uint8_t")]
        public byte datau8;

        [FieldOffset(0)]
        [NativeTypeName("int16_t")]
        public short data16;

        [FieldOffset(0)]
        [NativeTypeName("uint16_t")]
        public ushort datau16;

        [FieldOffset(0)]
        [NativeTypeName("int32_t")]
        public int data32;

        [FieldOffset(0)]
        [NativeTypeName("uint32_t")]
        public uint datau32;

        [FieldOffset(0)]
        [NativeTypeName("int64_t")]
        public long data64;

        [FieldOffset(0)]
        [NativeTypeName("uint64_t")]
        public ulong datau64;

        [FieldOffset(0)]
        public float datafloat;

        [FieldOffset(0)]
        public double datadouble;
    }
}
