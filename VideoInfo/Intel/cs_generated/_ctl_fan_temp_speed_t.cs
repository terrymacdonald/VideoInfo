namespace IGCLWrapper
{
    public partial struct _ctl_fan_temp_speed_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint32_t")]
        public uint temperature;

        [NativeTypeName("ctl_fan_speed_t")]
        public _ctl_fan_speed_t speed;
    }
}
