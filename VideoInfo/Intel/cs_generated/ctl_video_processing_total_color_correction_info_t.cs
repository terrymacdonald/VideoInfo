using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct ctl_video_processing_total_color_correction_info_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte total_color_correction_default_enable;

        public ctl_property_info_uint_t red;

        public ctl_property_info_uint_t green;

        public ctl_property_info_uint_t blue;

        public ctl_property_info_uint_t yellow;

        public ctl_property_info_uint_t cyan;

        public ctl_property_info_uint_t magenta;

        [NativeTypeName("uint32_t[16]")]
        public _ReservedFields_e__FixedBuffer ReservedFields;

        [InlineArray(16)]
        public partial struct _ReservedFields_e__FixedBuffer
        {
            public uint e0;
        }
    }
}
