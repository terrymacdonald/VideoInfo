namespace ADLXWrapper;

public partial struct ADLX_TimingInfo
{
    [NativeTypeName("adlx_int")]
    public int timingFlags;

    [NativeTypeName("adlx_int")]
    public int hTotal;

    [NativeTypeName("adlx_int")]
    public int vTotal;

    [NativeTypeName("adlx_int")]
    public int hDisplay;

    [NativeTypeName("adlx_int")]
    public int vDisplay;

    [NativeTypeName("adlx_int")]
    public int hFrontPorch;

    [NativeTypeName("adlx_int")]
    public int vFrontPorch;

    [NativeTypeName("adlx_int")]
    public int hSyncWidth;

    [NativeTypeName("adlx_int")]
    public int vSyncWidth;

    [NativeTypeName("adlx_int")]
    public int hPolarity;

    [NativeTypeName("adlx_int")]
    public int vPolarity;
}
