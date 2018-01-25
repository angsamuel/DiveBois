using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.IO;  


[System.Serializable]

public class Boi : System.Object {
	public string name = "DEFAULT NAME";

	public string sex = "X";

	public int age = 16;

	public int piloting, combat, psychology, management, computers, medicine, mechanics = 2;

	public List<string> traits;

	public Boi(){
	}

	public void Save(string filePath){
		//string filePath = "Assets/Resources/SaveData/" + name + ".json";
		string jsonString = JsonUtility.ToJson (this);
		if(!Directory.Exists(filePath))
		{    
			Directory.CreateDirectory(filePath);
		}
		System.IO.File.WriteAllText(filePath + name + ".json", jsonString);


	}
}


