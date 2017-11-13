using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextChanger : MonoBehaviour {

	[SerializeField] GameObject wasdText;
	[SerializeField] GameObject humText;

	// Use this for initialization
	void Start () {

		wasdText.SetActive (true);
		humText.SetActive (false);
		StartCoroutine (ChangeUIText ());

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator ChangeUIText(){
		yield return new WaitForSeconds (4f);
		wasdText.SetActive (false);
		yield return new WaitForSeconds (1f);
		humText.SetActive (true);
		yield return new WaitForSeconds (4f);
		humText.SetActive (false);
	}
}
