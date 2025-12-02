namespace IGCLWrapper
{
    public enum _ctl_firmware_config_flag_t
    {
        CTL_FIRMWARE_CONFIG_FLAG_IS_DEVICE_LINK_SPEED_DOWNGRADE_CAPABLE = (1 << 0),
        CTL_FIRMWARE_CONFIG_FLAG_IS_DEVICE_LINK_SPEED_DOWNGRADE_ACTIVE = (1 << 1),
        CTL_FIRMWARE_CONFIG_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
