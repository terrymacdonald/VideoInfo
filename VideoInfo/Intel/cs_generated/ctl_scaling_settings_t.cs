namespace IGCLWrapper
{
    public partial struct ctl_scaling_settings_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte Enable;

        [NativeTypeName("ctl_scaling_type_flags_t")]
        public uint ScalingType;

        [NativeTypeName("uint32_t")]
        public uint CustomScalingX;

        [NativeTypeName("uint32_t")]
        public uint CustomScalingY;

        [NativeTypeName("bool")]
        public byte HardwareModeSet;

        [NativeTypeName("ctl_scaling_type_flags_t")]
        public uint PreferredScalingType;
    }
}
