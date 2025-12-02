namespace IGCLWrapper
{
    public enum _ctl_3d_feature_misc_flag_t
    {
        CTL_3D_FEATURE_MISC_FLAG_DX9 = (1 << 0),
        CTL_3D_FEATURE_MISC_FLAG_DX11 = (1 << 1),
        CTL_3D_FEATURE_MISC_FLAG_DX12 = (1 << 2),
        CTL_3D_FEATURE_MISC_FLAG_VULKAN = (1 << 3),
        CTL_3D_FEATURE_MISC_FLAG_LIVE_CHANGE = (1 << 4),
        CTL_3D_FEATURE_MISC_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
