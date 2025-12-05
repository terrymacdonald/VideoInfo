namespace IGCLWrapper
{
    public unsafe partial struct ctl_pixtx_pipe_get_config_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_pixtx_config_query_type_t QueryType;

        public ctl_pixtx_pixel_format_t InputPixelFormat;

        public ctl_pixtx_pixel_format_t OutputPixelFormat;

        [NativeTypeName("uint32_t")]
        public uint NumBlocks;

        public ctl_pixtx_block_config_t* pBlockConfigs;
    }
}
