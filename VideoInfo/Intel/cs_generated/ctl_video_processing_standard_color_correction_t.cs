using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct ctl_video_processing_standard_color_correction_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte standard_color_correction_enable;

        public float brightness;

        public float contrast;

        public float hue;

        public float saturation;

        [NativeTypeName("uint32_t[16]")]
        public _ReservedFields_e__FixedBuffer ReservedFields;

        [InlineArray(16)]
        public partial struct _ReservedFields_e__FixedBuffer
        {
            public uint e0;
        }
    }
}
