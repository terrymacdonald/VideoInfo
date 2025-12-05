namespace IGCLWrapper
{
    public enum ctl_display_setting_picture_ar_flag_t
    {
        CTL_DISPLAY_SETTING_PICTURE_AR_FLAG_DEFAULT = (1 << 0),
        CTL_DISPLAY_SETTING_PICTURE_AR_FLAG_DISABLED = (1 << 1),
        CTL_DISPLAY_SETTING_PICTURE_AR_FLAG_AR_4_3 = (1 << 2),
        CTL_DISPLAY_SETTING_PICTURE_AR_FLAG_AR_16_9 = (1 << 3),
        CTL_DISPLAY_SETTING_PICTURE_AR_FLAG_AR_64_27 = (1 << 4),
        CTL_DISPLAY_SETTING_PICTURE_AR_FLAG_AR_256_135 = (1 << 5),
        CTL_DISPLAY_SETTING_PICTURE_AR_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
