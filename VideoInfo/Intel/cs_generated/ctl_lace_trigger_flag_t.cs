namespace IGCLWrapper
{
    public enum ctl_lace_trigger_flag_t
    {
        CTL_LACE_TRIGGER_FLAG_AMBIENT_LIGHT = (1 << 0),
        CTL_LACE_TRIGGER_FLAG_FIXED_AGGRESSIVENESS = (1 << 1),
        CTL_LACE_TRIGGER_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
