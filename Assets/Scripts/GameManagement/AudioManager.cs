using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	public static AudioSource player;

	public static void AdjustVolume(float volume)
	{
		player.volume = volume;
	}

	public void Start()
	{
		player = GetComponent<AudioSource>();
	}

	public static void PlayMusic(string music)
	{
		Debug.Log (music);
		AudioClip clip = ResourceLoader.LoadMusic (music);
		player.clip = clip;
		player.Play ();
	}

}
