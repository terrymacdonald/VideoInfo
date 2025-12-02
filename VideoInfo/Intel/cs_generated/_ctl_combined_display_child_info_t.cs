namespace IGCLWrapper
{
    public unsafe partial struct _ctl_combined_display_child_info_t
    {
        [NativeTypeName("ctl_display_output_handle_t")]
        public _ctl_display_output_handle_t* hDisplayOutput;

        [NativeTypeName("ctl_rect_t")]
        public _ctl_rect_t FbSrc;

        [NativeTypeName("ctl_rect_t")]
        public _ctl_rect_t FbPos;

        [NativeTypeName("ctl_display_orientation_t")]
        public _ctl_display_orientation_t DisplayOrientation;

        [NativeTypeName("ctl_child_display_target_mode_t")]
        public _ctl_child_display_target_mode_t TargetMode;
    }
}
