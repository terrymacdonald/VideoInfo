namespace IGCLWrapper
{
    public unsafe partial struct ctl_combined_display_args_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_combined_display_optype_t OpType;

        [NativeTypeName("bool")]
        public byte IsSupported;

        [NativeTypeName("uint8_t")]
        public byte NumOutputs;

        [NativeTypeName("uint32_t")]
        public uint CombinedDesktopWidth;

        [NativeTypeName("uint32_t")]
        public uint CombinedDesktopHeight;

        public ctl_combined_display_child_info_t* pChildInfo;

        [NativeTypeName("ctl_display_output_handle_t")]
        public _ctl_display_output_handle_t* hCombinedDisplayOutput;
    }
}
