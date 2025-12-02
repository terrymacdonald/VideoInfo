namespace IGCLWrapper
{
    public partial struct _ctl_pci_address_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint32_t")]
        public uint domain;

        [NativeTypeName("uint32_t")]
        public uint bus;

        [NativeTypeName("uint32_t")]
        public uint device;

        [NativeTypeName("uint32_t")]
        public uint function;
    }
}
