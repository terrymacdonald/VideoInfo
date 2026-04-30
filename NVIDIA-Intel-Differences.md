# NVIDIA vs Intel Library Architecture Differences

## Purpose
This document compares `NVIDIALibrary` and `IntelLibrary` in terms of orchestration style, object model shape, and settings-application flow. It intentionally focuses on structure and behavior patterns, not vendor-specific API calls.

## High-Level Summary
`IntelLibrary` is already close to `NVIDIALibrary` in lifecycle shape (`CreateDefaultConfig`, `UpdateActiveConfig`, `GetActiveConfig`, `SetActiveConfig`, `SetActiveConfigOverride`) and logging style.

The major remaining gap is model normalization:
- `NVIDIALibrary` persists mostly capability-gated DTO settings.
- `IntelLibrary` still persists a mixed model (DTOs plus many native interop structs) in `INTEL_DISPLAY_WITH_SETTINGS`.

### Progress since original assessment
The `IGCLWrapper` library has been **completely rewritten** with a three-layer DTO-first facade: `IGCLApi` (low-level native interop) → `IGCLApiHelper` / `IGCLAdapterHelper` (mid-level adapter facades) → `IGCLDisplayHelper` and per-feature helpers (display and feature operations). All public wrapper methods now return DTOs; native structs are confined to `private` helper methods and explicitly-named `*Native()` overloads for advanced callers.

`IntelLibrary` has been updated to use this wrapper hierarchy for all IGCL operations. No direct IGCL native calls remain in `IntelLibrary`; all access is through `IGCLApiHelper`, `IGCLAdapterHelper`, and `IGCLDisplayHelper`.

The adapter/display enumeration and stable-key lookup pipeline in `SetActiveConfigOverride` has also been implemented. The remaining gaps — inline per-feature apply blocks, mixed native struct persistence in `INTEL_DISPLAY_WITH_SETTINGS`, and initialization/error policy alignment — are still open.

## Structural Comparison

### 1) Configuration Model Shape
NVIDIA pattern:
- Per-display config is capability-gated (`HasX`) plus typed setting payload (`XData`).
- Adapter-level and display-level data are clearly separated.
- Top-level config includes global profile domains (for example Mosaic and DRS).

Intel pattern:
- Per-display config includes both business settings and many native transport/intermediate structs.
- Adapter/display separation exists, but display struct still carries boundary-level native fields.
- Combined display is handled, but there is no generalized top-level profile bucket like NVIDIA's Mosaic/DRS split.

Impact:
- Intel serialization and equality are heavier and less stable than NVIDIA's DTO-first approach.

### 2) Get/Apply Orchestration
NVIDIA pattern:
- Build helper lookups once.
- For each target display: fetch active, compare target vs active, apply only diffs.
- Uses consistent per-feature "is supported / is different / apply / log" blocks.

Intel pattern:
- Enumeration and helper creation use the `IGCLApiHelper` / `IGCLAdapterHelper` / `IGCLDisplayHelper` wrapper chain; no direct IGCL native calls remain in `IntelLibrary`.
- Adapter/display lookup in `SetActiveConfigOverride` builds a stable display device ID key per display (`VEN_xxxx&DEV_xxxx&REV_xx-PORT_xx`), then uses `TryGetValue` against both stored and current config snapshots.
- Application logic remains fully inline per-feature in `SetActiveConfigOverride`; no extracted helper methods exist yet.
- An inline local function `IsUnsupportedResult()` consistently classifies unsupported IGCL result codes within the apply loop.

Impact:
- Intel behavior is functional and the enumeration/lookup pipeline is clean, but per-feature apply blocks are still inline and not reusable.

### 3) Error and Initialization Policy
NVIDIA pattern:
- In non-initialized paths, often logs and returns false.

Intel pattern:
- In some non-initialized set paths, throws `IntelLibraryException`.

Impact:
- Cross-vendor behavior diverges for callers that rely on consistent failure semantics.

### 4) Equality and Hashing Strategy
NVIDIA pattern:
- Uses DTO/value-object comparisons and capability flags to limit comparisons to meaningful domains.

Intel pattern:
- Equality and hash include many native/native-array fields in `INTEL_DISPLAY_WITH_SETTINGS`.

Impact:
- Higher chance of noisy profile mismatch and brittle persistence behavior in Intel.

## Intel Changes Needed To Operate More Like NVIDIA

### A) Normalize the Intel display model around capability-gated settings
Target pattern per setting:
- `IsSupportedX`
- `XSettingsDto`

Keep native structs only at call boundaries (local variables used to call IGCL), not in persisted display config structs.

