namespace IGCLWrapper
{
    public enum ctl_data_type_t
    {
        CTL_DATA_TYPE_INT8 = 0,
        CTL_DATA_TYPE_UINT8 = 1,
        CTL_DATA_TYPE_INT16 = 2,
        CTL_DATA_TYPE_UINT16 = 3,
        CTL_DATA_TYPE_INT32 = 4,
        CTL_DATA_TYPE_UINT32 = 5,
        CTL_DATA_TYPE_INT64 = 6,
        CTL_DATA_TYPE_UINT64 = 7,
        CTL_DATA_TYPE_FLOAT = 8,
        CTL_DATA_TYPE_DOUBLE = 9,
        CTL_DATA_TYPE_STRING_ASCII = 10,
        CTL_DATA_TYPE_STRING_UTF16 = 11,
        CTL_DATA_TYPE_STRING_UTF132 = 12,
        CTL_DATA_TYPE_UNKNOWN = 0x4800FFFF,
        CTL_DATA_TYPE_MAX,
    }
}
