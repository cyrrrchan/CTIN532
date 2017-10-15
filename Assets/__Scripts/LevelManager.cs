using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour {

    public Image black;
    public Animator anim;

    public GameObject audioInputObject; //microphoneInput object
    MicrophoneInput micIn;
    public Image humUI;
    public GameObject chargedUI;
    public GameObject chargedText;
	public GameObject instructionText;
	public string levelName;

    public float dbThreshold;
    public float humTime;
    public float cooldownTime;

    private float t = 0.0f;

    float db; // volume
    float count = 0.0f;
    float duration = 1.0f; //how many seconds before UI disappears
    private float meterFilled = 0.0f;

    private bool charged;

    void Start()
    {
        if (audioInputObject == null)
            audioInputObject = GameObject.Find(Microphone.devices[0]);
        micIn = (MicrophoneInput)audioInputObject.GetComponent("MicrophoneInput");

        humUI.fillAmount = 0.0f;
        chargedUI.SetActive(false);
        chargedText.SetActive(false);

        charged = false;
    }

    void Update()
    {
        db = micIn.loudness; //set db to be volume from player input

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

                humUI.fillAmount = 0.0f;
                chargedText.SetActive(true);
                chargedUI.SetActive(true);

                charged = true;
            }

            if (charged == true) // open the door
            {
                count += Time.deltaTime;

                if (count >= duration)
                {
                    chargedText.SetActive(false);
                    chargedUI.SetActive(false);
					instructionText.SetActive (false);
                    count = 0.0f;
                    t = 0.0f;
					black.fillAmount = 1.0f;

                    StartCoroutine(Fading());
                }
            }
    }		

    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
		SceneManager.LoadScene(levelName);
    }
}
