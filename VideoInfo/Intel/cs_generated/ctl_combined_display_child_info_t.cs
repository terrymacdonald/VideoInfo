namespace IGCLWrapper
{
    public unsafe partial struct ctl_combined_display_child_info_t
    {
        [NativeTypeName("ctl_display_output_handle_t")]
        public _ctl_display_output_handle_t* hDisplayOutput;

        public ctl_rect_t FbSrc;

        public ctl_rect_t FbPos;

        public ctl_display_orientation_t DisplayOrientation;

        public ctl_child_display_target_mode_t TargetMode;
    }
}
