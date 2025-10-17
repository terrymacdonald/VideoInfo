using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace ADLXWrapper
{
    // DTO for ADLX_Point
    [JsonObject(MemberSerialization.Fields)]
    public class ADLX_Point_DTO
    {
        public int x;
        public int y;

        public ADLX_Point_DTO() { }

        public ADLX_Point_DTO(ADLX_Point point)
        {
            x = point.x;
            y = point.y;
        }

        public ADLX_Point ToADLX_Point()
        {
            ADLX_Point point = new ADLX_Point();
            point.x = x;
            point.y = y;
            return point;
        }

        public override bool Equals(object? obj)
        {
            return obj is ADLX_Point_DTO dTO &&
                   x == dTO.x &&
                   y == dTO.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }
    }

    // DTO for ADLX_RGB
    [JsonObject(MemberSerialization.Fields)]
    public class ADLX_RGB_DTO
    {
        public double red;
        public double green;
        public double blue;

        public ADLX_RGB_DTO() { }

        public ADLX_RGB_DTO(ADLX_RGB rgb)
        {
            red = rgb.gamutR;
            green = rgb.gamutG;
            blue = rgb.gamutB;
        }

        public ADLX_RGB ToADLX_RGB()
        {
            ADLX_RGB rgb = new ADLX_RGB();
            rgb.gamutR = red;
            rgb.gamutG = green;
            rgb.gamutB = blue;
            return rgb;
        }

        public override bool Equals(object? obj)
        {
            return obj is ADLX_RGB_DTO dTO &&
                   red == dTO.red &&
                   green == dTO.green &&
                   blue == dTO.blue;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(red, green, blue);
        }
    }

    // DTO for ADLX_TimingInfo
    [JsonObject(MemberSerialization.Fields)]
    public class ADLX_TimingInfo_DTO
    {
        public int timingFlags;
        public int hTotal;
        public int vTotal;
        public int hDisplay;
        public int vDisplay;
        public int hFrontPorch;
        public int vFrontPorch;
        public int hSyncWidth;
        public int vSyncWidth;
        public int hPolarity;
        public int vPolarity;

        public ADLX_TimingInfo_DTO() { }

        public ADLX_TimingInfo_DTO(ADLX_TimingInfo timingInfo)
        {
            timingFlags = timingInfo.timingFlags;
            hTotal = timingInfo.hTotal;
            vTotal = timingInfo.vTotal;
            hDisplay = timingInfo.hDisplay;
            vDisplay = timingInfo.vDisplay;
            hFrontPorch = timingInfo.hFrontPorch;
            vFrontPorch = timingInfo.vFrontPorch;
            hSyncWidth = timingInfo.hSyncWidth;
            vSyncWidth = timingInfo.vSyncWidth;
            hPolarity = timingInfo.hPolarity;
            vPolarity = timingInfo.vPolarity;
        }

        public ADLX_TimingInfo ToADLX_TimingInfo()
        {
            ADLX_TimingInfo timingInfo = new ADLX_TimingInfo();
            timingInfo.timingFlags = timingFlags;
            timingInfo.hTotal = hTotal;
            timingInfo.vTotal = vTotal;
            timingInfo.hDisplay = hDisplay;
            timingInfo.vDisplay = vDisplay;
            timingInfo.hFrontPorch = hFrontPorch;
            timingInfo.vFrontPorch = vFrontPorch;
            timingInfo.hSyncWidth = hSyncWidth;
            timingInfo.vSyncWidth = vSyncWidth;
            timingInfo.hPolarity = hPolarity;
            timingInfo.vPolarity = vPolarity;
            return timingInfo;
        }

        public override bool Equals(object? obj)
        {
            return obj is ADLX_TimingInfo_DTO dTO &&
                   timingFlags == dTO.timingFlags &&
                   hTotal == dTO.hTotal &&
                   vTotal == dTO.vTotal &&
                   hDisplay == dTO.hDisplay &&
                   vDisplay == dTO.vDisplay &&
                   hFrontPorch == dTO.hFrontPorch &&
                   vFrontPorch == dTO.vFrontPorch &&
                   hSyncWidth == dTO.hSyncWidth &&
                   vSyncWidth == dTO.vSyncWidth &&
                   hPolarity == dTO.hPolarity &&
                   vPolarity == dTO.vPolarity;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(timingFlags);
            hash.Add(hTotal);
            hash.Add(vTotal);
            hash.Add(hDisplay);
            hash.Add(vDisplay);
            hash.Add(hFrontPorch);
            hash.Add(vFrontPorch);
            hash.Add(hSyncWidth);
            hash.Add(vSyncWidth);
            hash.Add(hPolarity);
            hash.Add(vPolarity);
            return hash.ToHashCode();
        }
    }
}
