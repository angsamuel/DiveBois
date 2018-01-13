using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceController : MonoBehaviour {
	public Ship ship;

	public TextMesh modeText;
	public TextMesh messageText;
	bool blinkModeActive = true;

	bool canDive = false;

	public Color goodColor;
	public Color badColor;

	public GameObject frontScreen, leftScreen, rightScreen, topScreen;
	public GameObject redView, blackView;

	public TunnelGenerator tunnelGenerator;

	float UIrate = 0.1f;

	//Ship Stats
	public TextMesh shieldPercentage, shieldBar;
	public TextMesh armorPercentage, armorBar;

	//Scanner
	public GameObject scannerDot;
	public GameObject scannerBackground;
	public GameObject scannerRedPanel;



	// Use this for initialization
	void Start () {
		StartCoroutine (BlinkMode ());
		StartCoroutine (SeeColor (blackView,10f));
		StartCoroutine (OpenBlinds ());
		StartCoroutine(ApproachInStealth());

		StartCoroutine (UpdateHealthPanel ());
		StartCoroutine (UpdateScanner ());

		//scanner scaling
	}


	IEnumerator OpenBlinds(){
		blackView.SetActive (true);
		blackView.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 2, 0);
		yield return new WaitForSeconds (3);
		blackView.SetActive (false);
	}


	IEnumerator UpdateHealthPanel(){
		float shieldHolder = ship.shields;
		float armorHolder = ship.shields;

		while (true) {
			shieldPercentage.text = (((int)((ship.shields / ship.maxShields) * 100)).ToString() + "%");
			shieldBar.text = "";
			for (int i = 0; i < (ship.shields / ship.maxShields) * 10; i++) {
				shieldBar.text += "#";
			}
			//Debug.Log ((ship.shields / ship.maxShields) * 10);

			armorPercentage.text = (((int)((ship.armor / ship.maxArmor) * 100)).ToString() + "%");
			armorBar.text = "";

			armorBar.text = "";
			for (int i = 0; i < (ship.armor / ship.maxArmor) * 10; i++) {
				armorBar.text += "#";
			}

			if (shieldHolder > ship.shields || armorHolder > ship.armor) {
				//DamageEffect ();
			}

			shieldHolder = ship.shields; armorHolder = ship.armor;

			yield return new WaitForSeconds (UIrate);


		}
	}

	public void DamageEffect(){
		StartCoroutine (SeeColor (redView, 1f));
	}

	IEnumerator SeeColor(GameObject plane, float timeToComplete){

		plane.SetActive (true);

		float t = timeToComplete;
		Color rColor = plane.GetComponent<MeshRenderer> ().material.color;
		while (t > 0) {
			plane.GetComponent<MeshRenderer> ().material.color = new Color (rColor.r, rColor.g, rColor.b, t/timeToComplete);
			t -= Time.deltaTime;
			yield return null;
		}

		plane.SetActive (false);


	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetAxisRaw ("Dive") != 0 && canDive) {
			Dive ();
			canDive = false;
		}

	}

	IEnumerator BlinkMode(){
		float blinkTime = 3f;
		float t = blinkTime;
		Color c = modeText.color;


		while (blinkModeActive) {
			t -= Time.deltaTime;
			modeText.color = new Color (c.r, c.g, c.b, t / blinkTime);
			if (t <= .5f) {
				t = blinkTime;
			}

			yield return null;
		}
	}

	IEnumerator ApproachInStealth(){
		yield return new WaitForSeconds (5);
		StartCoroutine (TypeText (messageText, "ARRIVED AT TARGET\nPRESS [SPACE] TO DIVE"));
		yield return new WaitForSeconds (3);
		canDive = true;
	}

	void Dive(){
		blinkModeActive = false;
		modeText.text = "[ALL SYSTEMS GO]";
		modeText.color = goodColor;
		StartCoroutine (FadeText (modeText));
		StartCoroutine (TypeText (messageText, "GOOD LUCK BOI"));
		StartCoroutine (FadeText (messageText));
		StartCoroutine (BlinkScreensOn ());
		tunnelGenerator.tunnelActive = true;
		ship.speed = ship.minSpeed;

	}

	IEnumerator FadeText(TextMesh tm){
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


	IEnumerator TypeText(TextMesh tm, string message){
		float textDelay = 0.05f;
		tm.text = "";
		for (int i = 0; i < message.Length; i++) {
			yield return new WaitForSeconds (textDelay);
			tm.text += message [i];
		}
	}

	IEnumerator LiftScreenVeils(){
		yield return new WaitForSeconds (2f);
		StartCoroutine (LiftScreenVeil (frontScreen)); StartCoroutine (LiftScreenVeil (leftScreen));
		StartCoroutine (LiftScreenVeil (rightScreen)); StartCoroutine (LiftScreenVeil (topScreen));
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

	IEnumerator BlinkScreensOn(){
		//frontScreen.GetComponent<MeshRenderer> ().material.color = new Color(0,0,0,0f);
		yield return new WaitForSeconds(1f);
		StartCoroutine (BlinkScreenOn (frontScreen)); StartCoroutine (BlinkScreenOn (leftScreen));
		StartCoroutine (BlinkScreenOn (rightScreen)); StartCoroutine (BlinkScreenOn (topScreen));

	}

	IEnumerator BlinkScreenOn(GameObject screen){
		int blinks = Random.Range (30, 30);
		for (int i = 0; i < blinks; i++) {
			screen.GetComponent<MeshRenderer> ().material.color = new Color (0, 0, 0, Random.Range (0f, 1f));
			yield return new WaitForSeconds (Random.Range(0.1f, 0.05f));
		}
		screen.GetComponent<MeshRenderer> ().material.color = new Color (0, 0, 0, 1);
		StartCoroutine (LiftScreenVeil (screen));
	}


	//Scanner 
	IEnumerator UpdateScanner(){
		while (true) {
			//scannerDot.transform.position = new Vector3 (Remap (ship.transform.position.x,-60f, 60, -.45f, .45f), Remap (ship.transform.position.y,-60f, 60, -.45f, .45f), scannerDot.transform.position.z);
			//scannerDot.transform.position = new Vector3 (.45f,scannerDot.transform.position.y, scannerDot.transform.position.z);

			//scannerDot.transform.position = new Vector3 (scannerDot.transform.position.x, scannerDot.transform.position.y, scannerDot.transform.position.z);

			Vector3 newPos = new Vector3 (Remap (ship.transform.position.x, -60f, 60, -.45f, .45f), Remap (ship.transform.position.y, -60f, 60, -.45f, .45f), -2f);

			scannerDot.transform.localPosition = newPos;

			if (tunnelGenerator.obstacleQueue.Count > 0) {
				switch (tunnelGenerator.obstacleQueue [0]) {
				case "up":
					scannerRedPanel.transform.localScale = new Vector3 (1f, 0.5f, 0.1f);
					scannerRedPanel.transform.localPosition = new Vector3 (0f, 0.25f, -1.05f);
					break;
				case "down":
					scannerRedPanel.transform.localScale = new Vector3 (1f, 0.5f, 0.1f);
					scannerRedPanel.transform.localPosition = new Vector3 (0f, -0.25f, -1.05f);
					break;
				case "left":
					scannerRedPanel.transform.localScale = new Vector3 (0.5f, 1f, 0.1f);
					scannerRedPanel.transform.localPosition = new Vector3 (-0.25f, 0f, -1.05f);
					break;
				case "right":
					scannerRedPanel.transform.localScale = new Vector3 (0.5f, 1f, 0.1f);
					scannerRedPanel.transform.localPosition = new Vector3 (0.25f, 0f, -1.05f);
					break;
					
				}
			} else {
				scannerRedPanel.transform.localScale = new Vector3 (0f, 0f, 0f);
			}

			yield return new WaitForSeconds(0.1f);
			Debug.Log ("NEW POS: " +newPos);
		}

	}




	public float Remap ( float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}





}


