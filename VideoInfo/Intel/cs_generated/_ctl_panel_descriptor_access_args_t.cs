namespace IGCLWrapper
{
    public unsafe partial struct _ctl_panel_descriptor_access_args_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_operation_type_t")]
        public _ctl_operation_type_t OpType;

        [NativeTypeName("uint32_t")]
        public uint BlockNumber;

        [NativeTypeName("uint32_t")]
        public uint DescriptorDataSize;

        [NativeTypeName("uint8_t *")]
        public byte* pDescriptorData;
    }
}
