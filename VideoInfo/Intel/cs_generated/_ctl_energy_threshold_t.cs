namespace IGCLWrapper
{
    public partial struct _ctl_energy_threshold_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte enable;

        public double threshold;

        [NativeTypeName("uint32_t")]
        public uint processId;
    }
}
