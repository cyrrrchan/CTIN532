using UnityEngine;
using System.Collections;
using UnityEngine.UI;


[System.Serializable]
public class FrequencyThing {
	public float maxFreq;
	public float minFreq;
	public AnimationCurve sweetSpotCurve;

	public FrequencyThing(float _maxFreq, float _minFreq, AnimationCurve _sweetSpotCurve){
		maxFreq = _maxFreq;
		minFreq = _minFreq;
		sweetSpotCurve = _sweetSpotCurve;
	}
}

public class MicMover : MonoBehaviour
{
	[SerializeField] FrequencyThing [] _frequencyThings;

    public GameObject audioInputObject; //microphoneInput object
    MicrophoneInput micIn;
    public GameObject humUI;

    //public float lThreshold;
	public float dbThreshold;
    public float maxFreq;
    public float minFreq;

    //public GameObject objectToMove;
    //public float sensitivity;
    //public float speed;
    //public float maxHeight;
    //public float minHeight;
    //public float matchFreq;

    [SerializeField] AnimationCurve sweetSpotCurve;
    [SerializeField] Color currentColor;
    [SerializeField] Color desiredColor;
	[SerializeField] Image rend;

    float l; // frequency
	float db; // volume
    float count;
    float duration = 8.0f; // how long players hold hum

    private bool charging;
    private bool humMode; //toggle humming UI on/off
    private IEnumerator coroutine;

    void Start()
    {
        if (audioInputObject == null)
            audioInputObject = GameObject.Find(Microphone.devices[0]);
        micIn = (MicrophoneInput)audioInputObject.GetComponent("MicrophoneInput");
        humUI.SetActive(false);

        charging = false;
        humMode = false;
    }

    void Update()
    {
        l = micIn.frequency; //set l to be pitch from player input
        db = micIn.loudness; //set db to be volume from player input

        //Debug.Log(db);

        if (humMode == false && Input.GetKeyDown(KeyCode.E)) {
            //calls the function to randomly choose the frequency thing you want to use
            l = Mathf.Clamp(l, minFreq, maxFreq);
            FrequencyThingRandomChooser();
        }

        if (humMode == true && Input.GetKeyDown(KeyCode.R))
        {
            humUI.SetActive(false);
            humMode = false;

            AkSoundEngine.PostEvent("Charging_Stop", gameObject);
            //Debug.Log("off");
        }

        if (humMode == true)
        {
            if (db > dbThreshold)
            {
                charging = true;
                AkSoundEngine.PostEvent("Charging", gameObject);
                //l = Mathf.Clamp(l, minFreq, maxFreq);

                float proportion = MathHelpers.LinMapTo01(minFreq, maxFreq, l);
                float curveValue = sweetSpotCurve.Evaluate(proportion);
                Color tempColor = Color.Lerp(desiredColor, currentColor, curveValue);
                rend.material.SetColor("_TintColor", tempColor);
            }

            if (db < dbThreshold)
            {
                charging = false;
                AkSoundEngine.PostEvent("Charging_Pause", gameObject);
            }

            if (charging == true)
            {
                count += Time.deltaTime;
            }

            if (count >= duration)
            {
                AkSoundEngine.PostEvent("Charging_Stop", gameObject);
                AkSoundEngine.PostEvent("Success", gameObject);

                count = 0.0f;

                humUI.SetActive(false);
                humMode = false;

                AkSoundEngine.PostEvent("Charging_Stop", gameObject);
            }
        }
    }

	public void FrequencyThingRandomChooser(){
        count = 0.0f;
        humMode = true;
        humUI.SetActive(true);
        //Debug.Log("on");

        int frequencyThingsIndex = Random.Range (0, _frequencyThings.Length);
		maxFreq = _frequencyThings [frequencyThingsIndex].maxFreq;
		minFreq = _frequencyThings [frequencyThingsIndex].minFreq;
		sweetSpotCurve = _frequencyThings [frequencyThingsIndex].sweetSpotCurve;
	}

}