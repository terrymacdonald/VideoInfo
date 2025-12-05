using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct ctl_video_processing_adaptive_contrast_enhancement_info_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_property_info_uint_t adaptive_contrast_enhancement;

        [NativeTypeName("bool")]
        public byte adaptive_contrast_enhancement_coexistence_supported;

        public ctl_property_info_boolean_t adaptive_contrast_enhancement_coexistence;

        [NativeTypeName("uint32_t[16]")]
        public _ReservedFields_e__FixedBuffer ReservedFields;

        [InlineArray(16)]
        public partial struct _ReservedFields_e__FixedBuffer
        {
            public uint e0;
        }
    }
}
