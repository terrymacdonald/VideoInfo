namespace IGCLWrapper
{
    public partial struct ctl_lace_lux_aggr_map_entry_t
    {
        [NativeTypeName("uint32_t")]
        public uint Lux;

        [NativeTypeName("uint8_t")]
        public byte AggressivenessPercent;
    }
}
