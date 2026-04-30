# Plan: Update IntelLibrary to Use IGCLWrapper DTOs

## Goal
Update `VideoInfo/Intel/IntelLibrary.cs` so that `INTEL_DISPLAY_WITH_SETTINGS` and all code that reads/writes it uses the new DTO types exposed by the rewritten IGCLWrapper, instead of the raw native IGCL structs. The existing functionality (same features, same settings applied) is preserved; only the types change.

## Root cause of the problem
The IGCLWrapper was rewritten so that all public methods on `IGCLDisplayHelper` and `IGCLAdapterHelper` return DTOs (e.g. `ScalingCapsDto`, `BrightnessGetDto`, `SharpnessCapsDto`, etc.) instead of the raw `ctl_xxx_t` native structs. `IntelLibrary.cs` has not been updated to match, so the file currently contains type mismatches that prevent compilation. These are:

- `GetSupportedRetroScalingCapability()` returns `RetroScalingCapsDto` but is stored into `ctl_retro_scaling_caps_t RetroScalingCaps`
- `GetSupportedScalingCapability()` returns `ScalingCapsDto` but is stored into `ctl_scaling_caps_t ScalingCaps`
- `GetSharpnessCaps()` returns `SharpnessCapsDto` (single value) but is destructured into `(ctl_sharpness_caps_t, ctl_sharpness_filter_properties_t[])` tuple
- `GetPowerOptimizationCaps()` returns `PowerOptimizationCapsDto` but is stored into `ctl_power_optimization_caps_t PowerOptimizationCaps`
- `GetBrightnessSetting()` returns `BrightnessGetDto` but is stored into `ctl_get_brightness_t Brightness`
- `GetIntelArcSyncProfile()` returns `IntelArcSyncProfileParamsDto` but is stored into `ctl_intel_arc_sync_profile_params_t IntelArcSyncProfile`
- `GetCustomModes()` returns `CustomModesResultDto` (single value) but is destructured into `(ctl_get_set_custom_mode_args_t, ctl_custom_src_mode_t[])` tuple
- `GetVblankTimestamp()` returns `VblankTimestampArgsDto` but is stored into `ctl_vblank_ts_args_t VblankTimestamp`
- `GetMuxProperties(handle)` returns `MuxPropertiesDto` (single value) but is destructured into `(ctl_mux_properties_t, IntPtr[])` tuple
- `SetBrightnessSetting` now takes `BrightnessSetDto` but is called with `ctl_set_brightness_t`
- `SetIntelArcSyncProfile` now takes `IntelArcSyncProfileParamsDto` but is called with `ctl_intel_arc_sync_profile_params_t`
- `SetCustomModes` DTO overload takes `(CustomModeArgsDto, IReadOnlyList<CustomSourceModeDto>)` but is called with native types

---

## File to change
`c:\vs-code\VideoInfo\VideoInfo\Intel\IntelLibrary.cs`

---

## Step-by-Step Changes

### STEP 1 — Remove legacy primitive mirror fields from `INTEL_DISPLAY_WITH_SETTINGS`

These 7 fields are redundant copies of values already held in the DTO fields `RetroScalingSettings`, `ScalingSettings`, and `SharpnessSettings`. They were populated in `GetIntelDisplayConfig` by copying values out of the DTOs, and read back in `SetActiveConfigOverride`. They must be removed so the code uses the DTOs directly.

Remove these field declarations:
```
bool IsEnabledIntegerScaling
ctl_retro_scaling_type_flag_t IntegerScalingType
bool IsEnabledGPUScaling
ctl_scaling_type_flag_t ScalingType
bool IsEnabledImageSharpening
ctl_sharpness_filter_type_flag_t SharpeningFilterType
float SharpeningIntensity
```

Also remove their initializations in the constructor:
```
IsEnabledIntegerScaling = false;
IntegerScalingType = ctl_retro_scaling_type_flag_t.CTL_RETRO_SCALING_TYPE_FLAG_INTEGER;
IsEnabledGPUScaling = false;
ScalingType = ctl_scaling_type_flag_t.CTL_SCALING_TYPE_FLAG_IDENTITY;
IsEnabledImageSharpening = false;
SharpeningFilterType = ctl_sharpness_filter_type_flag_t.CTL_SHARPNESS_FILTER_TYPE_FLAG_NON_ADAPTIVE;
SharpeningIntensity = 0.0f;
```

