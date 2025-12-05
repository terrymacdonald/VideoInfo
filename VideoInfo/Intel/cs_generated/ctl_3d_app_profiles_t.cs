namespace IGCLWrapper
{
    public partial struct ctl_3d_app_profiles_t
    {
        public ctl_3d_tier_type_flag_t TierType;

        [NativeTypeName("ctl_3d_tier_profile_flags_t")]
        public uint SupportedTierProfiles;

        [NativeTypeName("ctl_3d_tier_profile_flags_t")]
        public uint DefaultEnabledTierProfiles;

        [NativeTypeName("ctl_3d_tier_profile_flags_t")]
        public uint CustomizationSupportedTierProfiles;

        [NativeTypeName("ctl_3d_tier_profile_flags_t")]
        public uint EnabledTierProfiles;

        [NativeTypeName("ctl_3d_tier_profile_flags_t")]
        public uint CustomizationEnabledTierProfiles;

        [NativeTypeName("uint64_t")]
        public ulong Reserved;
    }
}
