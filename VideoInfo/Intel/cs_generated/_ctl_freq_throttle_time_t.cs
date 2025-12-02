namespace IGCLWrapper
{
    public partial struct _ctl_freq_throttle_time_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint64_t")]
        public ulong throttleTime;

        [NativeTypeName("uint64_t")]
        public ulong timestamp;
    }
}
