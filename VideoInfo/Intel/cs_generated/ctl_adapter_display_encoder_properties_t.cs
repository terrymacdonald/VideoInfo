using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct ctl_adapter_display_encoder_properties_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_os_display_encoder_identifier_t Os_display_encoder_handle;

        public ctl_display_output_types_t Type;

        [NativeTypeName("bool")]
        public byte IsOnBoardProtocolConverterOutputPresent;

        public ctl_revision_datatype_t SupportedSpec;

        [NativeTypeName("ctl_output_bpc_flags_t")]
        public uint SupportedOutputBPCFlags;

        [NativeTypeName("ctl_encoder_config_flags_t")]
        public uint EncoderConfigFlags;

        [NativeTypeName("ctl_std_display_feature_flags_t")]
        public uint FeatureSupportedFlags;

        [NativeTypeName("ctl_intel_display_feature_flags_t")]
        public uint AdvancedFeatureSupportedFlags;

        [NativeTypeName("uint32_t[16]")]
        public _ReservedFields_e__FixedBuffer ReservedFields;

        [InlineArray(16)]
        public partial struct _ReservedFields_e__FixedBuffer
        {
            public uint e0;
        }
    }
}
