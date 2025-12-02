using System.Runtime.CompilerServices;

namespace ADLXWrapper;

public partial struct ADLX_3DLUT_Data
{
    [NativeTypeName("ADLX_UINT16_RGB[4913]")]
    public _data_e__FixedBuffer data;

    [InlineArray(4913)]
    public partial struct _data_e__FixedBuffer
    {
        public ADLX_UINT16_RGB e0;
    }
}
