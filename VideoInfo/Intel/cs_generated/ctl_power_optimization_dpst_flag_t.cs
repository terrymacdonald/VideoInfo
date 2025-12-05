namespace IGCLWrapper
{
    public enum ctl_power_optimization_dpst_flag_t
    {
        CTL_POWER_OPTIMIZATION_DPST_FLAG_BKLT = (1 << 0),
        CTL_POWER_OPTIMIZATION_DPST_FLAG_PANEL_CABC = (1 << 1),
        CTL_POWER_OPTIMIZATION_DPST_FLAG_OPST = (1 << 2),
        CTL_POWER_OPTIMIZATION_DPST_FLAG_ELP = (1 << 3),
        CTL_POWER_OPTIMIZATION_DPST_FLAG_EPSM = (1 << 4),
        CTL_POWER_OPTIMIZATION_DPST_FLAG_APD = (1 << 5),
        CTL_POWER_OPTIMIZATION_DPST_FLAG_PIXOPTIX = (1 << 6),
        CTL_POWER_OPTIMIZATION_DPST_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
