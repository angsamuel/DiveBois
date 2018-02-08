using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;  


[System.Serializable]

public class Boi : System.Object {
	public string name = "DEFAULT NAME";
	public string callsign = "DEFAULT CALLSIGN";

	public int hp = 100;
	public int mp = 100;

	List<string> gradeList;

	public string sex = "X";

	public int age = 16;

	public int piloting, combat, psychology, management, computers, medicine, mechanics = 2;

	public List<string> traits;

	public Boi(){
		hp = 100;
		mp = 100;
	}

	public void Save(string filePath){
		//string filePath = "Assets/Resources/SaveData/" + name + ".json";
		string jsonString = JsonUtility.ToJson (this);
		if(!Directory.Exists(filePath))
		{    
			Directory.CreateDirectory(filePath);
		}
		System.IO.File.WriteAllText(filePath + name + ".json", jsonString);
		Debug.Log (filePath + name + ".json");
	}

	public Boi Load(string filePath){
		string jsonString = File.ReadAllText (filePath);
		Boi loadedData = JsonUtility.FromJson<Boi> (jsonString);
		return loadedData;
	}

	public void GenerateStartingStats(){
		piloting = 0;
		List<int> traits = new List<int> ();
		for (int i = 0; i < 6; i++) {
			traits.Add (0);
		}
		int credits = 20;
		while (credits > 0) {
			int index = Random.Range (0, 6);
			if (traits [index] < 5) {
				traits [index] = traits [index] + 1;
				credits = credits - 1;
			}
		}
		combat = traits [0];
		psychology = traits [1];
		management = traits [2];
		computers = traits [3];
		medicine = traits [4];
		mechanics = traits [5];
	}

	public void TakeDamage(){

	}
}


