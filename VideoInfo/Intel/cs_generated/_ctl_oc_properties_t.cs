namespace IGCLWrapper
{
    public partial struct _ctl_oc_properties_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte bSupported;

        [NativeTypeName("ctl_oc_control_info_t")]
        public _ctl_oc_control_info_t gpuFrequencyOffset;

        [NativeTypeName("ctl_oc_control_info_t")]
        public _ctl_oc_control_info_t gpuVoltageOffset;

        [NativeTypeName("ctl_oc_control_info_t")]
        public _ctl_oc_control_info_t vramFrequencyOffset;

        [NativeTypeName("ctl_oc_control_info_t")]
        public _ctl_oc_control_info_t vramVoltageOffset;

        [NativeTypeName("ctl_oc_control_info_t")]
        public _ctl_oc_control_info_t powerLimit;

        [NativeTypeName("ctl_oc_control_info_t")]
        public _ctl_oc_control_info_t temperatureLimit;

        [NativeTypeName("ctl_oc_control_info_t")]
        public _ctl_oc_control_info_t vramMemSpeedLimit;

        [NativeTypeName("ctl_oc_control_info_t")]
        public _ctl_oc_control_info_t gpuVFCurveVoltageLimit;

        [NativeTypeName("ctl_oc_control_info_t")]
        public _ctl_oc_control_info_t gpuVFCurveFrequencyLimit;
    }
}
