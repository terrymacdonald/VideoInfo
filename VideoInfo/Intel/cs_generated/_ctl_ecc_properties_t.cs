namespace IGCLWrapper
{
    public partial struct _ctl_ecc_properties_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte isSupported;

        [NativeTypeName("bool")]
        public byte canControl;
    }
}
