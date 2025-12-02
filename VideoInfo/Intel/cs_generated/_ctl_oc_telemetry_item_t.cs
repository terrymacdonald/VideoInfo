namespace IGCLWrapper
{
    public partial struct _ctl_oc_telemetry_item_t
    {
        [NativeTypeName("bool")]
        public byte bSupported;

        [NativeTypeName("ctl_units_t")]
        public _ctl_units_t units;

        [NativeTypeName("ctl_data_type_t")]
        public _ctl_data_type_t type;

        [NativeTypeName("ctl_data_value_t")]
        public _ctl_data_value_t value;
    }
}
