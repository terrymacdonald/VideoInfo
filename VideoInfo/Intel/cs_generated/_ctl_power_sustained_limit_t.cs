namespace IGCLWrapper
{
    public partial struct _ctl_power_sustained_limit_t
    {
        [NativeTypeName("bool")]
        public byte enabled;

        [NativeTypeName("int32_t")]
        public int power;

        [NativeTypeName("int32_t")]
        public int interval;
    }
}
