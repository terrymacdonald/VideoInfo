namespace IGCLWrapper
{
    public partial struct _ctl_property_info_uint_t
    {
        [NativeTypeName("bool")]
        public byte DefaultEnable;

        [NativeTypeName("ctl_property_range_info_uint_t")]
        public _ctl_property_range_info_uint_t RangeInfo;
    }
}
