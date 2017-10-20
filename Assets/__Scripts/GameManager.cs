using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

        AkSoundEngine.PostEvent("VO_Welcome", gameObject);
    }
	
	// Update is called once per frame
	void Update () {

    }
}
