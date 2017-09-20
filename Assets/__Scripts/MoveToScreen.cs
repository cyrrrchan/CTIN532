using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToScreen : MonoBehaviour {

	private bool isLookingAtScreen;
	private bool inCollider;
	public Vector3 lockCameraPosition;
	public Camera playerCamera;
	public Camera screenCamera;
	public GameObject playerUI;
	public GameObject screenUI;

	//public FirstPersonController other;

	// Use this for initialization
	void Start () {
		isLookingAtScreen = false;
		inCollider = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (inCollider == true && isLookingAtScreen == false && Input.GetKeyDown (KeyCode.E)) { //go to screen camera
			isLookingAtScreen = true;
			playerCamera.enabled = false;
			screenCamera.enabled = true;
			screenUI.SetActive(true);
            AkSoundEngine.PostEvent("Computer_startup", gameObject);
			//other.MoveCameraToScreen();
			//playerCamera.transform.position = lockCameraPosition; 
		}

		if (isLookingAtScreen == true && Input.GetKeyDown (KeyCode.R)) { //back to fps camera
			isLookingAtScreen = false;
			screenCamera.enabled = false;
			playerCamera.enabled = true;
			screenUI.SetActive(false);
		}

		if (isLookingAtScreen == true) {
			playerUI.SetActive (false);
		}
	}

	void OnTriggerEnter(Collider other) { //check to see if player is in front of desk
		inCollider = true;
		playerUI.SetActive (true);
	}

	void OnTriggerExit(Collider other) {
		inCollider = false;
		playerUI.SetActive (false);
	}
}
