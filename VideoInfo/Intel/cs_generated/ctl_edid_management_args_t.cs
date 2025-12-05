namespace IGCLWrapper
{
    public unsafe partial struct ctl_edid_management_args_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_edid_management_optype_t OpType;

        public ctl_edid_type_t EdidType;

        [NativeTypeName("uint32_t")]
        public uint EdidSize;

        [NativeTypeName("uint8_t *")]
        public byte* pEdidBuf;

        [NativeTypeName("ctl_edid_management_out_flags_t")]
        public uint OutFlags;
    }
}
