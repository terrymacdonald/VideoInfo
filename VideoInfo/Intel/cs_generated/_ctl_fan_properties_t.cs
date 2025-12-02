namespace IGCLWrapper
{
    public partial struct _ctl_fan_properties_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte canControl;

        [NativeTypeName("uint32_t")]
        public uint supportedModes;

        [NativeTypeName("uint32_t")]
        public uint supportedUnits;

        [NativeTypeName("int32_t")]
        public int maxRPM;

        [NativeTypeName("int32_t")]
        public int maxPoints;
    }
}
