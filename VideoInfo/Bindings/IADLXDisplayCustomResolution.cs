//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.3.0
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class IADLXDisplayCustomResolution : IADLXInterface {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal IADLXDisplayCustomResolution(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ADLXPINVOKE.IADLXDisplayCustomResolution_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IADLXDisplayCustomResolution obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IADLXDisplayCustomResolution obj) {
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
          ADLXPINVOKE.delete_IADLXDisplayCustomResolution(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public new static SWIGTYPE_p_wchar_t IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLXDisplayCustomResolution_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public virtual ADLX_RESULT IsSupported(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomResolution_IsSupported(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT GetResolutionList(SWIGTYPE_p_p_adlx__IADLXDisplayResolutionList ppResolutionList) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomResolution_GetResolutionList(swigCPtr, SWIGTYPE_p_p_adlx__IADLXDisplayResolutionList.getCPtr(ppResolutionList));
    return ret;
  }

  public virtual ADLX_RESULT GetCurrentAppliedResolution(SWIGTYPE_p_p_adlx__IADLXDisplayResolution ppResolution) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomResolution_GetCurrentAppliedResolution(swigCPtr, SWIGTYPE_p_p_adlx__IADLXDisplayResolution.getCPtr(ppResolution));
    return ret;
  }

  public virtual ADLX_RESULT CreateNewResolution(IADLXDisplayResolution pResolution) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomResolution_CreateNewResolution(swigCPtr, IADLXDisplayResolution.getCPtr(pResolution));
    return ret;
  }

  public virtual ADLX_RESULT DeleteResolution(IADLXDisplayResolution pResolution) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomResolution_DeleteResolution(swigCPtr, IADLXDisplayResolution.getCPtr(pResolution));
    return ret;
  }

}
