using System.Runtime.InteropServices;

namespace IGCLWrapper
{
    [StructLayout(LayoutKind.Explicit)]
    public partial struct _ctl_property_t
    {
        [FieldOffset(0)]
        [NativeTypeName("ctl_property_boolean_t")]
        public _ctl_property_boolean_t BoolType;

        [FieldOffset(0)]
        [NativeTypeName("ctl_property_float_t")]
        public _ctl_property_float_t FloatType;

        [FieldOffset(0)]
        [NativeTypeName("ctl_property_int_t")]
        public _ctl_property_int_t IntType;

        [FieldOffset(0)]
        [NativeTypeName("ctl_property_enum_t")]
        public _ctl_property_enum_t EnumType;

        [FieldOffset(0)]
        [NativeTypeName("ctl_property_uint_t")]
        public _ctl_property_uint_t UIntType;
    }
}
