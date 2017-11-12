using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOutageUIController : MonoBehaviour {

	// Use this for initialization
	void Start () {

		StartCoroutine ("DisableThisImage");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator DisableThisImage(){
		yield return new WaitForSeconds(4f);
		gameObject.SetActive (false);
	}
}