### B) Finish DTO-first persistence in `INTEL_DISPLAY_WITH_SETTINGS`
Current progress:
- `DisplayTiming` is now DTO-based (`DisplayTimingDto`).
- The `IGCLWrapper` public API is fully DTO-first; all get/set operations return or accept DTOs.
- Most settings domains in `INTEL_DISPLAY_WITH_SETTINGS` now have DTO-based fields (`ScalingSettingsDto`, `SharpnessSettingsDto`, `RetroScalingSettingsDto`, `DceArgsDto`, `PowerOptimizationSettingsDto`, `LaceConfigDto`, `SwPsrSettingsDto`, `WireFormatConfigDto`, `DisplaySettingsDto`, `DisplayPropertiesDto`, `AdapterDisplayEncoderPropertiesDto`, `IntelArcSyncMonitorParamsDto`, `GenlockArgsDto`).

Next goal:
- Remove the remaining native struct fields that are still persisted in `INTEL_DISPLAY_WITH_SETTINGS`: `ctl_scaling_caps_t ScalingCaps`, `ctl_sharpness_caps_t SharpnessCaps`, `ctl_sharpness_filter_properties_t[] SharpnessFilterProperties`, `ctl_retro_scaling_caps_t RetroScalingCaps`, `ctl_power_optimization_caps_t PowerOptimizationCaps`, `ctl_intel_arc_sync_profile_params_t IntelArcSyncProfile`, `ctl_get_set_custom_mode_args_t CustomModeArgs`, `ctl_custom_src_mode_t[] CustomModes`, `ctl_lda_args_t LinkedDisplayAdaptersArgs`, `IntPtr[] LinkedDisplayAdapters`, `ctl_mux_properties_t MuxProperties`, `IntPtr[] MuxDisplayOutputs`, `ctl_vblank_ts_args_t VblankTimestamp`, `ctl_get_brightness_t Brightness`.
- Move these to transient runtime variables only (not persisted in the display config struct).

### C) Complete the helper-lookup application pipeline
Steps 1–3 are now implemented in `SetActiveConfigOverride`:
1. ✓ Build adapter/display helper map — uses `IGCLApiHelper.EnumerateAdapters()` and `adapter.EnumerateDisplayOutputs()`.
2. ✓ Resolve stored config entry by stable key — constructs a `VEN_xxxx&DEV_xxxx&REV_xx-PORT_xx` key and uses `TryGetValue`.
3. ✓ Resolve current config entry — snapshots `currentDisplayConfig` via `GetIntelDisplayConfig()` at method entry and resolves per display via `TryGetValue`.
4. ✗ Apply per-feature through a common extracted helper pattern — all feature apply blocks are still inline; extracting these into private helper methods remains to be done.

This mirrors NVIDIA's maintainable "lookup then apply" orchestration.

### D) Standardize unsupported/unsupported-version handling
Centralize result handling for:
- unsupported feature
- unsupported version
- invalid operation
- invalid arguments

Use a single helper for consistent skip/log behavior across all feature blocks.

### E) Unify non-initialized failure semantics across vendors
Pick one policy and apply consistently:
- either return false and log
- or throw for all set/apply entry points

Recommended for alignment with NVIDIA style: return false + log in operational methods.

### F) Reduce equality/hash sensitivity to transport-level fields
For profile matching behavior, prefer comparing persisted capability-gated settings rather than internal/native transport details.

## Suggested Refactor Sequence (PR-Sized)

1. Model cleanup pass
- Keep only persisted settings domains in `INTEL_DISPLAY_WITH_SETTINGS`.
- Move native-only transport fields behind runtime helper methods or transient locals.

2. Apply-flow helper extraction pass
- Extract repeated compare/apply blocks into private methods with a consistent signature.

3. Unsupported-result policy pass
- An inline local function `IsUnsupportedResult()` already handles detection and skip logging within `SetActiveConfigOverride`. Promote it to a class-level private helper so it can be reused by `SetActiveConfig` and any future apply methods.

4. Initialization/failure policy pass
- Normalize return/throw behavior in `SetActiveConfig` and `SetActiveConfigOverride`.

5. Equality/hash stabilization pass
- Align comparisons with persisted settings semantics.

6. Final consistency pass
- Ensure logging conventions, naming, and per-setting gating follow one pattern end to end.

## Notes
- This comparison deliberately excludes vendor API parity concerns.
- The goal is operational similarity in structure, call flow, object modeling, and maintainability.

## Tracked Implementation Checklist

Status legend:
- [x] completed
- [ ] not started

