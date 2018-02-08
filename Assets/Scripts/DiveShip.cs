﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveShip : MonoBehaviour {

	public Boi leftBoi, rightBoi, backBoi, playerBoi;



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
	public SpeedPanel speedPanel;
	public DialoguePanel dialoguePanel;
	public CrewPanel crewPanel;
	public Scanner scannerPanel;
	public TimeDisplay clockPanel;
	public SpeedPanel boosterPanel;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		
		LoadBois ();
		maxArmor = armor;
		maxShields = shields;

		rb = GetComponent<Rigidbody> ();
		dialoguePanel.transform.localScale = new Vector3(0,0,0);


		StartCoroutine (ShieldGenerator ());

		speedPanel.gameObject.SetActive (false);
		crewPanel.gameObject.SetActive (false);
		scannerPanel.gameObject.SetActive (false);
		clockPanel.gameObject.SetActive (false);
		boosterPanel.gameObject.SetActive (false);
		shipIntegrityPanel.gameObject.SetActive (false);
	}

	void LoadBois(){
		Crew shipCrew = new Crew ();
		shipCrew = shipCrew.Load ("MISSIONCREW");
		leftBoi = shipCrew.bois [0];
		backBoi = shipCrew.bois [1];
		rightBoi = shipCrew.bois [2];
		Debug.Log (leftBoi.name);
	}
	
	// Update is called once per frame

	bool canToggleHealth = true;
	bool canToggleScanner = true;
	bool canToggleBooster = true;
	bool healthMode = false;
	bool scannerMode = false;
	bool boosterMode = false;
	bool clockMode = false;
	bool canToggleClock = true;
	bool crewMode = false;
	bool canToggleCrew = true;


	void Update () {
		ScanForInput ();
		shipIntegrityPanel.SetValues (shields, maxShields, armor, maxArmor);
		speedPanel.SetValues (currentSpeed, minSpeed, maxSpeed);

		float deltaPos = transform.position.z - pos;
		if (Mathf.Abs (deltaPos) < 15000) {
			distanceTraveled += Mathf.Abs (deltaPos);
		}

		crewPanel.SetValues (leftBoi, backBoi, rightBoi);
		pos = transform.position.z;


	
	}
		

	void ScanForInput(){
		if (Input.GetAxisRaw ("ToggleHealthPanel") != 0 && healthMode && canToggleHealth) {
			shipIntegrityPanel.gameObject.SetActive (false);
			canToggleHealth = false;
			healthMode = !healthMode;
		} else if (Input.GetAxisRaw ("ToggleHealthPanel") != 0 && !healthMode && canToggleHealth) {
			shipIntegrityPanel.gameObject.SetActive (true);
			canToggleHealth = false;
			healthMode = !healthMode;
		} else if (Input.GetAxisRaw ("ToggleHealthPanel") == 0) {
			canToggleHealth = true;
		}

		if (Input.GetAxisRaw ("ToggleScannerPanel") != 0 && scannerMode && canToggleScanner) {
			scannerPanel.gameObject.SetActive (false);
			canToggleScanner = false;
			scannerMode = !scannerMode;
		} else if (Input.GetAxisRaw ("ToggleScannerPanel") != 0 && !scannerMode && canToggleScanner) {
			scannerPanel.gameObject.SetActive (true);
			canToggleScanner = false;
			scannerMode = !scannerMode;
		} else if (Input.GetAxisRaw ("ToggleScannerPanel") == 0) {
			canToggleScanner = true;
		}

		if (Input.GetAxisRaw ("ToggleClockPanel") != 0 && clockMode && canToggleClock) {
			clockPanel.gameObject.SetActive (false);
			canToggleClock = false;
			clockMode = !clockMode;
		} else if (Input.GetAxisRaw ("ToggleClockPanel") != 0 && !clockMode && canToggleClock) {
			clockPanel.gameObject.SetActive (true);
			canToggleClock = false;
			clockMode = !clockMode;
		} else if (Input.GetAxisRaw ("ToggleClockPanel") == 0) {
			canToggleClock = true;
		}

		if (Input.GetAxisRaw ("ToggleBoosterPanel") != 0 && boosterMode && canToggleBooster) {
			boosterPanel.gameObject.SetActive (false);
			canToggleBooster = false;
			boosterMode = !boosterMode;
		} else if (Input.GetAxisRaw ("ToggleBoosterPanel") != 0 && !boosterMode && canToggleBooster) {
			boosterPanel.gameObject.SetActive (true);
			canToggleBooster = false;
			boosterMode = !boosterMode;
		} else if (Input.GetAxisRaw ("ToggleBoosterPanel") == 0) {
			canToggleBooster = true;
		}

		if (Input.GetAxisRaw ("ToggleCrewPanel") != 0 && crewMode && canToggleCrew) {
			crewPanel.gameObject.SetActive (false);
			canToggleCrew = false;
			crewMode = !crewMode;
		} else if (Input.GetAxisRaw ("ToggleCrewPanel") != 0 && !crewMode && canToggleCrew) {
			crewPanel.gameObject.SetActive (true);
			canToggleCrew = false;
			crewMode = !crewMode;
		} else if (Input.GetAxisRaw ("ToggleCrewPanel") == 0) {
			canToggleCrew = true;
		}


		if (shipActive) {
			//HUD

		





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
				//StartCoroutine (GrindWalls ());
			}

			if (transform.position.x > 39) {
				StartCoroutine (GrindWalls (1));
			}	else if (transform.position.x < -39) {
				StartCoroutine (GrindWalls (-1));
			}	

			transform.position = new Vector3 (newX, newY, transform.position.z);
		}
	}

	bool canGrindWalls = true;
	IEnumerator GrindWalls(int direction){
		if (canGrindWalls) {
			TakeDamage (5, direction);
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
			TakeDamage (50, 0);
			damageEffectController.DamageEffect ();
			canTakeCollision = false;

			yield return new WaitForSeconds (.1f);
			canTakeCollision = true;
		}

	}

	void TakeDamage(float damage, int direction){
		shields = shields -damage;
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
		dialoguePanel.transform.localScale = new Vector3(1,1,1);
		dialoguePanel.WriteMessage ("ATTENTION! Our team has taken your power core hostage! Surrender immediately!");
	}



}

