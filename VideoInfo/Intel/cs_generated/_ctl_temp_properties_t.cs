namespace IGCLWrapper
{
    public partial struct _ctl_temp_properties_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_temp_sensors_t")]
        public _ctl_temp_sensors_t type;

        public double maxTemperature;
    }
}
