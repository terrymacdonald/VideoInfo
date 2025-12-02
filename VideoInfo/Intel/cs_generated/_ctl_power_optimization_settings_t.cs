namespace IGCLWrapper
{
    public partial struct _ctl_power_optimization_settings_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_power_optimization_plan_t")]
        public _ctl_power_optimization_plan_t PowerOptimizationPlan;

        [NativeTypeName("ctl_power_optimization_flags_t")]
        public uint PowerOptimizationFeature;

        [NativeTypeName("bool")]
        public byte Enable;

        [NativeTypeName("ctl_power_optimization_feature_specific_info_t")]
        public _ctl_power_optimization_feature_specific_info_t FeatureSpecificData;

        [NativeTypeName("ctl_power_source_t")]
        public _ctl_power_source_t PowerSource;
    }
}
