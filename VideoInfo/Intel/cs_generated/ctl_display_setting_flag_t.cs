namespace IGCLWrapper
{
    public enum ctl_display_setting_flag_t
    {
        CTL_DISPLAY_SETTING_FLAG_LOW_LATENCY = (1 << 0),
        CTL_DISPLAY_SETTING_FLAG_SOURCE_TM = (1 << 1),
        CTL_DISPLAY_SETTING_FLAG_CONTENT_TYPE = (1 << 2),
        CTL_DISPLAY_SETTING_FLAG_QUANTIZATION_RANGE = (1 << 3),
        CTL_DISPLAY_SETTING_FLAG_PICTURE_AR = (1 << 4),
        CTL_DISPLAY_SETTING_FLAG_AUDIO = (1 << 5),
        CTL_DISPLAY_SETTING_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
