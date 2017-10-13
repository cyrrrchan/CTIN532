﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMover : MonoBehaviour {

	// animate the game object from -1 to +1 and back
	public float minimum;
	public float maximum;

	// starting value for the Lerp
	static float t = 0.0f;

	void Update()
	{
		// animate the position of the game object...
		transform.position = new Vector3(transform.position.x, Mathf.Lerp(minimum, maximum, t), 0);

		// .. and increate the t interpolater
		t += 0.5f * Time.deltaTime;

		// now check if the interpolator has reached 1.0
		// and swap maximum and minimum so game object moves
		// in the opposite direction.
		if (t > 1.0f)
		{
			float temp = maximum;
			maximum = minimum;
			minimum = temp;
			t = 0.0f;
		}
	}
}
