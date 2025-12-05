namespace IGCLWrapper
{
    public enum ctl_supported_functions_flag_t
    {
        CTL_SUPPORTED_FUNCTIONS_FLAG_DISPLAY = (1 << 0),
        CTL_SUPPORTED_FUNCTIONS_FLAG_3D = (1 << 1),
        CTL_SUPPORTED_FUNCTIONS_FLAG_MEDIA = (1 << 2),
        CTL_SUPPORTED_FUNCTIONS_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
