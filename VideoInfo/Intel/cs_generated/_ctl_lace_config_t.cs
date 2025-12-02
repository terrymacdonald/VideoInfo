namespace IGCLWrapper
{
    public partial struct _ctl_lace_config_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte Enabled;

        [NativeTypeName("ctl_get_operation_flags_t")]
        public uint OpTypeGet;

        [NativeTypeName("ctl_set_operation_t")]
        public _ctl_set_operation_t OpTypeSet;

        [NativeTypeName("ctl_lace_trigger_flags_t")]
        public uint Trigger;

        [NativeTypeName("ctl_lace_aggr_config_t")]
        public _ctl_lace_aggr_config_t LaceConfig;
    }
}
