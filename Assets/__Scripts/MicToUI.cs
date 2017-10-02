using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicToUI : MonoBehaviour {

    public GameObject audioInputObject; //microphoneInput object
    MicrophoneInput micIn;
    public Image humUI;
    public GameObject humText;
    public GameObject chargedUI;
    public GameObject chargedText;

    //public float lThreshold;
    public float dbThreshold;
    public float humTime;
    public float cooldownTime;

    /*[SerializeField] AnimationCurve sweetSpotCurve;
    [SerializeField] Color currentColor;
    [SerializeField] Color desiredColor;
    [SerializeField] Image rend;*/

    float db; // volume
    float count = 0.0f;
    float duration = 2.0f;
    private float meterFilled = 0.0f;

    private bool charged;
    private bool humMode; //toggle humming UI on/off

    void Start()
    {
        if (audioInputObject == null)
            audioInputObject = GameObject.Find(Microphone.devices[0]);
        micIn = (MicrophoneInput)audioInputObject.GetComponent("MicrophoneInput");

        humUI.fillAmount = 0.0f;
        humText.SetActive(false);
        chargedUI.SetActive(false);
        chargedText.SetActive(false);

        charged = false;
        humMode = false;
    }

    void Update()
    {
        db = micIn.loudness; //set db to be volume from player input

        //Debug.Log(db);

        if (humMode == false && Input.GetKeyDown(KeyCode.E)) //turn on hum mode
        {
            count = 0.0f;
            humMode = true;
            humText.SetActive(true);
            humUI.fillAmount = meterFilled;
        }

        if (humMode == true && Input.GetKeyDown(KeyCode.R)) //turn off hum mode
        {
            humText.SetActive(false);
            humUI.fillAmount = 0.0f;
            humMode = false;

            AkSoundEngine.PostEvent("Charging_Stop", gameObject);
            //Debug.Log("off");
        }

        if (humMode == true)
        {
            if (db > dbThreshold && charged == false)
            {
                AkSoundEngine.PostEvent("Charging", gameObject);
                //l = Mathf.Clamp(l, minFreq, maxFreq);

                humUI.fillAmount += Time.deltaTime / humTime;
            }

            if (db < dbThreshold && charged == false)
            {
                AkSoundEngine.PostEvent("Charging_Pause", gameObject);

                humUI.fillAmount -= Time.deltaTime / cooldownTime;
            }

            if (humUI.fillAmount == 1.0f)
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
                count += Time.deltaTime;

                if (count >= duration)
                {
                    chargedText.SetActive(false);
                    chargedUI.SetActive(false);
                    count = 0.0f;
                    humMode = false;
                    charged = false;
                }
            }
        }
    }

}
