using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {

	[SerializeField] float FadeRate;
	[SerializeField] Image blackBoxUIImage;
	private float targetAlpha;

	// Use this for initialization
	void Start () {
		this.blackBoxUIImage = this.GetComponent<Image> ();
		if (this.blackBoxUIImage == null) {
			Debug.LogError ("Error: No image on " + this.name);
		}
		this.targetAlpha = this.blackBoxUIImage.color.a;
		
	}
	
	// Update is called once per frame
	void Update () {
		Color curColor = this.blackBoxUIImage.color;
		float alphaDiff = Mathf.Abs (curColor.a - this.targetAlpha);
		if (alphaDiff > 0.0001f) {
			curColor.a = Mathf.Lerp (curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
			this.blackBoxUIImage.color = curColor;
		}
		
	}

	public void FadeMe(){
		//StartCoroutine (DoFade ());
		print("fade function has been called");
		this.targetAlpha = 0.0f;	
		
	}

	public void FadeMeIn(){
		print ("fading in");
		this.targetAlpha = 1.0f;
	}

//	public IEnumerator DoFadeUI(){
//
//	}

//	public IEnumerator DoFade(){
//		CanvasGroup canvasGroup = GetComponent<CanvasGroup> ();
//		while (canvasGroup.alpha > 0) {
//			canvasGroup.alpha -= Time.deltaTime / 2;
//			yield return null;
//		}
//	}
}
