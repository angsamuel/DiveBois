using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.IO;  


[System.Serializable]

public class Boi : System.Object {
	public string name = "DEFAULT NAME";

	public char gender = 'F';

	public int age = 16;

	public int piloting, combat, psychology, management, computers, medicine, mechanics = 2;

	public List<string> traits;

	public Boi(){
	}

	public void Save(){
		string filePath = "Assets/Resources/SaveData/" + name + ".json";
		string jsonString = JsonUtility.ToJson (this);
		System.IO.File.WriteAllText("Assets/SaveData/" + name + ".json", jsonString);
	}
}


