namespace IGCLWrapper
{
    public partial struct ctl_3d_app_profiles_caps_t
    {
        [NativeTypeName("ctl_3d_tier_type_flags_t")]
        public uint SupportedTierTypes;

        [NativeTypeName("uint64_t")]
        public ulong Reserved;
    }
}
