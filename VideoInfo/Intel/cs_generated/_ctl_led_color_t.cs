namespace IGCLWrapper
{
    public partial struct _ctl_led_color_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public double red;

        public double green;

        public double blue;
    }
}
