using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MainMenuController : MonoBehaviour {

	public GameObject introPanel, mainPanel, namePanel, sexPanel, statsPanel, summaryPanel;
	List<GameObject> panels;

	int panelIndex = 0;

	Boi playerBoi;

	public Text nameText;
	public Text sexText;

	// Use this for initialization
	void Start () {
		panels = new List<GameObject> ();
		panels.Add (mainPanel); panels.Add (introPanel); panels.Add (namePanel); panels.Add (sexPanel); panels.Add (statsPanel); panels.Add (summaryPanel);
		playerBoi = new Boi ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	//Boi Info
	public void SetBoiAge(int a){
		playerBoi.age = a;
	}

	public void SetBoiName(string n){
		playerBoi.name = n;
	}

	public void SetBoiSex(string g){
		playerBoi.sex = g;
	}

	public void SetBoiStats(int piloting, int combat, int psychology, int management, int computers, int medicine, int mechanics){
		playerBoi.piloting = piloting; playerBoi.combat = combat; playerBoi.psychology = psychology; 
		playerBoi.management = management; playerBoi.computers = computers; playerBoi.medicine = medicine; playerBoi.mechanics = mechanics;
	}


	//start new game
	public void SaveBoi(){
		SetBoiAge (18);
		SetBoiName (nameText.text);
		SetBoiSex (sexText.text);
		List<int> boiStats = statsPanel.GetComponent<StatsPanel>().stats;
		SetBoiStats (5, boiStats [0], boiStats [1], boiStats [2], boiStats [3], boiStats [4], boiStats [5]);
		playerBoi.Save ("Assets/Resources/SaveData/" + playerBoi.name + "/");
		PlayerPrefs.SetString ("player", playerBoi.name);
		PlayerPrefs.SetInt (playerBoi.name, 100);



		Debug.Log ("SAVED BOI");
	}


	public void SetBoiTraits(){

	}

	public void NewGame(){

	}

	public void HidePanels(){
		foreach(GameObject p in panels){
			p.transform.position = new Vector3 (10000, 10000, 1000);
		}
	}

	public void NextPanel(){
		panelIndex += 1;
		if (panelIndex < panels.Count) {
			HidePanels ();
			panels [panelIndex].transform.position = new Vector3 (0, 1, 90);
		} else {
			StartNewGame ();
		}
		summaryPanel.GetComponent<SummaryPanel> ().UpdateSummary ();
	}

	public void ShowNamePanel(){
		namePanel.transform.position = new Vector2 (0, 0);
	}

	void StartNewGame(){
		Debug.Log ("NEW GAME START");
		SaveBoi ();
		SceneManager.LoadScene ("Hanger");
		Crew newCrew = new Crew ();
		newCrew.Save ("CREW");
	}
}
