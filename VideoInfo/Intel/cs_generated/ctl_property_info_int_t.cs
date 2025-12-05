namespace IGCLWrapper
{
    public partial struct ctl_property_info_int_t
    {
        [NativeTypeName("bool")]
        public byte DefaultEnable;

        public ctl_property_range_info_int_t RangeInfo;
    }
}
