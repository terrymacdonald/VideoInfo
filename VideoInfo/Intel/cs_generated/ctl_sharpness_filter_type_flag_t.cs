namespace IGCLWrapper
{
    public enum ctl_sharpness_filter_type_flag_t
    {
        CTL_SHARPNESS_FILTER_TYPE_FLAG_NON_ADAPTIVE = (1 << 0),
        CTL_SHARPNESS_FILTER_TYPE_FLAG_ADAPTIVE = (1 << 1),
        CTL_SHARPNESS_FILTER_TYPE_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
