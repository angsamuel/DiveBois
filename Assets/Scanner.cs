using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour {
	public Ship ship;
	public GameObject scannerDot;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator UpdateScanner(){
		while (true) {
			//scannerDot.transform.position = new Vector3 (Remap (ship.transform.position.x,-60f, 60, -.45f, .45f), Remap (ship.transform.position.y,-60f, 60, -.45f, .45f), scannerDot.transform.position.z);
			//scannerDot.transform.position = new Vector3 (.45f,scannerDot.transform.position.y, scannerDot.transform.position.z);

			//scannerDot.transform.position = new Vector3 (scannerDot.transform.position.x, scannerDot.transform.position.y, scannerDot.transform.position.z);

			Vector3 newPos = new Vector3 (.45f, .45f, -.6f);

			scannerDot.transform.localPosition = newPos;
			yield return new WaitForSeconds(0.1f);
			Debug.Log ("NEW POS: " +newPos);
		}

	}




	public float Remap ( float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}


}
