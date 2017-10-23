using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool isListening;
    public AudioClip welcomeVO;
    public AudioClip humIntroVO;
    public AudioClip humIntroVO2;
    public AudioClip doorOpenVO;
    public AudioClip powerOutageVO;

    private AudioSource audioSource;
    private bool hasPlayedHumIntroVO = false;
    private bool hasPlayedHumIntroVO2 = false;
    private bool hasPlayedDoorVO = false;
    private bool hasPlayedPowerOutageVO = false;

    float count = 0.0f;
    float duration = 1.0f;

    public bool hasEndedDoorVO = false;

	// Use this for initialization
	void Start () {

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = welcomeVO;
        audioSource.PlayOneShot(welcomeVO);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E))
            audioSource.Stop();

        if(audioSource.isPlaying)
            isListening = true;

        if (!audioSource.isPlaying)
            isListening = false;

        if (!isListening)
        {
            if (GameObject.Find("GlowingPanel").GetComponent<GlowingPanelCollider>().activated && !hasPlayedHumIntroVO)
            {
                audioSource.PlayOneShot(humIntroVO);
                hasPlayedHumIntroVO = true;
            }

            if (GameObject.Find("Trigger").GetComponent<OpenDoor>().inTrigger && !hasPlayedHumIntroVO2)
            {
                audioSource.PlayOneShot(humIntroVO2);
                hasPlayedHumIntroVO2 = true;
            }

            if (GameObject.Find("Trigger").GetComponent<OpenDoor>().doorOpened && !hasPlayedDoorVO)
            {
                audioSource.PlayOneShot(doorOpenVO);
                hasPlayedDoorVO = true;
            }

            if (GameObject.Find("LightingManager").GetComponent<LightingChanger>().hasTurnedOff && !hasPlayedPowerOutageVO)
            {
                count += Time.deltaTime;

                if (count >= duration)
                {
                    audioSource.PlayOneShot(powerOutageVO);
                    hasPlayedPowerOutageVO = true;
                    count = 0.0f;
                    duration = 3.0f;
                }
            }

            if (hasPlayedDoorVO)
                hasEndedDoorVO = true;

            if(hasPlayedPowerOutageVO)
            {
                count += Time.deltaTime;

                if (count >= duration)
                {
                    count = 0.0f;
                    duration = 1.0f;
                    LoadEndScreen();
                }
            }
        }
    }

    void LoadEndScreen()
    {
        SceneManager.LoadScene("Start");
    }

	/*void MyCallbackFunction(object in_cookie, AkCallbackType in_type, object in_info) {

		if (in_type == AK_EndOfEvent) {
			AkEventCallbackInfo info = (AkEventCallbackInfo)in_info; //Then do stuff.
            isListening = false;
			//Debug.Log (isListening);
		}
	}*/
}
