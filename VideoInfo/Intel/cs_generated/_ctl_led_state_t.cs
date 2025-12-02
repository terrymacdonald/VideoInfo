namespace IGCLWrapper
{
    public partial struct _ctl_led_state_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte isOn;

        public double pwm;

        [NativeTypeName("ctl_led_color_t")]
        public _ctl_led_color_t color;
    }
}
