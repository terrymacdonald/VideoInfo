namespace IGCLWrapper
{
    public unsafe partial struct _ctl_genlock_topology_t
    {
        [NativeTypeName("uint8_t")]
        public byte NumGenlockDisplays;

        [NativeTypeName("bool")]
        public byte IsPrimaryGenlockSystem;

        [NativeTypeName("ctl_display_timing_t")]
        public _ctl_display_timing_t CommonTargetMode;

        [NativeTypeName("ctl_genlock_display_info_t *")]
        public _ctl_genlock_display_info_t* pGenlockDisplayInfo;

        [NativeTypeName("ctl_genlock_target_mode_list_t *")]
        public _ctl_genlock_target_mode_list_t* pGenlockModeList;
    }
}
