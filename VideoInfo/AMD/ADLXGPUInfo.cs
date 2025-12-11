using System;

namespace ADLXWrapper
{
    /// <summary>
    /// Compatibility helpers to materialize GPU information from raw pointers.
    /// </summary>
    public static unsafe class ADLXGPUInfo
    {
        public static GpuInfo GetBasicInfo(IntPtr pGpu)
        {
            if (pGpu == IntPtr.Zero) throw new ArgumentNullException(nameof(pGpu));
            return new GpuInfo((IADLXGPU*)pGpu);
        }

        public static GpuInfo GetIdentification(IntPtr pGpu)
        {
            if (pGpu == IntPtr.Zero) throw new ArgumentNullException(nameof(pGpu));
            return new GpuInfo((IADLXGPU*)pGpu);
        }
    }
}
