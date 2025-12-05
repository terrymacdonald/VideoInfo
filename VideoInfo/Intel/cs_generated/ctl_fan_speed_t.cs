namespace IGCLWrapper
{
    public partial struct ctl_fan_speed_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("int32_t")]
        public int speed;

        public ctl_fan_speed_units_t units;
    }
}
