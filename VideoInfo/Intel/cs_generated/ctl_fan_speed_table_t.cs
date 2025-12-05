using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct ctl_fan_speed_table_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("int32_t")]
        public int numPoints;

        [NativeTypeName("ctl_fan_temp_speed_t[32]")]
        public _table_e__FixedBuffer table;

        [InlineArray(32)]
        public partial struct _table_e__FixedBuffer
        {
            public ctl_fan_temp_speed_t e0;
        }
    }
}
