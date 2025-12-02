namespace ADLXWrapper;

public partial struct ADLX_CustomResolution
{
    [NativeTypeName("adlx_int")]
    public int resWidth;

    [NativeTypeName("adlx_int")]
    public int resHeight;

    [NativeTypeName("adlx_int")]
    public int refreshRate;

    public ADLX_DISPLAY_SCAN_TYPE presentation;

    public ADLX_TIMING_STANDARD timingStandard;

    [NativeTypeName("adlx_long")]
    public int GPixelClock;

    public ADLX_TimingInfo detailedTiming;
}
