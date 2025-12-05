namespace IGCLWrapper
{
    public unsafe partial struct ctl_pixtx_pipe_set_config_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_pixtx_config_opertaion_type_t OpertaionType;

        [NativeTypeName("ctl_pixtx_pipe_set_config_flags_t")]
        public uint Flags;

        [NativeTypeName("uint32_t")]
        public uint NumBlocks;

        public ctl_pixtx_block_config_t* pBlockConfigs;
    }
}
