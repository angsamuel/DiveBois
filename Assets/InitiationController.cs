using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InitiationController : MonoBehaviour {
	public DiveShip diveShip;
	public List<GameObject> screens;
	public Text stealthModeText;
	public Text instructionText;
	public GameObject initiationPanel;

	bool startup = false;
	bool blinkModeActive = true;
	bool canDive = false;

	public Color myRed;
	public Color myGreen;

	// Use this for initialization
	void Start () {
		StartCoroutine (BlinkMode ());
		StartCoroutine (ReadyUp ());

	}
	
	// Update is called once per frame
	void Update () {
		ScanInput ();
	}

	IEnumerator ReadyUp(){
		yield return new WaitForSeconds(3);
		StartCoroutine(TypeText(instructionText, "ARRIVED AT TARGET PRESS [SPACE] TO DIVE",0));
		yield return new WaitForSeconds(1.5f);
		canDive = true;
	}



	void ScanInput(){
		if (Input.GetAxisRaw ("Jump") != 0 && !startup && canDive) {
			canDive = false;
			blinkModeActive = false;
			stealthModeText.text = "ALL SYSTEMS GO";
			stealthModeText.color = myGreen;
			StartCoroutine(TypeText(instructionText, "GOOD LUCK BOI",0));
			StartCoroutine (FadeText (stealthModeText));
			//StartCoroutine (FadeText (instructionText));
			StartCoroutine(TypeText(instructionText, "[+]",4));
			//StartCoroutine (ChangeCenterTextColor (Color.black));
			BlinkScreensOn ();
			startup = true;
			diveShip.shipActive = true;
		}
	}

	IEnumerator ChangeCenterTextColor(Color c){
		yield return new WaitForSeconds (4);
		instructionText.color = c;
	}

	IEnumerator RetractInitiationPanel(){
		yield return new WaitForSeconds (1);
		initiationPanel.GetComponent<Rigidbody> ().AddForce (new Vector2 (0, 50));
	}

	void BlinkScreensOn(){
		for (int i = 0; i < screens.Count; i++) {
			StartCoroutine(BlinkScreenOn(screens[i]));

		}
	}

	IEnumerator FadeText(Text tm){
		float fadeTime = 3f;
		float t = fadeTime;
		Color c = tm.color;
		yield return new WaitForSeconds (2);

		while (t > 0) {
			t -= Time.deltaTime;
			tm.color = new Color (c.r, c.g, c.b, t / fadeTime);
			yield return null;
		}
	}

	IEnumerator TypeText(Text tm, string message, float delay){
		yield return new WaitForSeconds (delay);
		float textDelay = 0.025f;
		tm.text = "";
		for (int i = 0; i < message.Length; i++) {
			yield return new WaitForSeconds (textDelay);
			tm.text += message [i];
		}
	}


	IEnumerator BlinkScreenOn(GameObject screen){
		yield return new WaitForSeconds (1f);

		int blinks = 20;
		for (int i = 0; i < blinks; i++) {
			screen.GetComponent<MeshRenderer> ().material.color = new Color (0, 0, 0, Random.Range (0f, 1f));
			yield return new WaitForSeconds (Random.Range(0.1f, 0.05f));
		}
		screen.GetComponent<MeshRenderer> ().material.color = new Color (0, 0, 0, 1);
		StartCoroutine (LiftScreenVeil (screen));
	}

	IEnumerator LiftScreenVeil(GameObject screen){
		float limit = 3f;
		float t = 0;
		while (t < limit) {
			t += Time.deltaTime;
			screen.GetComponent<MeshRenderer> ().material.color = new Color (0, 0, 0, 1.0f - t/limit);

			yield return null;
		}
		screen.SetActive (false);
	}

	IEnumerator BlinkMode(){
		float blinkTime = 3f;
		float t = blinkTime;
		Color c = stealthModeText.color;


		while (blinkModeActive) {
			t -= Time.deltaTime;
			stealthModeText.color = new Color (c.r, c.g, c.b, t / blinkTime);
			if (t <= .5f) {
				t = blinkTime;
			}

			yield return null;
		}
	}
}
