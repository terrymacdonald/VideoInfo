namespace IGCLWrapper
{
    public partial struct ctl_engine_properties_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_engine_group_t type;
    }
}
