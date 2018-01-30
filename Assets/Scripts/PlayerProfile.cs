using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;  

[System.Serializable]

public class PlayerProfile {
	public int requisition; 

	public PlayerProfile(){
		requisition = 0;
	}

	public void Save(string filePath, string name){
		//string filePath = "Assets/Resources/SaveData/" + name + ".json";
		string jsonString = JsonUtility.ToJson (this);
		if(!Directory.Exists(filePath))
		{    
			Directory.CreateDirectory(filePath);
		}
		System.IO.File.WriteAllText(filePath + name + ".json", jsonString);
	}

	public PlayerProfile Load(string filePath){
		string jsonString = File.ReadAllText (filePath);
		PlayerProfile loadedData = JsonUtility.FromJson<PlayerProfile> (jsonString);
		return loadedData;
	}

}
