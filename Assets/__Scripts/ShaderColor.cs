using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderColor : MonoBehaviour {

	[SerializeField] Color currentColor;
	[SerializeField] Color desiredColor;

	[SerializeField] float _upperBound;
	[SerializeField] float _lowerBound;
	[SerializeField] AnimationCurve _sweetSpotCurve;
	[SerializeField] Transform _cube;

	[SerializeField] AudioSource audio;
	private bool isPlaying;

	private Image rend;


	// Use this for initialization
	void Start () {
		rend = GetComponent<Image> ();
		audio = gameObject.GetComponent<AudioSource> ();
		isPlaying = false;
		//audio.Play ();


	}

	// Update is called once per frame
	void Update () {
		float proportion = MathHelpers.LinMapTo01 (_lowerBound, _upperBound, _cube.localPosition.y);
		float curveValue = _sweetSpotCurve.Evaluate (proportion);
		Color tempColor = Color.Lerp (desiredColor, currentColor, curveValue);
		rend.material.SetColor ("_TintColor", tempColor);

		//print (isPlaying);

		if (_cube.localPosition.y >= _lowerBound && _cube.localPosition.y <= _upperBound && isPlaying == false) {
			print ("playing");
			audio.Play();
			isPlaying = true;
		
			//print ("charging");
		} else if (_cube.localPosition.y >= _lowerBound && _cube.localPosition.y <= _upperBound && isPlaying == true){
				print("do nothing");
		}else{
			print ("out of range");
			audio.Pause ();
			isPlaying = false;
			//print ("not charging");
		}

	


//		if (isPlaying) {
//			audio.Play ();
//			print ("sound should be playing");
//			print (isPlaying);
//		} else if (isPlaying == false) {
//			audio.Stop ();
//			print ("no sound");
//			print (isPlaying);
//		}
		//Color tempColor = Color.Lerp(desiredColor, currentColor,_sweetSpotCurve.Evaluate (MathHelpers.LinMapTo01 (_lowerBound, _upperBound, _cube.localPosition.y)))
	}
}
