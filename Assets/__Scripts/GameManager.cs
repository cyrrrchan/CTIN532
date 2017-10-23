using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool isListening;
    public AudioClip VO_welcome;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = VO_welcome;
        audioSource.Play();

        //AkSoundEngine.PostEvent("VO_Welcome", gameObject, AK_EndOfEvent, MyCallbackFunction, myCookieObject);
        isListening = true;
		//Debug.Log (isListening);
    }
	
	// Update is called once per frame
	void Update () {

        if(audioSource.isPlaying)
        {
            isListening = true;
        }
    }

	/*void MyCallbackFunction(object in_cookie, AkCallbackType in_type, object in_info) {

		if (in_type == AK_EndOfEvent) {
			AkEventCallbackInfo info = (AkEventCallbackInfo)in_info; //Then do stuff.
            isListening = false;
			//Debug.Log (isListening);
		}
	}*/
}
