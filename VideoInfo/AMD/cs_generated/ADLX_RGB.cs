namespace ADLXWrapper;

public partial struct ADLX_RGB
{
    [NativeTypeName("adlx_double")]
    public double gamutR;

    [NativeTypeName("adlx_double")]
    public double gamutG;

    [NativeTypeName("adlx_double")]
    public double gamutB;
}
