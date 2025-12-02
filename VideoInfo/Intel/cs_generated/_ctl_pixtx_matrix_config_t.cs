using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct _ctl_pixtx_matrix_config_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("double[3]")]
        public _PreOffsets_e__FixedBuffer PreOffsets;

        [NativeTypeName("double[3]")]
        public _PostOffsets_e__FixedBuffer PostOffsets;

        [NativeTypeName("double[3][3]")]
        public _Matrix_e__FixedBuffer Matrix;

        [InlineArray(3)]
        public partial struct _PreOffsets_e__FixedBuffer
        {
            public double e0;
        }

        [InlineArray(3)]
        public partial struct _PostOffsets_e__FixedBuffer
        {
            public double e0;
        }

        [InlineArray(3 * 3)]
        public partial struct _Matrix_e__FixedBuffer
        {
            public double e0_0;
        }
    }
}
