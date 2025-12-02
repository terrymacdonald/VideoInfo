namespace ADLXWrapper;

public partial struct ADLX_LUID
{
    [NativeTypeName("adlx_ulong")]
    public uint lowPart;

    [NativeTypeName("adlx_long")]
    public int highPart;
}
