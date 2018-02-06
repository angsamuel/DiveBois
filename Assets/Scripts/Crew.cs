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

	public void Save(string name){
		//string filePath = "Assets/Resources/SaveData/" + name + ".json";
		string jsonString = JsonUtility.ToJson (this);
		string filePath = "Assets/Resources/SaveData/" + PlayerPrefs.GetString ("player") + "/";

		if(!Directory.Exists(filePath))
		{    
			Directory.CreateDirectory(filePath);
		}
		System.IO.File.WriteAllText(filePath + name + ".json", jsonString);
		Debug.Log (filePath + name + ".json");
	}

	public Crew Load(string name){
		string filePath = "Assets/Resources/SaveData/" + PlayerPrefs.GetString ("player") + "/";

		string jsonString = File.ReadAllText (filePath + name + ".json");
		Crew loadedData = JsonUtility.FromJson<Crew> (jsonString);
		return loadedData;
	}

}

//erika in health care administration, attention required now health information alert.

//itshelpdesk.edu 