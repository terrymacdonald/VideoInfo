using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct ctl_video_processing_noise_reduction_info_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_property_info_uint_t noise_reduction;

        [NativeTypeName("bool")]
        public byte noise_reduction_auto_detect_supported;

        public ctl_property_info_boolean_t noise_reduction_auto_detect;

        [NativeTypeName("uint32_t[16]")]
        public _ReservedFields_e__FixedBuffer ReservedFields;

        [InlineArray(16)]
        public partial struct _ReservedFields_e__FixedBuffer
        {
            public uint e0;
        }
    }
}
