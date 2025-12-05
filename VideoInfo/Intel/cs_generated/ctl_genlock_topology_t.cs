namespace IGCLWrapper
{
    public unsafe partial struct ctl_genlock_topology_t
    {
        [NativeTypeName("uint8_t")]
        public byte NumGenlockDisplays;

        [NativeTypeName("bool")]
        public byte IsPrimaryGenlockSystem;

        public ctl_display_timing_t CommonTargetMode;

        public ctl_genlock_display_info_t* pGenlockDisplayInfo;

        public ctl_genlock_target_mode_list_t* pGenlockModeList;
    }
}
