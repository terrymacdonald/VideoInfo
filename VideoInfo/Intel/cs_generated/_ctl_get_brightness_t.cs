using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct _ctl_get_brightness_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint32_t")]
        public uint TargetBrightness;

        [NativeTypeName("uint32_t")]
        public uint CurrentBrightness;

        [NativeTypeName("uint32_t[4]")]
        public _ReservedFields_e__FixedBuffer ReservedFields;

        [InlineArray(4)]
        public partial struct _ReservedFields_e__FixedBuffer
        {
            public uint e0;
        }
    }
}
