namespace IGCLWrapper
{
    public partial struct ctl_oc_control_info_t
    {
        [NativeTypeName("bool")]
        public byte bSupported;

        [NativeTypeName("bool")]
        public byte bRelative;

        [NativeTypeName("bool")]
        public byte bReference;

        public ctl_units_t units;

        public double min;

        public double max;

        public double step;

        public double Default;

        public double reference;
    }
}
