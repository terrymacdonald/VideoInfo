using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public unsafe partial struct ctl_video_processing_feature_details_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_video_processing_feature_t FeatureType;

        public ctl_property_value_type_t ValueType;

        public ctl_property_info_t Value;

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
