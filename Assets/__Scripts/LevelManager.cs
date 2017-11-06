using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public static LevelManager levelManagerInstance = null;

    public GameObject door;
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

    public float offset;
    public float speed; //speed of door opening and closing

    private float t = 0.0f;

    float db; // volume
    float count = 0.0f;
    float duration = 1.0f; //how many seconds before UI disappears
    private float meterFilled = 0.0f;

    private bool charged = false;
    private bool humMode = false; //toggle humming UI on/off
    public bool doorOpened = false;
    public bool inTrigger = false;
    //private bool firstTimeHumAttemptMetric = false;

    void Awake()
    {
        if (levelManagerInstance == null)
        {
            levelManagerInstance = this;
        }
        else if (levelManagerInstance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        if (audioInputObject == null)
            audioInputObject = GameObject.Find(Microphone.devices[0]);
        micIn = (MicrophoneInput)audioInputObject.GetComponent("MicrophoneInput");

        humUI.fillAmount = 0.0f;
        humText.SetActive(false);
        chargedUI.SetActive(false);
        chargedText.SetActive(false);
    }

    void Update()
    {
        db = micIn.loudness; //set db to be volume from player input

        if (humMode == true && !doorOpened && !GameObject.Find("GameManager").GetComponent<AudioManager>().isListening)
        {
            if (db > dbThreshold && charged == false) // play sound and fill UI if loud enough
            {
                /*if (!firstTimeHumAttemptMetric)
                {
                    firstTimeHumAttemptMetric = true;
                    MetricManagerScript._metricsInstance.LogTime("Dark Room Door Started: ");
                }*/

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
                /*MetricManagerScript._metricsInstance.LogTime("Dark Room Door Ended: ");*/
            }

            if (charged == true && doorOpened == false) // open the door
            {
                door.GetComponent<BoxCollider>().enabled = false;
                door.transform.position = new Vector3(door.transform.position.x, Mathf.Lerp(door.transform.position.y, door.transform.position.y + offset, t), door.transform.position.z);
                t += Time.deltaTime * speed;
                count += Time.deltaTime;

                if (count >= duration)
                {
                    chargedText.SetActive(false);
                    chargedUI.SetActive(false);
                    count = 0.0f;
                    t = 0.0f;
                    humMode = false;
                    charged = false;
                    doorOpened = true;

                    LoadLevel(levelName);
                }
            }
        }
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    private void OnTriggerEnter(Collider other) //turn on UI when inside collider
    {

        if (!doorOpened && !GameObject.Find("GameManager").GetComponent<AudioManager>().isListening)
        {
            inTrigger = true;
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
}
