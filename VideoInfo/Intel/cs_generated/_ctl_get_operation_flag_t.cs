namespace IGCLWrapper
{
    public enum _ctl_get_operation_flag_t
    {
        CTL_GET_OPERATION_FLAG_CURRENT = (1 << 0),
        CTL_GET_OPERATION_FLAG_DEFAULT = (1 << 1),
        CTL_GET_OPERATION_FLAG_CAPABILITY = (1 << 2),
        CTL_GET_OPERATION_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
