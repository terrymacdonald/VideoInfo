using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Display settings helpers (FreeSync, GPU scaling, scaling mode, color depth, pixel format, VSR, integer scaling, HDCP, VariBright, display blanking, custom color).
    /// </summary>
    public static unsafe class ADLXDisplaySettingsHelpers
    {
        /// <summary>
        /// Gets the Gamma settings for a specific display.
        /// </summary>
        public static GammaInfo GetGamma(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
        {
            if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));

            IADLXDisplayGamma* pGamma;
            var result = pDisplayServices->GetGamma(pDisplay, &pGamma);
            if (result != ADLX_RESULT.ADLX_OK) 
                throw new ADLXException(result, "Failed to get Gamma interface");
            using var gamma = new ComPtr<IADLXDisplayGamma>(pGamma);
            return new GammaInfo(gamma.Get());
        }

        /// <summary>
        /// Applies the settings from a GammaInfo object to the hardware.
        /// </summary>
        public static void ApplyGamma(IADLXDisplayGamma* pGamma, GammaInfo info)
        {
            if (pGamma == null) throw new ArgumentNullException(nameof(pGamma));
            if (info.IsSupported == false) return;
            // The Reapply logic is what we need, but it reads the current state first. We can reuse it.
            ReapplyGamma(pGamma); // This will set based on the current state, which should match the info.
        }

        /// <summary>
        /// Reapplies the current Gamma settings.
        /// </summary>
        public static void ReapplyGamma(IADLXDisplayGamma* pGamma)
        {
            if (pGamma == null) throw new ArgumentNullException(nameof(pGamma));

            bool srgb = false, bt709 = false, pq = false, pq2084 = false, g36 = false, coeff = false, reRamp = false, deRamp = false;
            pGamma->IsCurrentReGammaSRGB(&srgb);
            pGamma->IsCurrentReGammaBT709(&bt709);
            pGamma->IsCurrentReGammaPQ(&pq);
            pGamma->IsCurrentReGammaPQ2084Interim(&pq2084);
            pGamma->IsCurrentReGamma36(&g36);
            pGamma->IsCurrentRegammaCoefficient(&coeff);
            pGamma->IsCurrentReGammaRamp(&reRamp);
            pGamma->IsCurrentDeGammaRamp(&deRamp);

            ADLX_RESULT r = ADLX_RESULT.ADLX_FAIL;

            if (srgb) { r = pGamma->SetReGammaSRGB(); }
            else if (bt709) { r = pGamma->SetReGammaBT709(); }
            else if (pq) { r = pGamma->SetReGammaPQ(); }
            else if (pq2084) { r = pGamma->SetReGammaPQ2084Interim(); }
            else if (g36) { r = pGamma->SetReGamma36(); }

            if (r == ADLX_RESULT.ADLX_OK) { return; }
            if (r != ADLX_RESULT.ADLX_FAIL) throw new ADLXException(r, "Failed to reapply preset gamma");

            if (coeff)
            {
                ADLX_RegammaCoeff current = default;
                pGamma->GetGammaCoefficient(&current);
                r = pGamma->SetReGammaCoefficient(current);
                if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to reapply gamma coefficient");
                return;
            }

            ADLX_GammaRamp ramp = default;
            if (reRamp)
            {
                pGamma->GetGammaRamp(&ramp);
                r = pGamma->SetReGammaRamp(ramp);
                if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to reapply re-gamma ramp");
                return;
            }

            if (deRamp)
            {
                pGamma->GetGammaRamp(&ramp);
                r = pGamma->SetDeGammaRamp(ramp);
                if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to reapply de-gamma ramp");
                return;
            }

            r = pGamma->ResetGammaRamp();
            if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to reset gamma ramp");
        }

        /// <summary>
        /// Gets the Gamut settings for a specific display.
        /// </summary>
        public static GamutInfo GetGamut(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
        {
            if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));

            IADLXDisplayGamut* pGamut;
            var result = pDisplayServices->GetGamut(pDisplay, &pGamut);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get Gamut interface");
            using var gamut = new ComPtr<IADLXDisplayGamut>(pGamut);
            return new GamutInfo(gamut.Get());
        }

        /// <summary>
        /// Applies the settings from a GamutInfo object to the hardware.
        /// </summary>
        public static void ApplyGamut(IADLXDisplayGamut* pGamut, GamutInfo info)
        {
            if (pGamut == null) throw new ArgumentNullException(nameof(pGamut));
            if (info.IsGamutSupported == false && info.IsWhitePointSupported == false) return;

            ReapplyGamut(pGamut); // Reapply logic is sufficient here as well.
        }

        /// <summary>
        /// Reapplies the current Gamut settings.
        /// </summary>
        public static void ReapplyGamut(IADLXDisplayGamut* pGamut)
        {
            if (pGamut == null) throw new ArgumentNullException(nameof(pGamut));

            bool cur5000 = false, cur6500 = false, cur7500 = false, cur9300 = false, curCustomWhite = false;
            pGamut->IsCurrent5000kWhitePoint(&cur5000);
            pGamut->IsCurrent6500kWhitePoint(&cur6500);
            pGamut->IsCurrent7500kWhitePoint(&cur7500);
            pGamut->IsCurrent9300kWhitePoint(&cur9300);
            pGamut->IsCurrentCustomWhitePoint(&curCustomWhite);

            bool cur709 = false, cur601 = false, curAdobe = false, curCIERgb = false, cur2020 = false, curCustomSpace = false;
            pGamut->IsCurrentCCIR709ColorSpace(&cur709);
            pGamut->IsCurrentCCIR601ColorSpace(&cur601);
            pGamut->IsCurrentAdobeRgbColorSpace(&curAdobe);
            pGamut->IsCurrentCIERgbColorSpace(&curCIERgb);
            pGamut->IsCurrentCCIR2020ColorSpace(&cur2020);
            pGamut->IsCurrentCustomColorSpace(&curCustomSpace);

            ADLX_GamutColorSpace currentSpace = default;
            pGamut->GetGamutColorSpace(&currentSpace);

            ADLX_Point whitePoint = default;
            bool hasCustomWhitePoint = pGamut->GetWhitePoint(&whitePoint) == ADLX_RESULT.ADLX_OK;

            ADLX_WHITE_POINT? wp = cur5000 ? ADLX_WHITE_POINT.WHITE_POINT_5000K :
                                   cur6500 ? ADLX_WHITE_POINT.WHITE_POINT_6500K :
                                   cur7500 ? ADLX_WHITE_POINT.WHITE_POINT_7500K :
                                   cur9300 ? ADLX_WHITE_POINT.WHITE_POINT_9300K :
                                   (ADLX_WHITE_POINT?)null;

            ADLX_GAMUT_SPACE? gamutSpace = cur709 ? ADLX_GAMUT_SPACE.GAMUT_SPACE_CCIR_709 :
                                         cur601 ? ADLX_GAMUT_SPACE.GAMUT_SPACE_CCIR_601 :
                                         curAdobe ? ADLX_GAMUT_SPACE.GAMUT_SPACE_ADOBE_RGB :
                                         curCIERgb ? ADLX_GAMUT_SPACE.GAMUT_SPACE_CIE_RGB :
                                         cur2020 ? ADLX_GAMUT_SPACE.GAMUT_SPACE_CCIR_2020 :
                                         (ADLX_GAMUT_SPACE?)null;

            ADLX_RESULT r = ADLX_RESULT.ADLX_FAIL;

            if (gamutSpace.HasValue)
            {
                if (wp.HasValue)
                {
                    r = pGamut->SetGamut(wp.Value, gamutSpace.Value);
                }
                else if (hasCustomWhitePoint || curCustomWhite)
                {
                    ADLX_RGB white = new ADLX_RGB { gamutR = whitePoint.x, gamutG = whitePoint.y, gamutB = 0 };
                    r = pGamut->SetGamut(white, gamutSpace.Value);
                }
            }
            else if (wp.HasValue)
            {
                r = pGamut->SetGamut(wp.Value, currentSpace);
            }
            else if (hasCustomWhitePoint || curCustomWhite || curCustomSpace)
            {
                ADLX_RGB white = new ADLX_RGB { gamutR = whitePoint.x, gamutG = whitePoint.y, gamutB = 0 };
                r = pGamut->SetGamut(white, currentSpace);
            }
            else
            {
                return; // Nothing to reapply
            }

            if (r != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r, "Failed to reapply gamut configuration");
        }

        /// <summary>
        /// Gets the 3DLUT settings for a specific display.
        /// </summary>
        public static ThreeDLUTInfo Get3DLUT(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
        {
            if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));

            IADLXDisplay3DLUT* pLut;
            var result = pDisplayServices->Get3DLUT(pDisplay, &pLut);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get 3DLUT interface");
            using var lut = new ComPtr<IADLXDisplay3DLUT>(pLut);
            return new ThreeDLUTInfo(lut.Get());
        }

        /// <summary>
        /// Applies the settings from a ThreeDLUTInfo object to the hardware.
        /// </summary>
        public static void Apply3DLUT(IADLXDisplay3DLUT* p3dLut, ThreeDLUTInfo info)
        {
            if (p3dLut == null) throw new ArgumentNullException(nameof(p3dLut));
            if (info.IsSceSupported == false && info.IsSceDynamicContrastSupported == false) return;

            Reapply3DLUT(p3dLut);
        }

        /// <summary>
        /// Reapplies the current 3DLUT settings.
        /// </summary>
        public static void Reapply3DLUT(IADLXDisplay3DLUT* p3dLut)
        {
            if (p3dLut == null) throw new ArgumentNullException(nameof(p3dLut));

            bool supSce = false, supVivid = false, curDis = false, curVivid = false;
            p3dLut->IsSupportedSCE(&supSce);
            p3dLut->IsSupportedSCEVividGaming(&supVivid);
            p3dLut->IsCurrentSCEDisabled(&curDis);
            p3dLut->IsCurrentSCEVividGaming(&curVivid);

            ADLX_RESULT r = ADLX_RESULT.ADLX_FAIL;

            if (supSce || supVivid)
            {
                if (curVivid && supVivid) { r = p3dLut->SetSCEVividGaming(); }
                else { r = p3dLut->SetSCEDisabled(); }

                if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to reapply SCE state");
            }

            bool supDynamic = false;
            p3dLut->IsSupportedSCEDynamicContrast(&supDynamic);
            if (supDynamic)
            {
                ADLX_IntRange range = default;
                p3dLut->GetSCEDynamicContrastRange(&range);

                int currentContrast = 0;
                p3dLut->GetSCEDynamicContrast(&currentContrast);

                int clamped = Math.Clamp(currentContrast, range.minValue, range.maxValue);
                r = p3dLut->SetSCEDynamicContrast(clamped);
                if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to reapply SCE dynamic contrast");
            }
        }

        /// <summary>
        /// Gets the Display Connectivity Experience settings for a specific display.
        /// </summary>
        public static ConnectivityExperienceInfo GetDisplayConnectivityExperience(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
        {
            if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));

            var services3 = (IADLXDisplayServices3*)pDisplayServices;
            IADLXDisplayConnectivityExperience* pConn;
            var result = services3->GetDisplayConnectivityExperience(pDisplay, &pConn);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get Display Connectivity Experience interface");

            using var connectivity = new ComPtr<IADLXDisplayConnectivityExperience>(pConn);
            return new ConnectivityExperienceInfo(connectivity.Get());
        }

        /// <summary>
        /// Applies the settings from a ConnectivityExperienceInfo object to the hardware.
        /// </summary>
        public static void ApplyDisplayConnectivityExperience(IADLXDisplayConnectivityExperience* pConnectivity, ConnectivityExperienceInfo info)
        {
            if (pConnectivity == null) throw new ArgumentNullException(nameof(pConnectivity));

            if (info.IsHdmiQualityDetectionSupported) SetDisplayConnectivityHDMIQualityDetectionEnabled(pConnectivity, info.IsHdmiQualityDetectionEnabled);
            if (info.IsRelativePreEmphasisSupported) SetDisplayConnectivityRelativePreEmphasis(pConnectivity, info.RelativePreEmphasis);
            if (info.IsRelativeVoltageSwingSupported) SetDisplayConnectivityRelativeVoltageSwing(pConnectivity, info.RelativeVoltageSwing);
        }

        /// <summary>
        /// Sets the enabled state of HDMI Quality Detection.
        /// </summary>
        public static void SetDisplayConnectivityHDMIQualityDetectionEnabled(IADLXDisplayConnectivityExperience* pConnectivity, bool enable)
        {
            if (pConnectivity == null) throw new ArgumentNullException(nameof(pConnectivity));

            var result = pConnectivity->SetEnabledHDMIQualityDetection(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set HDMI quality detection");
        }

        /// <summary>
        /// Sets the relative pre-emphasis for display connectivity.
        /// </summary>
        public static void SetDisplayConnectivityRelativePreEmphasis(IADLXDisplayConnectivityExperience* pConnectivity, int value)
        {
            if (pConnectivity == null) throw new ArgumentNullException(nameof(pConnectivity));
            var result = pConnectivity->SetRelativePreEmphasis(value);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set relative pre-emphasis");
        }

        /// <summary>
        /// Sets the relative voltage swing for display connectivity.
        /// </summary>
        public static void SetDisplayConnectivityRelativeVoltageSwing(IADLXDisplayConnectivityExperience* pConnectivity, int value)
        {
            if (pConnectivity == null) throw new ArgumentNullException(nameof(pConnectivity));
            var result = pConnectivity->SetRelativeVoltageSwing(value);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set relative voltage swing");
        }

        /// <summary>
        /// Gets the Custom Resolution settings for a specific display.
        /// </summary>
        public static CustomResolutionInfo GetCustomResolution(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
        {
            if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));

            IADLXDisplayCustomResolution* pCustomRes;
            var result = pDisplayServices->GetCustomResolution(pDisplay, &pCustomRes);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Custom Resolution interface");
            }

            using var customRes = new ComPtr<IADLXDisplayCustomResolution>(pCustomRes);
            return new CustomResolutionInfo(customRes.Get());
        }

        /// <summary>
        /// Applies a custom resolution.
        /// </summary>
        public static void ApplyCustomResolution(IADLXDisplayCustomResolution* pCustomRes, DisplayResolutionInfo info)
        {
            if (pCustomRes == null) throw new ArgumentNullException(nameof(pCustomRes));

            // Creating a new resolution is a complex operation that requires a valid IADLXDisplayResolution object.
            // For now, we assume the user would handle creating the IADLXDisplayResolution object themselves.
        }

        /// <summary>
        /// Sets the enabled state of Vari-Bright Backlight Adaptive.
        /// </summary>
        public static void SetVariBrightBacklightAdaptiveEnabled(IADLXDisplayVariBright1* pVariBright, bool enable)
        {
            if (pVariBright == null) throw new ArgumentNullException(nameof(pVariBright));
            var result = pVariBright->SetBacklightAdaptiveEnabled(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set backlight adaptive mode");
        }

        /// <summary>
        /// Sets the enabled state of Vari-Bright Battery Life.
        /// </summary>
        public static void SetVariBrightBatteryLifeEnabled(IADLXDisplayVariBright1* pVariBright, bool enable)
        {
            if (pVariBright == null) throw new ArgumentNullException(nameof(pVariBright));
            var result = pVariBright->SetBatteryLifeEnabled(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set battery life mode");
        }

        /// <summary>
        /// Gets the Custom Color settings for a specific display.
        /// </summary>
        public static CustomColorInfo GetCustomColor(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
        {
            if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));

            IADLXDisplayCustomColor* pCustomColor;
            var result = pDisplayServices->GetCustomColor(pDisplay, &pCustomColor);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Custom Color interface");
            }

            using var customColor = new ComPtr<IADLXDisplayCustomColor>(pCustomColor);
            return new CustomColorInfo(customColor.Get());
        }

        /// <summary>
        /// Applies the settings from a CustomColorInfo object to the hardware.
        /// </summary>
        public static void ApplyCustomColor(IADLXDisplayCustomColor* pCustomColor, CustomColorInfo info)
        {
            if (pCustomColor == null) throw new ArgumentNullException(nameof(pCustomColor));
            if (info.IsSupported == false) return;

            if (info.IsHueSupported) { var r = pCustomColor->SetHue(info.Hue); if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to set Custom Color hue"); }
            if (info.IsSaturationSupported) { var r = pCustomColor->SetSaturation(info.Saturation); if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to set Custom Color saturation"); }
            if (info.IsBrightnessSupported) { var r = pCustomColor->SetBrightness(info.Brightness); if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to set Custom Color brightness"); }
            if (info.IsContrastSupported) { var r = pCustomColor->SetContrast(info.Contrast); if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to set Custom Color contrast"); }
            if (info.IsTemperatureSupported) { var r = pCustomColor->SetTemperature(info.Temperature); if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to set Custom Color temperature"); }
        }

        public static unsafe IntPtr GetDisplayBlankingHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var services = (IADLXDisplayServices3*)pDisplayServices;
            IADLXDisplayBlanking* pBlanking;
            var result = services->GetDisplayBlanking((IADLXDisplay*)pDisplay, &pBlanking);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Display Blanking interface");
            }

            return (IntPtr)pBlanking;
        }

        public static unsafe (bool supported, bool blanked) GetDisplayBlankingState(IntPtr pBlanking)
        {
            if (pBlanking == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pBlanking));

            var blanking = (IADLXDisplayBlanking*)pBlanking;
            bool supported = false;
            var r1 = blanking->IsSupported(&supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query Display Blanking support");

            bool blanked = false;
            var r2 = blanking->IsCurrentBlanked(&blanked);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query current blanking state");

            return (supported, blanked);
        }

        public static unsafe void SetDisplayBlanked(IntPtr pBlanking, bool blank)
        {
            if (pBlanking == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pBlanking));

            var blanking = (IADLXDisplayBlanking*)pBlanking;
            var result = blank ? blanking->SetBlanked() : blanking->SetUnblanked();
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, blank ? "Failed to set display blanked" : "Failed to set display unblanked");
        }

        public static unsafe IntPtr GetVirtualSuperResolutionHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var services = (IADLXDisplayServices3*)pDisplayServices;
            IADLXDisplayVSR* pVsr;
            var result = services->GetVirtualSuperResolution((IADLXDisplay*)pDisplay, &pVsr);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Virtual Super Resolution interface");
            }

            return (IntPtr)pVsr;
        }

        public static unsafe (bool supported, bool enabled) GetVirtualSuperResolutionState(IntPtr pVsr)
        {
            if (pVsr == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVsr));

            var vsr = (IADLXDisplayVSR*)pVsr;
            bool supported = false;
            bool enabled = false;

            var r1 = vsr->IsSupported(&supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query VSR support");

            var r2 = vsr->IsEnabled(&enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query VSR enabled");

            return (supported, enabled);
        }

        public static unsafe void SetVirtualSuperResolutionEnabled(IntPtr pVsr, bool enable)
        {
            if (pVsr == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVsr));

            var vsr = (IADLXDisplayVSR*)pVsr;
            var result = vsr->SetEnabled(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set VSR state");
        }

        public static unsafe IntPtr GetIntegerScalingHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var services = (IADLXDisplayServices3*)pDisplayServices;
            IADLXDisplayIntegerScaling* pIntegerScaling;
            var result = services->GetIntegerScaling((IADLXDisplay*)pDisplay, &pIntegerScaling);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Integer Scaling interface");
            }

            return (IntPtr)pIntegerScaling;
        }

        public static unsafe (bool supported, bool enabled) GetIntegerScalingState(IntPtr pIntegerScaling)
        {
            if (pIntegerScaling == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pIntegerScaling));

            var integerScaling = (IADLXDisplayIntegerScaling*)pIntegerScaling;
            bool supported = false;
            bool enabled = false;

            var r1 = integerScaling->IsSupported(&supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query Integer Scaling support");

            var r2 = integerScaling->IsEnabled(&enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query Integer Scaling enabled");

            return (supported, enabled);
        }

        public static unsafe void SetIntegerScalingEnabled(IntPtr pIntegerScaling, bool enable)
        {
            if (pIntegerScaling == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pIntegerScaling));

            var integerScaling = (IADLXDisplayIntegerScaling*)pIntegerScaling;
            var result = integerScaling->SetEnabled(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set Integer Scaling state");
        }

        public static unsafe IntPtr GetHDCPHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var services = (IADLXDisplayServices3*)pDisplayServices;
            IADLXDisplayHDCP* pHdcp;
            var result = services->GetHDCP((IADLXDisplay*)pDisplay, &pHdcp);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get HDCP interface");
            }

            return (IntPtr)pHdcp;
        }

        public static unsafe (bool supported, bool enabled) GetHDCPState(IntPtr pHdcp)
        {
            if (pHdcp == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pHdcp));

            var hdcp = (IADLXDisplayHDCP*)pHdcp;
            bool supported = false;
            bool enabled = false;

            var r1 = hdcp->IsSupported(&supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query HDCP support");

            var r2 = hdcp->IsEnabled(&enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query HDCP enabled");

            return (supported, enabled);
        }

        public static unsafe void SetHDCPEnabled(IntPtr pHdcp, bool enable)
        {
            if (pHdcp == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pHdcp));

            var hdcp = (IADLXDisplayHDCP*)pHdcp;
            var result = hdcp->SetEnabled(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set HDCP state");
        }

        public enum VariBrightMode
        {
            Unknown = 0,
            MaximizeBrightness,
            OptimizeBrightness,
            Balanced,
            OptimizeBattery,
            MaximizeBattery
        }

        public static unsafe IntPtr GetVariBrightHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var services = (IADLXDisplayServices3*)pDisplayServices;
            IADLXDisplayVariBright* pVariBright;
            var result = services->GetVariBright((IADLXDisplay*)pDisplay, &pVariBright);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get VariBright interface");
            }

            return (IntPtr)pVariBright;
        }

        public static unsafe (bool supported, bool enabled, VariBrightMode mode) GetVariBrightState(IntPtr pVariBright)
        {
            if (pVariBright == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVariBright));

            var variBright = (IADLXDisplayVariBright*)pVariBright;
            bool supported = false;
            bool enabled = false;
            var r1 = variBright->IsSupported(&supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query VariBright support");

            var r2 = variBright->IsEnabled(&enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query VariBright enabled");

            var mode = supported ? DetectVariBrightMode(variBright) : VariBrightMode.Unknown;

            return (supported, enabled, mode);
        }

        private static unsafe VariBrightMode DetectVariBrightMode(IADLXDisplayVariBright* pVariBright)
        {
            bool flag = false;

            if (pVariBright->IsCurrentMaximizeBrightness(&flag) == ADLX_RESULT.ADLX_OK && flag) return VariBrightMode.MaximizeBrightness;
            if (pVariBright->IsCurrentOptimizeBrightness(&flag) == ADLX_RESULT.ADLX_OK && flag) return VariBrightMode.OptimizeBrightness;
            if (pVariBright->IsCurrentBalanced(&flag) == ADLX_RESULT.ADLX_OK && flag) return VariBrightMode.Balanced;
            if (pVariBright->IsCurrentOptimizeBattery(&flag) == ADLX_RESULT.ADLX_OK && flag) return VariBrightMode.OptimizeBattery;
            if (pVariBright->IsCurrentMaximizeBattery(&flag) == ADLX_RESULT.ADLX_OK && flag) return VariBrightMode.MaximizeBattery;

            return VariBrightMode.Unknown;
        }

        public static unsafe void SetVariBright(IntPtr pVariBright, bool enable, VariBrightMode mode)
        {
            if (pVariBright == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVariBright));

            var variBright = (IADLXDisplayVariBright*)pVariBright;
            var result = variBright->SetEnabled(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set VariBright enabled state");

            if (!enable)
                return;

            ADLX_RESULT modeResult;
            switch (mode)
            {
                case VariBrightMode.MaximizeBrightness:
                    modeResult = variBright->SetMaximizeBrightness();
                    break;
                case VariBrightMode.OptimizeBrightness:
                    modeResult = variBright->SetOptimizeBrightness();
                    break;
                case VariBrightMode.Balanced:
                    modeResult = variBright->SetBalanced();
                    break;
                case VariBrightMode.OptimizeBattery:
                    modeResult = variBright->SetOptimizeBattery();
                    break;
                case VariBrightMode.MaximizeBattery:
                    modeResult = variBright->SetMaximizeBattery();
                    break;
                default:
                    modeResult = ADLX_RESULT.ADLX_OK;
                    break;
            }

            if (modeResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(modeResult, "Failed to set VariBright mode");
        }

        public static unsafe IntPtr GetColorDepthHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var services = (IADLXDisplayServices3*)pDisplayServices;
            IADLXDisplayColorDepth* pColorDepth;
            var result = services->GetColorDepth((IADLXDisplay*)pDisplay, &pColorDepth);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Color Depth interface");
            }

            return (IntPtr)pColorDepth;
        }

        public static unsafe (bool supported, ADLX_COLOR_DEPTH current) GetColorDepthState(IntPtr pColorDepth)
        {
            if (pColorDepth == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pColorDepth));

            var colorDepth = (IADLXDisplayColorDepth*)pColorDepth;
            bool supported = false;
            var r1 = colorDepth->IsSupported(&supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query Color Depth support");

            ADLX_COLOR_DEPTH depth = default;
            var r2 = colorDepth->GetValue(&depth);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query Color Depth value");

            return (supported, depth);
        }

        public static unsafe void SetColorDepth(IntPtr pColorDepth, ADLX_COLOR_DEPTH depth)
        {
            if (pColorDepth == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pColorDepth));

            var colorDepth = (IADLXDisplayColorDepth*)pColorDepth;
            var result = colorDepth->SetValue(depth);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set Color Depth");
        }

        public static unsafe IntPtr GetPixelFormatHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var services = (IADLXDisplayServices3*)pDisplayServices;
            IADLXDisplayPixelFormat* pPixelFormat;
            var result = services->GetPixelFormat((IADLXDisplay*)pDisplay, &pPixelFormat);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Pixel Format interface");
            }

            return (IntPtr)pPixelFormat;
        }

        public static unsafe (bool supported, ADLX_PIXEL_FORMAT current) GetPixelFormatState(IntPtr pPixelFormat)
        {
            if (pPixelFormat == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPixelFormat));

            var pixelFormat = (IADLXDisplayPixelFormat*)pPixelFormat;
            bool supported = false;
            var r1 = pixelFormat->IsSupported(&supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query Pixel Format support");

            ADLX_PIXEL_FORMAT format = default;
            var r2 = pixelFormat->GetValue(&format);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query Pixel Format value");

            return (supported, format);
        }

        public static unsafe void SetPixelFormat(IntPtr pPixelFormat, ADLX_PIXEL_FORMAT format)
        {
            if (pPixelFormat == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPixelFormat));

            var pixelFormat = (IADLXDisplayPixelFormat*)pPixelFormat;
            var result = pixelFormat->SetValue(format);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set Pixel Format");
        }

        public static unsafe IntPtr GetFreeSyncHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var services = (IADLXDisplayServices3*)pDisplayServices;
            IADLXDisplayFreeSync* pFS;
            var result = services->GetFreeSync((IADLXDisplay*)pDisplay, &pFS);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get FreeSync interface");
            }

            return (IntPtr)pFS;
        }

        public static unsafe IntPtr GetGPUScalingHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var services = (IADLXDisplayServices3*)pDisplayServices;
            IADLXDisplayGPUScaling* pScaling;
            var result = services->GetGPUScaling((IADLXDisplay*)pDisplay, &pScaling);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU scaling interface");
            }

            return (IntPtr)pScaling;
        }

        public static unsafe IntPtr GetScalingModeHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var services = (IADLXDisplayServices3*)pDisplayServices;
            IADLXDisplayScalingMode* pMode;
            var result = services->GetScalingMode((IADLXDisplay*)pDisplay, &pMode);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get scaling mode interface");
            }

            return (IntPtr)pMode;
        }

        public static unsafe (bool supported, bool enabled) GetFreeSyncState(IntPtr pFreeSync)
        {
            if (pFreeSync == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pFreeSync));

            var freeSync = (IADLXDisplayFreeSync*)pFreeSync;
            bool supported = false;
            bool enabled = false;
            var r1 = freeSync->IsSupported(&supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query FreeSync support");

            var r2 = freeSync->IsEnabled(&enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query FreeSync enabled");

            return (supported, enabled);
        }

        public static unsafe void SetFreeSyncEnabled(IntPtr pFreeSync, bool enable)
        {
            if (pFreeSync == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pFreeSync));

            var freeSync = (IADLXDisplayFreeSync*)pFreeSync;
            var result = freeSync->SetEnabled(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to set FreeSync enabled state");
            }
        }

        public static unsafe (bool supported, bool enabled) GetGPUScalingState(IntPtr pGPUScaling)
        {
            if (pGPUScaling == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUScaling));

            var scaling = (IADLXDisplayGPUScaling*)pGPUScaling;
            bool supported = false;
            bool enabled = false;
            var r1 = scaling->IsSupported(&supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query GPU scaling support");

            var r2 = scaling->IsEnabled(&enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query GPU scaling enabled");

            return (supported, enabled);
        }

        public static unsafe void SetGPUScalingEnabled(IntPtr pGPUScaling, bool enable)
        {
            if (pGPUScaling == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUScaling));

            var scaling = (IADLXDisplayGPUScaling*)pGPUScaling;
            var result = scaling->SetEnabled(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to set GPU scaling enabled state");
            }
        }

        public static unsafe (bool supported, ADLX_SCALE_MODE mode) GetScalingMode(IntPtr pScalingMode)
        {
            if (pScalingMode == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pScalingMode));

            var scaling = (IADLXDisplayScalingMode*)pScalingMode;
            bool supported = false;
            var r1 = scaling->IsSupported(&supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query scaling mode support");

            ADLX_SCALE_MODE mode;
            var r2 = scaling->GetMode(&mode);
            if (r2 != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(r2, "Failed to get scaling mode");
            }

            return (supported, mode);
        }

        public static unsafe void SetScalingMode(IntPtr pScalingMode, ADLX_SCALE_MODE mode)
        {
            if (pScalingMode == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pScalingMode));

            var scaling = (IADLXDisplayScalingMode*)pScalingMode;
            var result = scaling->SetMode(mode);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to set scaling mode");
            }
        }

        public static unsafe IntPtr GetFreeSyncColorAccuracyHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var services = (IADLXDisplayServices3*)pDisplayServices;
            IADLXDisplayFreeSyncColorAccuracy* pFSCA;
            var result = services->GetFreeSyncColorAccuracy((IADLXDisplay*)pDisplay, &pFSCA);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get FreeSync Color Accuracy interface");
            }

            return (IntPtr)pFSCA;
        }

        public static unsafe (bool supported, bool enabled) GetFreeSyncColorAccuracyState(IntPtr pFSCA)
        {
            if (pFSCA == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pFSCA));

            var fsca = (IADLXDisplayFreeSyncColorAccuracy*)pFSCA;
            bool supported = false;
            bool enabled = false;
            var r1 = fsca->IsSupported(&supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query FreeSync Color Accuracy support");

            var r2 = fsca->IsEnabled(&enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query FreeSync Color Accuracy enabled");

            return (supported, enabled);
        }

        public static unsafe void SetFreeSyncColorAccuracyEnabled(IntPtr pFSCA, bool enable)
        {
            if (pFSCA == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pFSCA));

            var fsca = (IADLXDisplayFreeSyncColorAccuracy*)pFSCA;
            var result = fsca->SetEnabled(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to set FreeSync Color Accuracy state");
            }
        }

        public static unsafe IntPtr GetDynamicRefreshRateControlHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var services = (IADLXDisplayServices3*)pDisplayServices;
            IADLXDisplayDynamicRefreshRateControl* pDRR;
            var result = services->GetDynamicRefreshRateControl((IADLXDisplay*)pDisplay, &pDRR);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Dynamic Refresh Rate Control interface");
            }

            return (IntPtr)pDRR;
        }

        public static unsafe (bool supported, bool enabled) GetDynamicRefreshRateControlState(IntPtr pDRR)
        {
            if (pDRR == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDRR));

            var drr = (IADLXDisplayDynamicRefreshRateControl*)pDRR;
            bool supported = false;
            bool enabled = false;
            var r1 = drr->IsSupported(&supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query DRR support");

            var r2 = drr->IsEnabled(&enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query DRR enabled");

            return (supported, enabled);
        }

        public static unsafe void SetDynamicRefreshRateControlEnabled(IntPtr pDRR, bool enable)
        {
            if (pDRR == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDRR));

            var drr = (IADLXDisplayDynamicRefreshRateControl*)pDRR;
            var result = drr->SetEnabled(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to set DRR state");
            }
        }
    }

    /// <summary>
    /// Safe handle for an unmanaged IADLXDisplaySettingsChangedListener backed by a managed delegate. 
    /// </summary>
    public sealed unsafe class DisplaySettingsListenerHandle : SafeHandle
    {
        public delegate bool DisplaySettingsChangedCallback(IntPtr pEvent);
        private static readonly ConcurrentDictionary<IntPtr, DisplaySettingsChangedCallback> _map = new();
        private static readonly IntPtr _thunkPtr = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, byte>)&OnDisplaySettingsChanged;
        private readonly GCHandle _gcHandle;
        private readonly IntPtr _vtbl;

        private DisplaySettingsListenerHandle(DisplaySettingsChangedCallback cb) : base(IntPtr.Zero, true)
        {
            _gcHandle = GCHandle.Alloc(cb);
            _vtbl = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(_vtbl, _thunkPtr);

            var inst = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(inst, _vtbl);
            handle = inst;
            _map[inst] = cb;
        }

        public static DisplaySettingsListenerHandle Create(DisplaySettingsChangedCallback cb)
        {
            if (cb == null) throw new ArgumentNullException(nameof(cb));
            return new DisplaySettingsListenerHandle(cb);
        }

        protected override bool ReleaseHandle()
        {
            _map.TryRemove(handle, out _);
            if (_gcHandle.IsAllocated) _gcHandle.Free();
            if (_vtbl != IntPtr.Zero) Marshal.FreeHGlobal(_vtbl);
            if (handle != IntPtr.Zero) Marshal.FreeHGlobal(handle);
            return true;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static byte OnDisplaySettingsChanged(IntPtr pThis, IntPtr pEvent)
        {
            if (_map.TryGetValue(pThis, out var cb))
            {
                return cb(pEvent) ? (byte)1 : (byte)0;
            }
            return 0;
        }

        public override bool IsInvalid => handle == IntPtr.Zero;
    }

    //================================================================================================
    // Info Structs for Display Settings
    //================================================================================================

    public readonly struct GammaInfo
    {
        public bool IsSupported { get; init; }
        // Other properties would go here if we were to store the exact gamma state.
        // For now, we only store support status, as re-applying is complex.

        [JsonConstructor]
        public GammaInfo(bool isSupported)
        {
            IsSupported = isSupported;
        }

        internal unsafe GammaInfo(IADLXDisplayGamma* pGamma)
        {
            // A simple check. A full implementation would check each gamma type.
            bool supported = false;
            pGamma->IsSupportedReGammaSRGB(&supported);
            IsSupported = supported;
        }
    }

    public readonly struct GamutInfo
    {
        public bool IsWhitePointSupported { get; init; }
        public bool IsGamutSupported { get; init; }
        // Further details can be added here.

        [JsonConstructor]
        public GamutInfo(bool isWhitePointSupported, bool isGamutSupported)
        {
            IsWhitePointSupported = isWhitePointSupported;
            IsGamutSupported = isGamutSupported;
        }

        internal unsafe GamutInfo(IADLXDisplayGamut* pGamut)
        {
            bool wp = false, gamut = false;
            pGamut->IsSupportedCustomWhitePoint(&wp);
            pGamut->IsSupportedCustomColorSpace(&gamut);
            IsWhitePointSupported = wp;
            IsGamutSupported = gamut;
        }
    }

    public readonly struct ThreeDLUTInfo
    {
        public bool IsSceSupported { get; init; }
        public bool IsSceVividGamingSupported { get; init; }
        public bool IsSceDynamicContrastSupported { get; init; }
        public bool IsUser3DLutSupported { get; init; }

        [JsonConstructor]
        public ThreeDLUTInfo(bool isSceSupported, bool isSceVividGamingSupported, bool isSceDynamicContrastSupported, bool isUser3DLutSupported)
        {
            IsSceSupported = isSceSupported;
            IsSceVividGamingSupported = isSceVividGamingSupported;
            IsSceDynamicContrastSupported = isSceDynamicContrastSupported;
            IsUser3DLutSupported = isUser3DLutSupported;
        }

        internal unsafe ThreeDLUTInfo(IADLXDisplay3DLUT* p3dLut)
        {
            bool sce = false, vivid = false, dynamic = false, user = false;
            p3dLut->IsSupportedSCE(&sce);
            p3dLut->IsSupportedSCEVividGaming(&vivid);
            p3dLut->IsSupportedSCEDynamicContrast(&dynamic);
            p3dLut->IsSupportedUser3DLUT(&user);
            IsSceSupported = sce;
            IsSceVividGamingSupported = vivid;
            IsSceDynamicContrastSupported = dynamic;
            IsUser3DLutSupported = user;
        }
    }

    public readonly struct ConnectivityExperienceInfo
    {
        public bool IsHdmiQualityDetectionSupported { get; init; }
        public bool IsHdmiQualityDetectionEnabled { get; init; }
        public bool IsDpLinkRateSupported { get; init; }
        public ADLX_DP_LINK_RATE DpLinkRate { get; init; }
        public bool IsRelativePreEmphasisSupported { get; init; }
        public int RelativePreEmphasis { get; init; }
        public bool IsRelativeVoltageSwingSupported { get; init; }
        public int RelativeVoltageSwing { get; init; }

        [JsonConstructor]
        public ConnectivityExperienceInfo(bool isHdmiQualityDetectionSupported, bool isHdmiQualityDetectionEnabled, bool isDpLinkRateSupported, ADLX_DP_LINK_RATE dpLinkRate, bool isRelativePreEmphasisSupported, int relativePreEmphasis, bool isRelativeVoltageSwingSupported, int relativeVoltageSwing)
        {
            IsHdmiQualityDetectionSupported = isHdmiQualityDetectionSupported;
            IsHdmiQualityDetectionEnabled = isHdmiQualityDetectionEnabled;
            IsDpLinkRateSupported = isDpLinkRateSupported;
            DpLinkRate = dpLinkRate;
            IsRelativePreEmphasisSupported = isRelativePreEmphasisSupported;
            RelativePreEmphasis = relativePreEmphasis;
            IsRelativeVoltageSwingSupported = isRelativeVoltageSwingSupported;
            RelativeVoltageSwing = relativeVoltageSwing;
        }

        internal unsafe ConnectivityExperienceInfo(IADLXDisplayConnectivityExperience* pConn)
        {
            bool supported = false, enabled = false;
            pConn->IsSupportedHDMIQualityDetection(&supported);
            IsHdmiQualityDetectionSupported = supported;
            pConn->IsEnabledHDMIQualityDetection(&enabled);
            IsHdmiQualityDetectionEnabled = enabled;

            supported = false;
            pConn->IsSupportedDPLink(&supported);
            IsDpLinkRateSupported = supported;
            ADLX_DP_LINK_RATE rate = default;
            if (IsDpLinkRateSupported && pConn->GetDPLinkRate(&rate) == ADLX_RESULT.ADLX_OK)
            {
                DpLinkRate = rate;
            }
            else
            {
                DpLinkRate = default;
            }

            int relPre = 0;
            IsRelativePreEmphasisSupported = pConn->GetRelativePreEmphasis(&relPre) == ADLX_RESULT.ADLX_OK;
            RelativePreEmphasis = relPre;

            int relVolt = 0;
            IsRelativeVoltageSwingSupported = pConn->GetRelativeVoltageSwing(&relVolt) == ADLX_RESULT.ADLX_OK;
            RelativeVoltageSwing = relVolt;
        }
    }

    public readonly struct CustomResolutionInfo
    {
        public bool IsSupported { get; init; }
        public IReadOnlyList<DisplayResolutionInfo> Resolutions { get; init; }

        [JsonConstructor]
        public CustomResolutionInfo(bool isSupported, IReadOnlyList<DisplayResolutionInfo> resolutions)
        {
            IsSupported = isSupported;
            Resolutions = resolutions;
        }

        internal unsafe CustomResolutionInfo(IADLXDisplayCustomResolution* pCustomRes)
        {
            bool supported = false;
            pCustomRes->IsSupported(&supported);
            IsSupported = supported;

            var resolutions = new List<DisplayResolutionInfo>();
            if (IsSupported)
            {
                IADLXDisplayResolutionList* pResList;
                pCustomRes->GetResolutionList(&pResList);
                using var resList = new ComPtr<IADLXDisplayResolutionList>(pResList);
                for (uint i = 0; i < resList.Get()->Size(); i++)
                {
                    IADLXDisplayResolution* pRes;
                    resList.Get()->At(i, &pRes);
                    using var res = new ComPtr<IADLXDisplayResolution>(pRes);
                    resolutions.Add(new DisplayResolutionInfo(res.Get()));
                }
            }
            Resolutions = resolutions;
        }
    }

    public readonly struct DisplayResolutionInfo
    {
        public int ResWidth { get; init; }
        public int ResHeight { get; init; }
        public int RefreshRate { get; init; }
        // Add other ADLX_CustomResolution fields as needed

        [JsonConstructor]
        public DisplayResolutionInfo(int resWidth, int resHeight, int refreshRate)
        {
            ResWidth = resWidth;
            ResHeight = resHeight;
            RefreshRate = refreshRate;
        }

        internal unsafe DisplayResolutionInfo(IADLXDisplayResolution* pRes)
        {
            ADLX_CustomResolution res = default;
            pRes->GetValue(&res);
            ResWidth = res.resWidth;
            ResHeight = res.resHeight;
            RefreshRate = res.refreshRate;
        }
    }

    public readonly struct CustomColorInfo
    {
        public bool IsSupported { get; init; }
        public bool IsHueSupported { get; init; }
        public int Hue { get; init; }
        public bool IsSaturationSupported { get; init; }
        public int Saturation { get; init; }
        public bool IsBrightnessSupported { get; init; }
        public int Brightness { get; init; }
        public bool IsContrastSupported { get; init; }
        public int Contrast { get; init; }
        public bool IsTemperatureSupported { get; init; }
        public int Temperature { get; init; }

        [JsonConstructor]
        public CustomColorInfo(bool isSupported, bool isHueSupported, int hue, bool isSaturationSupported, int saturation, bool isBrightnessSupported, int brightness, bool isContrastSupported, int contrast, bool isTemperatureSupported, int temperature)
        {
            IsSupported = isSupported; IsHueSupported = isHueSupported; Hue = hue;
            IsSaturationSupported = isSaturationSupported; Saturation = saturation;
            IsBrightnessSupported = isBrightnessSupported; Brightness = brightness;
            IsContrastSupported = isContrastSupported; Contrast = contrast;
            IsTemperatureSupported = isTemperatureSupported; Temperature = temperature;
        }

        internal unsafe CustomColorInfo(IADLXDisplayCustomColor* pCustomColor)
        {
            bool supported = false;
            pCustomColor->IsHueSupported(&supported); IsHueSupported = supported;
            int hue = 0; pCustomColor->GetHue(&hue); Hue = hue;

            supported = false;
            pCustomColor->IsSaturationSupported(&supported); IsSaturationSupported = supported;
            int sat = 0; pCustomColor->GetSaturation(&sat); Saturation = sat;

            supported = false;
            pCustomColor->IsBrightnessSupported(&supported); IsBrightnessSupported = supported;
            int bright = 0; pCustomColor->GetBrightness(&bright); Brightness = bright;

            supported = false;
            pCustomColor->IsContrastSupported(&supported); IsContrastSupported = supported;
            int cont = 0; pCustomColor->GetContrast(&cont); Contrast = cont;

            supported = false;
            pCustomColor->IsTemperatureSupported(&supported); IsTemperatureSupported = supported;
            int temp = 0; pCustomColor->GetTemperature(&temp); Temperature = temp;

            IsSupported = IsHueSupported || IsSaturationSupported || IsBrightnessSupported || IsContrastSupported || IsTemperatureSupported;
        }
    }
}