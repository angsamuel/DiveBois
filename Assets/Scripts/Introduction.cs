using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Introduction : MonoBehaviour {
	int stage = 0;
	public List<string> messages;
	public List<string> responses;
	public DialoguePanel dialoguePanel;
	public Button continueButton;
	public Text responseText;

	public bool special = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Next(){


		if (stage >= messages.Count) {
			EndDialogueFunction ();
		} else {
			dialoguePanel.WriteMessage (messages [stage]);
			responseText.text = responses [stage];
			stage += 1;
		}
	}

	public void EndDialogueFunction(){
		if (special) {
			GameObject.Find ("MainMenuController").GetComponent<MainMenuController> ().NextPanel ();
		}
	}


}

/*
monica reina

may need extra ethernet port

comm department, laurie 316 g to 316 e
USS team 1
*/