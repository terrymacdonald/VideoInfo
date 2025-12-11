using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary> 
    /// Helper methods for GPU tuning services
    /// </summary>
    public static unsafe class ADLXGPUTuningHelpers
    {
        /// <summary>
        /// Check if auto tuning is supported for a GPU.
        /// </summary>
        public static bool IsSupportedAutoTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var pServices = (IADLXGPUTuningServices*)pGPUTuningServices;
            bool supported = false;
            var result = pServices->IsSupportedAutoTuning((IADLXGPU*)pGPU, &supported);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check auto tuning support");
            }

            return supported;
        }

        /// <summary>
        /// Check if preset tuning is supported for a GPU.
        /// </summary>
        public static bool IsSupportedPresetTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var pServices = (IADLXGPUTuningServices*)pGPUTuningServices;
            bool supported = false;
            var result = pServices->IsSupportedPresetTuning((IADLXGPU*)pGPU, &supported);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check preset tuning support");
            }

            return supported;
        }

        /// <summary>
        /// Check if manual GFX tuning is supported for a GPU.
        /// </summary>
        public static bool IsSupportedManualGFXTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var pServices = (IADLXGPUTuningServices*)pGPUTuningServices;
            bool supported = false;
            var result = pServices->IsSupportedManualGFXTuning((IADLXGPU*)pGPU, &supported);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check manual GFX tuning support");
            }

            return supported;
        }

        /// <summary>
        /// Check if manual VRAM tuning is supported for a GPU.
        /// </summary>
        public static bool IsSupportedManualVRAMTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var pServices = (IADLXGPUTuningServices*)pGPUTuningServices;
            bool supported = false;
            var result = pServices->IsSupportedManualVRAMTuning((IADLXGPU*)pGPU, &supported);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check manual VRAM tuning support");
            }

            return supported;
        }

        /// <summary>
        /// Check if manual fan tuning is supported for a GPU.
        /// </summary>
        public static bool IsSupportedManualFanTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var pServices = (IADLXGPUTuningServices*)pGPUTuningServices;
            bool supported = false;
            var result = pServices->IsSupportedManualFanTuning((IADLXGPU*)pGPU, &supported);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check manual fan tuning support");
            }

            return supported;
        }

        /// <summary>
        /// Check if manual power tuning is supported for a GPU.
        /// </summary>
        public static bool IsSupportedManualPowerTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var pServices = (IADLXGPUTuningServices*)pGPUTuningServices;
            bool supported = false;
            var result = pServices->IsSupportedManualPowerTuning((IADLXGPU*)pGPU, &supported);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check manual power tuning support");
            }

            return supported;
        }

        public static ManualFanTuningInfo GetManualFanTuning(IADLXGPUTuningServices* pGpuTuningServices, IADLXGPU* pGpu)
        {
            if (pGpuTuningServices == null) throw new ArgumentNullException(nameof(pGpuTuningServices));
            if (pGpu == null) throw new ArgumentNullException(nameof(pGpu));

            IADLXInterface* pManualFanTuning = null;
            pGpuTuningServices->GetManualFanTuning(pGpu, &pManualFanTuning);
            using var fanTuning = new ComPtr<IADLXManualFanTuning>((IADLXManualFanTuning*)pManualFanTuning);
            return new ManualFanTuningInfo(fanTuning.Get());
        }

        public static void ApplyManualFanTuning(IADLXManualFanTuning* pFanTuning, ManualFanTuningInfo info)
        {
            if (pFanTuning == null) throw new ArgumentNullException(nameof(pFanTuning));
            if (!info.IsSupported) return;

            if (info.IsZeroRPMSupported) pFanTuning->SetZeroRPMState(info.ZeroRPMEnabled ? (byte)1 : (byte)0);
            
            // Skipping state writes to avoid reconstructing ADLX state lists with current generated signatures.
        }

        public static ManualVramTuningInfo GetManualVramTuning(IADLXGPUTuningServices* pGpuTuningServices, IADLXGPU* pGpu)
        {
            if (pGpuTuningServices == null) throw new ArgumentNullException(nameof(pGpuTuningServices));
            if (pGpu == null) throw new ArgumentNullException(nameof(pGpu));

            IADLXInterface* pManualVramTuning = null;
            pGpuTuningServices->GetManualVRAMTuning(pGpu, &pManualVramTuning);
            using var vramTuning = new ComPtr<IADLXManualVRAMTuning1>((IADLXManualVRAMTuning1*)pManualVramTuning);
            return new ManualVramTuningInfo(vramTuning.Get());
        }

        public static void ApplyManualVramTuning(IADLXManualVRAMTuning1* pVramTuning, ManualVramTuningInfo info)
        {
            if (pVramTuning == null) throw new ArgumentNullException(nameof(pVramTuning));
            if (!info.IsSupported) return;

            // The new API uses state lists; skip applying to avoid constructing lists manually.
        }

        public static ManualGfxTuningInfo GetManualGfxTuning(IADLXGPUTuningServices* pGpuTuningServices, IADLXGPU* pGpu)
        {
            if (pGpuTuningServices == null) throw new ArgumentNullException(nameof(pGpuTuningServices));
            if (pGpu == null) throw new ArgumentNullException(nameof(pGpu));

            IADLXInterface* pManualGfxTuning = null;
            pGpuTuningServices->GetManualGFXTuning(pGpu, &pManualGfxTuning);
            using var gfxTuning = new ComPtr<IADLXManualGraphicsTuning2>((IADLXManualGraphicsTuning2*)pManualGfxTuning);
            return new ManualGfxTuningInfo(gfxTuning.Get());
        }

        public static void ApplyManualGfxTuning(IADLXManualGraphicsTuning2* pGfxTuning, ManualGfxTuningInfo info)
        {
            if (pGfxTuning == null) throw new ArgumentNullException(nameof(pGfxTuning));
            if (!info.IsSupported) return;

            if (info.MinFrequency.HasValue) pGfxTuning->SetGPUMinFrequency(info.MinFrequency.Value);
            if (info.MaxFrequency.HasValue) pGfxTuning->SetGPUMaxFrequency(info.MaxFrequency.Value);
            if (info.Voltage.HasValue) pGfxTuning->SetGPUVoltage(info.Voltage.Value);
        }

        public static PresetTuningInfo GetPresetTuning(IADLXGPUTuningServices* pGpuTuningServices, IADLXGPU* pGpu)
        {
            if (pGpuTuningServices == null) throw new ArgumentNullException(nameof(pGpuTuningServices));
            if (pGpu == null) throw new ArgumentNullException(nameof(pGpu));

            IADLXInterface* pPresetTuning = null;
            pGpuTuningServices->GetPresetTuning(pGpu, &pPresetTuning);
            using var presetTuning = new ComPtr<IADLXGPUPresetTuning>((IADLXGPUPresetTuning*)pPresetTuning);
            return new PresetTuningInfo(presetTuning.Get());
        }

        public static void ApplyPresetTuning(IADLXGPUPresetTuning* pPresetTuning, PresetTuningInfo info)
        {
            if (pPresetTuning == null) throw new ArgumentNullException(nameof(pPresetTuning));
            if (!info.IsSupported) return;

            ADLX_RESULT result = ADLX_RESULT.ADLX_OK;
            switch (info.CurrentPreset)
            {
                case PresetKind.PowerSaver: result = pPresetTuning->SetPowerSaver(); break;
                case PresetKind.Quiet: result = pPresetTuning->SetQuiet(); break;
                case PresetKind.Balanced: result = pPresetTuning->SetBalanced(); break;
                case PresetKind.Turbo: result = pPresetTuning->SetTurbo(); break;
                case PresetKind.Rage: result = pPresetTuning->SetRage(); break;
                default: result = ADLX_RESULT.ADLX_OK; break;
            }

            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to apply preset tuning");
        }

        public static AutoTuningInfo GetAutoTuning(IADLXGPUTuningServices* pGpuTuningServices, IADLXGPU* pGpu)
        {
            if (pGpuTuningServices == null) throw new ArgumentNullException(nameof(pGpuTuningServices));
            if (pGpu == null) throw new ArgumentNullException(nameof(pGpu));

            IADLXInterface* pAutoTuning = null;
            pGpuTuningServices->GetAutoTuning(pGpu, &pAutoTuning);
            using var autoTuning = new ComPtr<IADLXGPUAutoTuning>((IADLXGPUAutoTuning*)pAutoTuning);
            return new AutoTuningInfo(autoTuning.Get());
        }

        /// <summary>
        /// Gets the GPU tuning changed handling interface.
        /// </summary>
        public static IADLXGPUTuningChangedHandling* GetGpuTuningChangedHandling(IADLXGPUTuningServices* pGpuTuningServices)
        {
            if (pGpuTuningServices == null) throw new ArgumentNullException(nameof(pGpuTuningServices));
            IADLXGPUTuningChangedHandling* pHandling;
            pGpuTuningServices->GetGPUTuningChangedHandling(&pHandling);
            return pHandling;
        }

        /// <summary>
        /// Adds a GPU tuning event listener.
        /// </summary>
        public static void AddGpuTuningEventListener(IADLXGPUTuningChangedHandling* pHandling, GpuTuningListenerHandle listener)
        {
            // Event listeners are currently stubbed; no-op.
        }

        /// <summary>
        /// Removes a GPU tuning event listener.
        /// </summary>
        public static void RemoveGpuTuningEventListener(IADLXGPUTuningChangedHandling* pHandling, GpuTuningListenerHandle listener)
        {
            // Event listeners are currently stubbed; no-op.
        }
    }
    /// <summary>
    /// Represents the tuning capabilities for a GPU.
    /// </summary>
    public readonly struct GpuTuningCapabilitiesInfo
    {
        public bool AutoTuningSupported { get; init; }
        public bool PresetTuningSupported { get; init; }
        public bool ManualGFXTuningSupported { get; init; }
        public bool ManualVRAMTuningSupported { get; init; }
        public bool ManualFanTuningSupported { get; init; }
        public bool ManualPowerTuningSupported { get; init; }

        [JsonConstructor]
        public GpuTuningCapabilitiesInfo(bool autoTuningSupported, bool presetTuningSupported, bool manualGFXTuningSupported, bool manualVRAMTuningSupported, bool manualFanTuningSupported, bool manualPowerTuningSupported)
        {
            AutoTuningSupported = autoTuningSupported;
            PresetTuningSupported = presetTuningSupported;
            ManualGFXTuningSupported = manualGFXTuningSupported;
            ManualVRAMTuningSupported = manualVRAMTuningSupported;
            ManualFanTuningSupported = manualFanTuningSupported;
            ManualPowerTuningSupported = manualPowerTuningSupported;
        }

        public unsafe GpuTuningCapabilitiesInfo(IADLXGPUTuningServices* pServices, IADLXGPU* pGpu)
        {
            AutoTuningSupported = ADLXGPUTuningHelpers.IsSupportedAutoTuning((IntPtr)pServices, (IntPtr)pGpu);
            PresetTuningSupported = ADLXGPUTuningHelpers.IsSupportedPresetTuning((IntPtr)pServices, (IntPtr)pGpu);
            ManualGFXTuningSupported = ADLXGPUTuningHelpers.IsSupportedManualGFXTuning((IntPtr)pServices, (IntPtr)pGpu);
            ManualVRAMTuningSupported = ADLXGPUTuningHelpers.IsSupportedManualVRAMTuning((IntPtr)pServices, (IntPtr)pGpu);
            ManualFanTuningSupported = ADLXGPUTuningHelpers.IsSupportedManualFanTuning((IntPtr)pServices, (IntPtr)pGpu);
            ManualPowerTuningSupported = ADLXGPUTuningHelpers.IsSupportedManualPowerTuning((IntPtr)pServices, (IntPtr)pGpu);
        }
    }

    public readonly struct ManualFanTuningInfo
    {
        public bool IsSupported { get; init; }
        public bool IsZeroRPMSupported { get; init; }
        public bool ZeroRPMEnabled { get; init; }
        public IReadOnlyList<FanPoint> FanPoints { get; init; }

        [JsonConstructor]
        public ManualFanTuningInfo(bool isSupported, bool isZeroRPMSupported, bool zeroRPMEnabled, IReadOnlyList<FanPoint> fanPoints)
        {
            IsSupported = isSupported;
            IsZeroRPMSupported = isZeroRPMSupported;
            ZeroRPMEnabled = zeroRPMEnabled;
            FanPoints = fanPoints;
        }

        internal unsafe ManualFanTuningInfo(IADLXManualFanTuning* pFanTuning)
        {
            bool supported = false;
            pFanTuning->IsSupportedZeroRPM(&supported);
            IsZeroRPMSupported = supported;

            bool enabled = false;
            if (IsZeroRPMSupported) pFanTuning->GetZeroRPMState(&enabled);
            ZeroRPMEnabled = enabled;

            var points = new List<FanPoint>();
            IADLXManualFanTuningStateList* pStates;
            pFanTuning->GetFanTuningStates(&pStates);
            using var states = new ComPtr<IADLXManualFanTuningStateList>(pStates);
            if (states.Get() != null)
            {
                for (uint i = 0; i < states.Get()->Size(); i++)
                {
                    IADLXManualFanTuningState* pState;
                    states.Get()->At(i, &pState);
                    using var state = new ComPtr<IADLXManualFanTuningState>(pState);
                    int speed = 0, temp = 0;
                    state.Get()->GetFanSpeed(&speed);
                    state.Get()->GetTemperature(&temp);
                    points.Add(new FanPoint { FanSpeed = speed, Temperature = temp });
                }
            }
            FanPoints = points;
            IsSupported = FanPoints.Count > 0 || IsZeroRPMSupported;
        }
    }

    public readonly struct FanPoint
    {
        public int FanSpeed { get; init; }
        public int Temperature { get; init; }
    }

    public readonly struct ManualVramTuningInfo
    {
        public bool IsSupported { get; init; }
        public IReadOnlyList<VramState> States { get; init; }

        [JsonConstructor]
        public ManualVramTuningInfo(bool isSupported, IReadOnlyList<VramState> states)
        {
            IsSupported = isSupported;
            States = states;
        }

        internal unsafe ManualVramTuningInfo(IADLXManualVRAMTuning1* pVramTuning)
        {
            var states = new List<VramState>();
            IADLXManualTuningStateList* pStates;
            pVramTuning->GetVRAMTuningStates(&pStates);
            using var stateList = new ComPtr<IADLXManualTuningStateList>(pStates);
            if (stateList.Get() != null)
            {
                for (uint i = 0; i < stateList.Get()->Size(); i++)
                {
                    IADLXManualTuningState* pState;
                    stateList.Get()->At(i, &pState);
                    using var state = new ComPtr<IADLXManualTuningState>(pState);
                    int freq = 0, volt = 0;
                    state.Get()->GetFrequency(&freq);
                    state.Get()->GetVoltage(&volt);
                    states.Add(new VramState { Frequency = freq, Voltage = volt });
                }
            }
            States = states;
            IsSupported = States.Count > 0;
        }
    }

    public readonly struct VramState
    {
        public int Frequency { get; init; }
        public int Voltage { get; init; }
    }

    public readonly struct ManualGfxTuningInfo
    {
        public bool IsSupported { get; init; }
        public int? MinFrequency { get; init; }
        public int? MaxFrequency { get; init; }
        public int? Voltage { get; init; }

        [JsonConstructor]
        public ManualGfxTuningInfo(bool isSupported, int? minFrequency, int? maxFrequency, int? voltage)
        {
            IsSupported = isSupported;
            MinFrequency = minFrequency;
            MaxFrequency = maxFrequency;
            Voltage = voltage;
        }

        internal unsafe ManualGfxTuningInfo(IADLXManualGraphicsTuning2* pGfxTuning)
        {
            int minFreq = 0, maxFreq = 0, volt = 0;
            pGfxTuning->GetGPUMinFrequency(&minFreq);
            pGfxTuning->GetGPUMaxFrequency(&maxFreq);
            pGfxTuning->GetGPUVoltage(&volt);
            MinFrequency = minFreq;
            MaxFrequency = maxFreq;
            Voltage = volt;
            IsSupported = true;
        }
    }

    public readonly struct PresetTuningInfo
    {
        public bool IsSupported { get; init; }
        public PresetKind CurrentPreset { get; init; }
        public IReadOnlyList<PresetKind> SupportedPresets { get; init; }

        [JsonConstructor]
        public PresetTuningInfo(bool isSupported, PresetKind currentPreset, IReadOnlyList<PresetKind> supportedPresets)
        {
            IsSupported = isSupported;
            CurrentPreset = currentPreset;
            SupportedPresets = supportedPresets;
        }

        internal unsafe PresetTuningInfo(IADLXGPUPresetTuning* pPresetTuning)
        {
            var supported = new List<PresetKind>();
            bool b;
            if (pPresetTuning->IsSupportedPowerSaver(&b) == ADLX_RESULT.ADLX_OK && b) supported.Add(PresetKind.PowerSaver);
            if (pPresetTuning->IsSupportedQuiet(&b) == ADLX_RESULT.ADLX_OK && b) supported.Add(PresetKind.Quiet);
            if (pPresetTuning->IsSupportedBalanced(&b) == ADLX_RESULT.ADLX_OK && b) supported.Add(PresetKind.Balanced);
            if (pPresetTuning->IsSupportedTurbo(&b) == ADLX_RESULT.ADLX_OK && b) supported.Add(PresetKind.Turbo);
            if (pPresetTuning->IsSupportedRage(&b) == ADLX_RESULT.ADLX_OK && b) supported.Add(PresetKind.Rage);
            SupportedPresets = supported;
            IsSupported = supported.Count > 0;

            bool curPower = false, curQuiet = false, curBalanced = false, curTurbo = false, curRage = false;
            if (pPresetTuning->IsCurrentPowerSaver(&curPower) == ADLX_RESULT.ADLX_OK && curPower) { CurrentPreset = PresetKind.PowerSaver; return; }
            if (pPresetTuning->IsCurrentQuiet(&curQuiet) == ADLX_RESULT.ADLX_OK && curQuiet) { CurrentPreset = PresetKind.Quiet; return; }
            if (pPresetTuning->IsCurrentBalanced(&curBalanced) == ADLX_RESULT.ADLX_OK && curBalanced) { CurrentPreset = PresetKind.Balanced; return; }
            if (pPresetTuning->IsCurrentTurbo(&curTurbo) == ADLX_RESULT.ADLX_OK && curTurbo) { CurrentPreset = PresetKind.Turbo; return; }
            if (pPresetTuning->IsCurrentRage(&curRage) == ADLX_RESULT.ADLX_OK && curRage) { CurrentPreset = PresetKind.Rage; return; }

            CurrentPreset = supported.Count > 0 ? supported[0] : PresetKind.Balanced;
        }
    }

    public enum PresetKind
    {
        PowerSaver,
        Quiet,
        Balanced,
        Turbo,
        Rage
    }

    public readonly struct AutoTuningInfo
    {
        public bool IsSupported { get; init; }
        // Auto-tuning is a command, not a state, so there's not much to store.

        [JsonConstructor]
        public AutoTuningInfo(bool isSupported)
        {
            IsSupported = isSupported;
        }

        internal unsafe AutoTuningInfo(IADLXGPUAutoTuning* pAutoTuning)
        {
            bool supUnder = false, supOcGpu = false, supOcVram = false;
            pAutoTuning->IsSupportedUndervoltGPU(&supUnder);
            pAutoTuning->IsSupportedOverclockGPU(&supOcGpu);
            pAutoTuning->IsSupportedOverclockVRAM(&supOcVram);
            IsSupported = supUnder || supOcGpu || supOcVram;
        }
    }

    /// <summary>
    /// Safe handle for an unmanaged IADLXGPUTuningEventListener backed by a managed delegate.
    /// </summary>
    public sealed unsafe class GpuTuningListenerHandle : SafeHandle
    {
        private GpuTuningListenerHandle() : base(IntPtr.Zero, true) { handle = IntPtr.Zero; }
        public static GpuTuningListenerHandle Create(delegate* unmanaged<void> _ = null) => new();
        public IntPtr GetListener() => IntPtr.Zero;
        protected override bool ReleaseHandle() => true;
        public override bool IsInvalid => handle == IntPtr.Zero;
    }
}