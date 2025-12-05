namespace IGCLWrapper
{
    public enum ctl_adapter_properties_flag_t
    {
        CTL_ADAPTER_PROPERTIES_FLAG_INTEGRATED = (1 << 0),
        CTL_ADAPTER_PROPERTIES_FLAG_LDA_PRIMARY = (1 << 1),
        CTL_ADAPTER_PROPERTIES_FLAG_LDA_SECONDARY = (1 << 2),
        CTL_ADAPTER_PROPERTIES_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
