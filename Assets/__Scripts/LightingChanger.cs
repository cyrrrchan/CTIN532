using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightingChanger : MonoBehaviour {

	public Color startingEmission;
	public Color lightsOutEmission;
	public Color scaryLightsEmission;
	public Color accessGrantedEmissionColor;
	public Color accessDeniedEmissionColor;
	public Color accessDeniedNoEmission;
	[SerializeField] Color normalLightColor;
	[SerializeField] Color lightsOutColor;
	[SerializeField] Color scaryLightsColor;
	public Material glowingTubeMat;
    public bool hasTurnedOff = false;
	[SerializeField] Renderer lightTubeRenderer;
	[SerializeField] Renderer glowingScreenRenderer;
	[SerializeField] Renderer glowingScreenAccessRenderer;
	//[SerializeField] Color testmatColor;

	//accessing the screen collider to turn it off once power goes out
	[SerializeField] BoxCollider glowingPanelBoxCollider;
	[SerializeField] Renderer glowingPanelRenderer;
	[SerializeField] Material glowingPanelTurnedOffMat;
	[SerializeField] Material glowingPanelAdMaterial;
	//[SerializeField] Material glowingPanelMaterial;


    float count = 0.0f;
    float duration = 1.75f; 

    // Use this for initialization
    void Start () {

		// Create a temporary reference to the current scene.
		Scene currentScene = SceneManager.GetActiveScene ();

		// Retrieve the name of this scene.
		string sceneName = currentScene.name;

		Debug.Assert (lightTubeRenderer);
		Debug.Assert (glowingScreenRenderer);
		Debug.Assert (glowingScreenAccessRenderer);
		lightTubeRenderer.sharedMaterial.EnableKeyword ("_Emission");


		//set lighting dependent on which scene we are in
		if (sceneName == "Main") {
			// Do something...
			LightsOn ();
		} else if (sceneName == "WaitingRoom2") {
			LightsOff ();
		} else if (sceneName == "WaitingRoom3") {
			LightsScary ();
		} else if (sceneName == "WaitingRoom4") {
			LightsOn ();
		} else {
			print ("this is a dark room");
		}

		
	}
	
	// Update is called once per frame
	void Update () {

		// Create a temporary reference to the current scene.
		Scene currentScene = SceneManager.GetActiveScene ();

		// Retrieve the name of this scene.
		string sceneName = currentScene.name;

		if (Input.GetKeyDown (KeyCode.Alpha1))
			LightsOff ();
			if (Input.GetKeyDown (KeyCode.Alpha2))
			LightsOn ();


		if (sceneName == "Main") {
			if (GameObject.Find("GameManager").GetComponent<AudioManager>().hasEndedDoorVO)
			{
				count += Time.deltaTime;

				if(count >= duration)
				{
					LightsOff();
					hasTurnedOff = true;
				}
			}
			
		}
	

		if (sceneName == "waitingRoom4") {
			// Do something...


			if (GameObject.Find("PylonTrigger4").GetComponent<PylonCharger>().charged)
			{
				LightsOn();
				hasTurnedOff = false;
			}
		}

        

    }

	public void LightsOn(){
		print ("lights on");
		GameObject[] allLights = GameObject.FindGameObjectsWithTag ("light");
		foreach (GameObject i in allLights) {
			i.GetComponent<Light> ().intensity = .5f;
			//lightTubeRenderer.sharedMaterial.SetColor ("_Emission", startingEmission);
			lightTubeRenderer.sharedMaterial.SetColor ("_EmissionColor", startingEmission);
			glowingScreenRenderer.sharedMaterial.SetColor ("_EmissionColor", accessDeniedEmissionColor);
			glowingScreenAccessRenderer.sharedMaterial.SetColor ("_EmissionColor", accessGrantedEmissionColor);

			//lightTubeRenderer.sharedMaterial.SetColor ("_Emission", startingEmission);
			i.GetComponent<Light> ().color = normalLightColor;
			glowingPanelRenderer.material = glowingPanelAdMaterial;
		}

//		GameObject[] allTubes = GameObject.FindGameObjectsWithTag ("tube");
//		foreach (GameObject i in allTubes) {
//			i.GetComponent<Renderer> ().sharedMaterial.SetColor ("_Emission", startingEmission);
//			i.GetComponent<Renderer>().sharedMaterial.EnableKeyword("_Emission");
//		}

	
	}

	public void LightsOff(){
		print ("lightsOff");
		GameObject[] allLights = GameObject.FindGameObjectsWithTag ("light");
		foreach (GameObject i in allLights) {
			i.GetComponent<Light> ().intensity = .1f;
			//lightTubeRenderer.sharedMaterial.SetColor ("_Emission", lightsOutEmission);
			lightTubeRenderer.sharedMaterial.SetColor ("_EmissionColor", lightsOutEmission);
			//lightTubeRenderer.SetColor ("_EmissionColor", lightsOutEmission);
			//lightTubeRenderer.sharedMaterial.color = testmatColor;
			i.GetComponent<Light> ().color = lightsOutColor;
			glowingScreenRenderer.sharedMaterial.SetColor ("_EmissionColor", accessDeniedNoEmission);
			glowingScreenAccessRenderer.sharedMaterial.SetColor ("_EmissionColor", accessDeniedNoEmission);
			glowingPanelBoxCollider.enabled = false;
			glowingPanelRenderer.material = glowingPanelTurnedOffMat;
		}



		//GameObject[] allTubes = GameObject.FindGameObjectsWithTag ("tube");
		//foreach (GameObject i in allTubes) {
			//i.GetComponent<Renderer> ().sharedMaterial.SetColor ("_Emission", lightsOutEmission);
			//i.GetComponent<Renderer>().sharedMaterial.DisableKeyword("_Emission");
		//}
	}

	public void LightsScary(){
		//placeholder script for now. This is for WaitingRoom3
		GameObject[] allLights = GameObject.FindGameObjectsWithTag ("light");
		foreach (GameObject i in allLights) {
			i.GetComponent<Light> ().intensity = .1f;
			//lightTubeRenderer.sharedMaterial.SetColor ("_Emission", lightsOutEmission);
			lightTubeRenderer.sharedMaterial.SetColor ("_EmissionColor", scaryLightsEmission);
			//lightTubeRenderer.SetColor ("_EmissionColor", lightsOutEmission);
			//lightTubeRenderer.sharedMaterial.color = testmatColor;
			i.GetComponent<Light> ().color = scaryLightsColor;
			glowingScreenRenderer.sharedMaterial.SetColor ("_EmissionColor", accessDeniedNoEmission);
			glowingScreenAccessRenderer.sharedMaterial.SetColor ("_EmissionColor", accessDeniedNoEmission);
			glowingPanelRenderer.material = glowingPanelTurnedOffMat;
		}
	}
}
