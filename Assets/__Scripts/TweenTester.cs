using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenTester : MonoBehaviour {

	[SerializeField] GameObject cube;
	[SerializeField] Color cubeColor;

	// Use this for initialization
	void Start () {

		iTween.ColorTo (cube, cubeColor, 2f); 
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
