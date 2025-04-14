//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.3.0
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class IADLXGPUTuningChangedEvent : IADLXChangedEvent {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal IADLXGPUTuningChangedEvent(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ADLXPINVOKE.IADLXGPUTuningChangedEvent_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IADLXGPUTuningChangedEvent obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IADLXGPUTuningChangedEvent obj) {
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
          ADLXPINVOKE.delete_IADLXGPUTuningChangedEvent(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public new static SWIGTYPE_p_wchar_t IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLXGPUTuningChangedEvent_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public virtual ADLX_RESULT GetGPU(SWIGTYPE_p_p_adlx__IADLXGPU ppGPU) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUTuningChangedEvent_GetGPU(swigCPtr, SWIGTYPE_p_p_adlx__IADLXGPU.getCPtr(ppGPU));
    return ret;
  }

  public virtual bool IsAutomaticTuningChanged() {
    bool ret = ADLXPINVOKE.IADLXGPUTuningChangedEvent_IsAutomaticTuningChanged(swigCPtr);
    return ret;
  }

  public virtual bool IsPresetTuningChanged() {
    bool ret = ADLXPINVOKE.IADLXGPUTuningChangedEvent_IsPresetTuningChanged(swigCPtr);
    return ret;
  }

  public virtual bool IsManualGPUCLKTuningChanged() {
    bool ret = ADLXPINVOKE.IADLXGPUTuningChangedEvent_IsManualGPUCLKTuningChanged(swigCPtr);
    return ret;
  }

  public virtual bool IsManualVRAMTuningChanged() {
    bool ret = ADLXPINVOKE.IADLXGPUTuningChangedEvent_IsManualVRAMTuningChanged(swigCPtr);
    return ret;
  }

  public virtual bool IsManualFanTuningChanged() {
    bool ret = ADLXPINVOKE.IADLXGPUTuningChangedEvent_IsManualFanTuningChanged(swigCPtr);
    return ret;
  }

  public virtual bool IsManualPowerTuningChanged() {
    bool ret = ADLXPINVOKE.IADLXGPUTuningChangedEvent_IsManualPowerTuningChanged(swigCPtr);
    return ret;
  }

}
