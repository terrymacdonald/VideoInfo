namespace IGCLWrapper
{
    public partial struct ctl_psu_info_t
    {
        [NativeTypeName("bool")]
        public byte bSupported;

        public ctl_psu_type_t psuType;

        public ctl_oc_telemetry_item_t energyCounter;

        public ctl_oc_telemetry_item_t voltage;
    }
}