---

### STEP 2 — Replace native struct fields with DTOs in `INTEL_DISPLAY_WITH_SETTINGS`

Each sub-step describes: the field(s) removed, the field(s) added, and the DTO type.

#### 2a) RetroScalingCaps
- Remove: `ctl_retro_scaling_caps_t RetroScalingCaps`
- Add: `RetroScalingCapsDto RetroScalingCaps`
- Constructor: change `RetroScalingCaps = new ctl_retro_scaling_caps_t();` to `RetroScalingCaps = new RetroScalingCapsDto();`
- Note: `RetroScalingCapsDto` has fields `Size`, `Version`, `SupportedRetroScaling` (same as the native struct). The `IsSupportedIntegerScaling` gate computes from `RetroScalingCaps.SupportedRetroScaling` — this field name is the same in the DTO.

#### 2b) ScalingCaps
- Remove: `ctl_scaling_caps_t ScalingCaps`
- Add: `ScalingCapsDto ScalingCaps`
- Constructor: change `ScalingCaps = new ctl_scaling_caps_t();` to `ScalingCaps = new ScalingCapsDto();`
- Note: `ScalingCapsDto` has field `SupportedScaling` — same name as the native struct. The `IsSupportedGPUScaling` gate uses this.

#### 2c) SharpnessCaps + SharpnessFilterProperties
- Remove: `ctl_sharpness_caps_t SharpnessCaps`
- Remove: `ctl_sharpness_filter_properties_t[] SharpnessFilterProperties`
- Add: `SharpnessCapsDto SharpnessCaps`
- Constructor: change `SharpnessCaps = new ctl_sharpness_caps_t();` and `SharpnessFilterProperties = Array.Empty<ctl_sharpness_filter_properties_t>();` to `SharpnessCaps = new SharpnessCapsDto();`
- Note: `SharpnessCapsDto` includes `FilterProperties` as `List<SharpnessFilterPropertiesDto>?` so the two old fields collapse into one. `IsSupportedImageSharpening` uses `SharpnessCaps.SupportedFilterFlags` — same field name in the DTO.

#### 2d) PowerOptimizationCaps
- Remove: `ctl_power_optimization_caps_t PowerOptimizationCaps`
- Add: `PowerOptimizationCapsDto PowerOptimizationCaps`
- Constructor: change `PowerOptimizationCaps = new ctl_power_optimization_caps_t();` to `PowerOptimizationCaps = new PowerOptimizationCapsDto();`
- Note: `IsSupportedPowerOptimization` uses `PowerOptimizationCaps.SupportedFeatures` — same field name.

#### 2e) Brightness
- Remove: `ctl_get_brightness_t Brightness`
- Add: `BrightnessGetDto Brightness`
- Constructor: change `Brightness = new ctl_get_brightness_t();` to `Brightness = new BrightnessGetDto();`
- Note: `BrightnessGetDto` has `TargetBrightness` and `CurrentBrightness` — same field names as native struct.

#### 2f) IntelArcSyncProfile
- Remove: `ctl_intel_arc_sync_profile_params_t IntelArcSyncProfile`
- Add: `IntelArcSyncProfileParamsDto IntelArcSyncProfile`
- Constructor: change `IntelArcSyncProfile = new ctl_intel_arc_sync_profile_params_t();` to `IntelArcSyncProfile = new IntelArcSyncProfileParamsDto();`
- Note: `IntelArcSyncProfileParamsDto` has `IntelArcSyncProfile`, `MaxRefreshRateInHz`, `MinRefreshRateInHz`, `MaxFrameTimeIncreaseInUs`, `MaxFrameTimeDecreaseInUs` — same names.

