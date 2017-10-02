using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDoor : MonoBehaviour {

    public GameObject door;
    public GameObject audioInputObject; //microphoneInput object
    MicrophoneInput micIn;
    public Image humUI;
    public GameObject humText;
    public GameObject chargedUI;
    public GameObject chargedText;

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

    private bool charged;
    private bool humMode; //toggle humming UI on/off
    private bool doorOpened;

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
        doorOpened = false;
    }

    void Update()
    {
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

            if (charged == true && doorOpened == false) // open the door
            {
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
                }
            }

            if (charged == true && doorOpened == true) // close the door
            {
                door.transform.position = new Vector3(door.transform.position.x, Mathf.Lerp(door.transform.position.y, door.transform.position.y - offset, t), door.transform.position.z);
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
                    doorOpened = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) //turn on UI when inside collider
    {
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
