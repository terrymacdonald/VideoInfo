namespace IGCLWrapper
{
    public unsafe partial struct _ctl_edid_management_args_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_edid_management_optype_t")]
        public _ctl_edid_management_optype_t OpType;

        [NativeTypeName("ctl_edid_type_t")]
        public _ctl_edid_type_t EdidType;

        [NativeTypeName("uint32_t")]
        public uint EdidSize;

        [NativeTypeName("uint8_t *")]
        public byte* pEdidBuf;

        [NativeTypeName("ctl_edid_management_out_flags_t")]
        public uint OutFlags;
    }
}
