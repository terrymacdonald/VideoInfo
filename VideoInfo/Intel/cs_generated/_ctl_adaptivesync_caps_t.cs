namespace IGCLWrapper
{
    public partial struct _ctl_adaptivesync_caps_t
    {
        [NativeTypeName("bool")]
        public byte AdaptiveBalanceSupported;

        [NativeTypeName("ctl_property_info_float_t")]
        public _ctl_property_info_float_t AdaptiveBalanceStrengthCaps;
    }
}
