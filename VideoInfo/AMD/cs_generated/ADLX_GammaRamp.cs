using System.Runtime.CompilerServices;

namespace ADLXWrapper;

public partial struct ADLX_GammaRamp
{
    [NativeTypeName("adlx_uint16[768]")]
    public _gamma_e__FixedBuffer gamma;

    [InlineArray(768)]
    public partial struct _gamma_e__FixedBuffer
    {
        public ushort e0;
    }
}
