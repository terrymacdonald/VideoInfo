namespace IGCLWrapper
{
    public partial struct ctl_property_range_info_int_t
    {
        [NativeTypeName("int32_t")]
        public int min_possible_value;

        [NativeTypeName("int32_t")]
        public int max_possible_value;

        [NativeTypeName("int32_t")]
        public int step_size;

        [NativeTypeName("int32_t")]
        public int default_value;
    }
}
