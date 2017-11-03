using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagramAnimation : MonoBehaviour {

	[SerializeField] Color redStarColor;

	[SerializeField] GameObject pentagramLine1;
	[SerializeField] GameObject pentagramLine2;
	[SerializeField] GameObject pentagramLine3;
	[SerializeField] GameObject pentagramLine4;


	//private MeshRenderer finalPentagramLineRenderer;
	[SerializeField] GameObject finalPentagramLine;

	// Use this for initialization
	void Start () {

		//iTween.ColorTo (pentagramLine1, redStarColor, 2f);
		//finalPentagramLineRenderer = gameObject.GetComponent<MeshRenderer> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Z))
			//iTween.FadeTo (finalPentagramLine, 255, 300f);
			StartCoroutine("FinalPylonAnimation");
		
	}

	IEnumerator FinalPylonAnimation(){
		iTween.FadeTo (finalPentagramLine, 255, 300f);
		yield return new WaitForSeconds (2f);
		print ("coroutine works");
		iTween.ColorTo (pentagramLine1, redStarColor, 2f);
		iTween.ColorTo (pentagramLine2, redStarColor, 2f);
		iTween.ColorTo (pentagramLine3, redStarColor, 2f);
		iTween.ColorTo (pentagramLine4, redStarColor, 2f);
		//iTween.ColorTo (finalPentagramLine, redStarColor, 2f);
	}
}
