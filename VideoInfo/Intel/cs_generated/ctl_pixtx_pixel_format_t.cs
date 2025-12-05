namespace IGCLWrapper
{
    public partial struct ctl_pixtx_pixel_format_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint32_t")]
        public uint BitsPerColor;

        [NativeTypeName("bool")]
        public byte IsFloat;

        public ctl_pixtx_gamma_encoding_type_t EncodingType;

        public ctl_pixtx_color_space_t ColorSpace;

        public ctl_pixtx_color_model_t ColorModel;

        public ctl_pixtx_color_primaries_t ColorPrimaries;

        public double MaxBrightness;

        public double MinBrightness;
    }
}
