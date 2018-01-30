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

	//panels
	public GameObject healthPanel;
	public GameObject progressPanel;
	public GameObject scannerPanel;

	//Ship Stats
	public TextMesh shieldPercentage, shieldBar;
	public TextMesh armorPercentage, armorBar;

	//Scanner
	public GameObject scannerDot;
	public GameObject scannerBackground;
	public GameObject scannerRedPanel;

	//Progress
	public GameObject progressDot;
	public GameObject progressStart;
	public GameObject progressTunnel;
	public GameObject progressEnd;
	public TextMesh timeLeftText;
	public TextMesh distanceTraveledText;

	//Engine
	public TextMesh engineBar1, engineBar2, engineBar3;
	public TextMesh engineText, engineTop, engineBottom;




	// Use this for initialization
	void Start () {
		StartCoroutine (BlinkMode ());
		StartCoroutine (SeeColor (blackView,10f));
		StartCoroutine (OpenBlinds ());
		StartCoroutine(ApproachInStealth());

		StartCoroutine (UpdateHealthPanel ());
		StartCoroutine (UpdateScanner ());

		//scanner scaling
		StartCoroutine(UpdateProgressPanel());
		StartCoroutine (UpdateEnginePanel ());
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
		ScanForInput ();

	


	}


	//toggles
	bool healthOn, scannerOn, progressOn = true;

	//canToggle
	bool canToggleHealth, canToggleScanner, canToggleProgress = true;



	void ScanForInput(){
		if (Input.GetAxisRaw ("Dive") != 0 && canDive) {
			Dive ();
			canDive = false;
		}

		if (Input.GetAxisRaw ("ToggleHealthPanel") != 0 && canToggleHealth == true) {
			Debug.Log ("TOGGLE");
			if (healthOn == true) {
				healthOn = false;
				healthPanel.transform.localScale = new Vector3 (0, 0, 0);
			} else {
				healthOn = true;
				healthPanel.transform.localScale = new Vector3 (1, 1, 1);
			}
			canToggleHealth = false;
		} else if(Input.GetAxisRaw ("ToggleHealthPanel") == 0){
			canToggleHealth = true;
		}

		if (Input.GetAxisRaw ("ToggleScannerPanel") != 0 && canToggleScanner == true) {
			Debug.Log ("TOGGLE Scanner");
			if (scannerOn == true) {
				scannerOn = false;
				scannerPanel.transform.localScale = new Vector3 (0, 0, 0);
			} else {
				scannerOn = true;
				scannerPanel.transform.localScale = new Vector3 (1, 1, 1);
			}
			canToggleScanner = false;
		} else if(Input.GetAxisRaw ("ToggleScannerPanel") == 0){
			canToggleScanner = true;
		}

		if (Input.GetAxisRaw ("ToggleProgressPanel") != 0 && canToggleProgress == true) {
			Debug.Log ("TOGGLE");
			if (progressOn == true) {
				progressOn = false;
				progressPanel.transform.localScale = new Vector3 (0, 0, 0);
			} else {
				progressOn = true;
				progressPanel.transform.localScale = new Vector3 (1, 1, 1);
			}
			canToggleProgress = false;
		} else if(Input.GetAxisRaw ("ToggleProgressPanel") == 0){
			canToggleProgress = true;
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
		float textDelay = 0.025f;
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
		yield return new WaitForSeconds(2f);
		StartCoroutine (BlinkScreenOn (frontScreen)); StartCoroutine (BlinkScreenOn (leftScreen));
		StartCoroutine (BlinkScreenOn (rightScreen)); StartCoroutine (BlinkScreenOn (topScreen));

	}

	IEnumerator BlinkScreenOn(GameObject screen){
		int blinks = 20;
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
		}

	}


	//Progress
	IEnumerator UpdateProgressPanel(){
		bool updateActive = true;
		while (updateActive) {


			float yPos =  progressStart.transform.localPosition.y;
			if (ship.transform.position.z + tunnelGenerator.distanceTraveled < 1) {
				yPos = progressStart.transform.localPosition.y;
			} else {
				 yPos = Remap (ship.transform.position.z + tunnelGenerator.distanceTraveled, 0, tunnelGenerator.tunnelLength, progressStart.transform.localPosition.y, progressEnd.transform.localPosition.y);
			}

			progressDot.transform.localPosition = new Vector3 (progressDot.transform.localPosition.x, yPos, progressDot.transform.localPosition.z);
			float dt = tunnelGenerator.distanceTraveled + ship.transform.position.z;
			if (dt < 1) {
				distanceTraveledText.text = "— 0u";
			} else {
				distanceTraveledText.text = "— " + (((int)dt).ToString ()) + "u";
			}
			yield return new WaitForSeconds (0.1f);


			if (ship.transform.position.z + tunnelGenerator.distanceTraveled >= tunnelGenerator.tunnelLength) {
				updateActive = false;
				progressDot.transform.localPosition = new Vector3 (progressDot.transform.localPosition.x, progressEnd.transform.localPosition.y, progressDot.transform.localPosition.z);
				distanceTraveledText.text = "— " + tunnelGenerator.tunnelLength + "u";

				progressEnd.GetComponent<MeshRenderer> ().material.color = goodColor;
				progressTunnel.GetComponent<MeshRenderer> ().material.color = goodColor;
				Material mymet = progressEnd.GetComponent<MeshRenderer> ().material;
			}
		}
	}

	IEnumerator UpdateEnginePanel(){
		while (true) {
			float yPos = 0;
			float xPos = 0;
			float zPos = 0;

			if (ship.speed < ship.minSpeed) {
				xPos = engineBottom.transform.localPosition.x;
				yPos = engineBottom.transform.localPosition.y;
				zPos = engineBottom.transform.localPosition.z;
			} else {
				xPos = Remap (ship.speed, ship.minSpeed, ship.maxSpeed, engineBottom.transform.localPosition.x, engineTop.transform.localPosition.x);
				yPos = Remap (ship.speed, ship.minSpeed, ship.maxSpeed, engineBottom.transform.localPosition.y, engineTop.transform.localPosition.y);
				zPos = Remap (ship.speed, ship.minSpeed, ship.maxSpeed, engineBottom.transform.localPosition.z, engineTop.transform.localPosition.z);
			}

			engineText.transform.localPosition = new Vector3 (xPos, yPos, zPos);
			if (ship.speed <= ship.minSpeed) {
				engineText.text = "BOOSTER 0%";
			} else {
				engineText.text = "BOOSTER " + ((int)(((ship.speed - ship.minSpeed) / (ship.maxSpeed - ship.minSpeed)) * 100)).ToString () + " %";
			}
			engineBar1.text = "";engineBar2.text = "";engineBar3.text = "";

			for(int i = 0; i < ((int)(((ship.speed - ship.minSpeed) / (ship.maxSpeed - ship.minSpeed)) * 10)); i++){
				engineBar1.text += "#";engineBar2.text += "#";engineBar3.text += "#";
			}

			yield return new WaitForSeconds(0.1f);
		}
	}


	public float Remap ( float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}





}


