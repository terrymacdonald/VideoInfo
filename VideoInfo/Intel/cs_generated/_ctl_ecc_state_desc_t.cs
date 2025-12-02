namespace IGCLWrapper
{
    public partial struct _ctl_ecc_state_desc_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_ecc_state_t")]
        public _ctl_ecc_state_t currentEccState;

        [NativeTypeName("ctl_ecc_state_t")]
        public _ctl_ecc_state_t pendingEccState;
    }
}
