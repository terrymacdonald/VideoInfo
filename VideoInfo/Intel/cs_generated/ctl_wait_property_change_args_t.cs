namespace IGCLWrapper
{
    public unsafe partial struct ctl_wait_property_change_args_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_property_type_flags_t")]
        public uint PropertyType;

        [NativeTypeName("uint32_t")]
        public uint TimeOutMilliSec;

        [NativeTypeName("uint32_t")]
        public uint EventMiscFlags;

        public void* pReserved;

        [NativeTypeName("uint64_t")]
        public ulong ReservedOutFlags;
    }
}
