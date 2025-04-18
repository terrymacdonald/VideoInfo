//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.3.0
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class IADLXDisplayGammaChangedEvent : IADLXChangedEvent {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal IADLXDisplayGammaChangedEvent(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ADLXPINVOKE.IADLXDisplayGammaChangedEvent_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IADLXDisplayGammaChangedEvent obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IADLXDisplayGammaChangedEvent obj) {
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
          ADLXPINVOKE.delete_IADLXDisplayGammaChangedEvent(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public new static SWIGTYPE_p_wchar_t IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLXDisplayGammaChangedEvent_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public virtual ADLX_RESULT GetDisplay(SWIGTYPE_p_p_adlx__IADLXDisplay ppDisplay) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayGammaChangedEvent_GetDisplay(swigCPtr, SWIGTYPE_p_p_adlx__IADLXDisplay.getCPtr(ppDisplay));
    return ret;
  }

  public virtual bool IsGammaRampChanged() {
    bool ret = ADLXPINVOKE.IADLXDisplayGammaChangedEvent_IsGammaRampChanged(swigCPtr);
    return ret;
  }

  public virtual bool IsGammaCoefficientChanged() {
    bool ret = ADLXPINVOKE.IADLXDisplayGammaChangedEvent_IsGammaCoefficientChanged(swigCPtr);
    return ret;
  }

  public virtual bool IsReGammaChanged() {
    bool ret = ADLXPINVOKE.IADLXDisplayGammaChangedEvent_IsReGammaChanged(swigCPtr);
    return ret;
  }

  public virtual bool IsDeGammaChanged() {
    bool ret = ADLXPINVOKE.IADLXDisplayGammaChangedEvent_IsDeGammaChanged(swigCPtr);
    return ret;
  }

}
