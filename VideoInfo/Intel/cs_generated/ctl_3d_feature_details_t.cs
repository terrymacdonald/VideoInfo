namespace IGCLWrapper
{
    public unsafe partial struct ctl_3d_feature_details_t
    {
        public ctl_3d_feature_t FeatureType;

        public ctl_property_value_type_t ValueType;

        public ctl_property_info_t Value;

        [NativeTypeName("int32_t")]
        public int CustomValueSize;

        public void* pCustomValue;

        [NativeTypeName("bool")]
        public byte PerAppSupport;

        [NativeTypeName("int64_t")]
        public long ConflictingFeatures;

        [NativeTypeName("int16_t")]
        public short FeatureMiscSupport;

        [NativeTypeName("int16_t")]
        public short Reserved;

        [NativeTypeName("int16_t")]
        public short Reserved1;

        [NativeTypeName("int16_t")]
        public short Reserved2;
    }
}
