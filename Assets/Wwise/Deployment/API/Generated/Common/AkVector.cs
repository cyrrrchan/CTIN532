#if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.
/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.11
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */


using System;
using System.Runtime.InteropServices;

public class AkVector : IDisposable {
  private IntPtr swigCPtr;
  protected bool swigCMemOwn;

  internal AkVector(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = cPtr;
  }

  internal static IntPtr getCPtr(AkVector obj) {
    return (obj == null) ? IntPtr.Zero : obj.swigCPtr;
  }

  ~AkVector() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          AkSoundEnginePINVOKE.CSharp_delete_AkVector(swigCPtr);
        }
        swigCPtr = IntPtr.Zero;
      }
      GC.SuppressFinalize(this);
    }
  }

  public float X {
    set {
      AkSoundEnginePINVOKE.CSharp_AkVector_X_set(swigCPtr, value);
    } 
    get {
      float ret = AkSoundEnginePINVOKE.CSharp_AkVector_X_get(swigCPtr);
      return ret;
    } 
  }

  public float Y {
    set {
      AkSoundEnginePINVOKE.CSharp_AkVector_Y_set(swigCPtr, value);
    } 
    get {
      float ret = AkSoundEnginePINVOKE.CSharp_AkVector_Y_get(swigCPtr);
      return ret;
    } 
  }

  public float Z {
    set {
      AkSoundEnginePINVOKE.CSharp_AkVector_Z_set(swigCPtr, value);
    } 
    get {
      float ret = AkSoundEnginePINVOKE.CSharp_AkVector_Z_get(swigCPtr);
      return ret;
    } 
  }

  public AkVector() : this(AkSoundEnginePINVOKE.CSharp_new_AkVector(), true) {

  }

}
#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.