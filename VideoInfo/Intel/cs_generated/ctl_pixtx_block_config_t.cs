namespace IGCLWrapper
{
    public partial struct ctl_pixtx_block_config_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint32_t")]
        public uint BlockId;

        public ctl_pixtx_block_type_t BlockType;

        public ctl_pixtx_config_t Config;
    }
}
