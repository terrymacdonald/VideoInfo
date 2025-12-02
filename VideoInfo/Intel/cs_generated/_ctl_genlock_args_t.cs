namespace IGCLWrapper
{
    public partial struct _ctl_genlock_args_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_genlock_operation_t")]
        public _ctl_genlock_operation_t Operation;

        [NativeTypeName("ctl_genlock_topology_t")]
        public _ctl_genlock_topology_t GenlockTopology;

        [NativeTypeName("bool")]
        public byte IsGenlockEnabled;

        [NativeTypeName("bool")]
        public byte IsGenlockPossible;
    }
}
