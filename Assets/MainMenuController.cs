using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	public GameObject mainPanel, namePanel, sexPanel, statsPanel;
	List<GameObject> panels;
	int panelIndex = 0;

	// Use this for initialization
	void Start () {
		panels = new List<GameObject> ();
		panels.Add (mainPanel); panels.Add (namePanel); panels.Add (sexPanel); panels.Add (statsPanel);
	}
	
	// Update is called once per frame
	void Update () {
		
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
