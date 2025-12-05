namespace IGCLWrapper
{
    public partial struct ctl_power_burst_limit_t
    {
        [NativeTypeName("bool")]
        public byte enabled;

        [NativeTypeName("int32_t")]
        public int power;
    }
}
