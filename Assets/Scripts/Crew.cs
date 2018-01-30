using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;  

[System.Serializable]
public class Crew {
	public List<Boi> bois;

	public Crew(){
		bois = new List<Boi> ();
	}

	public void Save(string filePath){
		//string filePath = "Assets/Resources/SaveData/" + name + ".json";
		string jsonString = JsonUtility.ToJson (this);
		if(!Directory.Exists(filePath))
		{    
			Directory.CreateDirectory(filePath);
		}
		System.IO.File.WriteAllText(filePath + "CREW" + ".json", jsonString);
		Debug.Log (filePath + "CREW" + ".json");
	}

	public Crew Load(string filePath){
		string jsonString = File.ReadAllText (filePath + "/CREW.json");
		Crew loadedData = JsonUtility.FromJson<Crew> (jsonString);
		return loadedData;
	}

}
