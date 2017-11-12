using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PylonCharger : MonoBehaviour {

    public GameObject audioInputObject; //microphoneInput object
    MicrophoneInput micIn;
    public Image humUI;
    public GameObject humText;
    public GameObject chargedUI;
    public GameObject chargedText;
    public string levelName;

    public float dbThreshold;
    public float humTime;
    public float cooldownTime;

    private string sceneName;
    private float t = 0.0f;

    float db; // volume
    float count = 0.0f;
    float duration = 1.0f; //how many seconds before UI disappears
    private float meterFilled = 0.0f;
    [SerializeField] GameObject star5;

    public bool charged = false;
    private bool humMode = false; //toggle humming UI on/off
    public bool inTrigger = false;
    private bool isSecondDarkRoom = false;
    public bool isLastScene = false;

    // Shame // Shame
    GameManager _GameManagerPylonIsTriggered;
    AudioManager _AudioManagerIsListening;


    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        MetricManagerScript._metricsInstance.LogTime(sceneName + " Started: ");

        _AudioManagerIsListening = GameObject.Find("GameManager").GetComponent<AudioManager>();

        if (audioInputObject == null)
            audioInputObject = GameObject.Find(Microphone.devices[0]);
        micIn = (MicrophoneInput)audioInputObject.GetComponent("MicrophoneInput");

        if (sceneName == "DarkRoom2")
        {
            isSecondDarkRoom = true;
            _GameManagerPylonIsTriggered = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        else if (sceneName == "WaitingRoom4")
        {
            isLastScene = true;
        }

        humUI.fillAmount = 0.0f;
        humText.SetActive(false);
        chargedUI.SetActive(false);
        chargedText.SetActive(false);
    }

    void Update()
    {
        db = micIn.loudness; //set db to be volume from player input

        if (humMode == true && isSecondDarkRoom)
            SecondDarkRoom();

        else if (humMode == true && !isSecondDarkRoom)
        {
            if (db > dbThreshold && charged == false) // play sound and fill UI if loud enough
            {
                AkSoundEngine.PostEvent("Charging", gameObject);

                humUI.fillAmount += Time.deltaTime / humTime;
            }

            if (db < dbThreshold && charged == false) // pause sound and cooldown UI if too soft
            {
                AkSoundEngine.PostEvent("Charging_Pause", gameObject);

                humUI.fillAmount -= Time.deltaTime / cooldownTime;
            }

            if (humUI.fillAmount == 1.0f) // done charging
            {
                AkSoundEngine.PostEvent("Charging_Stop", gameObject);
                AkSoundEngine.PostEvent("Success", gameObject);

                humText.SetActive(false);
                humUI.fillAmount = 0.0f;
                chargedText.SetActive(true);
                chargedUI.SetActive(true);

                charged = true;
            }

            if (charged == true && !isLastScene) 
            {
                count += Time.deltaTime;

                if (count >= duration)
                {
                    chargedText.SetActive(false);
                    chargedUI.SetActive(false);
                    count = 0.0f;
                    t = 0.0f;
                    humMode = false;
                    charged = false;
                    MetricManagerScript._metricsInstance.LogTime(sceneName + " Ended: ");
                    SceneManager.LoadScene(levelName);
                }
            }

            if (charged == true && isLastScene) //if last scene play death VO and add last pentagram
            {
                star5.SetActive(true);
                count += Time.deltaTime;

                if (count >= duration)
                {
                    chargedText.SetActive(false);
                    chargedUI.SetActive(false);
                    count = 0.0f;
                    t = 0.0f;
                    humMode = false;
                }
            }
        }

        if (charged && isLastScene && !_AudioManagerIsListening.isListening && _AudioManagerIsListening.hasPlayedPlayerDeathVO)
        {
            duration = 1.0f;
            count += Time.deltaTime;

            if (count >= duration)
            {
                //here is where we should add a coroutine to fade in the pentagram
				SceneManager.LoadScene(levelName);
            }
        }
    }

    private void SecondDarkRoom () //make sure both pylons are charged before loading level
    {
        if (db > dbThreshold && charged == false) // play sound and fill UI if loud enough
        {
            AkSoundEngine.PostEvent("Charging", gameObject);

            humUI.fillAmount += Time.deltaTime / humTime;
        }

        if (db < dbThreshold && charged == false) // pause sound and cooldown UI if too soft
        {
            AkSoundEngine.PostEvent("Charging_Pause", gameObject);

            humUI.fillAmount -= Time.deltaTime / cooldownTime;
        }

        if (humUI.fillAmount == 1.0f) // done charging
        {
            AkSoundEngine.PostEvent("Charging_Stop", gameObject);
            AkSoundEngine.PostEvent("Success", gameObject);

            humText.SetActive(false);
            humUI.fillAmount = 0.0f;
            chargedText.SetActive(true);
            chargedUI.SetActive(true);

            charged = true;
        }

        if (charged == true)
        {
            if (gameObject.name == "PylonTrigger2a")
                _GameManagerPylonIsTriggered.aPylonIsTriggered = true;

            else if (gameObject.name == "PylonTrigger2b")
                _GameManagerPylonIsTriggered.bPylonIsTriggered = true;

            count += Time.deltaTime;

            if (count >= duration)
            {
                chargedText.SetActive(false);
                chargedUI.SetActive(false);
                count = 0.0f;
                t = 0.0f;
                humMode = false;
                charged = false;

                if(_GameManagerPylonIsTriggered.aPylonIsTriggered && _GameManagerPylonIsTriggered.bPylonIsTriggered)
                {
                    MetricManagerScript._metricsInstance.LogTime(sceneName + " Ended: ");
                    SceneManager.LoadScene(levelName);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) //turn on UI when inside collider
    {
        inTrigger = true;

        if (!charged && ((gameObject.name == "PylonTrigger2a" && !_GameManagerPylonIsTriggered.aPylonIsTriggered) || (gameObject.name == "PylonTrigger2b" && !_GameManagerPylonIsTriggered.bPylonIsTriggered)))
        {
            count = 0.0f;
            humMode = true;
            humText.SetActive(true);
            humUI.fillAmount = meterFilled;
        }

        else if (!charged && gameObject.name != "PylonTrigger2b")
        {
            count = 0.0f;
            humMode = true;
            humText.SetActive(true);
            humUI.fillAmount = meterFilled;
        }
    }

    private void OnTriggerExit(Collider other) // turn off UI when outside of collider
    {
        humText.SetActive(false);
        humUI.fillAmount = 0.0f;
        humMode = false;

        AkSoundEngine.PostEvent("Charging_Stop", gameObject);
    }

	void IlluminateFloorPentagram(){
		//I'm putting in this example script in comments to remember later
		//iTween.FadeTo (finalPentagramLine, 255, 300f);
		//wait
		//load final scene
	}
}
