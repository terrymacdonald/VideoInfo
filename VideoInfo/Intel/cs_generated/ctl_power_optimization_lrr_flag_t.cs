namespace IGCLWrapper
{
    public enum ctl_power_optimization_lrr_flag_t
    {
        CTL_POWER_OPTIMIZATION_LRR_FLAG_LRR10 = (1 << 0),
        CTL_POWER_OPTIMIZATION_LRR_FLAG_LRR20 = (1 << 1),
        CTL_POWER_OPTIMIZATION_LRR_FLAG_LRR25 = (1 << 2),
        CTL_POWER_OPTIMIZATION_LRR_FLAG_ALRR = (1 << 3),
        CTL_POWER_OPTIMIZATION_LRR_FLAG_UBLRR = (1 << 4),
        CTL_POWER_OPTIMIZATION_LRR_FLAG_UBZRR = (1 << 5),
        CTL_POWER_OPTIMIZATION_LRR_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
