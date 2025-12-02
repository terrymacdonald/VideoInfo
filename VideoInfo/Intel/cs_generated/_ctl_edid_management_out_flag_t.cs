namespace IGCLWrapper
{
    public enum _ctl_edid_management_out_flag_t
    {
        CTL_EDID_MANAGEMENT_OUT_FLAG_OS_CONN_NOTIFICATION = (1 << 0),
        CTL_EDID_MANAGEMENT_OUT_FLAG_SUPPLIED_EDID = (1 << 1),
        CTL_EDID_MANAGEMENT_OUT_FLAG_MONITOR_EDID = (1 << 2),
        CTL_EDID_MANAGEMENT_OUT_FLAG_DISPLAY_CONNECTED = (1 << 3),
        CTL_EDID_MANAGEMENT_OUT_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
