using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public unsafe partial struct _ctl_lda_args_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint8_t")]
        public byte NumAdapters;

        [NativeTypeName("ctl_device_adapter_handle_t *")]
        public _ctl_device_adapter_handle_t** hLinkedAdapters;

        [NativeTypeName("uint64_t[4]")]
        public _Reserved_e__FixedBuffer Reserved;

        [InlineArray(4)]
        public partial struct _Reserved_e__FixedBuffer
        {
            public ulong e0;
        }
    }
}
