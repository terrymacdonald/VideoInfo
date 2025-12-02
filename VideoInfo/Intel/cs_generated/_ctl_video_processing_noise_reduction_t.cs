using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct _ctl_video_processing_noise_reduction_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_property_uint_t")]
        public _ctl_property_uint_t noise_reduction;

        [NativeTypeName("ctl_property_boolean_t")]
        public _ctl_property_boolean_t noise_reduction_auto_detect;

        [NativeTypeName("uint32_t[16]")]
        public _ReservedFields_e__FixedBuffer ReservedFields;

        [InlineArray(16)]
        public partial struct _ReservedFields_e__FixedBuffer
        {
            public uint e0;
        }
    }
}
