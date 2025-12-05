using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct ctl_application_id_t
    {
        [NativeTypeName("uint32_t")]
        public uint Data1;

        [NativeTypeName("uint16_t")]
        public ushort Data2;

        [NativeTypeName("uint16_t")]
        public ushort Data3;

        [NativeTypeName("uint8_t[8]")]
        public _Data4_e__FixedBuffer Data4;

        [InlineArray(8)]
        public partial struct _Data4_e__FixedBuffer
        {
            public byte e0;
        }
    }
}
