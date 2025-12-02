using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct _ctl_endurance_gaming2_t
    {
        [NativeTypeName("ctl_3d_endurance_gaming_control_t")]
        public _ctl_3d_endurance_gaming_control_t EGControl;

        [NativeTypeName("ctl_3d_endurance_gaming_mode_t")]
        public _ctl_3d_endurance_gaming_mode_t EGMode;

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
