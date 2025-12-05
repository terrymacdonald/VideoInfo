namespace IGCLWrapper
{
    public partial struct ctl_fan_config_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_fan_speed_mode_t mode;

        public ctl_fan_speed_t speedFixed;

        public ctl_fan_speed_table_t speedTable;
    }
}
