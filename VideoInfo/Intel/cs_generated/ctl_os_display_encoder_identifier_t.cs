using System.Runtime.InteropServices;

namespace IGCLWrapper
{
    [StructLayout(LayoutKind.Explicit)]
    public partial struct ctl_os_display_encoder_identifier_t
    {
        [FieldOffset(0)]
        [NativeTypeName("uint32_t")]
        public uint WindowsDisplayEncoderID;

        [FieldOffset(0)]
        public ctl_generic_void_datatype_t DisplayEncoderID;
    }
}
