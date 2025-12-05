namespace IGCLWrapper
{
    public enum ctl_gaming_flip_mode_flag_t
    {
        CTL_GAMING_FLIP_MODE_FLAG_APPLICATION_DEFAULT = (1 << 0),
        CTL_GAMING_FLIP_MODE_FLAG_VSYNC_OFF = (1 << 1),
        CTL_GAMING_FLIP_MODE_FLAG_VSYNC_ON = (1 << 2),
        CTL_GAMING_FLIP_MODE_FLAG_SMOOTH_SYNC = (1 << 3),
        CTL_GAMING_FLIP_MODE_FLAG_SPEED_FRAME = (1 << 4),
        CTL_GAMING_FLIP_MODE_FLAG_CAPPED_FPS = (1 << 5),
        CTL_GAMING_FLIP_MODE_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
