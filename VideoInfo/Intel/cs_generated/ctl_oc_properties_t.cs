namespace IGCLWrapper
{
    public partial struct ctl_oc_properties_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte bSupported;

        public ctl_oc_control_info_t gpuFrequencyOffset;

        public ctl_oc_control_info_t gpuVoltageOffset;

        public ctl_oc_control_info_t vramFrequencyOffset;

        public ctl_oc_control_info_t vramVoltageOffset;

        public ctl_oc_control_info_t powerLimit;

        public ctl_oc_control_info_t temperatureLimit;

        public ctl_oc_control_info_t vramMemSpeedLimit;

        public ctl_oc_control_info_t gpuVFCurveVoltageLimit;

        public ctl_oc_control_info_t gpuVFCurveFrequencyLimit;
    }
}
