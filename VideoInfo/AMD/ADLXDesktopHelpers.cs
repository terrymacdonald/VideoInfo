using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Helper methods for desktop and Eyefinity services
    /// </summary>
    public static unsafe class ADLXDesktopHelpers
    {
        /// <summary>
        /// Gets the IADLXDesktopServices interface from the system services. Callers must dispose the returned pointer.
        /// </summary>
        public static IADLXDesktopServices* GetDesktopServices(IADLXSystem* pSystem)
        {
            if (pSystem == null) throw new ArgumentNullException(nameof(pSystem));

            IADLXDesktopServices* pDesktopServices;
            var result = pSystem->GetDesktopsServices(&pDesktopServices);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get desktop services");
            }

            return pDesktopServices;
        }

        /// <summary>
        /// Enumerates all available desktops.
        /// </summary>
        public static IEnumerable<DesktopInfo> EnumerateAllDesktops(IADLXSystem* pSystem)
        {
            if (pSystem == null) return Array.Empty<DesktopInfo>();

            var desktops = new List<DesktopInfo>();
            using var desktopServices = new ComPtr<IADLXDesktopServices>(GetDesktopServices(pSystem));
            if (desktopServices.Get() == null) return desktops;

            IADLXDesktopList* pDesktopList;
            var listResult = desktopServices.Get()->GetDesktops(&pDesktopList);
            if (listResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(listResult, "Failed to get desktop list");

            using var desktopList = new ComPtr<IADLXDesktopList>(pDesktopList);

            for (uint i = 0; i < desktopList.Get()->Size(); i++)
            {
                IADLXDesktop* pDesktop;
                desktopList.Get()->At(i, &pDesktop);
                using var desktop = new ComPtr<IADLXDesktop>(pDesktop);
                desktops.Add(new DesktopInfo(desktop.Get()));
            }

            return desktops;
        }

        private static IADLXDisplay* GetEyefinityDisplayInternal(IADLXEyefinityDesktop* pEyefinityDesktop, uint row, uint col)
        {
            if (pEyefinityDesktop == null) throw new ArgumentNullException(nameof(pEyefinityDesktop));

            IADLXDisplay* pDisplay;
            var result = pEyefinityDesktop->GetDisplay(row, col, &pDisplay);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, $"Failed to get Eyefinity display at {row},{col}");
            return pDisplay;
        }

        /// <summary>
        /// Gets the Simple Eyefinity information.
        /// </summary>
        public static SimpleEyefinityInfo GetSimpleEyefinity(IADLXDesktopServices* pDesktopServices)
        {
            if (pDesktopServices == null) throw new ArgumentNullException(nameof(pDesktopServices));

            IADLXSimpleEyefinity* pSimple;
            var result = pDesktopServices->GetSimpleEyefinity(&pSimple);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get simple Eyefinity interface");
            }

            using var simpleEyefinity = new ComPtr<IADLXSimpleEyefinity>(pSimple);
            return new SimpleEyefinityInfo(simpleEyefinity.Get());
        }

        /// <summary>
        /// Creates an Eyefinity desktop.
        /// </summary>
        public static EyefinityDesktopInfo CreateEyefinityDesktop(IADLXSimpleEyefinity* pSimpleEyefinity)
        {
            if (pSimpleEyefinity == null) throw new ArgumentNullException(nameof(pSimpleEyefinity));

            IADLXEyefinityDesktop* pDesktop;
            var result = pSimpleEyefinity->Create(&pDesktop);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to create Eyefinity desktop");
            }

            using var eyefinityDesktop = new ComPtr<IADLXEyefinityDesktop>(pDesktop);
            return new EyefinityDesktopInfo(eyefinityDesktop.Get());
        }

        public static void DestroyEyefinityDesktop(IADLXSimpleEyefinity* pSimpleEyefinity, IADLXEyefinityDesktop* pEyefinityDesktop)
        {
            if (pSimpleEyefinity == null) throw new ArgumentNullException(nameof(pSimpleEyefinity));
            if (pEyefinityDesktop == null) throw new ArgumentNullException(nameof(pEyefinityDesktop));

            var result = pSimpleEyefinity->Destroy(pEyefinityDesktop);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to destroy Eyefinity desktop");
            }
        }

        public static (uint rows, uint cols) GetEyefinityGridSize(IADLXEyefinityDesktop* pEyefinityDesktop)
        {
            if (pEyefinityDesktop == null) throw new ArgumentNullException(nameof(pEyefinityDesktop));

            uint rows = 0, cols = 0;
            var result = pEyefinityDesktop->GridSize(&rows, &cols);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query Eyefinity grid size");
            return (rows, cols);
        }

        /// <summary>
        /// Enumerates displays within an Eyefinity desktop.
        /// </summary>
        public static IEnumerable<DisplayInfo> EnumerateEyefinityDisplays(IADLXEyefinityDesktop* pEyefinityDesktop)
        {
            if (pEyefinityDesktop == null) return Array.Empty<DisplayInfo>();

            var displays = new List<DisplayInfo>();
            var (rows, cols) = GetEyefinityGridSize(pEyefinityDesktop);
            for (uint r = 0; r < rows; r++)
            {
                for (uint c = 0; c < cols; c++)
                {
                    var pDisplay = GetEyefinityDisplayInternal(pEyefinityDesktop, r, c);
                    using var display = new ComPtr<IADLXDisplay>(pDisplay);
                    displays.Add(new DisplayInfo(display.Get()));
                }
            }

            return displays;
        }

        public static void DestroyAllEyefinityDesktops(IADLXSimpleEyefinity* pSimpleEyefinity)
        {
            if (pSimpleEyefinity == null) throw new ArgumentNullException(nameof(pSimpleEyefinity));

            var result = pSimpleEyefinity->DestroyAll();
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to destroy all Eyefinity desktops");
            }
        }

        /// <summary>
        /// Gets the desktop changed handling interface.
        /// </summary>
        public static IADLXDesktopChangedHandling* GetDesktopChangedHandling(IADLXDesktopServices* pDesktopServices)
        {
            if (pDesktopServices == null) throw new ArgumentNullException(nameof(pDesktopServices));
            IADLXDesktopChangedHandling* pHandling;
            var result = pDesktopServices->GetDesktopChangedHandling(&pHandling);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get desktop changed handling");
            return pHandling;
        }

        /// <summary>
        /// Adds a desktop list event listener.
        /// </summary>
        public static void AddDesktopListEventListener(IADLXDesktopChangedHandling* pHandling, DesktopListListenerHandle listener)
        {
            if (pHandling == null || listener == null || listener.IsInvalid) return;
            pHandling->AddDesktopListEventListener(listener.GetListener());
        }

        /// <summary>
        /// Removes a desktop list event listener.
        /// </summary>
        public static void RemoveDesktopListEventListener(IADLXDesktopChangedHandling* pHandling, DesktopListListenerHandle listener)
        {
            if (pHandling == null || listener == null || listener.IsInvalid) return;
            pHandling->RemoveDesktopListEventListener(listener.GetListener());
        }
    }

    /// <summary>
    /// Represents the collected information for a desktop.
    /// </summary>
    public readonly struct DesktopInfo
    {
        public ADLX_DESKTOP_TYPE Type { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }
        public int TopLeftX { get; init; }
        public int TopLeftY { get; init; }
        public ADLX_ORIENTATION Orientation { get; init; }

        [JsonConstructor]
        public DesktopInfo(ADLX_DESKTOP_TYPE type, int width, int height, int topLeftX, int topLeftY, ADLX_ORIENTATION orientation)
        {
            Type = type;
            Width = width;
            Height = height;
            TopLeftX = topLeftX;
            TopLeftY = topLeftY;
            Orientation = orientation;
        }

        internal unsafe DesktopInfo(IADLXDesktop* pDesktop)
        {
            ADLX_DESKTOP_TYPE type = default;
            pDesktop->Type(&type);
            Type = type;

            int w = 0, h = 0;
            pDesktop->Size(&w, &h);
            Width = w;
            Height = h;

            ADLX_Point topLeft = default;
            pDesktop->TopLeft(&topLeft);
            TopLeftX = topLeft.x;
            TopLeftY = topLeft.y;

            ADLX_ORIENTATION orientation = default;
            pDesktop->Orientation(&orientation);
            Orientation = orientation;
        }
    }

    /// <summary>
    /// Safe handle for an unmanaged IADLXDesktopListChangedListener backed by a managed delegate.
    /// </summary>
    public sealed unsafe class DesktopListListenerHandle : SafeHandle
    {
        public delegate void OnDesktopListChanged(IADLXDesktopList* pNewDesktops);

        private static readonly ConcurrentDictionary<IntPtr, OnDesktopListChanged> _map = new();
        private static readonly IntPtr _thunkPtr = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IADLXDesktopList*, byte>)&OnDesktopListChangedThunk;
        private readonly GCHandle _gcHandle;
        private readonly IntPtr _vtbl;

        private DesktopListListenerHandle(OnDesktopListChanged cb) : base(IntPtr.Zero, true)
        {
            _gcHandle = GCHandle.Alloc(cb);
            _vtbl = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(_vtbl, _thunkPtr);

            var inst = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(inst, _vtbl);
            handle = inst;
            _map[inst] = cb;
        }

        public static DesktopListListenerHandle Create(OnDesktopListChanged cb)
        {
            if (cb == null) throw new ArgumentNullException(nameof(cb));
            return new DesktopListListenerHandle(cb);
        }

        public IADLXDesktopListChangedListener* GetListener() => (IADLXDesktopListChangedListener*)handle;

        protected override bool ReleaseHandle()
        {
            _map.TryRemove(handle, out _);
            if (_gcHandle.IsAllocated) _gcHandle.Free();
            if (_vtbl != IntPtr.Zero) Marshal.FreeHGlobal(_vtbl);
            if (handle != IntPtr.Zero) Marshal.FreeHGlobal(handle);
            return true;
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static byte OnDesktopListChangedThunk(IntPtr pThis, IADLXDesktopList* pNewDesktops)
        {
            if (_map.TryGetValue(pThis, out var cb)) { cb(pNewDesktops); return 1; }
            return 0;
        }
    }

    public readonly struct SimpleEyefinityInfo
    {
        public bool IsSupported { get; init; }

        internal unsafe SimpleEyefinityInfo(IADLXSimpleEyefinity* p)
        {
            IsSupported = p != null;
        }
    }

    public readonly struct EyefinityDesktopInfo
    {
        public bool IsValid { get; init; }

        internal unsafe EyefinityDesktopInfo(IADLXEyefinityDesktop* p)
        {
            IsValid = p != null;
        }
    }
}