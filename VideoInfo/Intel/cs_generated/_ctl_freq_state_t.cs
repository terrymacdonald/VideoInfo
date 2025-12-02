namespace IGCLWrapper
{
    public partial struct _ctl_freq_state_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public double currentVoltage;

        public double request;

        public double tdp;

        public double efficient;

        public double actual;

        [NativeTypeName("ctl_freq_throttle_reason_flags_t")]
        public uint throttleReasons;
    }
}