### Phase 0: Completed Baseline
- [x] Fix CombinedDisplay child list usage to match DTO list shape.
- [x] Fix OsDisplayEncoderIdentifierDto property naming usage (WindowsDisplayEncoderId).
- [x] Convert DisplayTiming persistence to DisplayTimingDto.
- [x] Rewrite IGCLWrapper with DTO-first public API (IGCLApi / IGCLApiHelper / IGCLAdapterHelper / IGCLDisplayHelper three-layer hierarchy).
- [x] Update IntelLibrary to use IGCLWrapper helper hierarchy for all IGCL operations; no direct native IGCL calls remain.

### Phase 1: Model Normalization
- [ ] Define a persisted-settings-only target shape for INTEL_DISPLAY_WITH_SETTINGS.
- [ ] Mark native-only fields as runtime/transient concerns (no profile persistence intent).
- [ ] Keep capability gates consistent per setting domain (IsSupportedX + X settings).
- [ ] Ensure constructor defaults align to the normalized model.

### Phase 2: Apply Pipeline Normalization
- [x] Build a reusable display-helper lookup step in SetActiveConfigOverride.
- [x] Standardize stored/current lookup and skip rules per display.
- [ ] Extract per-feature apply blocks into private helper methods.
- [ ] Keep one compare/apply flow shape for all settings domains.

### Phase 3: Unsupported Result Handling
- [ ] Centralize unsupported-result classification helper (currently an inline local function in `SetActiveConfigOverride`; needs promotion to a class-level private method for broader reuse).
- [x] Use shared unsupported handling in every feature apply block.
- [x] Keep unsupported outcomes as skip + trace unless severity requires warning/error.

### Phase 4: Initialization and Error Policy Consistency
- [ ] Decide cross-vendor policy for non-initialized set paths.
- [ ] Apply the same policy across SetActiveConfig and SetActiveConfigOverride.
- [ ] Align logging level and message format with chosen policy.

### Phase 5: Equality and Hash Stabilization
- [ ] Restrict equality/hash to persisted, meaningful settings domains.
- [ ] Avoid transport-only/native state creating false mismatch noise.
- [ ] Verify profile matching behavior remains deterministic after reboot/apply cycles.

### Phase 6: Consistency and Cleanup
- [ ] Ensure naming and per-feature gating are uniform across Intel paths.
- [ ] Remove duplicated compare/apply patterns no longer needed after helper extraction.
- [ ] Final pass on trace/warn/error consistency.

## Proposed Branch and Commit Breakdown

Suggested branch name:
- develop/intel-alignment-with-nvidia-flow

Suggested commit plan:
1. Commit: Intel DTO baseline alignment ✓ COMPLETE
- IGCLWrapper rewritten with DTO-first public API (three-layer hierarchy).
- IntelLibrary updated to use IGCLWrapper helper hierarchy; no direct native calls remain.
- Baseline fixes (list shape, encoder id property, DisplayTiming DTO) also complete.
- Adapter/display enumeration and stable-key lookup pipeline in SetActiveConfigOverride complete.

2. Commit: Intel model normalization scaffold
- Remove remaining native struct fields from INTEL_DISPLAY_WITH_SETTINGS.
- Move caps and transport-only structs (ScalingCaps, SharpnessCaps, SharpnessFilterProperties, RetroScalingCaps, PowerOptimizationCaps, IntelArcSyncProfile, CustomModeArgs, CustomModes, LinkedDisplayAdaptersArgs, LinkedDisplayAdapters, MuxProperties, MuxDisplayOutputs, VblankTimestamp, Brightness) to transient runtime variables only.

3. Commit: Intel apply pipeline helpers
- Helper lookup and stored/current key resolution are complete.
- Remaining: extract inline per-feature apply blocks into private helper methods with a consistent signature.

4. Commit: Intel unsupported result normalization
- Unsupported-result handling is already consistent across all feature blocks via inline local function.
- Remaining: promote inline `IsUnsupportedResult()` to a class-level private helper for broader reuse.

5. Commit: Intel initialization/error policy alignment
- Unify non-initialized behavior and logging strategy in apply entry points.

6. Commit: Intel equality and hash stabilization
- Refocus equality/hash to persisted settings semantics.

7. Commit: Intel final consistency cleanup
- Naming, logging consistency, and duplicate code cleanup pass.

## Suggested PR Slicing

1. PR 1: Model + constructor normalization only.
2. PR 2: Apply helper extraction only.
3. PR 3: Error-policy and unsupported handling alignment.
4. PR 4: Equality/hash stabilization and cleanup.

This sequence keeps risk lower by separating data-model changes from application-flow refactors.
