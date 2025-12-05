namespace IGCLWrapper
{
    public partial struct ctl_engine_stats_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint64_t")]
        public ulong activeTime;

        [NativeTypeName("uint64_t")]
        public ulong timestamp;
    }
}
