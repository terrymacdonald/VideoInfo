namespace IGCLWrapper
{
    public partial struct ctl_oc_telemetry_item_t
    {
        [NativeTypeName("bool")]
        public byte bSupported;

        public ctl_units_t units;

        public ctl_data_type_t type;

        public ctl_data_value_t value;
    }
}
