namespace IGCLWrapper
{
    public unsafe partial struct _ctl_lace_lux_aggr_map_t
    {
        [NativeTypeName("uint32_t")]
        public uint MaxNumEntries;

        [NativeTypeName("uint32_t")]
        public uint NumEntries;

        [NativeTypeName("ctl_lace_lux_aggr_map_entry_t *")]
        public _ctl_lace_lux_aggr_map_entry_t* pLuxToAggrMappingTable;
    }
}
