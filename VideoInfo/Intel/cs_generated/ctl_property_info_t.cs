using System.Runtime.InteropServices;

namespace IGCLWrapper
{
    [StructLayout(LayoutKind.Explicit)]
    public partial struct ctl_property_info_t
    {
        [FieldOffset(0)]
        public ctl_property_info_boolean_t BoolType;

        [FieldOffset(0)]
        public ctl_property_info_float_t FloatType;

        [FieldOffset(0)]
        public ctl_property_info_int_t IntType;

        [FieldOffset(0)]
        public ctl_property_info_enum_t EnumType;

        [FieldOffset(0)]
        public ctl_property_info_uint_t UIntType;
    }
}
