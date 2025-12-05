namespace IGCLWrapper
{
    public unsafe partial struct ctl_reserved_args_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public void* pSpecialArg;

        [NativeTypeName("uint32_t")]
        public uint ArgSize;
    }
}
