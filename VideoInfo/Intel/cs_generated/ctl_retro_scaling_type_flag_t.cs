namespace IGCLWrapper
{
    public enum ctl_retro_scaling_type_flag_t
    {
        CTL_RETRO_SCALING_TYPE_FLAG_INTEGER = (1 << 0),
        CTL_RETRO_SCALING_TYPE_FLAG_NEAREST_NEIGHBOUR = (1 << 1),
        CTL_RETRO_SCALING_TYPE_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
