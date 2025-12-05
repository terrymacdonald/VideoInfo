namespace IGCLWrapper
{
    public enum ctl_display_config_flag_t
    {
        CTL_DISPLAY_CONFIG_FLAG_DISPLAY_ACTIVE = (1 << 0),
        CTL_DISPLAY_CONFIG_FLAG_DISPLAY_ATTACHED = (1 << 1),
        CTL_DISPLAY_CONFIG_FLAG_IS_DONGLE_CONNECTED_TO_ENCODER = (1 << 2),
        CTL_DISPLAY_CONFIG_FLAG_DITHERING_ENABLED = (1 << 3),
        CTL_DISPLAY_CONFIG_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
