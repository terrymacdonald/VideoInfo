using System.Runtime.InteropServices;

namespace IGCLWrapper
{
    [StructLayout(LayoutKind.Explicit)]
    public partial struct ctl_power_optimization_feature_specific_info_t
    {
        [FieldOffset(0)]
        public ctl_power_optimization_lrr_t LRRInfo;

        [FieldOffset(0)]
        public ctl_power_optimization_psr_t PSRInfo;

        [FieldOffset(0)]
        public ctl_power_optimization_dpst_t DPSTInfo;
    }
}
