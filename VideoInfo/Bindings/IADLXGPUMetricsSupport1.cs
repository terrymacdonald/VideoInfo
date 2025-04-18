//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.3.0
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class IADLXGPUMetricsSupport1 : IADLXGPUMetricsSupport {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal IADLXGPUMetricsSupport1(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ADLXPINVOKE.IADLXGPUMetricsSupport1_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IADLXGPUMetricsSupport1 obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IADLXGPUMetricsSupport1 obj) {
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
          ADLXPINVOKE.delete_IADLXGPUMetricsSupport1(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public new static SWIGTYPE_p_wchar_t IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLXGPUMetricsSupport1_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public virtual ADLX_RESULT IsSupportedGPUMemoryTemperature(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport1_IsSupportedGPUMemoryTemperature(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT GetGPUMemoryTemperatureRange(SWIGTYPE_p_int minValue, SWIGTYPE_p_int maxValue) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport1_GetGPUMemoryTemperatureRange(swigCPtr, SWIGTYPE_p_int.getCPtr(minValue), SWIGTYPE_p_int.getCPtr(maxValue));
    return ret;
  }

  public virtual ADLX_RESULT IsSupportedNPUFrequency(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport1_IsSupportedNPUFrequency(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT GetNPUFrequencyRange(SWIGTYPE_p_int minValue, SWIGTYPE_p_int maxValue) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport1_GetNPUFrequencyRange(swigCPtr, SWIGTYPE_p_int.getCPtr(minValue), SWIGTYPE_p_int.getCPtr(maxValue));
    return ret;
  }

  public virtual ADLX_RESULT IsSupportedNPUActivityLevel(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport1_IsSupportedNPUActivityLevel(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT GetNPUActivityLevelRange(SWIGTYPE_p_int minValue, SWIGTYPE_p_int maxValue) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport1_GetNPUActivityLevelRange(swigCPtr, SWIGTYPE_p_int.getCPtr(minValue), SWIGTYPE_p_int.getCPtr(maxValue));
    return ret;
  }

}
