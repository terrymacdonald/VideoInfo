using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public unsafe partial struct _ctl_video_processing_feature_getset_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_video_processing_feature_t")]
        public _ctl_video_processing_feature_t FeatureType;

        [NativeTypeName("char *")]
        public sbyte* ApplicationName;

        [NativeTypeName("int8_t")]
        public sbyte ApplicationNameLength;

        [NativeTypeName("bool")]
        public byte bSet;

        [NativeTypeName("ctl_property_value_type_t")]
        public _ctl_property_value_type_t ValueType;

        [NativeTypeName("ctl_property_t")]
        public _ctl_property_t Value;

        [NativeTypeName("int32_t")]
        public int CustomValueSize;

        public void* pCustomValue;

        [NativeTypeName("uint32_t[16]")]
        public _ReservedFields_e__FixedBuffer ReservedFields;

        [InlineArray(16)]
        public partial struct _ReservedFields_e__FixedBuffer
        {
            public uint e0;
        }
    }
}
