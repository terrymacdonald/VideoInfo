using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct _ctl_display_properties_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_os_display_encoder_identifier_t")]
        public _ctl_os_display_encoder_identifier_t Os_display_encoder_handle;

        [NativeTypeName("ctl_display_output_types_t")]
        public _ctl_display_output_types_t Type;

        [NativeTypeName("ctl_attached_display_mux_type_t")]
        public _ctl_attached_display_mux_type_t AttachedDisplayMuxType;

        [NativeTypeName("ctl_display_output_types_t")]
        public _ctl_display_output_types_t ProtocolConverterOutput;

        [NativeTypeName("ctl_revision_datatype_t")]
        public _ctl_revision_datatype_t SupportedSpec;

        [NativeTypeName("ctl_output_bpc_flags_t")]
        public uint SupportedOutputBPCFlags;

        [NativeTypeName("ctl_protocol_converter_location_flags_t")]
        public uint ProtocolConverterType;

        [NativeTypeName("ctl_display_config_flags_t")]
        public uint DisplayConfigFlags;

        [NativeTypeName("ctl_std_display_feature_flags_t")]
        public uint FeatureEnabledFlags;

        [NativeTypeName("ctl_std_display_feature_flags_t")]
        public uint FeatureSupportedFlags;

        [NativeTypeName("ctl_intel_display_feature_flags_t")]
        public uint AdvancedFeatureEnabledFlags;

        [NativeTypeName("ctl_intel_display_feature_flags_t")]
        public uint AdvancedFeatureSupportedFlags;

        [NativeTypeName("ctl_display_timing_t")]
        public _ctl_display_timing_t Display_Timing_Info;

        [NativeTypeName("uint32_t[16]")]
        public _ReservedFields_e__FixedBuffer ReservedFields;

        [InlineArray(16)]
        public partial struct _ReservedFields_e__FixedBuffer
        {
            public uint e0;
        }
    }
}
