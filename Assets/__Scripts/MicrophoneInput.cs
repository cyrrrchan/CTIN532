using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class MicrophoneInput : MonoBehaviour {
	
	public float sensitivity;
	public float loudness = 0;
	public float frequency = 0.0f;
	public int samplerate = 11024;

	private AudioSource audioSource;

	void Start() {
		foreach (string device in Microphone.devices) {
			Debug.Log("Name: " + device);
		}

		audioSource = GetComponent<AudioSource>();

		audioSource.clip = Microphone.Start(Microphone.devices[0], true, 10, 44100);
		audioSource.loop = true; // Set the AudioClip to loop
		while (!(Microphone.GetPosition(Microphone.devices[0]) > 0)){} // Wait until the recording has started
		audioSource.Play(); // Play the audio source!
	}

	void Update() {
		loudness = GetAveragedVolume() * sensitivity;
		frequency = GetFundamentalFrequency();

		Debug.Log (frequency);
	}

	float GetAveragedVolume() { 

		float[] data = new float[256];
		float a = 0;

		audioSource.GetOutputData(data,0);
		foreach(float s in data)
		{
			a += Mathf.Abs(s);
		}
		return a/256;
	}

	float GetFundamentalFrequency()
	{
		float fundamentalFrequency = 0.0f;
		float[] data = new float[8192];
		audioSource.GetSpectrumData(data,0,FFTWindow.BlackmanHarris);
		float s = 0.0f;
		int i = 0;
		for (int j = 1; j < 8192; j++)
		{
			if ( s < data[j] )
			{
				s = data[j];
				i = j;
			}
		}
		fundamentalFrequency = i * samplerate / 8192;
		return fundamentalFrequency;
	}
}