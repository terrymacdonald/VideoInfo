namespace IGCLWrapper
{
    public partial struct ctl_lace_config_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte Enabled;

        [NativeTypeName("ctl_get_operation_flags_t")]
        public uint OpTypeGet;

        public ctl_set_operation_t OpTypeSet;

        [NativeTypeName("ctl_lace_trigger_flags_t")]
        public uint Trigger;

        public ctl_lace_aggr_config_t LaceConfig;
    }
}
