using System.Runtime.InteropServices;

namespace IGCLWrapper
{
    [StructLayout(LayoutKind.Explicit)]
    public partial struct ctl_lace_aggr_config_t
    {
        [FieldOffset(0)]
        [NativeTypeName("uint8_t")]
        public byte FixedAggressivenessLevelPercent;

        [FieldOffset(0)]
        public ctl_lace_lux_aggr_map_t AggrLevelMap;
    }
}
