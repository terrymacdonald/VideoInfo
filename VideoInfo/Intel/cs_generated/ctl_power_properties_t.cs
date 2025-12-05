namespace IGCLWrapper
{
    public partial struct ctl_power_properties_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte canControl;

        [NativeTypeName("int32_t")]
        public int defaultLimit;

        [NativeTypeName("int32_t")]
        public int minLimit;

        [NativeTypeName("int32_t")]
        public int maxLimit;
    }
}
