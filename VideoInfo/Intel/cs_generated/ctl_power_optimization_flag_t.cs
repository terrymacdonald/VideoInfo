namespace IGCLWrapper
{
    public enum ctl_power_optimization_flag_t
    {
        CTL_POWER_OPTIMIZATION_FLAG_FBC = (1 << 0),
        CTL_POWER_OPTIMIZATION_FLAG_PSR = (1 << 1),
        CTL_POWER_OPTIMIZATION_FLAG_DPST = (1 << 2),
        CTL_POWER_OPTIMIZATION_FLAG_LRR = (1 << 3),
        CTL_POWER_OPTIMIZATION_FLAG_LACE = (1 << 4),
        CTL_POWER_OPTIMIZATION_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
