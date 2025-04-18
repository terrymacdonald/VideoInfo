//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.3.0
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class IADLXDisplayCustomColor : IADLXInterface {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal IADLXDisplayCustomColor(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ADLXPINVOKE.IADLXDisplayCustomColor_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IADLXDisplayCustomColor obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IADLXDisplayCustomColor obj) {
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
          ADLXPINVOKE.delete_IADLXDisplayCustomColor(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public new static SWIGTYPE_p_wchar_t IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLXDisplayCustomColor_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public virtual ADLX_RESULT IsHueSupported(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_IsHueSupported(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT GetHueRange(ADLX_IntRange range) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_GetHueRange(swigCPtr, ADLX_IntRange.getCPtr(range));
    return ret;
  }

  public virtual ADLX_RESULT GetHue(SWIGTYPE_p_int currentHue) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_GetHue(swigCPtr, SWIGTYPE_p_int.getCPtr(currentHue));
    return ret;
  }

  public virtual ADLX_RESULT SetHue(int hue) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_SetHue(swigCPtr, hue);
    return ret;
  }

  public virtual ADLX_RESULT IsSaturationSupported(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_IsSaturationSupported(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT GetSaturationRange(ADLX_IntRange range) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_GetSaturationRange(swigCPtr, ADLX_IntRange.getCPtr(range));
    return ret;
  }

  public virtual ADLX_RESULT GetSaturation(SWIGTYPE_p_int currentSaturation) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_GetSaturation(swigCPtr, SWIGTYPE_p_int.getCPtr(currentSaturation));
    return ret;
  }

  public virtual ADLX_RESULT SetSaturation(int saturation) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_SetSaturation(swigCPtr, saturation);
    return ret;
  }

  public virtual ADLX_RESULT IsBrightnessSupported(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_IsBrightnessSupported(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT GetBrightnessRange(ADLX_IntRange range) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_GetBrightnessRange(swigCPtr, ADLX_IntRange.getCPtr(range));
    return ret;
  }

  public virtual ADLX_RESULT GetBrightness(SWIGTYPE_p_int currentBrightness) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_GetBrightness(swigCPtr, SWIGTYPE_p_int.getCPtr(currentBrightness));
    return ret;
  }

  public virtual ADLX_RESULT SetBrightness(int brightness) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_SetBrightness(swigCPtr, brightness);
    return ret;
  }

  public virtual ADLX_RESULT IsContrastSupported(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_IsContrastSupported(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT GetContrastRange(ADLX_IntRange range) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_GetContrastRange(swigCPtr, ADLX_IntRange.getCPtr(range));
    return ret;
  }

  public virtual ADLX_RESULT GetContrast(SWIGTYPE_p_int currentContrast) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_GetContrast(swigCPtr, SWIGTYPE_p_int.getCPtr(currentContrast));
    return ret;
  }

  public virtual ADLX_RESULT SetContrast(int contrast) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_SetContrast(swigCPtr, contrast);
    return ret;
  }

  public virtual ADLX_RESULT IsTemperatureSupported(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_IsTemperatureSupported(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT GetTemperatureRange(ADLX_IntRange range) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_GetTemperatureRange(swigCPtr, ADLX_IntRange.getCPtr(range));
    return ret;
  }

  public virtual ADLX_RESULT GetTemperature(SWIGTYPE_p_int currentTemperature) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_GetTemperature(swigCPtr, SWIGTYPE_p_int.getCPtr(currentTemperature));
    return ret;
  }

  public virtual ADLX_RESULT SetTemperature(int temperature) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayCustomColor_SetTemperature(swigCPtr, temperature);
    return ret;
  }

}
