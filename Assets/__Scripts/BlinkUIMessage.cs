using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkUIMessage : MonoBehaviour {

	private Image blinkingUIImage;

	// Use this for initialization
	void Start () {

		blinkingUIImage = gameObject.GetComponent<Image> ();
		print (blinkingUIImage.name);
		StartCoroutine (BlinkingUICoroutine ());
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator BlinkingUICoroutine(){

		while (true) {
			yield return new WaitForSeconds (.5f);
			blinkingUIImage.enabled = false;
			yield return new WaitForSeconds (.5f);
			blinkingUIImage.enabled = true;
		}

	}
}
