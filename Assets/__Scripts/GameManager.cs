using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool aPylonIsTriggered = false;
    public bool bPylonIsTriggered = false;
    public bool stepThroughDoor_WR2 = false;
    public bool stepThroughDoor_WR3 = false;

    private string sceneName;

    // Use this for initialization
    void Start () {
        Scene scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) //turn on UI when inside collider
    {
        if (sceneName == "WaitingRoom2" && !stepThroughDoor_WR2)
            stepThroughDoor_WR2 = true;

        else if (sceneName == "WaitingRoom3" && !stepThroughDoor_WR3)
            stepThroughDoor_WR3 = true;
    }
}
