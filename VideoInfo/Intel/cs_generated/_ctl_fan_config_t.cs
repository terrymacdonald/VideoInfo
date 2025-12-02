namespace IGCLWrapper
{
    public partial struct _ctl_fan_config_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_fan_speed_mode_t")]
        public _ctl_fan_speed_mode_t mode;

        [NativeTypeName("ctl_fan_speed_t")]
        public _ctl_fan_speed_t speedFixed;

        [NativeTypeName("ctl_fan_speed_table_t")]
        public _ctl_fan_speed_table_t speedTable;
    }
}
