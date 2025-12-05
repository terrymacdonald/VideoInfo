using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct ctl_i2c_access_pinpair_args_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint32_t")]
        public uint DataSize;

        [NativeTypeName("uint32_t")]
        public uint Address;

        public ctl_operation_type_t OpType;

        [NativeTypeName("uint32_t")]
        public uint Offset;

        [NativeTypeName("ctl_i2c_pinpair_flags_t")]
        public uint Flags;

        [NativeTypeName("uint8_t[128]")]
        public _Data_e__FixedBuffer Data;

        [NativeTypeName("uint32_t[4]")]
        public _ReservedFields_e__FixedBuffer ReservedFields;

        [InlineArray(128)]
        public partial struct _Data_e__FixedBuffer
        {
            public byte e0;
        }

        [InlineArray(4)]
        public partial struct _ReservedFields_e__FixedBuffer
        {
            public uint e0;
        }
    }
}
