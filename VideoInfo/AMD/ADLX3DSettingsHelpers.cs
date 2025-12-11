using System;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Helper methods for 3D Settings services (Anti-Lag, Boost, RIS, etc.).
    /// </summary>
    public static unsafe class ADLX3DSettingsHelpers
    {
        /// <summary>
        /// Gets the IADLX3DSettingsServices interface from the system services.
        /// </summary>
        public static IADLX3DSettingsServices* Get3DSettingsServices(IADLXSystem* pSystem)
        {
            if (pSystem == null) throw new ArgumentNullException(nameof(pSystem));

            IADLX3DSettingsServices* p3DSettingsServices;
            var result = pSystem->Get3DSettingsServices(&p3DSettingsServices);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get 3D Settings services");
            }
            return p3DSettingsServices;
        }

        /// <summary>
        /// Gets all 3D settings for a specific GPU.
        /// </summary>
        public static All3DSettingsInfo GetAll3DSettings(IADLX3DSettingsServices* p3DSettingsServices, IADLXGPU* pGpu)
        {
            return new All3DSettingsInfo(p3DSettingsServices, pGpu);
        }

        /// <summary>
        /// Applies a complete set of 3D settings to a specific GPU.
        /// </summary>
        public static void ApplyAll3DSettings(IADLX3DSettingsServices* p3DSettingsServices, IADLXGPU* pGpu, All3DSettingsInfo info)
        {
            if (info.AntiLag.HasValue) ApplyAntiLag(p3DSettingsServices, pGpu, info.AntiLag.Value);
            if (info.Boost.HasValue) ApplyBoost(p3DSettingsServices, pGpu, info.Boost.Value);
            if (info.ImageSharpening.HasValue) ApplyRadeonImageSharpening(p3DSettingsServices, pGpu, info.ImageSharpening.Value);
            if (info.EnhancedSync.HasValue) ApplyEnhancedSync(p3DSettingsServices, pGpu, info.EnhancedSync.Value);
            if (info.WaitForVerticalRefresh.HasValue) ApplyWaitForVerticalRefresh(p3DSettingsServices, pGpu, info.WaitForVerticalRefresh.Value);
            if (info.FrameRateTargetControl.HasValue) ApplyFrameRateTargetControl(p3DSettingsServices, pGpu, info.FrameRateTargetControl.Value);
            if (info.AntiAliasing.HasValue) ApplyAntiAliasing(p3DSettingsServices, pGpu, info.AntiAliasing.Value);
            if (info.AnisotropicFiltering.HasValue) ApplyAnisotropicFiltering(p3DSettingsServices, pGpu, info.AnisotropicFiltering.Value);
            if (info.Tessellation.HasValue) ApplyTessellation(p3DSettingsServices, pGpu, info.Tessellation.Value);
        }

        // Individual Get/Apply helpers for each feature

        private static void ApplyAntiLag(IADLX3DSettingsServices* s, IADLXGPU* g, AntiLagInfo i)
        {
            IADLX3DAntiLag* p;
            if (s->GetAntiLag(g, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DAntiLag>(p);
                if (i.IsSupported) c.Get()->SetEnabled(i.IsEnabled ? (byte)1 : (byte)0);
            }
        }

        private static void ApplyBoost(IADLX3DSettingsServices* s, IADLXGPU* g, BoostInfo i)
        {
            IADLX3DBoost* p;
            if (s->GetBoost(g, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DBoost>(p);
                if (i.IsSupported)
                {
                    c.Get()->SetEnabled(i.IsEnabled ? (byte)1 : (byte)0);
                    if (i.IsMinResSupported) c.Get()->SetResolution(i.MinResolution);
                }
            }
        }

        private static void ApplyRadeonImageSharpening(IADLX3DSettingsServices* s, IADLXGPU* g, RadeonImageSharpeningInfo i)
        {
            IADLX3DImageSharpening* p;
            if (s->GetImageSharpening(g, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DImageSharpening>(p);
                if (i.IsSupported)
                {
                    c.Get()->SetEnabled(i.IsEnabled ? (byte)1 : (byte)0);
                    c.Get()->SetSharpness(i.Sharpness);
                }
            }
        }

        private static void ApplyEnhancedSync(IADLX3DSettingsServices* s, IADLXGPU* g, EnhancedSyncInfo i)
        {
            IADLX3DEnhancedSync* p;
            if (s->GetEnhancedSync(g, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DEnhancedSync>(p);
                if (i.IsSupported) c.Get()->SetEnabled(i.IsEnabled ? (byte)1 : (byte)0);
            }
        }

        private static void ApplyWaitForVerticalRefresh(IADLX3DSettingsServices* s, IADLXGPU* g, WaitForVerticalRefreshInfo i)
        {
            IADLX3DWaitForVerticalRefresh* p;
            if (s->GetWaitForVerticalRefresh(g, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DWaitForVerticalRefresh>(p);
                if (i.IsSupported) c.Get()->SetMode(i.Mode);
            }
        }

        private static void ApplyFrameRateTargetControl(IADLX3DSettingsServices* s, IADLXGPU* g, FrameRateTargetControlInfo i)
        {
            IADLX3DFrameRateTargetControl* p;
            if (s->GetFrameRateTargetControl(g, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DFrameRateTargetControl>(p);
                if (i.IsSupported)
                {
                    c.Get()->SetEnabled(i.IsEnabled ? (byte)1 : (byte)0);
                    c.Get()->SetFPS(i.Fps);
                }
            }
        }

        private static void ApplyAntiAliasing(IADLX3DSettingsServices* s, IADLXGPU* g, AntiAliasingInfo i)
        {
            IADLX3DAntiAliasing* p;
            if (s->GetAntiAliasing(g, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DAntiAliasing>(p);
                if (i.IsSupported) c.Get()->SetMode(i.Mode);
            }
        }

        private static void ApplyAnisotropicFiltering(IADLX3DSettingsServices* s, IADLXGPU* g, AnisotropicFilteringInfo i)
        {
            IADLX3DAnisotropicFiltering* p;
            if (s->GetAnisotropicFiltering(g, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DAnisotropicFiltering>(p);
                if (i.IsSupported) c.Get()->SetLevel(i.Level);
            }
        }

        private static void ApplyTessellation(IADLX3DSettingsServices* s, IADLXGPU* g, TessellationInfo i)
        {
            IADLX3DTessellation* p;
            if (s->GetTessellation(g, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DTessellation>(p);
                if (i.IsSupported)
                {
                    c.Get()->SetMode(i.Mode);
                    c.Get()->SetLevel(i.Level);
                }
            }
        }

    }

    //================================================================================================
    // Info Structs for 3D Settings
    //================================================================================================

    /// <summary>
    /// Represents a complete snapshot of all 3D settings for a GPU.
    /// </summary>
    public readonly struct All3DSettingsInfo
    {
        public AntiLagInfo? AntiLag { get; init; }
        public BoostInfo? Boost { get; init; }
        public RadeonImageSharpeningInfo? ImageSharpening { get; init; }
        public EnhancedSyncInfo? EnhancedSync { get; init; }
        public WaitForVerticalRefreshInfo? WaitForVerticalRefresh { get; init; }
        public FrameRateTargetControlInfo? FrameRateTargetControl { get; init; }
        public AntiAliasingInfo? AntiAliasing { get; init; }
        public AnisotropicFilteringInfo? AnisotropicFiltering { get; init; }
        public TessellationInfo? Tessellation { get; init; }

        [JsonConstructor]
        public All3DSettingsInfo(AntiLagInfo? antiLag, BoostInfo? boost, RadeonImageSharpeningInfo? imageSharpening, EnhancedSyncInfo? enhancedSync, WaitForVerticalRefreshInfo? waitForVerticalRefresh, FrameRateTargetControlInfo? frameRateTargetControl, AntiAliasingInfo? antiAliasing, AnisotropicFilteringInfo? anisotropicFiltering, TessellationInfo? tessellation)
        {
            AntiLag = antiLag;
            Boost = boost;
            ImageSharpening = imageSharpening;
            EnhancedSync = enhancedSync;
            WaitForVerticalRefresh = waitForVerticalRefresh;
            FrameRateTargetControl = frameRateTargetControl;
            AntiAliasing = antiAliasing;
            AnisotropicFiltering = anisotropicFiltering;
            Tessellation = tessellation;
        }

        internal unsafe All3DSettingsInfo(IADLX3DSettingsServices* s, IADLXGPU* g)
        {
            IADLX3DAntiLag* pAntiLag;
            if (s->GetAntiLag(g, &pAntiLag) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DAntiLag>(pAntiLag); AntiLag = new AntiLagInfo(c.Get()); } else { AntiLag = null; }

            IADLX3DBoost* pBoost;
            if (s->GetBoost(g, &pBoost) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DBoost>(pBoost); Boost = new BoostInfo(c.Get()); } else { Boost = null; }

            IADLX3DImageSharpening* pRis;
            if (s->GetImageSharpening(g, &pRis) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DImageSharpening>(pRis); ImageSharpening = new RadeonImageSharpeningInfo(c.Get()); } else { ImageSharpening = null; }

            IADLX3DEnhancedSync* pEs;
            if (s->GetEnhancedSync(g, &pEs) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DEnhancedSync>(pEs); EnhancedSync = new EnhancedSyncInfo(c.Get()); } else { EnhancedSync = null; }

            IADLX3DWaitForVerticalRefresh* pVsync;
            if (s->GetWaitForVerticalRefresh(g, &pVsync) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DWaitForVerticalRefresh>(pVsync); WaitForVerticalRefresh = new WaitForVerticalRefreshInfo(c.Get()); } else { WaitForVerticalRefresh = null; }

            IADLX3DFrameRateTargetControl* pFrtc;
            if (s->GetFrameRateTargetControl(g, &pFrtc) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DFrameRateTargetControl>(pFrtc); FrameRateTargetControl = new FrameRateTargetControlInfo(c.Get()); } else { FrameRateTargetControl = null; }

            IADLX3DAntiAliasing* pAa;
            if (s->GetAntiAliasing(g, &pAa) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DAntiAliasing>(pAa); AntiAliasing = new AntiAliasingInfo(c.Get()); } else { AntiAliasing = null; }

            IADLX3DAnisotropicFiltering* pAf;
            if (s->GetAnisotropicFiltering(g, &pAf) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DAnisotropicFiltering>(pAf); AnisotropicFiltering = new AnisotropicFilteringInfo(c.Get()); } else { AnisotropicFiltering = null; }

            IADLX3DTessellation* pTess;
            if (s->GetTessellation(g, &pTess) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DTessellation>(pTess); Tessellation = new TessellationInfo(c.Get()); } else { Tessellation = null; }
        }
    }

    public readonly struct AntiLagInfo
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }

        [JsonConstructor]
        public AntiLagInfo(bool isSupported, bool isEnabled)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
        }

        internal unsafe AntiLagInfo(IADLX3DAntiLag* p)
        {
            bool supported = false;
            p->IsSupported(&supported);
            IsSupported = supported;

            bool enabled = false;
            if (IsSupported) p->IsEnabled(&enabled);
            IsEnabled = enabled;
        }
    }

    public readonly struct BoostInfo
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }
        public bool IsMinResSupported { get; init; }
        public int MinResolution { get; init; }
        public ADLX_IntRange ResolutionRange { get; init; }

        [JsonConstructor]
        public BoostInfo(bool isSupported, bool isEnabled, bool isMinResSupported, int minResolution, ADLX_IntRange resolutionRange)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
            IsMinResSupported = isMinResSupported;
            MinResolution = minResolution;
            ResolutionRange = resolutionRange;
        }

        internal unsafe BoostInfo(IADLX3DBoost* p)
        {
            bool supported = false;
            p->IsSupported(&supported);
            IsSupported = supported;

            if (IsSupported)
            {
                bool enabled = false;
                p->IsEnabled(&enabled);
                IsEnabled = enabled;

                ADLX_IntRange range = default;
                p->GetResolutionRange(&range);
                ResolutionRange = range;

                int minRes = 0;
                p->GetResolution(&minRes);
                MinResolution = minRes;
                IsMinResSupported = true;
            }
            else
            {
                IsEnabled = false;
                IsMinResSupported = false;
                MinResolution = 0;
                ResolutionRange = default;
            }
        }
    }

    public readonly struct RadeonImageSharpeningInfo
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }
        public int Sharpness { get; init; }
        public ADLX_IntRange SharpnessRange { get; init; }

        [JsonConstructor]
        public RadeonImageSharpeningInfo(bool isSupported, bool isEnabled, int sharpness, ADLX_IntRange sharpnessRange)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
            Sharpness = sharpness;
            SharpnessRange = sharpnessRange;
        }

        internal unsafe RadeonImageSharpeningInfo(IADLX3DImageSharpening* p)
        {
            bool supported = false;
            p->IsSupported(&supported);
            IsSupported = supported;

            if (IsSupported)
            {
                bool enabled = false;
                p->IsEnabled(&enabled);
                IsEnabled = enabled;

                int sharpness = 0;
                p->GetSharpness(&sharpness);
                Sharpness = sharpness;

                ADLX_IntRange range = default;
                p->GetSharpnessRange(&range);
                SharpnessRange = range;
            }
            else
            {
                IsEnabled = false;
                Sharpness = 0;
                SharpnessRange = default;
            }
        }
    }

    public readonly struct EnhancedSyncInfo
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }

        [JsonConstructor]
        public EnhancedSyncInfo(bool isSupported, bool isEnabled)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
        }

        internal unsafe EnhancedSyncInfo(IADLX3DEnhancedSync* p)
        {
            bool supported = false;
            p->IsSupported(&supported);
            IsSupported = supported;

            bool enabled = false;
            if (IsSupported) p->IsEnabled(&enabled);
            IsEnabled = enabled;
        }
    }

    public readonly struct WaitForVerticalRefreshInfo
    {
        public bool IsSupported { get; init; }
        public ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE Mode { get; init; }

        [JsonConstructor]
        public WaitForVerticalRefreshInfo(bool isSupported, ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE mode)
        {
            IsSupported = isSupported;
            Mode = mode;
        }

        internal unsafe WaitForVerticalRefreshInfo(IADLX3DWaitForVerticalRefresh* p)
        {
            bool supported = false;
            p->IsSupported(&supported);
            IsSupported = supported;

            if (IsSupported)
            {
                ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE mode = default;
                p->GetMode(&mode);
                Mode = mode;
            }
            else
            {
                Mode = default;
            }
        }
    }

    public readonly struct FrameRateTargetControlInfo
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }
        public int Fps { get; init; }
        public ADLX_IntRange FpsRange { get; init; }

        [JsonConstructor]
        public FrameRateTargetControlInfo(bool isSupported, bool isEnabled, int fps, ADLX_IntRange fpsRange)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
            Fps = fps;
            FpsRange = fpsRange;
        }

        internal unsafe FrameRateTargetControlInfo(IADLX3DFrameRateTargetControl* p)
        {
            bool supported = false;
            p->IsSupported(&supported);
            IsSupported = supported;

            if (IsSupported)
            {
                bool enabled = false;
                p->IsEnabled(&enabled);
                IsEnabled = enabled;

                int fps = 0;
                p->GetFPS(&fps);
                Fps = fps;

                ADLX_IntRange range = default;
                p->GetFPSRange(&range);
                FpsRange = range;
            }
            else
            {
                IsEnabled = false;
                Fps = 0;
                FpsRange = default;
            }
        }
    }

    public readonly struct AntiAliasingInfo
    {
        public bool IsSupported { get; init; }
        public ADLX_ANTI_ALIASING_MODE Mode { get; init; }

        [JsonConstructor]
        public AntiAliasingInfo(bool isSupported, ADLX_ANTI_ALIASING_MODE mode)
        {
            IsSupported = isSupported;
            Mode = mode;
        }

        internal unsafe AntiAliasingInfo(IADLX3DAntiAliasing* p)
        {
            bool supported = false;
            p->IsSupported(&supported);
            IsSupported = supported;

            ADLX_ANTI_ALIASING_MODE mode = default;
            if (IsSupported) p->GetMode(&mode);
            Mode = mode;
        }
    }

    public readonly struct AnisotropicFilteringInfo
    {
        public bool IsSupported { get; init; }
        public ADLX_ANISOTROPIC_FILTERING_LEVEL Level { get; init; }

        [JsonConstructor]
        public AnisotropicFilteringInfo(bool isSupported, ADLX_ANISOTROPIC_FILTERING_LEVEL level)
        {
            IsSupported = isSupported;
            Level = level;
        }

        internal unsafe AnisotropicFilteringInfo(IADLX3DAnisotropicFiltering* p)
        {
            bool supported = false;
            p->IsSupported(&supported);
            IsSupported = supported;

            ADLX_ANISOTROPIC_FILTERING_LEVEL level = default;
            if (IsSupported) p->GetLevel(&level);
            Level = level;
        }
    }

    public readonly struct TessellationInfo
    {
        public bool IsSupported { get; init; }
        public ADLX_TESSELLATION_MODE Mode { get; init; }
        public ADLX_TESSELLATION_LEVEL Level { get; init; }

        [JsonConstructor]
        public TessellationInfo(bool isSupported, ADLX_TESSELLATION_MODE mode, ADLX_TESSELLATION_LEVEL level)
        {
            IsSupported = isSupported;
            Mode = mode;
            Level = level;
        }

        internal unsafe TessellationInfo(IADLX3DTessellation* p)
        {
            bool supported = false;
            p->IsSupported(&supported);
            IsSupported = supported;

            if (IsSupported)
            {
                ADLX_TESSELLATION_MODE mode = default;
                ADLX_TESSELLATION_LEVEL level = default;
                p->GetMode(&mode);
                Mode = mode;
                Level = level;
            }
            else
            {
                Mode = default;
                Level = default;
            }
        }
    }
}