namespace IGCLWrapper
{
    public partial struct ctl_firmware_version_t
    {
        [NativeTypeName("uint64_t")]
        public ulong major_version;

        [NativeTypeName("uint64_t")]
        public ulong minor_version;

        [NativeTypeName("uint64_t")]
        public ulong build_number;
    }
}
