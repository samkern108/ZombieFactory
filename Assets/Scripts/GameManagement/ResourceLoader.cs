using UnityEngine;
using System.Collections;
using System.IO;

public class ResourceLoader : MonoBehaviour {

	private static string pathToPortraits = "Sprites/Portraits/";
	private static string sfxPath = "Audio/SFX/";
	private static string musicPath = "Audio/Music/";
	private static string pathToGameObjects = "Entities/";

	public static GameObject LoadGameObject(string name)
	{
		return Resources.Load <GameObject>(pathToGameObjects + name);
	}

	public static Sprite LoadPortrait(string name)
	{
		return Resources.Load <Sprite> (pathToPortraits + name);
	}

	public static AudioClip LoadMusic(string name)
	{
		return Resources.Load (musicPath + name) as AudioClip;
	}

	public static AudioClip LoadSFX(string name, sfxlib type)
	{
		return Resources.Load (sfxPath + name + "/" + type.ToString()) as AudioClip;
	}

	public static AudioClip LoadSFX(string name, sfxlib type, int suffix)
	{
		return Resources.Load (sfxPath + name + "/" + type.ToString() + suffix) as AudioClip;
	}

	public static string[] GetSFXFiles(string name)
	{
		return Directory.GetFiles (sfxPath + name);
	}
}
