namespace IGCLWrapper
{
    public unsafe partial struct _ctl_pixtx_3dlut_config_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint32_t")]
        public uint NumSamplesPerChannel;

        [NativeTypeName("ctl_pixtx_3dlut_sample_t *")]
        public _ctl_pixtx_3dlut_sample_t* pSampleValues;
    }
}