#### 2g) CustomModeArgs + CustomModes
- Remove: `ctl_get_set_custom_mode_args_t CustomModeArgs`
- Remove: `ctl_custom_src_mode_t[] CustomModes`
- Add: `CustomModeArgsDto CustomModeArgs`
- Add: `List<CustomSourceModeDto> CustomModes`
- Constructor: change `CustomModeArgs = new ctl_get_set_custom_mode_args_t();` and `CustomModes = Array.Empty<ctl_custom_src_mode_t>();` to:
  ```csharp
  CustomModeArgs = new CustomModeArgsDto();
  CustomModes = new List<CustomSourceModeDto>();
  ```

#### 2h) LinkedDisplayAdaptersArgs + LinkedDisplayAdapters
- Remove: `ctl_lda_args_t LinkedDisplayAdaptersArgs`
- Remove: `IntPtr[] LinkedDisplayAdapters`
- Add: `LinkedDisplayAdaptersResultDto LinkedDisplayAdapters`
- Constructor: change `LinkedDisplayAdaptersArgs = new ctl_lda_args_t();` and `LinkedDisplayAdapters = Array.Empty<IntPtr>();` to `LinkedDisplayAdapters = new LinkedDisplayAdaptersResultDto();`
- Note: These fields are declared in INTEL_DISPLAY_WITH_SETTINGS but are not currently populated in GetIntelDisplayConfig. The type replacement still needs to happen for Equals/GetHashCode/constructor consistency.

#### 2i) MuxProperties + MuxDisplayOutputs
- Remove: `ctl_mux_properties_t MuxProperties`
- Remove: `IntPtr[] MuxDisplayOutputs`
- Add: `MuxPropertiesDto MuxProperties`
- Constructor: change `MuxProperties = new ctl_mux_properties_t();` and `MuxDisplayOutputs = Array.Empty<IntPtr>();` to `MuxProperties = new MuxPropertiesDto();`
- Note: `MuxPropertiesDto` includes `DisplayOutputs` as `List<nint>?` so the two old fields collapse into one.

#### 2j) VblankTimestamp
- Remove: `ctl_vblank_ts_args_t VblankTimestamp`
- Add: `VblankTimestampArgsDto VblankTimestamp`
- Constructor: change `VblankTimestamp = new ctl_vblank_ts_args_t();` to `VblankTimestamp = new VblankTimestampArgsDto();`

---

### STEP 3 — Update `GetIntelDisplayConfig` to fix type mismatches and remove legacy field population

After STEP 1 and STEP 2, `GetIntelDisplayConfig` will have compilation errors. Fix each section:

#### 3a) RetroScaling section
```csharp
// BEFORE:
newDisplay.RetroScalingCaps = display.GetSupportedRetroScalingCapability();  // type mismatch
// ...
newDisplay.IsSupportedIntegerScaling = (newDisplay.RetroScalingCaps.SupportedRetroScaling & retroScalingMask) != 0;
newDisplay.IsEnabledIntegerScaling = newDisplay.RetroScalingSettings.Enable;       // REMOVE
newDisplay.IntegerScalingType = (ctl_retro_scaling_type_flag_t)newDisplay.RetroScalingSettings.RetroScalingType;  // REMOVE

// AFTER:
newDisplay.RetroScalingCaps = display.GetSupportedRetroScalingCapability();  // now RetroScalingCapsDto — types match
// ...
newDisplay.IsSupportedIntegerScaling = (newDisplay.RetroScalingCaps.SupportedRetroScaling & retroScalingMask) != 0;
// (remove the two lines that populated the deleted legacy fields)
```

#### 3b) GPU Scaling section
```csharp
// BEFORE:
newDisplay.ScalingCaps = display.GetSupportedScalingCapability();  // type mismatch
// ...
newDisplay.IsSupportedGPUScaling = (newDisplay.ScalingCaps.SupportedScaling & gpuScalingMask) != 0;
newDisplay.IsEnabledGPUScaling = newDisplay.ScalingSettings.Enable;      // REMOVE
newDisplay.ScalingType = (ctl_scaling_type_flag_t)newDisplay.ScalingSettings.ScalingType;  // REMOVE

// AFTER:
newDisplay.ScalingCaps = display.GetSupportedScalingCapability();  // now ScalingCapsDto — types match
// ...
newDisplay.IsSupportedGPUScaling = (newDisplay.ScalingCaps.SupportedScaling & gpuScalingMask) != 0;
// (remove the two lines that populated the deleted legacy fields)
```

