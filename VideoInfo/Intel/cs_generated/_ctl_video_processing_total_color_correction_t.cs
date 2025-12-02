using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct _ctl_video_processing_total_color_correction_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte total_color_correction_enable;

        [NativeTypeName("uint32_t")]
        public uint red;

        [NativeTypeName("uint32_t")]
        public uint green;

        [NativeTypeName("uint32_t")]
        public uint blue;

        [NativeTypeName("uint32_t")]
        public uint yellow;

        [NativeTypeName("uint32_t")]
        public uint cyan;

        [NativeTypeName("uint32_t")]
        public uint magenta;

        [NativeTypeName("uint32_t[16]")]
        public _ReservedFields_e__FixedBuffer ReservedFields;

        [InlineArray(16)]
        public partial struct _ReservedFields_e__FixedBuffer
        {
            public uint e0;
        }
    }
}
