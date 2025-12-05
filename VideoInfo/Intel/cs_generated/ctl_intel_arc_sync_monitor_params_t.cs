namespace IGCLWrapper
{
    public partial struct ctl_intel_arc_sync_monitor_params_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte IsIntelArcSyncSupported;

        public float MinimumRefreshRateInHz;

        public float MaximumRefreshRateInHz;

        [NativeTypeName("uint32_t")]
        public uint MaxFrameTimeIncreaseInUs;

        [NativeTypeName("uint32_t")]
        public uint MaxFrameTimeDecreaseInUs;
    }
}
