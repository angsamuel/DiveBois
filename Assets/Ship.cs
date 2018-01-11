using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	float speed = 200;
	float sensativity = 20;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
	}
}
