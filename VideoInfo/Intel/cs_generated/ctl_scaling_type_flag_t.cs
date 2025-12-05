namespace IGCLWrapper
{
    public enum ctl_scaling_type_flag_t
    {
        CTL_SCALING_TYPE_FLAG_IDENTITY = (1 << 0),
        CTL_SCALING_TYPE_FLAG_CENTERED = (1 << 1),
        CTL_SCALING_TYPE_FLAG_STRETCHED = (1 << 2),
        CTL_SCALING_TYPE_FLAG_ASPECT_RATIO_CENTERED_MAX = (1 << 3),
        CTL_SCALING_TYPE_FLAG_CUSTOM = (1 << 4),
        CTL_SCALING_TYPE_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
