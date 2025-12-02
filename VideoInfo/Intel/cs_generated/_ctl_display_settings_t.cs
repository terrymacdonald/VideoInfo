using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct _ctl_display_settings_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte Set;

        [NativeTypeName("ctl_display_setting_flags_t")]
        public uint SupportedFlags;

        [NativeTypeName("ctl_display_setting_flags_t")]
        public uint ControllableFlags;

        [NativeTypeName("ctl_display_setting_flags_t")]
        public uint ValidFlags;

        [NativeTypeName("ctl_display_setting_low_latency_t")]
        public _ctl_display_setting_low_latency_t LowLatency;

        [NativeTypeName("ctl_display_setting_sourcetm_t")]
        public _ctl_display_setting_sourcetm_t SourceTM;

        [NativeTypeName("ctl_display_setting_content_type_t")]
        public _ctl_display_setting_content_type_t ContentType;

        [NativeTypeName("ctl_display_setting_quantization_range_t")]
        public _ctl_display_setting_quantization_range_t QuantizationRange;

        [NativeTypeName("ctl_display_setting_picture_ar_flags_t")]
        public uint SupportedPictureAR;

        [NativeTypeName("ctl_display_setting_picture_ar_flag_t")]
        public _ctl_display_setting_picture_ar_flag_t PictureAR;

        [NativeTypeName("ctl_display_setting_audio_t")]
        public _ctl_display_setting_audio_t AudioSettings;

        [NativeTypeName("uint32_t[25]")]
        public _Reserved_e__FixedBuffer Reserved;

        [InlineArray(25)]
        public partial struct _Reserved_e__FixedBuffer
        {
            public uint e0;
        }
    }
}
