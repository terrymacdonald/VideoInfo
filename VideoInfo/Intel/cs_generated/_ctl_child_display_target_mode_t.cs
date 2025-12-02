using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct _ctl_child_display_target_mode_t
    {
        [NativeTypeName("uint32_t")]
        public uint Width;

        [NativeTypeName("uint32_t")]
        public uint Height;

        public float RefreshRate;

        [NativeTypeName("uint32_t[4]")]
        public _ReservedFields_e__FixedBuffer ReservedFields;

        [InlineArray(4)]
        public partial struct _ReservedFields_e__FixedBuffer
        {
            public uint e0;
        }
    }
}
