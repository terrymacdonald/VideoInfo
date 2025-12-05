namespace IGCLWrapper
{
    public partial struct ctl_pixtx_color_primaries_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public double xR;

        public double yR;

        public double xG;

        public double yG;

        public double xB;

        public double yB;

        public double xW;

        public double yW;
    }
}
