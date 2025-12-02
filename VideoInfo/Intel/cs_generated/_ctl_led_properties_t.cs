namespace IGCLWrapper
{
    public partial struct _ctl_led_properties_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte canControl;

        [NativeTypeName("bool")]
        public byte isI2C;

        [NativeTypeName("bool")]
        public byte isPWM;

        [NativeTypeName("bool")]
        public byte haveRGB;
    }
}
