namespace IGCLWrapper
{
    public partial struct _ctl_sharpness_filter_properties_t
    {
        [NativeTypeName("ctl_sharpness_filter_type_flags_t")]
        public uint FilterType;

        [NativeTypeName("ctl_property_range_info_t")]
        public _ctl_property_range_info_t FilterDetails;
    }
}
