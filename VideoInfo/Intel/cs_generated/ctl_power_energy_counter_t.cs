namespace IGCLWrapper
{
    public partial struct ctl_power_energy_counter_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint64_t")]
        public ulong energy;

        [NativeTypeName("uint64_t")]
        public ulong timestamp;
    }
}
