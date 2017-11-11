using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRune : MonoBehaviour {

	[SerializeField] LightingChanger lightingManagerScript;
	[SerializeField] PentagramAnimation pentagramAnimationScript;
	[SerializeField] Renderer floorPentagramRenderer;
	[SerializeField] Renderer trapRuneRenderer;

	[SerializeField] Material trapRuneOffMaterial;
	[SerializeField] Material trapRuneOnMaterial;

	[SerializeField] GameObject evilParticles;

	[SerializeField] MoveAgainstWill _moveAgainstWillScript;

	private bool enteredPentagram = false;

	//[SerializeField] Color trapRuneEmissionOffColor;
	//[SerializeField] Color trapRuneEmissionOnColor;

	// Use this for initialization
	void Start () {

		//trapRuneRenderer.sharedMaterial.DisableKeyword ("_Emission");
		trapRuneRenderer.material = trapRuneOffMaterial;
		evilParticles.SetActive (false);
		
	}
	
	// Update is called once per frame
	void Update () {
		//StartCoroutine ("PylonEndGameAnimation");
	}

	void OnTriggerEnter(Collider other){

		if (enteredPentagram == false) {
			print ("entered pentagram");
			StartCoroutine ("PylonEndGameAnimation");
		}

	}

	//Crystal this is the important coroutine for the end animation
	IEnumerator PylonEndGameAnimation(){
		enteredPentagram = true;
		//make trap rune glow
		trapRuneRenderer.material = trapRuneOnMaterial;
		//turn off lights
		lightingManagerScript.LightsOff ();
		yield return new WaitForSeconds (2f);
		//surround player with particles
		evilParticles.SetActive (true);
		_moveAgainstWillScript.TriggerMoveAgainstWill ();
		yield return new WaitForSeconds (2f);
		//fill in last star on the pentagram
		pentagramAnimationScript.fadeInLastLine ();

		//we still need to freeze player
		//sound effect for trap rune
		//drag them towards pylon
		//extra sound effects
		//start VO
		//fill charging UI element
		//make floor pentagram appear
		//end game (load credits)
	}
}
