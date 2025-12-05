namespace IGCLWrapper
{
    public partial struct ctl_adaptivesync_getset_t
    {
        [NativeTypeName("bool")]
        public byte AdaptiveSync;

        [NativeTypeName("bool")]
        public byte AdaptiveBalance;

        [NativeTypeName("bool")]
        public byte AllowAsyncForHighFPS;

        public float AdaptiveBalanceStrength;
    }
}
