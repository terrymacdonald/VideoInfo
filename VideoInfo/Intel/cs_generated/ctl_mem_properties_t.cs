namespace IGCLWrapper
{
    public partial struct ctl_mem_properties_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_mem_type_t type;

        public ctl_mem_loc_t location;

        [NativeTypeName("uint64_t")]
        public ulong physicalSize;

        [NativeTypeName("int32_t")]
        public int busWidth;

        [NativeTypeName("int32_t")]
        public int numChannels;
    }
}
