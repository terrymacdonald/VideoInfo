using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace IGCLWrapper
{
    /// <summary>
    /// SafeHandle wrapper for the IGCL API handle.
    /// Ensures ctlClose is called exactly once to release native resources.
    /// </summary>
    internal sealed class IGCLApiHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public IGCLApiHandle() : base(true)
        {
        }

        public IGCLApiHandle(IntPtr existingHandle) : base(true)
        {
            SetHandle(existingHandle);
        }

        protected override bool ReleaseHandle()
        {
            unsafe
            {
                if (!IsInvalid && handle != IntPtr.Zero)
                {
                    IGCL.ctlClose((_ctl_api_handle_t*)handle);
                    handle = IntPtr.Zero;
                }
            }

            return true;
        }
    }
}
