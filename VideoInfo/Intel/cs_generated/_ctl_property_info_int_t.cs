namespace IGCLWrapper
{
    public partial struct _ctl_property_info_int_t
    {
        [NativeTypeName("bool")]
        public byte DefaultEnable;

        [NativeTypeName("ctl_property_range_info_int_t")]
        public _ctl_property_range_info_int_t RangeInfo;
    }
}