#### 3c) Image Sharpening section
```csharp
// BEFORE (broken destructure):
(newDisplay.SharpnessCaps, newDisplay.SharpnessFilterProperties) = display.GetSharpnessCaps();

// AFTER:
newDisplay.SharpnessCaps = display.GetSharpnessCaps();  // returns SharpnessCapsDto; includes FilterProperties inside

// Also remove these lines:
newDisplay.IsEnabledImageSharpening = newDisplay.SharpnessSettings.Enable;           // REMOVE
newDisplay.SharpeningFilterType = (ctl_sharpness_filter_type_flag_t)newDisplay.SharpnessSettings.FilterType;  // REMOVE
newDisplay.SharpeningIntensity = newDisplay.SharpnessSettings.Intensity;             // REMOVE
```

#### 3d) Power Optimization section
```csharp
// BEFORE:
newDisplay.PowerOptimizationCaps = display.GetPowerOptimizationCaps();  // type mismatch

// AFTER:
newDisplay.PowerOptimizationCaps = display.GetPowerOptimizationCaps();  // now PowerOptimizationCapsDto — types match
// (IsSupportedPowerOptimization uses .SupportedFeatures — same field name in the DTO; no other changes)
```

#### 3e) Brightness section
```csharp
// BEFORE:
newDisplay.Brightness = display.GetBrightnessSetting();  // type mismatch (returns BrightnessGetDto)

// AFTER:
newDisplay.Brightness = display.GetBrightnessSetting();  // now BrightnessGetDto — types match
```

#### 3f) Intel Arc Sync section
```csharp
// BEFORE:
newDisplay.IntelArcSyncProfile = display.GetIntelArcSyncProfile();  // type mismatch (returns IntelArcSyncProfileParamsDto)

// AFTER:
newDisplay.IntelArcSyncProfile = display.GetIntelArcSyncProfile();  // now IntelArcSyncProfileParamsDto — types match
```

#### 3g) Custom Modes section
```csharp
// BEFORE (broken destructure):
(newDisplay.CustomModeArgs, newDisplay.CustomModes) = display.GetCustomModes();

// AFTER:
var customModesResult = display.GetCustomModes();  // returns CustomModesResultDto
newDisplay.CustomModeArgs = customModesResult.Args;
newDisplay.CustomModes = customModesResult.Modes ?? new List<CustomSourceModeDto>();
```

#### 3h) VblankTimestamp section
```csharp
// BEFORE:
newDisplay.VblankTimestamp = display.GetVblankTimestamp();  // type mismatch (returns VblankTimestampArgsDto)

// AFTER:
newDisplay.VblankTimestamp = display.GetVblankTimestamp();  // now VblankTimestampArgsDto — types match
```

#### 3i) Mux Properties section
```csharp
// BEFORE (broken destructure):
(newDisplay.MuxProperties, newDisplay.MuxDisplayOutputs) = display.GetMuxProperties(muxHandles[0]);

// AFTER:
newDisplay.MuxProperties = display.GetMuxProperties(muxHandles[0]);  // returns MuxPropertiesDto; includes DisplayOutputs inside
```

---

### STEP 4 — Update `SetActiveConfigOverride` to use DTOs instead of removed legacy primitives

#### 4a) Integer Scaling block — read from RetroScalingSettings DTO instead of legacy primitives
```csharp
// BEFORE:
var retroScalingSettings = currentSettings.RetroScalingSettings;
if (retroScalingSettings.Enable != storedSettings.IsEnabledIntegerScaling ||
    (uint)retroScalingSettings.RetroScalingType != (uint)storedSettings.IntegerScalingType)
{
    retroScalingSettings.Get = false;
    retroScalingSettings.Enable = storedSettings.IsEnabledIntegerScaling;
    retroScalingSettings.RetroScalingType = (uint)storedSettings.IntegerScalingType;
    display.SetRetroScalingSettings(retroScalingSettings);

// AFTER:
var retroScalingSettings = currentSettings.RetroScalingSettings;
if (retroScalingSettings.Enable != storedSettings.RetroScalingSettings.Enable ||
    retroScalingSettings.RetroScalingType != storedSettings.RetroScalingSettings.RetroScalingType)
{
    retroScalingSettings.Get = false;
    retroScalingSettings.Enable = storedSettings.RetroScalingSettings.Enable;
    retroScalingSettings.RetroScalingType = storedSettings.RetroScalingSettings.RetroScalingType;
    display.SetRetroScalingSettings(retroScalingSettings);
```

