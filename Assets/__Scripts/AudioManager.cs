using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

    public string levelName;
    public bool isListening;

    public bool isStartingScene; //bools for starting scene
    public bool hasPlayedWelcomeVO = false;
    private bool hasPlayedEyeScanVO = false;
    private bool hasPlayedHumIntroVO = false;
    private bool hasPlayedHumIntroVO2 = false;
    private bool hasPlayedDoorVO = false;
    private bool hasPlayedPowerOutageVO = false;
    private bool hasPlayedNewInstructionsVO = false;

    private bool hasPlayedWaitingRoom2VO = false; //bools for after Dark Room 1

    private bool hasPlayedWaitingRoom3VO = false; //bools for after Dark Room 2

    public bool hasPlayedPlayerDeathVO = false; //bools for after Dark Room 3


    private string sceneName;
    float count = 0.0f;
    float duration = 1.0f;

    public bool hasEndedDoorVO = false;

	// Shame // // Shame //
	StartDoor _Trigger0StartDoor;
	GlowingPanelCollider _GlowingPanelGlowingPanelCollider;
	OpenDoor _TriggerOpenDoor;
	LightingChanger _LightingManagerLightingChanger;
	GameManager _TriggerWR2GameManager;
	GameManager _TriggerWR3GameManager;
	PylonCharger _PylonTrigger4PylonCharger;
	// Shame // // Shame //

	// Use this for initialization
	void Start () {
        Scene scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
		if (sceneName == "Main") {
			_Trigger0StartDoor = GameObject.Find ("Trigger0").GetComponent<StartDoor> ();
			_GlowingPanelGlowingPanelCollider = GameObject.Find ("GlowingPanel").GetComponent<GlowingPanelCollider> ();
			_TriggerOpenDoor = GameObject.Find ("Trigger").GetComponent<OpenDoor> ();
			_LightingManagerLightingChanger = GameObject.Find ("LightingManager").GetComponent<LightingChanger> ();
			isStartingScene = true;
		}
        else //set variables for after Dark Room 1
        {
            hasPlayedWelcomeVO = true;
            hasPlayedEyeScanVO = true;
            hasPlayedHumIntroVO = true;
            hasPlayedHumIntroVO2 = true;
            hasPlayedDoorVO = true;
            hasPlayedPowerOutageVO = true;
            hasPlayedNewInstructionsVO = true;
			if (sceneName == "WaitingRoom2") {
				_TriggerWR2GameManager = GameObject.Find ("TriggerWR2").GetComponent<GameManager> ();
			}
			else if (sceneName == "WaitingRoom3") {
				hasPlayedWaitingRoom2VO = true;
				_TriggerWR3GameManager = GameObject.Find ("TriggerWR3").GetComponent<GameManager> ();
			}
            else if (sceneName == "WaitingRoom4") //set variables for after Dark Room 3
            {	
				hasPlayedWaitingRoom2VO = true;
                hasPlayedWaitingRoom3VO = true;
				_PylonTrigger4PylonCharger = GameObject.Find ("PylonTrigger4").GetComponent<PylonCharger> ();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P)) //skip to next scene
            SceneManager.LoadScene(levelName);
        //AkSoundEngine.PostEvent("VO_Stop", gameObject);

        //if(audioSource.isPlaying)
        //isListening = true;

        //if (!audioSource.isPlaying)
        //isListening = false;

        if (isListening == false)
        {
            if (isStartingScene)
                WaitingRoom1();

            else if (sceneName == "WaitingRoom2")
                WaitingRoom2();

            else if (sceneName == "WaitingRoom3")
                WaitingRoom3();

            else if (sceneName == "WaitingRoom4")
                WaitingRoom4();
        }
    }

	void CheckWhenFinished(object in_cookie, AkCallbackType in_type, object in_info) {

		if (in_type == AkCallbackType.AK_EndOfEvent) {
			AkEventCallbackInfo info = (AkEventCallbackInfo)in_info; //Then do stuff.
            if (isStartingScene)
                hasPlayedWelcomeVO = true;

            else if (sceneName == "WaitingRoom2")
                hasPlayedWaitingRoom2VO = true;

            else if (sceneName == "WaitingRoom3")
                hasPlayedWaitingRoom3VO = true;

            isListening = false;
		}
	}


    void WaitingRoom1()
    {
		if (isStartingScene && !hasPlayedWelcomeVO && _Trigger0StartDoor.stepThroughDoor) //play welcome VO
        {
            object myCookie = new object();
            isListening = true;
            AkSoundEngine.PostEvent("VO_Welcome", gameObject, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
        }

		if (_GlowingPanelGlowingPanelCollider.inTrigger && !hasPlayedEyeScanVO) //play VO while eye scan
        {
            object myCookie = new object();
            isListening = true;
            AkSoundEngine.PostEvent("VO_EyeScan", gameObject, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
            hasPlayedEyeScanVO = true;
        }

		if (_GlowingPanelGlowingPanelCollider.activated && !hasPlayedHumIntroVO) //play VO after finish eye scan
        {
            //audioSource.PlayOneShot(humIntroVO);
            object myCookie = new object();
            isListening = true;
            AkSoundEngine.PostEvent("VO_HumIntro", gameObject, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
            hasPlayedHumIntroVO = true;
        }

		if (_TriggerOpenDoor.inTrigger && !hasPlayedHumIntroVO2) //play VO when in front of door
        {
            //audioSource.PlayOneShot(humIntroVO2);
            object myCookie = new object();
            isListening = true;
            AkSoundEngine.PostEvent("VO_HumDoor", gameObject, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
            hasPlayedHumIntroVO2 = true;
        }

		if (_TriggerOpenDoor.doorOpened && !hasPlayedDoorVO) //play VO after open door
        {
            //audioSource.PlayOneShot(doorOpenVO);
            object myCookie = new object();
            isListening = true;
            AkSoundEngine.PostEvent("VO_DoorOpen", gameObject, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
            hasPlayedDoorVO = true;
        }

		if (_LightingManagerLightingChanger.hasTurnedOff && !hasPlayedPowerOutageVO) //play VO after lights turn off
        {
            count += Time.deltaTime;

            if (count >= duration)
            {
                //audioSource.PlayOneShot(powerOutageVO);
                object myCookie = new object();
                isListening = true;
                AkSoundEngine.PostEvent("VO_PowerOutage", gameObject, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                hasPlayedPowerOutageVO = true;
                count = 0.0f;
                duration = 3.0f; //set duration until new instructions VO
            }
        }

        if (hasPlayedDoorVO)
            hasEndedDoorVO = true;

        if (hasPlayedPowerOutageVO && !hasPlayedNewInstructionsVO) //play new instructions VO
        {
            count += Time.deltaTime;

            if (count >= duration)
            {
                object myCookie = new object();
                isListening = true;
                AkSoundEngine.PostEvent("VO_NewInstructions", gameObject, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                hasPlayedNewInstructionsVO = true;
                count = 0.0f;
                duration = 1.0f;
            }
        }
    }



    void WaitingRoom2()
    {
		if (sceneName == "WaitingRoom2" && !hasPlayedWaitingRoom2VO && _TriggerWR2GameManager.stepThroughDoor_WR2) //after Dark Room 1
        {
            object myCookie = new object();
            isListening = true;
            AkSoundEngine.PostEvent("VO_WaitingRoom1", gameObject, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
        }
    }

    void WaitingRoom3()
    {
		if (sceneName == "WaitingRoom3" && !hasPlayedWaitingRoom3VO && _TriggerWR3GameManager.stepThroughDoor_WR3) //after Dark Room 2
        {
            object myCookie = new object();
            isListening = true;
            AkSoundEngine.PostEvent("VO_WaitingRoom2", gameObject, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
        }
    }

    void WaitingRoom4()
    {
		if (sceneName == "WaitingRoom4" && !hasPlayedPlayerDeathVO &&  _PylonTrigger4PylonCharger.charged) //after Dark Room 3
        {
            object myCookie = new object();
            isListening = true;
            AkSoundEngine.PostEvent("VO_PlayerDeath", gameObject, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
            hasPlayedPlayerDeathVO = true;
        }
    }
}
