namespace IGCLWrapper
{
    public partial struct _ctl_oc_vf_pair_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public double Voltage;

        public double Frequency;
    }
}
