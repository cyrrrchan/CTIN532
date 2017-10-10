using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStare : MonoBehaviour {

	[SerializeField] Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//rotate camera every frame to look at player
		transform.LookAt(target);
		
	}
}
