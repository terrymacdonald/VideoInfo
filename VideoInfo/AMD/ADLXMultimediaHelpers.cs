using System;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Helper methods for multimedia settings (video upscale and video super resolution).
    /// </summary>
    public static unsafe partial class ADLXMultimediaHelpers
    {
        /// <summary>
        /// Gets the IADLXMultimediaServices interface from the system services. Callers must dispose the returned pointer.
        /// </summary>
        public static IADLXMultimediaServices* GetMultimediaServices(IADLXSystem* pSystem)
        {
            if (pSystem == null) throw new ArgumentNullException(nameof(pSystem));

            var system2 = (IADLXSystem2*)pSystem;
            IADLXMultimediaServices* pServices;
            var result = system2->GetMultimediaServices(&pServices);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get multimedia services");
            return pServices;
        }

        /// <summary>
        /// Gets the Video Upscale information for a specific GPU.
        /// </summary>
        public static VideoUpscaleInfo GetVideoUpscale(IADLXMultimediaServices* pMultimediaServices, IADLXGPU* pGPU)
        {
            if (pMultimediaServices == null) throw new ArgumentNullException(nameof(pMultimediaServices));
            if (pGPU == null) throw new ArgumentNullException(nameof(pGPU));

            IADLXVideoUpscale* pUpscale;
            var result = pMultimediaServices->GetVideoUpscale(pGPU, &pUpscale);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get video upscale interface");

            using var upscale = new ComPtr<IADLXVideoUpscale>(pUpscale);
            return new VideoUpscaleInfo(upscale.Get());
        }

        /// <summary>
        /// Applies the settings from a VideoUpscaleInfo object to the hardware.
        /// </summary>
        public static void ApplyVideoUpscale(IADLXVideoUpscale* pVideoUpscale, VideoUpscaleInfo info)
        {
            if (pVideoUpscale == null) throw new ArgumentNullException(nameof(pVideoUpscale));
            if (!info.IsSupported) return;

            SetVideoUpscaleEnabled(pVideoUpscale, info.IsEnabled);
            SetVideoUpscaleSharpness(pVideoUpscale, info.Sharpness);
        }

        /// <summary>
        /// Gets the Video Super Resolution information for a specific GPU.
        /// </summary>
        public static VideoSuperResolutionInfo GetVideoSuperResolution(IADLXMultimediaServices* pMultimediaServices, IADLXGPU* pGPU)
        {
            if (pMultimediaServices == null) throw new ArgumentNullException(nameof(pMultimediaServices));
            if (pGPU == null) throw new ArgumentNullException(nameof(pGPU));

            IADLXVideoSuperResolution* pVsr;
            var result = pMultimediaServices->GetVideoSuperResolution(pGPU, &pVsr);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get video super resolution interface");

            using var vsr = new ComPtr<IADLXVideoSuperResolution>(pVsr);
            return new VideoSuperResolutionInfo(vsr.Get());
        }

        /// <summary>
        /// Applies the settings from a VideoSuperResolutionInfo object to the hardware.
        /// </summary>
        public static void ApplyVideoSuperResolution(IADLXVideoSuperResolution* pVsr, VideoSuperResolutionInfo info)
        {
            if (pVsr == null) throw new ArgumentNullException(nameof(pVsr));
            if (!info.IsSupported) return;

            SetVideoSuperResolutionEnabled(pVsr, info.IsEnabled);
        }

        /// <summary>
        /// Sets the enabled state of Video Upscale.
        /// </summary>
        public static void SetVideoUpscaleEnabled(IADLXVideoUpscale* pVideoUpscale, bool enable)
        {
            if (pVideoUpscale == null) throw new ArgumentNullException(nameof(pVideoUpscale));

            var result = pVideoUpscale->SetEnabled(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set video upscale enabled");
        }

        /// <summary>
        /// Sets the sharpness for Video Upscale.
        /// </summary>
        public static void SetVideoUpscaleSharpness(IADLXVideoUpscale* pVideoUpscale, int sharpness)
        {
            if (pVideoUpscale == null) throw new ArgumentNullException(nameof(pVideoUpscale));

            var result = pVideoUpscale->SetSharpness(sharpness);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set video upscale sharpness");
        }

        /// <summary>
        /// Sets the enabled state of Video Super Resolution.
        /// </summary>
        public static void SetVideoSuperResolutionEnabled(IADLXVideoSuperResolution* pVsr, bool enable)
        {
            if (pVsr == null) throw new ArgumentNullException(nameof(pVsr));

            var result = pVsr->SetEnabled(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set video super resolution enabled");
        }
    }

    /// <summary>
    /// Represents the collected information for Video Upscale.
    /// </summary>
    public readonly struct VideoUpscaleInfo
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }
        public int Sharpness { get; init; }
        public ADLX_IntRange SharpnessRange { get; init; }

        [JsonConstructor]
        public VideoUpscaleInfo(bool isSupported, bool isEnabled, int sharpness, ADLX_IntRange sharpnessRange)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
            Sharpness = sharpness;
            SharpnessRange = sharpnessRange;
        }

        internal unsafe VideoUpscaleInfo(IADLXVideoUpscale* pUpscale)
        {
            bool supported = false, enabled = false;
            pUpscale->IsSupported(&supported);
            pUpscale->IsEnabled(&enabled);
            IsSupported = supported;
            IsEnabled = enabled;

            int sharpness = 0;
            pUpscale->GetSharpness(&sharpness);
            Sharpness = sharpness;

            ADLX_IntRange range = default;
            pUpscale->GetSharpnessRange(&range);
            SharpnessRange = range;
        }
    }

    /// <summary>
    /// Represents the collected information for Video Super Resolution.
    /// </summary>
    public readonly struct VideoSuperResolutionInfo
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }

        [JsonConstructor]
        public VideoSuperResolutionInfo(bool isSupported, bool isEnabled)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
        }

        internal unsafe VideoSuperResolutionInfo(IADLXVideoSuperResolution* pVsr)
        {
            bool supported = false, enabled = false;
            pVsr->IsSupported(&supported);
            pVsr->IsEnabled(&enabled);
            IsSupported = supported;
            IsEnabled = enabled;
        }
    }
}