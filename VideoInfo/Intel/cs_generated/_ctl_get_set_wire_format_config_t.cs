using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct _ctl_get_set_wire_format_config_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_wire_format_operation_type_t")]
        public _ctl_wire_format_operation_type_t Operation;

        [NativeTypeName("ctl_wire_format_t[4]")]
        public _SupportedWireFormat_e__FixedBuffer SupportedWireFormat;

        [NativeTypeName("ctl_wire_format_t")]
        public _ctl_wire_format_t WireFormat;

        [InlineArray(4)]
        public partial struct _SupportedWireFormat_e__FixedBuffer
        {
            public _ctl_wire_format_t e0;
        }
    }
}
