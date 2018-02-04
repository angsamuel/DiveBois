using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour {
	public float minSpeed = 200;
	public float maxSpeed = 600;
	public float speed = 0;

	public float shields, maxShields = 100;
	public float armor, maxArmor = 100;

	public float sensativity = 20;


	public TextMesh modeText;
	public TextMesh messageText;
	bool blinkModeActive = true;

	bool canDive = false;
	bool canTakeCollision = true;
	bool shieldsOnline = true;

	public TunnelGenerator tunnelGenerator;
	public InterfaceController interfaceController;


	// Use this for initialization
	void Start () {
		StartCoroutine (ShieldGenerator ());
	}
		

	IEnumerator ShieldGenerator(){
		while (shieldsOnline) {

			if (shields < 100) {
				shields += 1f;
			}
			if (shields > maxShields) {
				shields = maxShields;
			}
			yield return new WaitForSeconds (1f);

		}
	}


	void OnTriggerEnter(Collider other) {
		Debug.Log ("COLLISION"); //put a timer on this
		StartCoroutine(TakeCollision());
	}



	IEnumerator TakeCollision(){
		if (canTakeCollision) {
			shields = shields - 50;
			if (shields < 0) {
				armor = armor + shields;
				shields = 0;
			}

			if (armor < 0) {
				Die ();
			}
			canTakeCollision = false;
			interfaceController.DamageEffect ();
			yield return new WaitForSeconds (.1f);
			canTakeCollision = true;
		}

	}



	bool inputEnabled = true;
	// Update is called once per frame
	void Update () {
		if (inputEnabled) {
			ScanForInput ();
		}
	}



	void ScanForInput(){

		if (Input.GetAxisRaw ("Engine") > 0 ) {
			speed += 100;
			if (speed > maxSpeed) {
				speed = maxSpeed;
			}
		} else if (Input.GetAxisRaw ("Engine") < 0) {
			speed -= 100;
			if (speed < minSpeed) {
				speed = minSpeed;
			}
		}

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


		shipVelocity = GetComponent<Rigidbody> ().velocity;

		if (transform.position.y < -(float)tunnelGenerator.tunnelHeight * .6f && shipVelocity.y < 0) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (shipVelocity.x, 0, speed);
		}else if (transform.position.y > (float)tunnelGenerator.tunnelHeight * .6f && shipVelocity.y > 0) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (shipVelocity.x, 0, speed);
		}

	}

	void Die(){
		speed = 1500;
		GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-100,100),Random.Range(-100,100),Random.Range(-100,100));
	}
}
