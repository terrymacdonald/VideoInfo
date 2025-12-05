namespace IGCLWrapper
{
    public partial struct ctl_voltage_frequency_point_t
    {
        [NativeTypeName("uint32_t")]
        public uint Voltage;

        [NativeTypeName("uint32_t")]
        public uint Frequency;
    }
}
