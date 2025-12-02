namespace IGCLWrapper
{
    public partial struct _ctl_freq_range_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public double min;

        public double max;
    }
}
