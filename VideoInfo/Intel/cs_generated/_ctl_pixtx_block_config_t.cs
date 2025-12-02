namespace IGCLWrapper
{
    public partial struct _ctl_pixtx_block_config_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint32_t")]
        public uint BlockId;

        [NativeTypeName("ctl_pixtx_block_type_t")]
        public _ctl_pixtx_block_type_t BlockType;

        [NativeTypeName("ctl_pixtx_config_t")]
        public _ctl_pixtx_config_t Config;
    }
}
