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
	[SerializeField] GameObject killYouParticles;

	[SerializeField] MoveAgainstWill _moveAgainstWillScript;

	private bool enteredPentagram = false;


	[SerializeField] GameObject floorPentagram;

	//finding script for the fadeout method
	public FadeOut fadeOutScript;



	// Use this for initialization
	void Start () {

		//trapRuneRenderer.sharedMaterial.DisableKeyword ("_Emission");
		trapRuneRenderer.material = trapRuneOffMaterial;
		evilParticles.SetActive (false);
		killYouParticles.SetActive (false);
		floorPentagram.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		//StartCoroutine ("PylonEndGameAnimation");
//		if (Input.GetKeyDown (KeyCode.P))
//			fadeOutScript.FadeMeIn ();
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
		_moveAgainstWillScript.TriggerMoveAgainstWill ();
        AkSoundEngine.PostEvent("Dragging", gameObject);
        yield return new WaitForSeconds (2f);
		//surround player with particles
		evilParticles.SetActive (true);
        AkSoundEngine.PostEvent("Ending", gameObject);
        yield return new WaitForSeconds (2f);
		//turn on floor pentagram
		floorPentagram.SetActive(true);
		yield return new WaitForSeconds (10f);
		//particles that appear as the pylon is charging
		killYouParticles.SetActive(true);
		yield return new WaitForSeconds (4f);
		//fill in last star on the pentagram
		pentagramAnimationScript.fadeInLastLine ();
		yield return new WaitForSeconds (2f);
		print ("fading");
		fadeOutScript.FadeMeIn ();

	
		//sound effect for trap rune

		//extra sound effects
		//start VO
		//fill charging UI element
		//make floor pentagram appear
		//end game (load credits)
	}
}
