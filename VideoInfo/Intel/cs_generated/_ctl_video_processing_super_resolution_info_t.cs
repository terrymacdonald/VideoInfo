using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct _ctl_video_processing_super_resolution_info_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_video_processing_super_resolution_flags_t")]
        public uint super_resolution_flag;

        [NativeTypeName("ctl_property_info_uint_t")]
        public _ctl_property_info_uint_t super_resolution_range_in_width;

        [NativeTypeName("ctl_property_info_uint_t")]
        public _ctl_property_info_uint_t super_resolution_range_in_height;

        [NativeTypeName("uint32_t[16]")]
        public _ReservedFields_e__FixedBuffer ReservedFields;

        [InlineArray(16)]
        public partial struct _ReservedFields_e__FixedBuffer
        {
            public uint e0;
        }
    }
}
