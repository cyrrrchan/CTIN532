using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumGlowSimple : MonoBehaviour {

	public GameObject audioInputObject; //microphoneInput object
	MicrophoneInput micIn;
	//public float dbThreshold;
	float db; // volume

	[SerializeField] Color noEmissionColor;
	[SerializeField] Color glowEmissionColor;
	private Renderer glowRend;

	//crazy lerp attempt
	private float lowerBound = .8f;
	private float upperBound = 1.3f;
	[SerializeField] AnimationCurve sweetSpotCurve;


	// Use this for initialization
	void Start () {

		//find microphone
		if (audioInputObject == null)
			audioInputObject = GameObject.Find(Microphone.devices[0]);
		micIn = (MicrophoneInput)audioInputObject.GetComponent("MicrophoneInput");

		//set up emission
		//Debug.Assert (glowingPylonRenderer);

		glowRend = gameObject.GetComponent<Renderer> ();
		glowRend.sharedMaterial.EnableKeyword ("_Emission");

	}

	// Update is called once per frame
	void Update () {

		db = micIn.loudness; //set db to be volume from player input

		//set up curve limits for lerp
		float proportion = MathHelpers.LinMapTo01(lowerBound, upperBound, db);
		float curveValue = sweetSpotCurve.Evaluate (proportion);

		//experimental lerpage
		Color tempColor = Color.Lerp(noEmissionColor, glowEmissionColor, curveValue);
		glowRend.material.SetColor ("_EmissionColor", tempColor);




//		if (db > dbThreshold) {
//
//			//HumLightGlow ();
//		}
//
//		if (db < dbThreshold) {
//			//HumLightDark ();
//		}

	}

	void HumLightGlow(){
		//turn on pylon light
		//print ("material should glow");
		glowRend.sharedMaterial.SetColor ("_EmissionColor", glowEmissionColor);
	}

	void HumLightDark(){
		//turn off pylon light
		//print ("turn off glow");
		glowRend.sharedMaterial.SetColor ("_EmissionColor", noEmissionColor);
	}
}