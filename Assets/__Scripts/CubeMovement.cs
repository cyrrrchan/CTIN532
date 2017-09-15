using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour {

	public float speed;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody> ();
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float moveVertical = Input.GetAxis ("Vertical");
		//float moveVertical = Input.GetKeyDown(KeyCode.UpArrow);
		Vector2 movement = new Vector2 (0, moveVertical);
		rb.AddForce (movement * speed);
		
	}
}
