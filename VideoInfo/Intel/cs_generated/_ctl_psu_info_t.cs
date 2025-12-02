namespace IGCLWrapper
{
    public partial struct _ctl_psu_info_t
    {
        [NativeTypeName("bool")]
        public byte bSupported;

        [NativeTypeName("ctl_psu_type_t")]
        public _ctl_psu_type_t psuType;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t energyCounter;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t voltage;
    }
}
