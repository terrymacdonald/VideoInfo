namespace IGCLWrapper
{
    public partial struct _ctl_pci_speed_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("int32_t")]
        public int gen;

        [NativeTypeName("int32_t")]
        public int width;

        [NativeTypeName("int64_t")]
        public long maxBandwidth;
    }
}
