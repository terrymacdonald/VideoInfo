namespace IGCLWrapper
{
    public partial struct ctl_wire_format_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_wire_format_color_model_t ColorModel;

        [NativeTypeName("ctl_output_bpc_flags_t")]
        public uint ColorDepth;
    }
}
