using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct _ctl_i2c_access_args_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint32_t")]
        public uint DataSize;

        [NativeTypeName("uint32_t")]
        public uint Address;

        [NativeTypeName("ctl_operation_type_t")]
        public _ctl_operation_type_t OpType;

        [NativeTypeName("uint32_t")]
        public uint Offset;

        [NativeTypeName("ctl_i2c_flags_t")]
        public uint Flags;

        [NativeTypeName("uint64_t")]
        public ulong RAD;

        [NativeTypeName("uint8_t[128]")]
        public _Data_e__FixedBuffer Data;

        [InlineArray(128)]
        public partial struct _Data_e__FixedBuffer
        {
            public byte e0;
        }
    }
}
