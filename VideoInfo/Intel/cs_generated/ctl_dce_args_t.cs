namespace IGCLWrapper
{
    public unsafe partial struct ctl_dce_args_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte Set;

        [NativeTypeName("uint32_t")]
        public uint TargetBrightnessPercent;

        public double PhaseinSpeedMultiplier;

        [NativeTypeName("uint32_t")]
        public uint NumBins;

        [NativeTypeName("bool")]
        public byte Enable;

        [NativeTypeName("bool")]
        public byte IsSupported;

        [NativeTypeName("uint32_t *")]
        public uint* pHistogram;
    }
}
