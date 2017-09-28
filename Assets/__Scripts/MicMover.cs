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
    public float lThreshold;
	public float dbThreshold;
    //public GameObject objectToMove;
    MicrophoneInput micIn;
    public float sensitivity;
    public float speed;
    //public float maxHeight;
    //public float minHeight;
    public float maxFreq;
    public float minFreq;
	public float matchFreq;

    [SerializeField] AnimationCurve _sweetSpotCurve;
    [SerializeField] Color currentColor;
    [SerializeField] Color desiredColor;

    //private Rigidbody rb;
	[SerializeField] Image rend;
    private float lowerF;
    float l; // frequency
	float db; // volume

    void Start()
    {
        //rb = GetComponent<Rigidbody>();

        //if (objectToMove == null)
        //Debug.LogError(“You need to set a prefab to Object To Spawn -parameter in the editor!“);
        if (audioInputObject == null)
            audioInputObject = GameObject.Find(Microphone.devices[0]);
        micIn = (MicrophoneInput)audioInputObject.GetComponent("MicrophoneInput");
    }

    void Update()
    {
		if (Input.GetKeyDown (KeyCode.Space)) {
			//calls the function to randomly choose the frequency thing you want to use
			FrequencyThingRandomChooser ();
		}

        l = micIn.frequency;
		db = micIn.loudness;

        //Vector3 newPosition = rb.transform.position;
        lowerF = minFreq * sensitivity;

		if (l > lThreshold && db > dbThreshold)
        {
			AkSoundEngine.PostEvent("Charging", gameObject);
			float clampedFrequency = Mathf.Clamp(l, minFreq, maxFreq);

			float proportion = MathHelpers.LinMapTo01(minFreq, maxFreq, clampedFrequency);
            float curveValue = _sweetSpotCurve.Evaluate(proportion);
            Color tempColor = Color.Lerp(desiredColor, currentColor, curveValue);
            rend.material.SetColor("_TintColor", tempColor);

            /*float moveVertical = l;
            Vector2 movement = new Vector2(0, moveVertical);
            rb.AddForce(movement * speed);

            if (l == maxFreq)
            {
                newPosition.y = maxHeight;
            }

            if (l == minFreq)
            {
                newPosition.y = minHeight;
            }

            if (l < maxFreq && l > minFreq)
            {
                newPosition.y = (l - lowerF) / (maxHeight - minHeight);
                if (newPosition.y > maxHeight)
                {
                    newPosition.y = maxHeight;
                }
                if (newPosition.y < minHeight)
                {
                    newPosition.y = minHeight;
                }
                else
                {

                }
            }

            rb.transform.position = newPosition;
        }*/
        }

		if (l < lThreshold) {
			AkSoundEngine.PostEvent ("Charging_Pause", gameObject);
		}
    }

	public void FrequencyThingRandomChooser(){
		int frequencyThingsIndex = Random.Range (0, _frequencyThings.Length);
		maxFreq = _frequencyThings [frequencyThingsIndex].maxFreq;
		minFreq = _frequencyThings [frequencyThingsIndex].minFreq;
		_sweetSpotCurve = _frequencyThings [frequencyThingsIndex].sweetSpotCurve;
	}

}