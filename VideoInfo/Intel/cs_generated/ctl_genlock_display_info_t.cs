namespace IGCLWrapper
{
    public unsafe partial struct ctl_genlock_display_info_t
    {
        [NativeTypeName("ctl_display_output_handle_t")]
        public _ctl_display_output_handle_t* hDisplayOutput;

        [NativeTypeName("bool")]
        public byte IsPrimary;
    }
}
