//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.3.0
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class IADLXManualFanTuning1 : IADLXManualFanTuning {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal IADLXManualFanTuning1(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ADLXPINVOKE.IADLXManualFanTuning1_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IADLXManualFanTuning1 obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IADLXManualFanTuning1 obj) {
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
          ADLXPINVOKE.delete_IADLXManualFanTuning1(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public new static SWIGTYPE_p_wchar_t IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLXManualFanTuning1_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public virtual ADLX_RESULT GetDefaultFanTuningStates(SWIGTYPE_p_p_adlx__IADLXManualFanTuningStateList ppStates) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualFanTuning1_GetDefaultFanTuningStates(swigCPtr, SWIGTYPE_p_p_adlx__IADLXManualFanTuningStateList.getCPtr(ppStates));
    return ret;
  }

  public virtual ADLX_RESULT GetMinAcousticLimitDefault(SWIGTYPE_p_int defaultVal) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualFanTuning1_GetMinAcousticLimitDefault(swigCPtr, SWIGTYPE_p_int.getCPtr(defaultVal));
    return ret;
  }

  public virtual ADLX_RESULT GetMinFanSpeedDefault(SWIGTYPE_p_int defaultVal) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualFanTuning1_GetMinFanSpeedDefault(swigCPtr, SWIGTYPE_p_int.getCPtr(defaultVal));
    return ret;
  }

  public virtual ADLX_RESULT GetTargetFanSpeedDefault(SWIGTYPE_p_int defaultVal) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualFanTuning1_GetTargetFanSpeedDefault(swigCPtr, SWIGTYPE_p_int.getCPtr(defaultVal));
    return ret;
  }

  public virtual ADLX_RESULT GetDefaultZeroRPMState(SWIGTYPE_p_bool defaultVal) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualFanTuning1_GetDefaultZeroRPMState(swigCPtr, SWIGTYPE_p_bool.getCPtr(defaultVal));
    return ret;
  }

}
