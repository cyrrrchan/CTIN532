using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingChanger : MonoBehaviour {

	public Color startingEmission;
	public Color lightsOutEmission;
	public Material glowingTubeMat;
    public bool hasTurnedOff = false;

    float count = 0.0f;
    float duration = 1.75f; 

    // Use this for initialization
    void Start () {

	
		
	}
	
	// Update is called once per frame
	void Update () {

		/*if (Input.GetKeyDown (KeyCode.Alpha1))
			LightsOff ();
		if (Input.GetKeyDown (KeyCode.Alpha2))
			LightsOn ();*/

        if (GameObject.Find("GameManager").GetComponent<GameManager>().hasEndedDoorVO)
        {
            count += Time.deltaTime;

            if(count >= duration)
            {
                LightsOff();
                hasTurnedOff = true;
            }
        }

    }

	public void LightsOn(){
		GameObject[] allLights = GameObject.FindGameObjectsWithTag ("light");
		foreach (GameObject i in allLights) {
			i.GetComponent<Light> ().intensity = .5f;
			//i.GetComponent<Light> ().color = Color.white;
		}

//		GameObject[] allTubes = GameObject.FindGameObjectsWithTag ("tube");
//		foreach (GameObject i in allTubes) {
//			i.GetComponent<Renderer> ().sharedMaterial.SetColor ("_Emission", startingEmission);
//			i.GetComponent<Renderer>().sharedMaterial.EnableKeyword("_Emission");
//		}

	
	}

	public void LightsOff(){
		GameObject[] allLights = GameObject.FindGameObjectsWithTag ("light");
		foreach (GameObject i in allLights) {
			i.GetComponent<Light> ().intensity = .1f;
			//i.GetComponent<Light> ().color = Color.yellow;
		}



		//GameObject[] allTubes = GameObject.FindGameObjectsWithTag ("tube");
		//foreach (GameObject i in allTubes) {
			//i.GetComponent<Renderer> ().sharedMaterial.SetColor ("_Emission", lightsOutEmission);
			//i.GetComponent<Renderer>().sharedMaterial.DisableKeyword("_Emission");
		//}
	}
}
