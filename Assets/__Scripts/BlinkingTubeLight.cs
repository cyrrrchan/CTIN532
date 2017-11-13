using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingTubeLight : MonoBehaviour {

	private Renderer rend;
	[SerializeField] Material mat;
	private Light blinkingLight;

	[SerializeField] Color glowingTubeColor;
	[SerializeField] Color darkTubeColor;



	// Use this for initialization
	void Start () {

		blinkingLight = gameObject.GetComponent<Light> ();
	
		mat.EnableKeyword ("_EMISSION");

		
	}
	
	// Update is called once per frame
	void Update () {

		print (blinkingLight.intensity);

		if (blinkingLight.intensity < .1f) {
			//mat.DisableKeyword ("_EMISSION");
			mat.SetColor ("_EmissionColor", darkTubeColor);
		} else {
			mat.SetColor ("_EmissionColor", glowingTubeColor);
		}
		
	}
}
