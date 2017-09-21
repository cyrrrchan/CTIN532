using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour {

	public float speed;
	private Rigidbody rb;

    public float min = 0.5f;
    public float max = 1.5f;

    static float t = 0.0f;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody> ();
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        /*float moveVertical = Input.GetAxis ("Vertical");
		Vector2 movement = new Vector2 (0, moveVertical);
		rb.AddForce (movement * speed);*/

        transform.position = new Vector3(Mathf.Lerp(min, max, t), 0, 0);
        t += 0.5f * Time.deltaTime;

        if(t > 1.0f)
        {
            float temp = max;
            max = min;
            min = temp;
            t = 0.0f;
        }
		
	}
}
