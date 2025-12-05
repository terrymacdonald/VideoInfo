namespace IGCLWrapper
{
    public partial struct ctl_mem_state_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint64_t")]
        public ulong free;

        [NativeTypeName("uint64_t")]
        public ulong size;
    }
}
