namespace IGCLWrapper
{
    public partial struct ctl_genlock_args_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_genlock_operation_t Operation;

        public ctl_genlock_topology_t GenlockTopology;

        [NativeTypeName("bool")]
        public byte IsGenlockEnabled;

        [NativeTypeName("bool")]
        public byte IsGenlockPossible;
    }
}
