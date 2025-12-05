using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct ctl_firmware_component_properties_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("char[64]")]
        public _name_e__FixedBuffer name;

        [NativeTypeName("char[64]")]
        public _version_e__FixedBuffer version;

        [NativeTypeName("char[20]")]
        public _reserved_e__FixedBuffer reserved;

        [InlineArray(64)]
        public partial struct _name_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(64)]
        public partial struct _version_e__FixedBuffer
        {
            public sbyte e0;
        }

        [InlineArray(20)]
        public partial struct _reserved_e__FixedBuffer
        {
            public sbyte e0;
        }
    }
}
