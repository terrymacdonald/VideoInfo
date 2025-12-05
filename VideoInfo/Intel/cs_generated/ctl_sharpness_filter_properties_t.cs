namespace IGCLWrapper
{
    public partial struct ctl_sharpness_filter_properties_t
    {
        [NativeTypeName("ctl_sharpness_filter_type_flags_t")]
        public uint FilterType;

        public ctl_property_range_info_t FilterDetails;
    }
}
