namespace IGCLWrapper
{
    public partial struct ctl_power_peak_limit_t
    {
        [NativeTypeName("int32_t")]
        public int powerAC;

        [NativeTypeName("int32_t")]
        public int powerDC;
    }
}
