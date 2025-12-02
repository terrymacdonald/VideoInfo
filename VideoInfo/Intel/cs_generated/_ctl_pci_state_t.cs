namespace IGCLWrapper
{
    public partial struct _ctl_pci_state_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_pci_speed_t")]
        public _ctl_pci_speed_t speed;
    }
}
