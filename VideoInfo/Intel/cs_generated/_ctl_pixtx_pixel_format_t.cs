namespace IGCLWrapper
{
    public partial struct _ctl_pixtx_pixel_format_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint32_t")]
        public uint BitsPerColor;

        [NativeTypeName("bool")]
        public byte IsFloat;

        [NativeTypeName("ctl_pixtx_gamma_encoding_type_t")]
        public _ctl_pixtx_gamma_encoding_type_t EncodingType;

        [NativeTypeName("ctl_pixtx_color_space_t")]
        public _ctl_pixtx_color_space_t ColorSpace;

        [NativeTypeName("ctl_pixtx_color_model_t")]
        public _ctl_pixtx_color_model_t ColorModel;

        [NativeTypeName("ctl_pixtx_color_primaries_t")]
        public _ctl_pixtx_color_primaries_t ColorPrimaries;

        public double MaxBrightness;

        public double MinBrightness;
    }
}
