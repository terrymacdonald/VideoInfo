namespace IGCLWrapper
{
    public enum _ctl_property_type_flag_t
    {
        CTL_PROPERTY_TYPE_FLAG_DISPLAY = (1 << 0),
        CTL_PROPERTY_TYPE_FLAG_3D = (1 << 1),
        CTL_PROPERTY_TYPE_FLAG_MEDIA = (1 << 2),
        CTL_PROPERTY_TYPE_FLAG_CORE = (1 << 3),
        CTL_PROPERTY_TYPE_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
