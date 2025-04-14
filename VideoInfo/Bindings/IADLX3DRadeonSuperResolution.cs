//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.3.0
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class IADLX3DRadeonSuperResolution : IADLXInterface {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal IADLX3DRadeonSuperResolution(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ADLXPINVOKE.IADLX3DRadeonSuperResolution_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IADLX3DRadeonSuperResolution obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IADLX3DRadeonSuperResolution obj) {
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
          ADLXPINVOKE.delete_IADLX3DRadeonSuperResolution(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public new static SWIGTYPE_p_wchar_t IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLX3DRadeonSuperResolution_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public virtual ADLX_RESULT IsSupported(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLX3DRadeonSuperResolution_IsSupported(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT IsEnabled(SWIGTYPE_p_bool enabled) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLX3DRadeonSuperResolution_IsEnabled(swigCPtr, SWIGTYPE_p_bool.getCPtr(enabled));
    return ret;
  }

  public virtual ADLX_RESULT SetEnabled(bool enable) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLX3DRadeonSuperResolution_SetEnabled(swigCPtr, enable);
    return ret;
  }

  public virtual ADLX_RESULT GetSharpnessRange(ADLX_IntRange range) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLX3DRadeonSuperResolution_GetSharpnessRange(swigCPtr, ADLX_IntRange.getCPtr(range));
    return ret;
  }

  public virtual ADLX_RESULT GetSharpness(SWIGTYPE_p_int currentSharpness) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLX3DRadeonSuperResolution_GetSharpness(swigCPtr, SWIGTYPE_p_int.getCPtr(currentSharpness));
    return ret;
  }

  public virtual ADLX_RESULT SetSharpness(int sharpness) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLX3DRadeonSuperResolution_SetSharpness(swigCPtr, sharpness);
    return ret;
  }

}
