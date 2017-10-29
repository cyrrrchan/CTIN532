using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumGlowChanger : MonoBehaviour {

	public GameObject audioInputObject; //microphoneInput object
	MicrophoneInput micIn;
	public float dbThreshold;
	float db; // volume

	[SerializeField] Color noEmissionColor;
	[SerializeField] Color pylonEmissionColor;
	[SerializeField] Renderer glowingPylonRenderer;


	// Use this for initialization
	void Start () {

		//find microphone
		if (audioInputObject == null)
			audioInputObject = GameObject.Find(Microphone.devices[0]);
		micIn = (MicrophoneInput)audioInputObject.GetComponent("MicrophoneInput");

		//set up arrowhead emission
		Debug.Assert (glowingPylonRenderer);
		glowingPylonRenderer.sharedMaterial.EnableKeyword ("_Emission");

	}
	
	// Update is called once per frame
	void Update () {

		db = micIn.loudness; //set db to be volume from player input

		if (db > dbThreshold) {
			
			PylonLightGlow ();
		}

		if (db < dbThreshold) {
			PylonLightDark ();
		}
		
	}

	void PylonLightGlow(){
		//turn on pylon light
		print ("material should glow");
		glowingPylonRenderer.sharedMaterial.SetColor ("_EmissionColor", pylonEmissionColor);
	}

	void PylonLightDark(){
		//turn off pylon light
		print ("turn off glow");
		glowingPylonRenderer.sharedMaterial.SetColor ("_EmissionColor", noEmissionColor);
	}
}
