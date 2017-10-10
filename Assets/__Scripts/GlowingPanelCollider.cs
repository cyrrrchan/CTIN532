using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingPanelCollider : MonoBehaviour {

	[SerializeField] GameObject glowingPanel;
	private Renderer glowingPanelRenderer;
	[SerializeField] Material plainScreenMat;
	[SerializeField] Material glowingScreenMat;
	[SerializeField] float duration;


	// Use this for initialization
	void Start () {

		glowingPanelRenderer = glowingPanel.GetComponent<Renderer> ();
		glowingPanelRenderer.material = plainScreenMat;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//this is going to switch which material the glowing panel uses
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			//print ("trigger is working");
//			float lerp = Mathf.PingPong(Time.time, duration) / duration;
//			glowingPanelRenderer.material.Lerp (plainScreenMat, glowingScreenMat, lerp);
			glowingPanelRenderer.material = glowingScreenMat;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			glowingPanelRenderer.material = plainScreenMat;
		}
	}
}
