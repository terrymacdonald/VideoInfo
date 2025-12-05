namespace IGCLWrapper
{
    public enum ctl_output_bpc_flag_t
    {
        CTL_OUTPUT_BPC_FLAG_6BPC = (1 << 0),
        CTL_OUTPUT_BPC_FLAG_8BPC = (1 << 1),
        CTL_OUTPUT_BPC_FLAG_10BPC = (1 << 2),
        CTL_OUTPUT_BPC_FLAG_12BPC = (1 << 3),
        CTL_OUTPUT_BPC_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
