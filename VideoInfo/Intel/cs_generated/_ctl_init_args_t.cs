namespace IGCLWrapper
{
    public partial struct _ctl_init_args_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_version_info_t")]
        public uint AppVersion;

        [NativeTypeName("ctl_init_flags_t")]
        public uint flags;

        [NativeTypeName("ctl_version_info_t")]
        public uint SupportedVersion;

        [NativeTypeName("ctl_application_id_t")]
        public _ctl_application_id_t ApplicationUID;
    }
}
