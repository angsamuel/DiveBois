using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveShip : MonoBehaviour {

	public float armor = 100;
	public float shields = 100;

	float maxArmor;
	float maxShields;

	bool canTakeCollision = true;


	//online
	bool shieldGeneratorOnline = true;

	//interface
	public ShipIntegrityPanel shipIntegrityPanel;



	// Use this for initialization
	void Start () {
		maxArmor = armor;
		maxShields = shields;
		GetComponent<Rigidbody> ().AddForce (0, 0, 50000);	


		StartCoroutine (ShieldGenerator ());
	}
	
	// Update is called once per frame
	void Update () {
		ScanForInput ();
		shipIntegrityPanel.SetValues (shields, maxShields, armor, maxArmor);

	}
		

	void ScanForInput(){
		if (Input.GetAxis ("Vertical") < 0) {
			GetComponent<Rigidbody> ().AddForce (0, -50, 0);	
		}

		if (Input.GetAxis ("Vertical") > 0) {
			GetComponent<Rigidbody> ().AddForce (0, 50, 0);	
		}

		if (Input.GetAxis ("Horizontal") < 0) {
				GetComponent<Rigidbody> ().AddForce (-50, 0, 0);
		}
		if (Input.GetAxis ("Horizontal") > 0) {
			if (transform.position.x < 40) {
				GetComponent<Rigidbody> ().AddForce (50, 0, 0);
			} else {
				transform.position = new Vector3 (40, transform.position.y, transform.position.z);
			}
		}

		float newX = transform.position.x;
		float newY = transform.position.y;


		//correct position
		if (transform.position.x > 40) {
			newX = 40;
		} else if (transform.position.x < -40) {
			newX = -40;
		}

		if (transform.position.y > 40) {
			newY = 40;
		} else if (transform.position.y < -40) {
			newY = -40;
		}

		transform.position = new Vector3 (newX, newY, transform.position.z);
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.tag == "Obstacle") {
			other.gameObject.tag = "Untagged";
			StartCoroutine(TakeCollision());
		}
	}

	IEnumerator TakeCollision(){
		if (canTakeCollision) {
			shields = shields - 50;
			if (shields < 0) {
				armor = armor + shields;
				shields = 0;
			}

			if (armor < 0) {
				//Die ();
			}
			canTakeCollision = false;

			yield return new WaitForSeconds (.1f);
			canTakeCollision = true;
		}

	}

	IEnumerator ShieldGenerator(){
		while (shieldGeneratorOnline) {
			yield return new WaitForSeconds (0.1f);
			if (shields < 100) {
				shields += 1;
			} 
			if (shields > 100) {
				shields = 100;
			}
		}
	}
	

}

