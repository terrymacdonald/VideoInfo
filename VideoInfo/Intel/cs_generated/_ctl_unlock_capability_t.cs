namespace IGCLWrapper
{
    public partial struct _ctl_unlock_capability_t
    {
        [NativeTypeName("ctl_application_id_t")]
        public _ctl_application_id_t ReservedFuncID;

        [NativeTypeName("ctl_application_id_t")]
        public _ctl_application_id_t UnlockCapsID;
    }
}
