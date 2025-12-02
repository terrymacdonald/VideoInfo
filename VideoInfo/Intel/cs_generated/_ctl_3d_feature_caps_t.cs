namespace IGCLWrapper
{
    public unsafe partial struct _ctl_3d_feature_caps_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint32_t")]
        public uint NumSupportedFeatures;

        [NativeTypeName("ctl_3d_feature_details_t *")]
        public _ctl_3d_feature_details_t* pFeatureDetails;
    }
}
