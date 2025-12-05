namespace IGCLWrapper
{
    public partial struct ctl_pci_state_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_pci_speed_t speed;
    }
}
