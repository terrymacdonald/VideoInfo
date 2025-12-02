using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct _ctl_video_processing_standard_color_correction_info_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte standard_color_correction_default_enable;

        [NativeTypeName("ctl_property_info_float_t")]
        public _ctl_property_info_float_t brightness;

        [NativeTypeName("ctl_property_info_float_t")]
        public _ctl_property_info_float_t contrast;

        [NativeTypeName("ctl_property_info_float_t")]
        public _ctl_property_info_float_t hue;

        [NativeTypeName("ctl_property_info_float_t")]
        public _ctl_property_info_float_t saturation;

        [NativeTypeName("uint32_t[16]")]
        public _ReservedFields_e__FixedBuffer ReservedFields;

        [InlineArray(16)]
        public partial struct _ReservedFields_e__FixedBuffer
        {
            public uint e0;
        }
    }
}
