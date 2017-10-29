﻿using System.Collections;
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

    private float t = 0.0f;

    float db; // volume
    float count = 0.0f;
    float duration = 1.0f; //how many seconds before UI disappears
    private float meterFilled = 0.0f;

    private bool charged = false;
    private bool humMode = false; //toggle humming UI on/off
    public bool inTrigger = false;
    private bool isSecondDarkRoom = false;

    void Start()
    {
        if (audioInputObject == null)
            audioInputObject = GameObject.Find(Microphone.devices[0]);
        micIn = (MicrophoneInput)audioInputObject.GetComponent("MicrophoneInput");

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "DarkRoom2")
            isSecondDarkRoom = true;

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

        if (humMode == true && !isSecondDarkRoom)
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
                count += Time.deltaTime;

                if (count >= duration)
                {
                    chargedText.SetActive(false);
                    chargedUI.SetActive(false);
                    count = 0.0f;
                    t = 0.0f;
                    humMode = false;
                    charged = false;
                    SceneManager.LoadScene(levelName);
                }
            }
        }
    }

    private void SecondDarkRoom ()
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
            MetricManagerScript._metricsInstance.LogTime("Pylon Ended: ");
        }

        if (charged == true)
        {
            if (gameObject.name == "PylonTrigger2a")
                GameObject.Find("GameManager").GetComponent<GameManager>().aPylonIsTriggered = true;

            if (gameObject.name == "PylonTrigger2b")
                GameObject.Find("GameManager").GetComponent<GameManager>().bPylonIsTriggered = true;

            count += Time.deltaTime;

            if (count >= duration)
            {
                chargedText.SetActive(false);
                chargedUI.SetActive(false);
                count = 0.0f;
                t = 0.0f;
                humMode = false;
                charged = false;

                if(GameObject.Find("GameManager").GetComponent<GameManager>().aPylonIsTriggered && GameObject.Find("GameManager").GetComponent<GameManager>().bPylonIsTriggered)
                    SceneManager.LoadScene(levelName);
            }
        }
    }

    private void OnTriggerEnter(Collider other) //turn on UI when inside collider
    {
            inTrigger = true;
            count = 0.0f;
            humMode = true;
            humText.SetActive(true);
            humUI.fillAmount = meterFilled;
    }

    private void OnTriggerExit(Collider other) // turn off UI when outside of collider
    {
        humText.SetActive(false);
        humUI.fillAmount = 0.0f;
        humMode = false;

        AkSoundEngine.PostEvent("Charging_Stop", gameObject);
    }
}