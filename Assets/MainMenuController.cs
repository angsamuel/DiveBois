using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MainMenuController : MonoBehaviour {

	public GameObject mainPanel, namePanel, sexPanel, statsPanel;
	List<GameObject> panels;
	int panelIndex = 0;

	Boi playerBoi;

	// Use this for initialization
	void Start () {
		panels = new List<GameObject> ();
		panels.Add (mainPanel); panels.Add (namePanel); panels.Add (sexPanel); panels.Add (statsPanel);
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

	public void SetBoiGender(char g){
		playerBoi.gender = g;
	}

	public void SetBoiStats(int piloting, int combat, int psychology, int management, int computers, int medicine, int mechanics){
		playerBoi.piloting = piloting; playerBoi.combat = combat; playerBoi.psychology = psychology; 
		playerBoi.management = management; playerBoi.computers = computers; playerBoi.medicine = medicine; playerBoi.mechanics = mechanics;
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
		HidePanels ();
		panelIndex += 1;
		if (panelIndex < panels.Count) {
			panels [panelIndex].transform.position = new Vector3 (0, 1, 90);
		} else {
			StartNewGame ();
		}
	}

	public void ShowNamePanel(){
		namePanel.transform.position = new Vector2 (0, 0);
	}

	void StartNewGame(){

	}
}
