using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MicMover : MonoBehaviour
{

    public GameObject audioInputObject; //microphoneInput object
    public float threshold;
    //public GameObject objectToMove;
    MicrophoneInput micIn;
    public float sensitivity;
    public float speed;
    public float maxHeight;
    public float minHeight;
    public float maxFreq;
    public float minFreq;

    [SerializeField] AnimationCurve _sweetSpotCurve;
    [SerializeField] Color currentColor;
    [SerializeField] Color desiredColor;

    //private Rigidbody rb;
    private RawImage rend;
    private float lowerF;
    float l;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        rend = GetComponent<RawImage>();

        //if (objectToMove == null)
        //Debug.LogError(“You need to set a prefab to Object To Spawn -parameter in the editor!“);
        if (audioInputObject == null)
            audioInputObject = GameObject.Find(Microphone.devices[0]);
        micIn = (MicrophoneInput)audioInputObject.GetComponent("MicrophoneInput");
    }

    void Update()
    {
        l = micIn.frequency;
        //Vector3 newPosition = rb.transform.position;
        lowerF = minFreq * sensitivity;

        if (l > threshold)
        {

            //Mathf.Clamp(l, minFreq, maxFreq);

            float proportion = MathHelpers.LinMapTo01(minFreq, maxFreq, l);
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
    }

}