using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

		AkSoundEngine.PostEvent("VO_Welcome", gameObject, AK_EndOfEvent, MyCallbackFunction, myCookieObject);
		GameObject.Find("FPSController").GetComponent<FirstPersonController>().isListening;
		//Debug.Log (isListening);
    }
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("GlowingPanel").GetComponent<GlowingPanelCollider> ().charged) {
			AkSoundEngine.PostEvent ("VO_HumIntro", gameObject, AK_EndOfEvent, MyCallbackFunction, myCookieObject); 
			GameObject.Find ("FPSController").GetComponent<FirstPersonController> ().isListening;
			//Debug.Log (isListening);
		}
    }

	void MyCallbackFunction(object in_cookie, AkCallbackType in_type, object in_info) {

		if (in_type == AK_EndOfEvent) {
			AkEventCallbackInfo info = (AkEventCallbackInfo)in_info; //Then do stuff.
			!GameObject.Find("FPSController").GetComponent<FirstPersonController>().isListening;
			//Debug.Log (isListening);
		}
	}
}
