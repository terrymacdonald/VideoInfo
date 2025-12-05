namespace IGCLWrapper
{
    public unsafe partial struct ctl_runtime_path_args_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_application_id_t UnlockID;

        [NativeTypeName("wchar_t *")]
        public ushort* pRuntimePath;

        [NativeTypeName("uint16_t")]
        public ushort DeviceID;

        [NativeTypeName("uint8_t")]
        public byte RevID;
    }
}
