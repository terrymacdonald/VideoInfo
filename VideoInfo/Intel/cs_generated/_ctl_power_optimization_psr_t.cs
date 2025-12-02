namespace IGCLWrapper
{
    public partial struct _ctl_power_optimization_psr_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint8_t")]
        public byte PSRVersion;

        [NativeTypeName("bool")]
        public byte FullFetchUpdate;
    }
}
