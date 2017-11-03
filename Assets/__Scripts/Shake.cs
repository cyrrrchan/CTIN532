using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour {

	[SerializeField] float shakeSpeed;
	[SerializeField] float shakeAmount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		//iTween.ShakePosition (gameObject, itween.Hash ("y", 0.3f, "time", 0.8f, "delay", 2.0f));
		//transform.position.y = Mathf.Sin (Time.time * shakeSpeed);
		
	}
}
