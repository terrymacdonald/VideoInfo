namespace IGCLWrapper
{
    public unsafe partial struct _ctl_pixtx_1dlut_config_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_pixtx_lut_sampling_type_t")]
        public _ctl_pixtx_lut_sampling_type_t SamplingType;

        [NativeTypeName("uint32_t")]
        public uint NumSamplesPerChannel;

        [NativeTypeName("uint32_t")]
        public uint NumChannels;

        public double* pSampleValues;

        public double* pSamplePositions;
    }
}
