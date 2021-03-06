﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowingPanelCollider : MonoBehaviour {

	[SerializeField] GameObject glowingPanel;
	private Renderer glowingPanelRenderer;
	[SerializeField] Material plainScreenMat;
	[SerializeField] Material glowingScreenMat;
	[SerializeField] Material glowingScreenCompleteMat;
	[SerializeField] Material glowingScreenAd;
	[SerializeField] float duration;

	[SerializeField] BoxCollider wallScannerTrigger;

	public Image humUI;
	public GameObject humText;
	public GameObject chargedUI;
	public GameObject chargedText;

	public float dbThreshold;
	public float humTime;
	public float cooldownTime;
    public bool inTrigger;

	private float t = 0.0f;

	float db; // volume
	float count = 0.0f;
	float durationUI = 1.0f; //how many seconds before UI disappears
	private float meterFilled = 0.0f;

    public bool activated; //check if player has scanned eyes
	public bool charged;
	private bool humMode; //toggle humming UI on/off

    // Shame // Shame
    AudioManager _AudioManager;


	// Use this for initialization
	void Start () {

		glowingPanelRenderer = glowingPanel.GetComponent<Renderer> ();
		//glowingPanelRenderer.material = glowingScreenAd;

		humUI.fillAmount = 0.0f;
		humText.SetActive(false);
		chargedUI.SetActive(false);
		chargedText.SetActive(false);

        _AudioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();

        activated = false;
		charged = false;
		humMode = false;
        inTrigger = false;

        if (!_AudioManager.isStartingScene)
            activated = true;
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
            if (hit.collider.tag == "WallScanner")
                humMode = true;
            //if(hit.collider.tag != "WallScanner")
            else
                humMode = false;

        if (humMode == true && activated == false && _AudioManager.hasPlayedWelcomeVO && _AudioManager.isStartingScene)
        {
            AkSoundEngine.PostEvent("EyeScan_Start", gameObject);
            humUI.fillAmount += Time.deltaTime / humTime;

            if (humUI.fillAmount == 1.0f) // done charging
            {
                AkSoundEngine.PostEvent("EyeScan_Done", gameObject);
				glowingPanelRenderer.material = glowingScreenCompleteMat;

                humText.SetActive(false);
                humUI.fillAmount = 0.0f;
                chargedText.SetActive(true);
                chargedUI.SetActive(true);

                charged = true;
            }
        }
            if (charged == true) // activate UI
            {
                count += Time.deltaTime;

                if (count >= durationUI)
                {
                    activated = true;
                    //AkSoundEngine.PostEvent("VO_HumIntro", gameObject, AK_EndOfEvent, MyCallbackFunction, myCookieObject); 
                    chargedText.SetActive(false);
                    chargedUI.SetActive(false);
                    humUI.fillAmount = 0.0f;
                    meterFilled = 0.0f;
                    count = 0.0f;
                    t = 0.0f;
                    humMode = false;
                    charged = false;
                }
            }

        if (humMode == false)
        {
            AkSoundEngine.PostEvent("EyeScan_Stop", gameObject);
            humUI.fillAmount -= Time.deltaTime / cooldownTime;
        }
	}
		

	//this is going to switch which material the glowing panel uses
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			//print ("trigger is working");
//			float lerp = Mathf.PingPong(Time.time, duration) / duration;
//			glowingPanelRenderer.material.Lerp (plainScreenMat, glowingScreenMat, lerp);
			if (activated == false) {
				glowingPanelRenderer.material = glowingScreenMat;
			} else {
				glowingPanelRenderer.material = glowingScreenCompleteMat;
			}

            inTrigger = true;

            if(activated == false && _AudioManager.hasPlayedWelcomeVO && _AudioManager.isStartingScene)
            {
                count = 0.0f;
                humText.SetActive(true);
                humUI.fillAmount = meterFilled;
            }
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			
			glowingPanelRenderer.material = glowingScreenAd;
            inTrigger = false;

			humText.SetActive(false);
			humUI.fillAmount = 0.0f;
			humMode = false;

			AkSoundEngine.PostEvent("EyeScan_Stop", gameObject);
		}
	}
}
