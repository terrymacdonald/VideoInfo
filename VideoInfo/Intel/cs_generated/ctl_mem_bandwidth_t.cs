namespace IGCLWrapper
{
    public partial struct ctl_mem_bandwidth_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint64_t")]
        public ulong maxBandwidth;

        [NativeTypeName("uint64_t")]
        public ulong timestamp;

        [NativeTypeName("uint64_t")]
        public ulong readCounter;

        [NativeTypeName("uint64_t")]
        public ulong writeCounter;
    }
}
