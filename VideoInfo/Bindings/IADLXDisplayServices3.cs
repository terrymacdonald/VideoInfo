//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.3.0
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class IADLXDisplayServices3 : IADLXDisplayServices2 {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal IADLXDisplayServices3(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ADLXPINVOKE.IADLXDisplayServices3_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IADLXDisplayServices3 obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IADLXDisplayServices3 obj) {
    if (obj != null) {
      if (!obj.swigCMemOwn)
        throw new global::System.ApplicationException("Cannot release ownership as memory is not owned");
      global::System.Runtime.InteropServices.HandleRef ptr = obj.swigCPtr;
      obj.swigCMemOwn = false;
      obj.Dispose();
      return ptr;
    } else {
      return new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
    }
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          ADLXPINVOKE.delete_IADLXDisplayServices3(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public new static SWIGTYPE_p_wchar_t IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLXDisplayServices3_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public virtual ADLX_RESULT GetDynamicRefreshRateControl(IADLXDisplay pDisplay, SWIGTYPE_p_p_adlx__IADLXDisplayDynamicRefreshRateControl ppDRRC) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayServices3_GetDynamicRefreshRateControl(swigCPtr, IADLXDisplay.getCPtr(pDisplay), SWIGTYPE_p_p_adlx__IADLXDisplayDynamicRefreshRateControl.getCPtr(ppDRRC));
    return ret;
  }

  public virtual ADLX_RESULT GetFreeSyncColorAccuracy(IADLXDisplay pDisplay, SWIGTYPE_p_p_adlx__IADLXDisplayFreeSyncColorAccuracy ppFSCA) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayServices3_GetFreeSyncColorAccuracy(swigCPtr, IADLXDisplay.getCPtr(pDisplay), SWIGTYPE_p_p_adlx__IADLXDisplayFreeSyncColorAccuracy.getCPtr(ppFSCA));
    return ret;
  }

}
