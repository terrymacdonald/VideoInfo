namespace IGCLWrapper
{
    public partial struct _ctl_display_timing_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint64_t")]
        public ulong PixelClock;

        [NativeTypeName("uint32_t")]
        public uint HActive;

        [NativeTypeName("uint32_t")]
        public uint VActive;

        [NativeTypeName("uint32_t")]
        public uint HTotal;

        [NativeTypeName("uint32_t")]
        public uint VTotal;

        [NativeTypeName("uint32_t")]
        public uint HBlank;

        [NativeTypeName("uint32_t")]
        public uint VBlank;

        [NativeTypeName("uint32_t")]
        public uint HSync;

        [NativeTypeName("uint32_t")]
        public uint VSync;

        public float RefreshRate;

        [NativeTypeName("ctl_signal_standard_type_t")]
        public _ctl_signal_standard_type_t SignalStandard;

        [NativeTypeName("uint8_t")]
        public byte VicId;
    }
}
