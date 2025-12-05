using System.Runtime.InteropServices;

namespace IGCLWrapper
{
    [StructLayout(LayoutKind.Explicit)]
    public partial struct ctl_pixtx_config_t
    {
        [FieldOffset(0)]
        public ctl_pixtx_1dlut_config_t OneDLutConfig;

        [FieldOffset(0)]
        public ctl_pixtx_3dlut_config_t ThreeDLutConfig;

        [FieldOffset(0)]
        public ctl_pixtx_matrix_config_t MatrixConfig;
    }
}
