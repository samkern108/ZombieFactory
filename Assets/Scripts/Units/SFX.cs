using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum sfxlib
{
	//entities
	hurt,
	attack,
	select,
	death,
	levelUp,
	kill,
	move,
	COUNT
}

public class SFX : MonoBehaviour {

	private AudioSource source;
	private int[] suffixes;
	public static float volume = 1f;
	private bool playing = false;

	public void Start()
	{
		source = GetComponent <AudioSource>();
	}

	public void SetSuffixes(int[] suffixes)
	{
		this.suffixes = suffixes;
	}

	public void PlayClip(string name, sfxlib type)
	{
		if (playing)
			return;

		int suffix = suffixes [(int)type] - 1;
		if (suffix == -1)
			return;
		suffix = Random.Range (0, suffixes[(int)type]);
		AudioClip clip = ResourceLoader.LoadSFX (name, type, suffix);
		source.PlayOneShot (clip, volume);
		playing = true;
		Invoke ("SafeToPlay", clip.length + .5f);
	}

	public void SafeToPlay()
	{
		playing = false;
	}
}