#### 4b) GPU Scaling block — read from ScalingSettings DTO instead of legacy primitives
```csharp
// BEFORE:
var scalingSettings = currentSettings.ScalingSettings;
if (scalingSettings.Enable != storedSettings.IsEnabledGPUScaling ||
    (uint)scalingSettings.ScalingType != (uint)storedSettings.ScalingType)
{
    scalingSettings.Enable = storedSettings.IsEnabledGPUScaling;
    scalingSettings.ScalingType = (uint)storedSettings.ScalingType;
    display.SetCurrentScaling(scalingSettings);

// AFTER:
var scalingSettings = currentSettings.ScalingSettings;
if (scalingSettings.Enable != storedSettings.ScalingSettings.Enable ||
    scalingSettings.ScalingType != storedSettings.ScalingSettings.ScalingType)
{
    scalingSettings.Enable = storedSettings.ScalingSettings.Enable;
    scalingSettings.ScalingType = storedSettings.ScalingSettings.ScalingType;
    display.SetCurrentScaling(scalingSettings);
```

#### 4c) Image Sharpening block — read from SharpnessSettings DTO instead of legacy primitives
```csharp
// BEFORE:
var sharpnessSettings = currentSettings.SharpnessSettings;
if (sharpnessSettings.Enable != storedSettings.IsEnabledImageSharpening ||
    (uint)sharpnessSettings.FilterType != (uint)storedSettings.SharpeningFilterType ||
    Math.Abs(sharpnessSettings.Intensity - storedSettings.SharpeningIntensity) > 0.001f)
{
    sharpnessSettings.Enable = storedSettings.IsEnabledImageSharpening;
    sharpnessSettings.FilterType = (uint)storedSettings.SharpeningFilterType;
    sharpnessSettings.Intensity = storedSettings.SharpeningIntensity;
    display.SetCurrentSharpness(sharpnessSettings);

// AFTER:
var sharpnessSettings = currentSettings.SharpnessSettings;
if (sharpnessSettings.Enable != storedSettings.SharpnessSettings.Enable ||
    sharpnessSettings.FilterType != storedSettings.SharpnessSettings.FilterType ||
    Math.Abs(sharpnessSettings.Intensity - storedSettings.SharpnessSettings.Intensity) > 0.001f)
{
    sharpnessSettings.Enable = storedSettings.SharpnessSettings.Enable;
    sharpnessSettings.FilterType = storedSettings.SharpnessSettings.FilterType;
    sharpnessSettings.Intensity = storedSettings.SharpnessSettings.Intensity;
    display.SetCurrentSharpness(sharpnessSettings);
```

#### 4d) Brightness block — use BrightnessSetDto constructor instead of CreateSetBrightness()
```csharp
// BEFORE:
var currentBrightness = currentSettings.Brightness;   // ctl_get_brightness_t
if (currentBrightness.TargetBrightness != storedSettings.Brightness.TargetBrightness)
{
    var brightnessToSet = IGCLDisplayHelper.CreateSetBrightness();  // ctl_set_brightness_t
    brightnessToSet.TargetBrightness = storedSettings.Brightness.TargetBrightness;
    brightnessToSet.SmoothTransitionTimeInMs = 0;
    display.SetBrightnessSetting(brightnessToSet);

// AFTER:
var currentBrightness = currentSettings.Brightness;   // BrightnessGetDto
if (currentBrightness.TargetBrightness != storedSettings.Brightness.TargetBrightness)
{
    var brightnessToSet = new BrightnessSetDto
    {
        TargetBrightness = storedSettings.Brightness.TargetBrightness,
        SmoothTransitionTimeInMs = 0
    };
    display.SetBrightnessSetting(brightnessToSet);
```

