namespace IGCLWrapper
{
    public enum _ctl_i2c_pinpair_flag_t
    {
        CTL_I2C_PINPAIR_FLAG_ATOMICI2C = (1 << 0),
        CTL_I2C_PINPAIR_FLAG_1BYTE_INDEX = (1 << 1),
        CTL_I2C_PINPAIR_FLAG_2BYTE_INDEX = (1 << 2),
        CTL_I2C_PINPAIR_FLAG_4BYTE_INDEX = (1 << 3),
        CTL_I2C_PINPAIR_FLAG_SPEED_SLOW = (1 << 4),
        CTL_I2C_PINPAIR_FLAG_SPEED_FAST = (1 << 5),
        CTL_I2C_PINPAIR_FLAG_SPEED_BIT_BASH = (1 << 6),
        CTL_I2C_PINPAIR_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
