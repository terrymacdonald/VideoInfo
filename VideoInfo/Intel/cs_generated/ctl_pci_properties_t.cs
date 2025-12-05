namespace IGCLWrapper
{
    public partial struct ctl_pci_properties_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_pci_address_t address;

        public ctl_pci_speed_t maxSpeed;

        [NativeTypeName("bool")]
        public byte resizable_bar_supported;

        [NativeTypeName("bool")]
        public byte resizable_bar_enabled;
    }
}