#### 4e) Intel Arc Sync block — types now match, no logic change needed
The comparison `!currentArcSyncProfile.Equals(storedSettings.IntelArcSyncProfile)` and the set call `display.SetIntelArcSyncProfile(storedSettings.IntelArcSyncProfile)` both work once the field type is `IntelArcSyncProfileParamsDto`.  No logic change required — only the field type changes from the struct update in Step 2f.

#### 4f) Custom Modes block — use List<CustomSourceModeDto> and DTO overload of SetCustomModes
```csharp
// BEFORE:
if (storedSettings.CustomModes != null && storedSettings.CustomModes.Length > 0)
{
    var currentCustomModes = currentSettings.CustomModes;
    bool customModesDifferent = currentCustomModes == null ||
        currentCustomModes.Length != storedSettings.CustomModes.Length ||
        !currentCustomModes.SequenceEqual(storedSettings.CustomModes);
    if (customModesDifferent)
    {
        var desiredCustomModeArgs = storedSettings.CustomModeArgs;
        desiredCustomModeArgs.CustomModeOpType = ctl_custom_mode_operation_types_t.CTL_CUSTOM_MODE_OPERATION_TYPES_ADD_CUSTOM_SOURCE_MODE;
        desiredCustomModeArgs.NumOfModes = (uint)storedSettings.CustomModes.Length;
        display.SetCustomModes(desiredCustomModeArgs, storedSettings.CustomModes);

// AFTER:
var storedModes = storedSettings.CustomModes;   // List<CustomSourceModeDto>
if (storedModes != null && storedModes.Count > 0)
{
    var currentModes = currentSettings.CustomModes;   // List<CustomSourceModeDto>
    bool customModesDifferent = currentModes == null ||
        currentModes.Count != storedModes.Count ||
        !currentModes.SequenceEqual(storedModes);
    if (customModesDifferent)
    {
        var desiredCustomModeArgs = storedSettings.CustomModeArgs;
        desiredCustomModeArgs.CustomModeOpType = ctl_custom_mode_operation_types_t.CTL_CUSTOM_MODE_OPERATION_TYPES_ADD_CUSTOM_SOURCE_MODE;
        desiredCustomModeArgs.NumOfModes = (uint)storedModes.Count;
        display.SetCustomModes(desiredCustomModeArgs, (IReadOnlyList<CustomSourceModeDto>)storedModes);
```
Note: `IGCLDisplayHelper.SetCustomModes` DTO overload signature is `void SetCustomModes(CustomModeArgsDto, IReadOnlyList<CustomSourceModeDto>)`.

---

### STEP 5 — Update `Equals()` in `INTEL_DISPLAY_WITH_SETTINGS`

#### 5a) Remove comparisons for the 7 deleted legacy primitive fields
Remove the following blocks entirely (each is a simple `!=` check followed by a Trace log and `return false`):
- `IsEnabledIntegerScaling != other.IsEnabledIntegerScaling`
- `IntegerScalingType != other.IntegerScalingType`
- `IsEnabledGPUScaling != other.IsEnabledGPUScaling`
- `ScalingType != other.ScalingType`
- `IsEnabledImageSharpening != other.IsEnabledImageSharpening`
- `SharpeningFilterType != other.SharpeningFilterType`
- `Math.Abs(SharpeningIntensity - other.SharpeningIntensity) > 0.001f`

#### 5b) Replace native struct comparison helpers with DTO equality

Replace each native-struct helper call with the appropriate DTO comparison:

