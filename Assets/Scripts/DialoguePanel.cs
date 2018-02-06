using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour {
	public Text nameText;
	public Text dialogueText;
	public GameObject portraitSprite;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	bool wiggleEnabled = false;
	IEnumerator WigglePortrait(){
		float direction = 4;
		wiggleEnabled = true;
		float delay = 0.5f;
		while (wiggleEnabled) {
			yield return new WaitForSeconds (delay);
			portraitSprite.transform.localPosition -= new Vector3 (0, direction, 0);
			portraitSprite.transform.localPosition = new Vector3 (portraitSprite.transform.localPosition.x, portraitSprite.transform.localPosition.y, -1);
			direction = -direction;
		}
		portraitSprite.transform.localPosition += new Vector3 (direction, 0, 0);
		portraitSprite.transform.localPosition = new Vector3 (portraitSprite.transform.localPosition.x, portraitSprite.transform.localPosition.y, -1);
	}

	public void WriteMessage(string message){
		StartCoroutine (WriteMessage (message, 0));
	}

	IEnumerator WriteMessage(string message, float delay){
		yield return new WaitForSeconds (delay);
		float textDelay = .025f;
		StartCoroutine (WigglePortrait ());
		dialogueText.text = "";
		for (int i = 0; i < message.Length; i++) {
			yield return new WaitForSeconds (textDelay);
			dialogueText.text += message [i];
		}
		wiggleEnabled = false;

	}
}
