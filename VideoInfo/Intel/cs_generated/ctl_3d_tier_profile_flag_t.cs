namespace IGCLWrapper
{
    public enum ctl_3d_tier_profile_flag_t
    {
        CTL_3D_TIER_PROFILE_FLAG_TIER_1 = (1 << 0),
        CTL_3D_TIER_PROFILE_FLAG_TIER_2 = (1 << 1),
        CTL_3D_TIER_PROFILE_FLAG_TIER_RECOMMENDED = (1 << 30),
        CTL_3D_TIER_PROFILE_FLAG_MAX = unchecked((int)(0x80000000)),
    }
}
