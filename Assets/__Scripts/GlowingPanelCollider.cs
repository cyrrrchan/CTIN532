using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowingPanelCollider : MonoBehaviour {

	[SerializeField] GameObject glowingPanel;
	private Renderer glowingPanelRenderer;
	[SerializeField] Material plainScreenMat;
	[SerializeField] Material glowingScreenMat;
	[SerializeField] float duration;

	public GameObject audioInputObject; //microphoneInput object
	MicrophoneInput micIn;
	public Image humUI;
	public GameObject humText;
	public GameObject chargedUI;
	public GameObject chargedText;

	public float dbThreshold;
	public float humTime;
	public float cooldownTime;

	private float t = 0.0f;

	float db; // volume
	float count = 0.0f;
	float durationUI = 1.0f; //how many seconds before UI disappears
	private float meterFilled = 0.0f;

	private bool charged;
	private bool humMode; //toggle humming UI on/off


	// Use this for initialization
	void Start () {

		glowingPanelRenderer = glowingPanel.GetComponent<Renderer> ();
		glowingPanelRenderer.material = plainScreenMat;

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
	
	// Update is called once per frame
	void Update () {
		db = micIn.loudness; //set db to be volume from player input

		if (humMode == true)
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

			if (charged == true) // open the door
			{
				count += Time.deltaTime;

				if (count >= durationUI)
				{
					chargedText.SetActive(false);
					chargedUI.SetActive(false);
					count = 0.0f;
					t = 0.0f;
					humMode = false;
					charged = false;
				}
			}

			if (charged == true) // close the door
			{
				count += Time.deltaTime;

				if (count >= durationUI)
				{
					chargedText.SetActive(false);
					chargedUI.SetActive(false);
					count = 0.0f;
					t = 0.0f;
					humMode = false;
					charged = false;
				}
			}
		}
		
	}

	//this is going to switch which material the glowing panel uses
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			//print ("trigger is working");
//			float lerp = Mathf.PingPong(Time.time, duration) / duration;
//			glowingPanelRenderer.material.Lerp (plainScreenMat, glowingScreenMat, lerp);
			glowingPanelRenderer.material = glowingScreenMat;

			count = 0.0f;
			humMode = true;
			humText.SetActive(true);
			humUI.fillAmount = meterFilled;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			glowingPanelRenderer.material = plainScreenMat;

			humText.SetActive(false);
			humUI.fillAmount = 0.0f;
			humMode = false;

			AkSoundEngine.PostEvent("Charging_Stop", gameObject);
		}
	}
}
