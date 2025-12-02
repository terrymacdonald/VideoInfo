namespace IGCLWrapper
{
    public partial struct _ctl_intel_arc_sync_profile_params_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_intel_arc_sync_profile_t")]
        public _ctl_intel_arc_sync_profile_t IntelArcSyncProfile;

        public float MaxRefreshRateInHz;

        public float MinRefreshRateInHz;

        [NativeTypeName("uint32_t")]
        public uint MaxFrameTimeIncreaseInUs;

        [NativeTypeName("uint32_t")]
        public uint MaxFrameTimeDecreaseInUs;
    }
}
