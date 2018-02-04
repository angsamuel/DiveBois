using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveShip : MonoBehaviour {
	public bool shipActive = false;
	public float armor = 100;
	public float shields = 100;

	public float backThrusterPower;
	public float directionalThrusterPower;
	public float maxDirectionalSpeed;

	float maxArmor;
	float maxShields;

	public float currentSpeed = 50;
	float maxSpeed = 5000;
	float minSpeed = 1000;

	public float distanceTraveled = -7000;

	float pos = 0;
	bool canTakeCollision = true;


	//online
	bool shieldGeneratorOnline = true;

	//interface
	public ShipIntegrityPanel shipIntegrityPanel;
	public DamageEffectController damageEffectController;


	Rigidbody rb;

	// Use this for initialization
	void Start () {
		maxArmor = armor;
		maxShields = shields;

		rb = GetComponent<Rigidbody> ();


		StartCoroutine (ShieldGenerator ());
	}
	
	// Update is called once per frame
	void Update () {
		ScanForInput ();
		shipIntegrityPanel.SetValues (shields, maxShields, armor, maxArmor);

		float deltaPos = transform.position.z - pos;
		if (Mathf.Abs (deltaPos) < 15000) {
			distanceTraveled += Mathf.Abs (deltaPos);
		}


		pos = transform.position.z;
	}
		

	void ScanForInput(){
		if (shipActive) {
			//directions
			if (Input.GetAxisRaw ("Vertical") < 0) {
				//rb.AddForce (0, -directionalThrusterPower, 0);	
				rb.velocity = new Vector3 (rb.velocity.x, -maxDirectionalSpeed, rb.velocity.z);
			}

			if (Input.GetAxisRaw ("Vertical") > 0) {
				//rb.AddForce (0, directionalThrusterPower, 0);	
				rb.velocity = new Vector3 (rb.velocity.x, maxDirectionalSpeed, rb.velocity.z);
			}

			if (Input.GetAxisRaw ("Vertical") == 0) {
				rb.velocity = new Vector3 (rb.velocity.x, 0, rb.velocity.z);
			}

			if (Input.GetAxisRaw ("Horizontal") < 0) {
				//rb.AddForce (-directionalThrusterPower, 0, 0);
				rb.velocity = new Vector3 (-maxDirectionalSpeed, rb.velocity.y, rb.velocity.z);
			}
			if (Input.GetAxisRaw ("Horizontal") > 0) {
				//rb.AddForce (directionalThrusterPower, 0, 0);
				rb.velocity = new Vector3 (maxDirectionalSpeed, rb.velocity.y, rb.velocity.z);
			}

			if (Input.GetAxisRaw ("Horizontal") == 0) {
				rb.velocity = new Vector3 (0, rb.velocity.y, rb.velocity.z);
			}

			//booster
			if (Input.GetAxisRaw ("Engine") > 0) {
				currentSpeed += 10; // use IEnumerator later
			} else if (Input.GetAxisRaw ("Engine") < 0) {
				currentSpeed -= 10;
			}

			if (currentSpeed > maxSpeed) {
				currentSpeed = maxSpeed;
			} else if (currentSpeed < minSpeed) {
				currentSpeed = minSpeed;
			}




			float newX = transform.position.x;
			float newY = transform.position.y;

			//correct velocity
			rb.velocity = new Vector3 (rb.velocity.x, rb.velocity.y, currentSpeed);

			if (rb.velocity.x > maxDirectionalSpeed) {
				rb.velocity = new Vector3 (maxDirectionalSpeed, rb.velocity.y, rb.velocity.z);
			} else if (rb.velocity.x < -maxDirectionalSpeed) {
				rb.velocity = new Vector3 (-maxDirectionalSpeed, rb.velocity.y, rb.velocity.z);
			}


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

			if (Mathf.Abs (transform.position.x) > 39 || Mathf.Abs (transform.position.y) > 39) {
				StartCoroutine (GrindWalls ());
			}

			transform.position = new Vector3 (newX, newY, transform.position.z);
		}
	}

	bool canGrindWalls = true;
	IEnumerator GrindWalls(){
		if (canGrindWalls) {
			TakeDamage (5);
			canGrindWalls = false;
			yield return new WaitForSeconds (.1f);
			canGrindWalls = true;
		}
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.tag == "Obstacle") {
			other.gameObject.tag = "Untagged";
			StartCoroutine(TakeCollision());
		}
	}

	IEnumerator TakeCollision(){
		if (canTakeCollision) {
			TakeDamage (50);
			damageEffectController.DamageEffect ();
			canTakeCollision = false;

			yield return new WaitForSeconds (.1f);
			canTakeCollision = true;
		}

	}

	void TakeDamage(float damage){
		shields = shields - damage;
		if (shields < 0) {
			armor = armor + shields;
			shields = 0;
		}

		if (armor < 0) {
			//Die ();
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
	
	public void Halt(){
		currentSpeed = 0;
		rb.velocity = new Vector3(0,0,0);
		shipActive = false;
	}

}

