using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct _ctl_vblank_ts_args_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint8_t")]
        public byte NumOfTargets;

        [NativeTypeName("uint64_t[16]")]
        public _VblankTS_e__FixedBuffer VblankTS;

        [InlineArray(16)]
        public partial struct _VblankTS_e__FixedBuffer
        {
            public ulong e0;
        }
    }
}
