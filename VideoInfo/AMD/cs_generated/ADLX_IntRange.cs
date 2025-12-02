namespace ADLXWrapper;

public partial struct ADLX_IntRange
{
    [NativeTypeName("adlx_int")]
    public int minValue;

    [NativeTypeName("adlx_int")]
    public int maxValue;

    [NativeTypeName("adlx_int")]
    public int step;
}
