namespace IGCLWrapper
{
    public partial struct ctl_adapter_bdf_t
    {
        [NativeTypeName("uint8_t")]
        public byte bus;

        [NativeTypeName("uint8_t")]
        public byte device;

        [NativeTypeName("uint8_t")]
        public byte function;
    }
}