| Old (native struct helper) | New (DTO equality) |
|---|---|
| `IGCLDisplayHelper.AreGetBrightnessEqual(Brightness, other.Brightness)` | `EqualityComparer<BrightnessGetDto>.Default.Equals(Brightness, other.Brightness)` |
| `IGCLDisplayHelper.AreScalingCapsEqual(ScalingCaps, other.ScalingCaps)` | `EqualityComparer<ScalingCapsDto>.Default.Equals(ScalingCaps, other.ScalingCaps)` |
| `IGCLDisplayHelper.AreSharpnessCapsEqual(SharpnessCaps, other.SharpnessCaps)` | `EqualityComparer<SharpnessCapsDto>.Default.Equals(SharpnessCaps, other.SharpnessCaps)` |
| `SharpnessFilterProperties.SequenceEqual(other.SharpnessFilterProperties)` | *(removed — now inside SharpnessCapsDto comparison above)* |
| `IGCLDisplayHelper.AreRetroScalingCapsEqual(RetroScalingCaps, other.RetroScalingCaps)` | `EqualityComparer<RetroScalingCapsDto>.Default.Equals(RetroScalingCaps, other.RetroScalingCaps)` |
| `IGCLDisplayHelper.ArePowerOptimizationCapsEqual(PowerOptimizationCaps, other.PowerOptimizationCaps)` | `EqualityComparer<PowerOptimizationCapsDto>.Default.Equals(PowerOptimizationCaps, other.PowerOptimizationCaps)` |
| `IGCLDisplayHelper.AreIntelArcSyncProfileParamsEqual(IntelArcSyncProfile, other.IntelArcSyncProfile)` | `EqualityComparer<IntelArcSyncProfileParamsDto>.Default.Equals(IntelArcSyncProfile, other.IntelArcSyncProfile)` |
| `IGCLDisplayHelper.AreCustomModeArgsEqual(CustomModeArgs, other.CustomModeArgs)` | `EqualityComparer<CustomModeArgsDto>.Default.Equals(CustomModeArgs, other.CustomModeArgs)` |
| `CustomModes.SequenceEqual(other.CustomModes)` on `ctl_custom_src_mode_t[]` | `(CustomModes ?? new List<CustomSourceModeDto>()).SequenceEqual(other.CustomModes ?? new List<CustomSourceModeDto>())` on `List<CustomSourceModeDto>` |
| `IGCLAdapterHelper.AreLinkedDisplayAdaptersArgsEqual(LinkedDisplayAdaptersArgs, other.LinkedDisplayAdaptersArgs)` | `EqualityComparer<LinkedDisplayAdaptersResultDto>.Default.Equals(LinkedDisplayAdapters, other.LinkedDisplayAdapters)` |
| `LinkedDisplayAdapters.SequenceEqual(other.LinkedDisplayAdapters)` on `IntPtr[]` | *(removed — now inside LinkedDisplayAdaptersResultDto comparison above)* |
| `IGCLDisplayHelper.AreMuxPropertiesEqual(MuxProperties, other.MuxProperties)` | `EqualityComparer<MuxPropertiesDto>.Default.Equals(MuxProperties, other.MuxProperties)` |
| `MuxDisplayOutputs.SequenceEqual(other.MuxDisplayOutputs)` on `IntPtr[]` | *(removed — now inside MuxPropertiesDto comparison above)* |
| `IGCLDisplayHelper.AreVblankTimestampArgsEqual(VblankTimestamp, other.VblankTimestamp)` | `EqualityComparer<VblankTimestampArgsDto>.Default.Equals(VblankTimestamp, other.VblankTimestamp)` |

Note: The wrapper DTOs are plain `struct` types, so `EqualityComparer<T>.Default` will use their auto-generated value comparison. If any DTO does not implement `IEquatable<T>` or `Equals`, this will fall back to identity comparison, which would be incorrect. If that turns out to be the case, we may need to compare field-by-field; but this should be verified at compile/test time — do not pre-emptively add field-by-field comparisons without checking.

---

### STEP 6 — Update `GetHashCode()` in `INTEL_DISPLAY_WITH_SETTINGS`

The current `GetHashCode()` is a tuple hash. After Steps 1 and 2:

1. Remove the 7 deleted legacy primitive fields from the tuple.
2. Remove `SharpnessFilterProperties` (now inside `SharpnessCaps`), `MuxDisplayOutputs` (now inside `MuxProperties`), `LinkedDisplayAdaptersArgs` and `LinkedDisplayAdapters` (now `LinkedDisplayAdapters` single DTO), from the tuple.
3. Replace each removed native struct entry with its new DTO field.
4. The replacement DTO fields to add are: `RetroScalingCaps`, `ScalingCaps`, `SharpnessCaps`, `PowerOptimizationCaps`, `Brightness`, `IntelArcSyncProfile`, `CustomModeArgs`, `CustomModes` (as Count or hash of List), `LinkedDisplayAdapters`, `MuxProperties`, `VblankTimestamp`.

