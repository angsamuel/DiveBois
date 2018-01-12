using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour {
	public float minSpeed = 200;
	public float maxSpeed = 600;
	public float speed = 0;

	public float sensativity = 20;
	public TextMesh modeText;
	public TextMesh messageText;
	bool blinkModeActive = true;

	bool canDive = false;

	public Color goodColor;
	public Color badColor;

	public GameObject frontScreen, leftScreen, rightScreen, topScreen;

	public TunnelGenerator tunnelGenerator;

	// Use this for initialization
	void Start () {
		StartCoroutine (BlinkMode ());
		//StartCoroutine(BlinkScreensOn ());
		StartCoroutine(ApproachInStealth());
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
		speed = minSpeed;

	}



	IEnumerator ApproachInStealth(){
		yield return new WaitForSeconds (5);
		StartCoroutine (TypeText (messageText, "ARRIVED AT TARGET \n PRESS [SPACE] TO DIVE"));
		yield return new WaitForSeconds (3);
		canDive = true;
	}


	IEnumerator TypeText(TextMesh tm, string message){
		float textDelay = 0.05f;
		tm.text = "";
		for (int i = 0; i < message.Length; i++) {
			Debug.Log (message [i]);
			yield return new WaitForSeconds (textDelay);
			tm.text += message [i];
		}
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


	IEnumerator LiftScreenVeils(){
		yield return new WaitForSeconds (1.5f);
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
		yield return new WaitForSeconds (1);
		//frontScreen.GetComponent<MeshRenderer> ().material.color = new Color(0,0,0,0f);
		StartCoroutine (BlinkScreenOn (frontScreen)); StartCoroutine (BlinkScreenOn (leftScreen));
	    StartCoroutine (BlinkScreenOn (rightScreen)); StartCoroutine (BlinkScreenOn (topScreen));
		StartCoroutine(LiftScreenVeils ());

	}

	IEnumerator BlinkScreenOn(GameObject screen){
		int blinks = Random.Range (10, 20);
		for (int i = 0; i < blinks; i++) {
			screen.GetComponent<MeshRenderer> ().material.color = new Color (0, 0, 0, Random.Range (0f, 1f));
			yield return new WaitForSeconds (Random.Range(0.1f, 0.05f));
		}
		screen.GetComponent<MeshRenderer> ().material.color = new Color (0, 0, 0, 1);
	}



	// Update is called once per frame
	void Update () {



		ScanForInput ();
	}

	void ScanForInput(){
		Vector3 shipVelocity = GetComponent<Rigidbody> ().velocity;

		if (Input.GetAxisRaw ("Vertical") > 0) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (shipVelocity.x, sensativity, 0);
		}
		if (Input.GetAxisRaw ("Vertical") < 0) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (shipVelocity.x, -sensativity, 0);
		}

		shipVelocity = GetComponent<Rigidbody> ().velocity;

		if (Input.GetAxisRaw ("Horizontal") > 0) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (sensativity, shipVelocity.y, 0);
		}
		if (Input.GetAxisRaw ("Horizontal") < 0) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (-sensativity, shipVelocity.y, 0);
		}

		shipVelocity = GetComponent<Rigidbody> ().velocity;

		if (Input.GetAxisRaw ("Vertical") == 0) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (shipVelocity.x, 0, 0);
		}

		shipVelocity = GetComponent<Rigidbody> ().velocity;

		if (Input.GetAxisRaw ("Horizontal") == 0) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (0, shipVelocity.y, 0);
		}

		shipVelocity = GetComponent<Rigidbody> ().velocity;

		GetComponent<Rigidbody> ().velocity = new Vector3 (shipVelocity.x, shipVelocity.y, speed);

		//check boundaries, may need to change for different layouts
		if (transform.position.x < -(float)tunnelGenerator.tunnelWidth * .6f && shipVelocity.x < 0) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (0, shipVelocity.y, speed);
		} else if (transform.position.x > (float)tunnelGenerator.tunnelWidth * .6f && shipVelocity.x > 0) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (0, shipVelocity.y, speed);
		}


		if (transform.position.y < -(float)tunnelGenerator.tunnelHeight * .6f && shipVelocity.y < 0) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (shipVelocity.x, 0, speed);
		}else if (transform.position.y > (float)tunnelGenerator.tunnelHeight * .6f && shipVelocity.y > 0) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (shipVelocity.x, 0, speed);
		}



		if (Input.GetAxisRaw ("Dive") != 0 && canDive) {
			Dive ();
			canDive = false;
		}




	}
}
