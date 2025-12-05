namespace IGCLWrapper
{
    public enum ctl_aux_flag_t
    {
        CTL_AUX_FLAG_NATIVE_AUX = (1 << 0),
        CTL_AUX_FLAG_I2C_AUX = (1 << 1),
        CTL_AUX_FLAG_I2C_AUX_MOT = (1 << 2),
        CTL_AUX_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
