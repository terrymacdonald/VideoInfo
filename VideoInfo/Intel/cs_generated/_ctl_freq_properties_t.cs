namespace IGCLWrapper
{
    public partial struct _ctl_freq_properties_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_freq_domain_t")]
        public _ctl_freq_domain_t type;

        [NativeTypeName("bool")]
        public byte canControl;

        public double min;

        public double max;
    }
}
