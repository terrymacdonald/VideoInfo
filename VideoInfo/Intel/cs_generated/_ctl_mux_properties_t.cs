namespace IGCLWrapper
{
    public unsafe partial struct _ctl_mux_properties_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint8_t")]
        public byte MuxId;

        [NativeTypeName("uint32_t")]
        public uint Count;

        [NativeTypeName("ctl_display_output_handle_t *")]
        public _ctl_display_output_handle_t** phDisplayOutputs;

        [NativeTypeName("uint8_t")]
        public byte IndexOfDisplayOutputOwningMux;
    }
}
