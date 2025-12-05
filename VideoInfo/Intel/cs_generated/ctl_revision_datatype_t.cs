namespace IGCLWrapper
{
    public partial struct ctl_revision_datatype_t
    {
        [NativeTypeName("uint8_t")]
        public byte major_version;

        [NativeTypeName("uint8_t")]
        public byte minor_version;

        [NativeTypeName("uint8_t")]
        public byte revision_version;
    }
}
