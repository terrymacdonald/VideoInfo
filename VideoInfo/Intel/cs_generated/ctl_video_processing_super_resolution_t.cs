using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct ctl_video_processing_super_resolution_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_video_processing_super_resolution_flags_t")]
        public uint super_resolution_flag;

        [NativeTypeName("bool")]
        public byte super_resolution_max_in_enabled;

        [NativeTypeName("uint32_t")]
        public uint super_resolution_max_in_width;

        [NativeTypeName("uint32_t")]
        public uint super_resolution_max_in_height;

        [NativeTypeName("bool")]
        public byte super_resolution_reboot_reset;

        [NativeTypeName("uint32_t[15]")]
        public _ReservedFields_e__FixedBuffer ReservedFields;

        [NativeTypeName("char[3]")]
        public _ReservedBytes_e__FixedBuffer ReservedBytes;

        [InlineArray(15)]
        public partial struct _ReservedFields_e__FixedBuffer
        {
            public uint e0;
        }

        [InlineArray(3)]
        public partial struct _ReservedBytes_e__FixedBuffer
        {
            public sbyte e0;
        }
    }
}
