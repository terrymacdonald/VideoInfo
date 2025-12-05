namespace IGCLWrapper
{
    public partial struct ctl_fan_temp_speed_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint32_t")]
        public uint temperature;

        public ctl_fan_speed_t speed;
    }
}
