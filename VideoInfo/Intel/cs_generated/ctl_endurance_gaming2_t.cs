using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct ctl_endurance_gaming2_t
    {
        public ctl_3d_endurance_gaming_control_t EGControl;

        public ctl_3d_endurance_gaming_mode_t EGMode;

        [NativeTypeName("bool")]
        public byte IsFPRequired;

        public double TargetFPS;

        public double RefreshRate;

        [NativeTypeName("uint32_t[4]")]
        public _Reserved_e__FixedBuffer Reserved;

        [InlineArray(4)]
        public partial struct _Reserved_e__FixedBuffer
        {
            public uint e0;
        }
    }
}
