//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.3.0
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class IADLXPowerTuningChangedHandling : IADLXInterface {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal IADLXPowerTuningChangedHandling(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ADLXPINVOKE.IADLXPowerTuningChangedHandling_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IADLXPowerTuningChangedHandling obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IADLXPowerTuningChangedHandling obj) {
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
          ADLXPINVOKE.delete_IADLXPowerTuningChangedHandling(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public new static SWIGTYPE_p_wchar_t IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLXPowerTuningChangedHandling_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public virtual ADLX_RESULT AddPowerTuningEventListener(IADLXPowerTuningChangedListener pPowerTuningChangedListener) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXPowerTuningChangedHandling_AddPowerTuningEventListener(swigCPtr, IADLXPowerTuningChangedListener.getCPtr(pPowerTuningChangedListener));
    return ret;
  }

  public virtual ADLX_RESULT RemovePowerTuningEventListener(IADLXPowerTuningChangedListener pPowerTuningChangedListener) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXPowerTuningChangedHandling_RemovePowerTuningEventListener(swigCPtr, IADLXPowerTuningChangedListener.getCPtr(pPowerTuningChangedListener));
    return ret;
  }

}
