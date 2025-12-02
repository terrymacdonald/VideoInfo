namespace IGCLWrapper
{
    public unsafe partial struct _ctl_genlock_target_mode_list_t
    {
        [NativeTypeName("ctl_display_output_handle_t")]
        public _ctl_display_output_handle_t* hDisplayOutput;

        [NativeTypeName("uint32_t")]
        public uint NumModes;

        [NativeTypeName("ctl_display_timing_t *")]
        public _ctl_display_timing_t* pTargetModes;
    }
}
