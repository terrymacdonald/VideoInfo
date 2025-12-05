namespace IGCLWrapper
{
    public enum ctl_intel_display_feature_flag_t
    {
        CTL_INTEL_DISPLAY_FEATURE_FLAG_DPST = (1 << 0),
        CTL_INTEL_DISPLAY_FEATURE_FLAG_LACE = (1 << 1),
        CTL_INTEL_DISPLAY_FEATURE_FLAG_DRRS = (1 << 2),
        CTL_INTEL_DISPLAY_FEATURE_FLAG_ARC_ADAPTIVE_SYNC_CERTIFIED = (1 << 3),
        CTL_INTEL_DISPLAY_FEATURE_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
