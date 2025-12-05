namespace IGCLWrapper
{
    public unsafe partial struct ctl_get_set_custom_mode_args_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_custom_mode_operation_types_t CustomModeOpType;

        [NativeTypeName("uint32_t")]
        public uint NumOfModes;

        public ctl_custom_src_mode_t* pCustomSrcModeList;
    }
}
