using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public static class IOManager {

	private static string pathToDataFolder;

	public static void Initialize()
	{
		pathToDataFolder = Application.dataPath + "/Data/";
	}

	public static EntityStats LoadEntityStats(string name)
	{
		string data = ReadFromFile(name);
		return JsonConvert.DeserializeObject<EntityStats> (data);
	}

	public static void SaveEntityStats(EntityStats u)
	{
		SerializeAndSave (u, u.uid);
	}

	private static string ReadFromFile(string filepath)
	{
		StreamReader streamReader = new StreamReader(pathToDataFolder + filepath + ".json");
		string data = streamReader.ReadToEnd ();
		streamReader.Close();
		return data;
	}

	//once again, we should really only need to save a few objects.
	//boolean flags, player state, room map, world state.
	public static void SerializeAndSave(object obj, string filename)
	{
		string serialized = JsonConvert.SerializeObject(obj);

		StreamWriter streamWriter = new StreamWriter (pathToDataFolder + filename + ".json");

		streamWriter.Write (serialized);
		streamWriter.Flush ();
		streamWriter.Close ();
	}
}
