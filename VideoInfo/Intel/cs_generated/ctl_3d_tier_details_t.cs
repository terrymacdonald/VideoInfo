using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct ctl_3d_tier_details_t
    {
        public ctl_3d_tier_type_flag_t TierType;

        public ctl_3d_tier_profile_flag_t TierProfile;

        [NativeTypeName("uint64_t[4]")]
        public _Reserved_e__FixedBuffer Reserved;

        [InlineArray(4)]
        public partial struct _Reserved_e__FixedBuffer
        {
            public ulong e0;
        }
    }
}
