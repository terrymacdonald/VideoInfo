namespace IGCLWrapper
{
    public unsafe partial struct _ctl_pixtx_pipe_get_config_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_pixtx_config_query_type_t")]
        public _ctl_pixtx_config_query_type_t QueryType;

        [NativeTypeName("ctl_pixtx_pixel_format_t")]
        public _ctl_pixtx_pixel_format_t InputPixelFormat;

        [NativeTypeName("ctl_pixtx_pixel_format_t")]
        public _ctl_pixtx_pixel_format_t OutputPixelFormat;

        [NativeTypeName("uint32_t")]
        public uint NumBlocks;

        [NativeTypeName("ctl_pixtx_block_config_t *")]
        public _ctl_pixtx_block_config_t* pBlockConfigs;
    }
}
