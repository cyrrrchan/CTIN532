using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingChanger : MonoBehaviour {

	public Color startingEmission;
	public Color lightsOutEmission;
	[SerializeField] Color normalLightColor;
	[SerializeField] Color lightsOutColor;
	public Material glowingTubeMat;
    public bool hasTurnedOff = false;
	[SerializeField] Renderer lightTubeRenderer;
	//[SerializeField] Color testmatColor;

    float count = 0.0f;
    float duration = 1.75f; 

    // Use this for initialization
    void Start () {
		Debug.Assert (lightTubeRenderer);
		lightTubeRenderer.sharedMaterial.EnableKeyword ("_Emission");
		LightsOn ();
		
	}
	
	// Update is called once per frame
	void Update () {

//		if (Input.GetKeyDown (KeyCode.Alpha1))
//			LightsOff ();
//			if (Input.GetKeyDown (KeyCode.Alpha2))
//			LightsOn ();

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
			//lightTubeRenderer.sharedMaterial.SetColor ("_Emission", startingEmission);
			lightTubeRenderer.sharedMaterial.SetColor ("_EmissionColor", startingEmission);

			//lightTubeRenderer.sharedMaterial.SetColor ("_Emission", startingEmission);
			i.GetComponent<Light> ().color = normalLightColor;
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
			//lightTubeRenderer.sharedMaterial.SetColor ("_Emission", lightsOutEmission);
			lightTubeRenderer.sharedMaterial.SetColor ("_EmissionColor", lightsOutEmission);
			//lightTubeRenderer.SetColor ("_EmissionColor", lightsOutEmission);
			//lightTubeRenderer.sharedMaterial.color = testmatColor;
			i.GetComponent<Light> ().color = lightsOutColor;
		}



		//GameObject[] allTubes = GameObject.FindGameObjectsWithTag ("tube");
		//foreach (GameObject i in allTubes) {
			//i.GetComponent<Renderer> ().sharedMaterial.SetColor ("_Emission", lightsOutEmission);
			//i.GetComponent<Renderer>().sharedMaterial.DisableKeyword("_Emission");
		//}
	}
}
