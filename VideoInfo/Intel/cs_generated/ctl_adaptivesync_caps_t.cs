namespace IGCLWrapper
{
    public partial struct ctl_adaptivesync_caps_t
    {
        [NativeTypeName("bool")]
        public byte AdaptiveBalanceSupported;

        public ctl_property_info_float_t AdaptiveBalanceStrengthCaps;
    }
}
