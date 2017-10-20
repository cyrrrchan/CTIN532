using UnityEngine;
using System.Collections;
using System.IO;

public class MetricManagerScript : MonoBehaviour {

	string createText = "";

	public int sampleMetric1, sampleMetric2;

	void Start () {}
	void Update () {}
	
	//When the game quits we'll actually write the file.
	void OnApplicationQuit(){
		GenerateMetricsString ();
		string time = System.DateTime.UtcNow.ToString ();string dateTime = System.DateTime.Now.ToString (); //Get the time to tack on to the file name
		time = time.Replace ("/", "-"); //Replace slashes with dashes, because Unity thinks they are directories..
		string reportFile = "GameName_Metrics_" + time + ".txt"; 
		File.WriteAllText (reportFile, createText);
		//In Editor, this will show up in the project folder root (with Library, Assets, etc.)
		//In Standalone, this will show up in the same directory as your executable
	}

	void GenerateMetricsString(){
		createText = 
			"Number of times something happened 1: " + sampleMetric1 + "\n" +
			"Number of times something happened 2: " + sampleMetric2;
	}

	//Add to your set metrics from other classes whenever you want
	public void AddToSample1(int amtToAdd){
		sampleMetric1 += amtToAdd;
	}
}
