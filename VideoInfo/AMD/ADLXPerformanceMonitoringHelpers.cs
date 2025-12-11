using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Helper methods for performance monitoring services
    /// </summary>
    public static unsafe partial class ADLXPerformanceMonitoringHelpers
    {
        /// <summary>
        /// Gets the IADLXPerformanceMonitoringServices interface from the system services. Callers must dispose the returned pointer.
        /// </summary>
        public static IADLXPerformanceMonitoringServices* GetPerformanceMonitoringServices(IADLXSystem* pSystem)
        {
            if (pSystem == null) throw new ArgumentNullException(nameof(pSystem));

            IADLXPerformanceMonitoringServices* pServices;
            var result = pSystem->GetPerformanceMonitoringServices(&pServices);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get performance monitoring services");
            return pServices;
        }

        /// <summary>
        /// Gets the GPU metrics support capabilities for a specific GPU.
        /// </summary>
        public static GpuMetricsSupportInfo GetGpuMetricsSupport(IADLXPerformanceMonitoringServices* pServices, IADLXGPU* pGpu)
        {
            if (pServices == null) throw new ArgumentNullException(nameof(pServices));
            if (pGpu == null) throw new ArgumentNullException(nameof(pGpu));

            IADLXGPUMetricsSupport* pMetricsSupport;
            var result = pServices->GetSupportedGPUMetrics(pGpu, &pMetricsSupport);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get supported GPU metrics");

            using var metricsSupport = new ComPtr<IADLXGPUMetricsSupport>(pMetricsSupport);
            return new GpuMetricsSupportInfo(metricsSupport.Get());
        }

        /// <summary>
        /// Gets the current GPU metrics snapshot for a specific GPU.
        /// </summary>
        public static GpuMetricsSnapshotInfo GetCurrentGpuMetrics(IADLXPerformanceMonitoringServices* pServices, IADLXGPU* pGpu)
        {
            if (pServices == null) throw new ArgumentNullException(nameof(pServices));
            if (pGpu == null) throw new ArgumentNullException(nameof(pGpu));

            IADLXGPUMetrics* pMetrics;
            var result = pServices->GetCurrentGPUMetrics(pGpu, &pMetrics);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get current GPU metrics");

            using var metrics = new ComPtr<IADLXGPUMetrics>(pMetrics);
            return new GpuMetricsSnapshotInfo(metrics.Get());
        }

        // Compatibility alias for older naming used in tests
        public static GpuMetricsSnapshotInfo GetCurrentGPUMetrics(IADLXPerformanceMonitoringServices* pServices, IADLXGPU* pGpu)
            => GetCurrentGpuMetrics(pServices, pGpu);

        /// <summary>
        /// Gets the current System metrics snapshot.
        /// </summary>
        public static SystemMetricsSnapshotInfo GetCurrentSystemMetrics(IADLXPerformanceMonitoringServices* pServices)
        {
            if (pServices == null) throw new ArgumentNullException(nameof(pServices));

            IADLXSystemMetrics* pMetrics;
            var result = pServices->GetCurrentSystemMetrics(&pMetrics);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get current System metrics");

            using var metrics = new ComPtr<IADLXSystemMetrics>(pMetrics);
            return new SystemMetricsSnapshotInfo(metrics.Get());
        }

        /// <summary>
        /// Gets the current All metrics snapshot.
        /// </summary>
        public static AllMetricsSnapshotInfo GetCurrentAllMetrics(IADLXPerformanceMonitoringServices* pServices)
        {
            if (pServices == null) throw new ArgumentNullException(nameof(pServices));

            IADLXAllMetrics* pAllMetrics;
            var result = pServices->GetCurrentAllMetrics(&pAllMetrics);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get current All metrics");

            using var allMetrics = new ComPtr<IADLXAllMetrics>(pAllMetrics);
            return new AllMetricsSnapshotInfo(allMetrics.Get());
        }

        /// <summary>
        /// Enumerates GPU metrics history for a specific GPU within a time range.
        /// </summary>
        public static IEnumerable<GpuMetricsSnapshotInfo> EnumerateGpuMetricsHistory(IADLXPerformanceMonitoringServices* pServices, IADLXGPU* pGpu, int startMs, int stopMs)
        {
            if (pServices == null) throw new ArgumentNullException(nameof(pServices));
            if (pGpu == null) throw new ArgumentNullException(nameof(pGpu));

            IADLXGPUMetricsList* pMetricsList;
            var result = pServices->GetGPUMetricsHistory(pGpu, startMs, stopMs, &pMetricsList);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get GPU metrics history");

            var snapshots = new List<GpuMetricsSnapshotInfo>();
            using var metricsList = new ComPtr<IADLXGPUMetricsList>(pMetricsList);
            for (uint i = 0; i < metricsList.Get()->Size(); i++)
            {
                IADLXGPUMetrics* pMetrics;
                metricsList.Get()->At(i, &pMetrics);
                using var metrics = new ComPtr<IADLXGPUMetrics>(pMetrics);
                snapshots.Add(new GpuMetricsSnapshotInfo(metrics.Get()));
            }

            return snapshots;
        }

        /// <summary>
        /// Enumerates System metrics history within a time range.
        /// </summary>
        public static IEnumerable<SystemMetricsSnapshotInfo> EnumerateSystemMetricsHistory(IADLXPerformanceMonitoringServices* pServices, int startMs, int stopMs)
        {
            if (pServices == null) throw new ArgumentNullException(nameof(pServices));

            IADLXSystemMetricsList* pMetricsList;
            var result = pServices->GetSystemMetricsHistory(startMs, stopMs, &pMetricsList);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get System metrics history");

            var snapshots = new List<SystemMetricsSnapshotInfo>();
            using var metricsList = new ComPtr<IADLXSystemMetricsList>(pMetricsList);
            for (uint i = 0; i < metricsList.Get()->Size(); i++)
            {
                IADLXSystemMetrics* pMetrics;
                metricsList.Get()->At(i, &pMetrics);
                using var metrics = new ComPtr<IADLXSystemMetrics>(pMetrics);
                snapshots.Add(new SystemMetricsSnapshotInfo(metrics.Get()));
            }

            return snapshots;
        }

        /// <summary>
        /// Enumerates All metrics history within a time range.
        /// </summary>
        public static IEnumerable<AllMetricsSnapshotInfo> EnumerateAllMetricsHistory(IADLXPerformanceMonitoringServices* pServices, int startMs, int stopMs)
        {
            if (pServices == null) throw new ArgumentNullException(nameof(pServices));

            IADLXAllMetricsList* pMetricsList;
            var result = pServices->GetAllMetricsHistory(startMs, stopMs, &pMetricsList);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get All metrics history");

            var snapshots = new List<AllMetricsSnapshotInfo>();
            using var metricsList = new ComPtr<IADLXAllMetricsList>(pMetricsList);
            for (uint i = 0; i < metricsList.Get()->Size(); i++)
            {
                IADLXAllMetrics* pMetrics;
                metricsList.Get()->At(i, &pMetrics);
                using var metrics = new ComPtr<IADLXAllMetrics>(pMetrics);
                snapshots.Add(new AllMetricsSnapshotInfo(metrics.Get()));
            }

            return snapshots;
        }

        /// <summary>
        /// Gets the sampling interval range.
        /// </summary>
        public static ADLX_IntRange GetSamplingIntervalRange(IADLXPerformanceMonitoringServices* pServices)
        {
            if (pServices == null) throw new ArgumentNullException(nameof(pServices));

            ADLX_IntRange range = default;
            var result = pServices->GetSamplingIntervalRange(&range);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get sampling interval range");
            return range;
        }

        /// <summary>
        /// Gets the current sampling interval.
        /// </summary>
        public static int GetSamplingInterval(IADLXPerformanceMonitoringServices* pServices)
        {
            if (pServices == null) throw new ArgumentNullException(nameof(pServices));

            int interval = 0;
            var result = pServices->GetSamplingInterval(&interval);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get sampling interval");
            return interval;
        }

        /// <summary>
        /// Sets the sampling interval.
        /// </summary>
        public static void SetSamplingInterval(IADLXPerformanceMonitoringServices* pServices, int intervalMs)
        {
            if (pServices == null) throw new ArgumentNullException(nameof(pServices));

            var result = pServices->SetSamplingInterval(intervalMs);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set sampling interval");
        }

        /// <summary>
        /// Gets the configurable settings for the performance monitoring service.
        /// </summary>
        public static PerformanceMonitoringSettingsInfo GetPerformanceMonitoringSettings(IADLXPerformanceMonitoringServices* pServices)
        {
            if (pServices == null) throw new ArgumentNullException(nameof(pServices));
            return new PerformanceMonitoringSettingsInfo(pServices);
        }

        /// <summary>
        /// Applies configurable settings to the performance monitoring service.
        /// </summary>
        public static void ApplyPerformanceMonitoringSettings(IADLXPerformanceMonitoringServices* pServices, PerformanceMonitoringSettingsInfo info)
        {
            if (pServices == null) throw new ArgumentNullException(nameof(pServices));

            // Apply settings if they are within valid ranges
            var intervalRange = GetSamplingIntervalRange(pServices);
            if (info.SamplingIntervalMs >= intervalRange.minValue && info.SamplingIntervalMs <= intervalRange.maxValue)
                SetSamplingInterval(pServices, info.SamplingIntervalMs);

            SetMaxPerformanceMetricsHistorySize(pServices, info.MaxHistorySizeSec);
        }

        /// <summary>
        /// Gets the maximum history size range.
        /// </summary>
        public static ADLX_IntRange GetMaxHistorySizeRange(IADLXPerformanceMonitoringServices* pServices)
        {
            if (pServices == null) throw new ArgumentNullException(nameof(pServices));

            // Max history size range isn't exposed directly; use max getter as upper bound and 0 as min
            int max = GetMaxPerformanceMetricsHistorySize(pServices);
            return new ADLX_IntRange { minValue = 0, maxValue = max, step = 1 };
        }

        /// <summary>
        /// Gets the maximum performance metrics history size.
        /// </summary>
        public static int GetMaxPerformanceMetricsHistorySize(IADLXPerformanceMonitoringServices* pServices)
        {
            if (pServices == null) throw new ArgumentNullException(nameof(pServices));

            int size = 0;
            var result = pServices->GetMaxPerformanceMetricsHistorySize(&size);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get max performance metrics history size");
            return size;
        }

        /// <summary>
        /// Gets the current performance metrics history size.
        /// </summary>
        public static int GetCurrentPerformanceMetricsHistorySize(IADLXPerformanceMonitoringServices* pServices)
        {
            if (pServices == null) throw new ArgumentNullException(nameof(pServices));

            int size = 0;
            var result = pServices->GetCurrentPerformanceMetricsHistorySize(&size);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get current performance metrics history size");
            return size;
        }

        /// <summary>
        /// Sets the maximum performance metrics history size.
        /// </summary>
        public static void SetMaxPerformanceMetricsHistorySize(IADLXPerformanceMonitoringServices* pServices, int sizeSec)
        {
            if (pServices == null) throw new ArgumentNullException(nameof(pServices));

            var result = pServices->SetMaxPerformanceMetricsHistorySize(sizeSec);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set max performance metrics history size");
        }

        /// <summary>
        /// Clears the performance metrics history.
        /// </summary>
        public static void ClearPerformanceMetricsHistory(IADLXPerformanceMonitoringServices* pServices)
        {
            if (pServices == null) throw new ArgumentNullException(nameof(pServices));

            var result = pServices->ClearPerformanceMetricsHistory();
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to clear performance metrics history");
        }

        /// <summary>
        /// Starts performance metrics tracking.
        /// </summary>
        public static void StartPerformanceMetricsTracking(IADLXPerformanceMonitoringServices* pServices)
        {
            if (pServices == null) throw new ArgumentNullException(nameof(pServices));
            var result = pServices->StartPerformanceMetricsTracking();
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to start performance metrics tracking");
        }

        /// <summary>
        /// Stops performance metrics tracking.
        /// </summary>
        public static void StopPerformanceMetricsTracking(IADLXPerformanceMonitoringServices* pServices)
        {
            if (pServices == null) throw new ArgumentNullException(nameof(pServices));
            var result = pServices->StopPerformanceMetricsTracking();
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to stop performance metrics tracking");
        }

        // Individual metric support checks (used by GpuMetricsSupportInfo)
        internal static bool IsSupportedGPUUsage(IADLXGPUMetricsSupport* pMetricsSupport)
        {
            bool supported = false;
            pMetricsSupport->IsSupportedGPUUsage(&supported);
            return supported;
        }
        internal static bool IsSupportedGPUClockSpeed(IADLXGPUMetricsSupport* pMetricsSupport)
        {
            bool supported = false;
            pMetricsSupport->IsSupportedGPUClockSpeed(&supported);
            return supported;
        }
        internal static bool IsSupportedGPUTemperature(IADLXGPUMetricsSupport* pMetricsSupport)
        {
            bool supported = false;
            pMetricsSupport->IsSupportedGPUTemperature(&supported);
            return supported;
        }
        internal static bool IsSupportedGPUHotspotTemperature(IADLXGPUMetricsSupport* pMetricsSupport)
        {
            bool supported = false;
            pMetricsSupport->IsSupportedGPUHotspotTemperature(&supported);
            return supported;
        }
        internal static bool IsSupportedGPUPower(IADLXGPUMetricsSupport* pMetricsSupport)
        {
            bool supported = false;
            pMetricsSupport->IsSupportedGPUPower(&supported);
            return supported;
        }
        internal static bool IsSupportedGPUVoltage(IADLXGPUMetricsSupport* pMetricsSupport)
        {
            bool supported = false;
            pMetricsSupport->IsSupportedGPUVoltage(&supported);
            return supported;
        }
        internal static bool IsSupportedGPUTotalBoardPower(IADLXGPUMetricsSupport* pMetricsSupport)
        {
            bool supported = false;
            pMetricsSupport->IsSupportedGPUTotalBoardPower(&supported);
            return supported;
        }
        internal static bool IsSupportedGPUVRAMClockSpeed(IADLXGPUMetricsSupport* pMetricsSupport)
        {
            bool supported = false;
            pMetricsSupport->IsSupportedGPUVRAMClockSpeed(&supported);
            return supported;
        }
        internal static bool IsSupportedGPUFanSpeed(IADLXGPUMetricsSupport* pMetricsSupport)
        {
            bool supported = false;
            pMetricsSupport->IsSupportedGPUFanSpeed(&supported);
            return supported;
        }
        internal static bool IsSupportedGPUVRAM(IADLXGPUMetricsSupport* pMetricsSupport)
        {
            bool supported = false;
            pMetricsSupport->IsSupportedGPUVRAM(&supported);
            return supported;
        }

        // Individual metric getters (used by GpuMetricsSnapshotInfo)
        internal static long GetMetricTimestamp(IADLXGPUMetrics* pMetrics)
        {
            long ts = 0;
            pMetrics->TimeStamp(&ts);
            return ts;
        }
        internal static double GetGPUTemperature(IADLXGPUMetrics* pMetrics)
        {
            double temperature = 0;
            pMetrics->GPUTemperature(&temperature);
            return temperature;
        }
        internal static double GetGPUUsage(IADLXGPUMetrics* pMetrics)
        {
            double usage = 0;
            pMetrics->GPUUsage(&usage);
            return usage;
        }
        internal static int GetGPUClockSpeed(IADLXGPUMetrics* pMetrics)
        {
            int clockSpeed = 0;
            pMetrics->GPUClockSpeed(&clockSpeed);
            return clockSpeed;
        }
        internal static int GetGPUVRAMClockSpeed(IADLXGPUMetrics* pMetrics)
        {
            int vramClockSpeed = 0;
            pMetrics->GPUVRAMClockSpeed(&vramClockSpeed);
            return vramClockSpeed;
        }
        internal static int GetGPUVRAM(IADLXGPUMetrics* pMetrics)
        {
            int vram = 0;
            pMetrics->GPUVRAM(&vram);
            return vram;
        }
        internal static int GetGPUFanSpeed(IADLXGPUMetrics* pMetrics)
        {
            int fanSpeed = 0;
            pMetrics->GPUFanSpeed(&fanSpeed);
            return fanSpeed;
        }
        internal static double GetGPUPower(IADLXGPUMetrics* pMetrics)
        {
            double power = 0;
            pMetrics->GPUPower(&power);
            return power;
        }
        internal static double GetGPUHotspotTemperature(IADLXGPUMetrics* pMetrics)
        {
            double temp = 0;
            pMetrics->GPUHotspotTemperature(&temp);
            return temp;
        }
        internal static int GetGPUVoltage(IADLXGPUMetrics* pMetrics)
        {
            int voltage = 0;
            pMetrics->GPUVoltage(&voltage);
            return voltage;
        }
        internal static double GetGPUTotalBoardPower(IADLXGPUMetrics* pMetrics)
        {
            double value = 0;
            pMetrics->GPUTotalBoardPower(&value);
            return value;
        }
    }

    /// <summary>
    /// GPU metrics support capabilities.
    /// </summary>
    public readonly struct GpuMetricsSupportInfo
    {
        public bool UsageSupported { get; init; }
        public bool ClockSpeedSupported { get; init; }
        public bool TemperatureSupported { get; init; }
        public bool HotspotTemperatureSupported { get; init; }
        public bool PowerSupported { get; init; }
        public bool FanSpeedSupported { get; init; }
        public bool VRAMSupported { get; init; }
        public bool VRAMClockSpeedSupported { get; init; }
        public bool VoltageSupported { get; init; }
        public bool TotalBoardPowerSupported { get; init; }

        [JsonConstructor]
        public GpuMetricsSupportInfo(bool usageSupported, bool clockSpeedSupported, bool temperatureSupported, bool hotspotTemperatureSupported, bool powerSupported, bool fanSpeedSupported, bool vramSupported, bool vramClockSpeedSupported, bool voltageSupported, bool totalBoardPowerSupported)
        {
            UsageSupported = usageSupported;
            ClockSpeedSupported = clockSpeedSupported;
            TemperatureSupported = temperatureSupported;
            HotspotTemperatureSupported = hotspotTemperatureSupported;
            PowerSupported = powerSupported;
            FanSpeedSupported = fanSpeedSupported;
            VRAMSupported = vramSupported;
            VRAMClockSpeedSupported = vramClockSpeedSupported;
            VoltageSupported = voltageSupported;
            TotalBoardPowerSupported = totalBoardPowerSupported;
        }

        internal unsafe GpuMetricsSupportInfo(IADLXGPUMetricsSupport* pMetricsSupport)
        {
            UsageSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUUsage(pMetricsSupport);
            ClockSpeedSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUClockSpeed(pMetricsSupport);
            TemperatureSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUTemperature(pMetricsSupport);
            HotspotTemperatureSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUHotspotTemperature(pMetricsSupport);
            PowerSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUPower(pMetricsSupport);
            FanSpeedSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUFanSpeed(pMetricsSupport);
            VRAMSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUVRAM(pMetricsSupport);
            VRAMClockSpeedSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUVRAMClockSpeed(pMetricsSupport);
            VoltageSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUVoltage(pMetricsSupport);
            TotalBoardPowerSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUTotalBoardPower(pMetricsSupport);
        }
    }

    /// <summary>
    /// Current GPU metrics values.
    /// </summary>
    public readonly struct GpuMetricsSnapshotInfo
    {
        public double Temperature { get; init; }
        public double Usage { get; init; }
        public int ClockSpeed { get; init; }
        public int VRAMClockSpeed { get; init; }
        public int VRAMUsage { get; init; }
        public int FanSpeed { get; init; }
        public double Power { get; init; }
        public double TotalBoardPower { get; init; }
        public int Voltage { get; init; }
        public long TimestampMs { get; init; }

        [JsonConstructor]
        public GpuMetricsSnapshotInfo(double temperature, double usage, int clockSpeed, int vramClockSpeed, int vramUsage, int fanSpeed, double power, double totalBoardPower, int voltage, long timestampMs)
        {
            Temperature = temperature;
            Usage = usage;
            ClockSpeed = clockSpeed;
            VRAMClockSpeed = vramClockSpeed;
            VRAMUsage = vramUsage;
            FanSpeed = fanSpeed;
            Power = power;
            TotalBoardPower = totalBoardPower;
            Voltage = voltage;
            TimestampMs = timestampMs;
        }

        internal unsafe GpuMetricsSnapshotInfo(IADLXGPUMetrics* pMetrics)
        {
            TimestampMs = ADLXPerformanceMonitoringHelpers.GetMetricTimestamp(pMetrics);
            Temperature = ADLXPerformanceMonitoringHelpers.GetGPUTemperature(pMetrics);
            Usage = ADLXPerformanceMonitoringHelpers.GetGPUUsage(pMetrics);
            ClockSpeed = ADLXPerformanceMonitoringHelpers.GetGPUClockSpeed(pMetrics);
            VRAMClockSpeed = ADLXPerformanceMonitoringHelpers.GetGPUVRAMClockSpeed(pMetrics);
            VRAMUsage = ADLXPerformanceMonitoringHelpers.GetGPUVRAM(pMetrics);
            FanSpeed = ADLXPerformanceMonitoringHelpers.GetGPUFanSpeed(pMetrics);
            Power = ADLXPerformanceMonitoringHelpers.GetGPUPower(pMetrics);
            TotalBoardPower = ADLXPerformanceMonitoringHelpers.GetGPUTotalBoardPower(pMetrics);
            Voltage = ADLXPerformanceMonitoringHelpers.GetGPUVoltage(pMetrics);
        }
    }

    /// <summary>
    /// SmartShift power distribution values (when available via IADLXSystemMetrics1).
    /// </summary>
    public readonly struct PowerDistributionSnapshotInfo
    {
        public int ApuShiftValue { get; init; }
        public int GpuShiftValue { get; init; }
        public int ApuShiftLimit { get; init; }
        public int GpuShiftLimit { get; init; }
        public int TotalShiftLimit { get; init; }

        [JsonConstructor]
        public PowerDistributionSnapshotInfo(int apuShiftValue, int gpuShiftValue, int apuShiftLimit, int gpuShiftLimit, int totalShiftLimit)
        {
            ApuShiftValue = apuShiftValue;
            GpuShiftValue = gpuShiftValue;
            ApuShiftLimit = apuShiftLimit;
            GpuShiftLimit = gpuShiftLimit;
            TotalShiftLimit = totalShiftLimit;
        }
    }

    /// <summary>
    /// System-level metrics values.
    /// </summary>
    public readonly struct SystemMetricsSnapshotInfo
    {
        public long TimestampMs { get; init; }
        public double CpuUsage { get; init; }
        public int SystemRam { get; init; }
        public int SmartShift { get; init; }
        public PowerDistributionSnapshotInfo? PowerDistribution { get; init; }

        [JsonConstructor]
        public SystemMetricsSnapshotInfo(long timestampMs, double cpuUsage, int systemRam, int smartShift, PowerDistributionSnapshotInfo? powerDistribution)
        {
            TimestampMs = timestampMs;
            CpuUsage = cpuUsage;
            SystemRam = systemRam;
            SmartShift = smartShift;
            PowerDistribution = powerDistribution;
        }

        internal unsafe SystemMetricsSnapshotInfo(IADLXSystemMetrics* pMetrics)
        {
            long ts = 0; pMetrics->TimeStamp(&ts); TimestampMs = ts;
            double cpu = 0; pMetrics->CPUUsage(&cpu); CpuUsage = cpu;
            int ram = 0; pMetrics->SystemRAM(&ram); SystemRam = ram;
            int ss = 0; pMetrics->SmartShift(&ss); SmartShift = ss;

            PowerDistribution = null;
            if (ADLXHelpers.TryQueryInterface((IntPtr)pMetrics, nameof(IADLXSystemMetrics1), out var pMetrics1Ptr))
            {
                using var metrics1 = new ComPtr<IADLXSystemMetrics1>((IADLXSystemMetrics1*)pMetrics1Ptr);
                int apu = 0, gpu = 0, apuLimit = 0, gpuLimit = 0, total = 0;
                if (metrics1.Get()->PowerDistribution(&apu, &gpu, &apuLimit, &gpuLimit, &total) == ADLX_RESULT.ADLX_OK)
                {
                    PowerDistribution = new PowerDistributionSnapshotInfo
                    {
                        ApuShiftValue = apu,
                        GpuShiftValue = gpu,
                        ApuShiftLimit = apuLimit,
                        GpuShiftLimit = gpuLimit,
                        TotalShiftLimit = total
                    };
                }
            }
        }
    }

    /// <summary>
    /// GPU metrics paired with a GPU identifier.
    /// </summary>
    public readonly struct GpuMetricsEntryInfo
    {
        public int GpuUniqueId { get; init; }
        public GpuMetricsSnapshotInfo Metrics { get; init; }

        [JsonConstructor]
        public GpuMetricsEntryInfo(int gpuUniqueId, GpuMetricsSnapshotInfo metrics)
        {
            GpuUniqueId = gpuUniqueId;
            Metrics = metrics;
        }
    }

    /// <summary>
    /// Aggregate snapshot for system + GPU metrics.
    /// </summary>
    public readonly struct AllMetricsSnapshotInfo
    {
        public long TimestampMs { get; init; }
        public SystemMetricsSnapshotInfo? System { get; init; }
        public int? FPS { get; init; }
        public GpuMetricsEntryInfo[] GpuMetrics { get; init; }

        [JsonConstructor]
        public AllMetricsSnapshotInfo(long timestampMs, SystemMetricsSnapshotInfo? system, int? fps, GpuMetricsEntryInfo[] gpuMetrics)
        {
            TimestampMs = timestampMs;
            System = system;
            FPS = fps;
            GpuMetrics = gpuMetrics;
        }

        internal unsafe AllMetricsSnapshotInfo(IADLXAllMetrics* pMetrics)
        {
            long ts = 0; pMetrics->TimeStamp(&ts); TimestampMs = ts;

            System = null;
            IADLXSystemMetrics* pSys = null;
            if (pMetrics->GetSystemMetrics(&pSys) == ADLX_RESULT.ADLX_OK && pSys != null)
            {
                using var sysMetrics = new ComPtr<IADLXSystemMetrics>(pSys);
                System = new SystemMetricsSnapshotInfo(sysMetrics.Get());
            }

            FPS = null;
            IADLXFPS* pFps = null;
            if (pMetrics->GetFPS(&pFps) == ADLX_RESULT.ADLX_OK && pFps != null)
            {
                using var fpsMetrics = new ComPtr<IADLXFPS>(pFps);
                int fpsValue = 0;
                if (fpsMetrics.Get()->FPS(&fpsValue) == ADLX_RESULT.ADLX_OK)
                {
                    FPS = fpsValue;
                }
            }

            GpuMetrics = Array.Empty<GpuMetricsEntryInfo>();
        }
    }

    /// <summary>
    /// Represents the configurable settings for the performance monitoring service.
    /// </summary>
    public readonly struct PerformanceMonitoringSettingsInfo
    {
        public int SamplingIntervalMs { get; init; }
        public int MaxHistorySizeSec { get; init; }

        [JsonConstructor]
        public PerformanceMonitoringSettingsInfo(int samplingIntervalMs, int maxHistorySizeSec)
        {
            SamplingIntervalMs = samplingIntervalMs;
            MaxHistorySizeSec = maxHistorySizeSec;
        }

        internal unsafe PerformanceMonitoringSettingsInfo(IADLXPerformanceMonitoringServices* pServices)
        {
            int interval = 0;
            pServices->GetSamplingInterval(&interval);
            SamplingIntervalMs = interval;

            int size = 0;
            pServices->GetCurrentPerformanceMetricsHistorySize(&size);
            MaxHistorySizeSec = size;
        }
    }
}