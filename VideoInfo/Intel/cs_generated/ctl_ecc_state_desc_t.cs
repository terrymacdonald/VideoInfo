namespace IGCLWrapper
{
    public partial struct ctl_ecc_state_desc_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_ecc_state_t currentEccState;

        public ctl_ecc_state_t pendingEccState;
    }
}
