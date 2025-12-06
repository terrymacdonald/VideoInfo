using System;
using Microsoft.Win32.SafeHandles;

namespace ADLXWrapper
{
    /// <summary>
    /// SafeHandle wrapper for ADLX COM-like interfaces. Ensures Release() is called even if user forgets.
    /// </summary>
    public sealed class AdlxInterfaceHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private readonly bool _addRef;

        private AdlxInterfaceHandle(bool addRef)
            : base(true)
        {
            _addRef = addRef;
        }

        /// <summary>
        /// Wrap an ADLX interface pointer. If addRef is true, increments the reference count.
        /// </summary>
        public static AdlxInterfaceHandle From(IntPtr ptr, bool addRef = false)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentNullException(nameof(ptr));

            var handle = new AdlxInterfaceHandle(addRef);

            if (addRef)
            {
                ADLXHelpers.AddRefInterface(ptr);
            }

            handle.SetHandle(ptr);
            return handle;
        }

        /// <summary>
        /// Implicit conversion to IntPtr for interoperability with existing helper methods.
        /// </summary>
        public static implicit operator IntPtr(AdlxInterfaceHandle h) => h.handle;

        protected override bool ReleaseHandle()
        {
            try
            {
                ADLXHelpers.ReleaseInterface(handle);
                return true;
            }
            catch
            {
                // We can't throw from a SafeHandle release; report false to indicate failure.
                return false;
            }
        }
    }
}