Note: `GetHashCode()` on a `List<T>` gives the identity hash, not a content hash. For consistency and to avoid noise, include only `CustomModes?.Count` and `LinkedDisplayAdapters.LinkedAdapters?.Count` etc. rather than the full list in the hash. This matches the existing pattern (arrays often contributed only their length to the hash in the old code).

---

## Execution Order

The steps above must be done in the order given because each step removes a compilation dependency on the previous one:

1. Step 1 (remove legacy primitive fields) — removes redundant field declarations
2. Step 2 (replace native struct fields with DTOs) — fixes the field types
3. Step 3 (update GetIntelDisplayConfig) — fixes type-mismatch assignments
4. Step 4 (update SetActiveConfigOverride) — fixes reads from deleted fields and type-mismatch calls
5. Step 5 (update Equals) — fixes comparisons for deleted/replaced fields
6. Step 6 (update GetHashCode) — fixes hash for deleted/replaced fields

After all 6 steps the file should compile. Do a build to verify, fix any remaining compile errors (may be minor field-name differences), then run the tests.

---

## Checklist (update as work progresses)

- [ ] Step 1: Remove 7 legacy primitive fields and constructor initializations
- [ ] Step 2a: RetroScalingCaps field → RetroScalingCapsDto
- [ ] Step 2b: ScalingCaps field → ScalingCapsDto
- [ ] Step 2c: SharpnessCaps + SharpnessFilterProperties fields → SharpnessCapsDto
- [ ] Step 2d: PowerOptimizationCaps field → PowerOptimizationCapsDto
- [ ] Step 2e: Brightness field → BrightnessGetDto
- [ ] Step 2f: IntelArcSyncProfile field → IntelArcSyncProfileParamsDto
- [ ] Step 2g: CustomModeArgs + CustomModes fields → CustomModeArgsDto + List<CustomSourceModeDto>
- [ ] Step 2h: LinkedDisplayAdaptersArgs + LinkedDisplayAdapters fields → LinkedDisplayAdaptersResultDto
- [ ] Step 2i: MuxProperties + MuxDisplayOutputs fields → MuxPropertiesDto
- [ ] Step 2j: VblankTimestamp field → VblankTimestampArgsDto
- [ ] Step 3a: Fix RetroScaling section in GetIntelDisplayConfig
- [ ] Step 3b: Fix GPU Scaling section in GetIntelDisplayConfig
- [ ] Step 3c: Fix Image Sharpening section in GetIntelDisplayConfig
- [ ] Step 3d: Fix Power Optimization section in GetIntelDisplayConfig
- [ ] Step 3e: Fix Brightness section in GetIntelDisplayConfig
- [ ] Step 3f: Fix Intel Arc Sync section in GetIntelDisplayConfig
- [ ] Step 3g: Fix Custom Modes section in GetIntelDisplayConfig
- [ ] Step 3h: Fix VblankTimestamp section in GetIntelDisplayConfig
- [ ] Step 3i: Fix Mux Properties section in GetIntelDisplayConfig
- [ ] Step 4a: Fix Integer Scaling block in SetActiveConfigOverride
- [ ] Step 4b: Fix GPU Scaling block in SetActiveConfigOverride
- [ ] Step 4c: Fix Image Sharpening block in SetActiveConfigOverride
- [ ] Step 4d: Fix Brightness block in SetActiveConfigOverride
- [ ] Step 4e: Verify Intel Arc Sync block types match (no logic change)
- [ ] Step 4f: Fix Custom Modes block in SetActiveConfigOverride
- [ ] Step 5a: Remove 7 deleted legacy field comparisons from Equals()
- [ ] Step 5b: Replace native struct helper calls with DTO equality in Equals()
- [ ] Step 6: Update GetHashCode() for deleted/replaced fields
- [ ] Build and verify no compile errors
- [ ] Run tests
