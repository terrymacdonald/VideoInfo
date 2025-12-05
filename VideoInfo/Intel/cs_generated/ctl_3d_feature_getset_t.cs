namespace IGCLWrapper
{
    public unsafe partial struct ctl_3d_feature_getset_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_3d_feature_t FeatureType;

        [NativeTypeName("char *")]
        public sbyte* ApplicationName;

        [NativeTypeName("int8_t")]
        public sbyte ApplicationNameLength;

        [NativeTypeName("bool")]
        public byte bSet;

        public ctl_property_value_type_t ValueType;

        public ctl_property_t Value;

        [NativeTypeName("int32_t")]
        public int CustomValueSize;

        public void* pCustomValue;
    }
}
