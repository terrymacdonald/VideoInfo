namespace IGCLWrapper
{
    public partial struct _ctl_fan_speed_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("int32_t")]
        public int speed;

        [NativeTypeName("ctl_fan_speed_units_t")]
        public _ctl_fan_speed_units_t units;
    }
}
