using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
	public int levelLength;

	public GameObject wallDecorator;
	public GameObject levelExterior;
	public GameObject generatorCore;

	int numDecorationsPerWall = 500;

	public GameObject obstacle;
	List<GameObject> decoratorPool;
	List<GameObject> obstaclePool;

	public FollowScript tunnelFollow;
	public DiveShip diveShip;
	public FollowScript cullingFollow;

	bool obstacleSpawningEnabled = true;


	// Use this for initialization
	void Start () {
		decoratorPool = new List<GameObject> ();
		obstaclePool = new List<GameObject> ();
		//decorate right side
		//left
		for (int i = 0; i < numDecorationsPerWall; i++) {
			GameObject tmp = Instantiate (wallDecorator) as GameObject;
			tmp.transform.position = new Vector3(-50, Random.Range(-50,50) , Random.Range(-5000f, 5000));
			tmp.transform.localScale = new Vector3 (Random.Range(2, 12), tmp.transform.localScale.y, tmp.transform.localScale.z);
			tmp.transform.SetParent (transform);
			decoratorPool.Add (tmp);
		}
		//right
		for (int i = 0; i < numDecorationsPerWall; i++) {
			GameObject tmp = Instantiate (wallDecorator) as GameObject;
			tmp.transform.position = new Vector3(50, Random.Range(-50,50) , Random.Range(-5000f, 5000));
			tmp.transform.localScale = new Vector3 (Random.Range(2, 12), tmp.transform.localScale.y, tmp.transform.localScale.z);
			tmp.transform.SetParent (transform);
			decoratorPool.Add (tmp);
		}

		//bottom
		for (int i = 0; i < numDecorationsPerWall; i++) {
			GameObject tmp = Instantiate (wallDecorator) as GameObject;
			tmp.transform.position = new Vector3(Random.Range(-50,50),-50, Random.Range(-5000f, 5000));
			tmp.transform.localScale = new Vector3 (tmp.transform.localScale.x, Random.Range(2, 12), tmp.transform.localScale.z);
			tmp.transform.SetParent (transform);
			decoratorPool.Add (tmp);
		}

		//top
		for (int i = 0; i < numDecorationsPerWall; i++) {
			GameObject tmp = Instantiate (wallDecorator) as GameObject;
			tmp.transform.position = new Vector3(Random.Range(-50,50),50, Random.Range(-5000f, 5000));
			tmp.transform.localScale = new Vector3 (tmp.transform.localScale.x, Random.Range(2, 12), tmp.transform.localScale.z);
			tmp.transform.SetParent (transform);
			decoratorPool.Add (tmp);
		}

		StartCoroutine (SpawnObstacles());
		StartCoroutine (Countdown ());
		StartCoroutine (UpdateScanner ());
	}

	bool levelStopped = false;
	// Update is called once per frame
	void Update () {
		if (diveShip.transform.position.z > tunnelFollow.transform.position.z) {
			tunnelFollow.StartFollow ();
		}
		if (diveShip.distanceTraveled >= levelLength && !levelStopped) {
			StartCoroutine (EndLevelRoutine ());
			obstacleHeadStart = -5000;
			Destroy (levelExterior);
			levelStopped = true;

			if (diveShip.transform.position.z > 15000) {
				diveShip.transform.Translate (0, 0, -15000);
				for (int i = 0; i < decoratorPool.Count; i++) {
					decoratorPool [i].transform.Translate (0, 0, -15000);
				}
				for (int i = 0; i < obstaclePool.Count; i++) {
					obstaclePool [i].transform.Translate (0, 0, -15000);
				}
			}

		}

		CorrectLevel ();
	}

	IEnumerator EndLevelRoutine(){
		while (diveShip.distanceTraveled <= levelLength + 15000) {
			yield return null;
		}
		correctEnabled = false;
		cullingFollow.enabled = false;
		tunnelFollow.enabled = false;
		Instantiate (generatorCore, new Vector3 (0, 0, tunnelFollow.transform.position.z + 9000), Quaternion.identity);
		tunnelFollow.transform.Translate (0, 0, -700);
		while(diveShip.transform.position.z < tunnelFollow.transform.position.z + 9000){
			yield return null;
		}
		diveShip.Halt ();

	}

	float obstacleSpawnDelay = 1;
	int obstacleMilestone = 5000;
	int obstacleHeadStart = 5000;
	int milestoneCount = 0;
	IEnumerator SpawnObstacles(){
		while (obstacleSpawningEnabled) {
			yield return new WaitForSeconds (obstacleSpawnDelay);
			if(milestoneCount < (int)(diveShip.distanceTraveled / obstacleMilestone)){
				Debug.Log (milestoneCount + " < " + (int)(diveShip.distanceTraveled / obstacleMilestone));
				milestoneCount = (int)(diveShip.distanceTraveled / obstacleMilestone);

				GameObject tmp;

				if (obstaclePool.Count > 0 && obstaclePool [0].GetComponent<Obstacle> ().passed) {
					tmp = obstaclePool [0];
					obstaclePool.RemoveAt (0);
					tmp.transform.position = new Vector3 (0, 0, diveShip.transform.position.z + obstacleHeadStart);
					tmp.GetComponent<Obstacle> ().passed = false;
					tmp.tag = "Obstacle";

				} else {
					tmp = GameObject.Instantiate (obstacle, new Vector3 (0, 0, diveShip.transform.position.z + obstacleHeadStart), Quaternion.identity);
				}

				int orientation = Random.Range (0, 4);
				if (orientation == 0) {
					tmp.transform.Translate (-50, 0, 0);
					tmp.GetComponent<Obstacle> ().orientation = "vertleft";
				}else if (orientation == 1) {
					tmp.transform.Translate (50, 0, 0);
					tmp.GetComponent<Obstacle> ().orientation = "vertright";
				}else if (orientation == 2) {
					tmp.transform.Translate (0, -50, 0);
					tmp.GetComponent<Obstacle> ().orientation = "horzdown";
				}else if (orientation == 3) {
					tmp.transform.Translate (0, 50, 0);
					tmp.GetComponent<Obstacle> ().orientation = "horzup";
				}

				obstaclePool.Add (tmp);

			}
		}

	}

	int maxLoc = 12000;
	bool correctEnabled = true;
	void CorrectLevel(){ //avoid floating point errors
		if (correctEnabled) {
			if (diveShip.transform.position.z > maxLoc) {
				diveShip.transform.Translate (0, 0, -maxLoc/2);
				for (int i = 0; i < decoratorPool.Count; i++) {
					decoratorPool [i].transform.Translate (0, 0, -maxLoc/2);
				}
				for (int i = 0; i < obstaclePool.Count; i++) {
					obstaclePool [i].transform.Translate (0, 0, -maxLoc/2);
				}
			}
		}
			
	}

	public int timeLimit = 80;
	public TimeDisplay timeDisplay; 
	bool countdownActive = true;
	IEnumerator Countdown(){
		while (countdownActive) {
			yield return new WaitForSeconds (1);
			timeLimit -= 1;
			int timeToTarget = (int)((levelLength - diveShip.distanceTraveled) / diveShip.GetComponent<Rigidbody> ().velocity.z);

			timeDisplay.SetValues (timeLimit, timeToTarget);
		}
	}

	bool scannerActive = true;
	public Scanner scanner;
	IEnumerator UpdateScanner(){
		while (scannerActive) {
			yield return new WaitForSeconds (0.1f);
			float minDistance = 1000000;
			string minOrient = "";
			for (int i = 0; i < obstaclePool.Count; i++) {
				float tmpDistance = obstaclePool [i].transform.position.z - diveShip.transform.position.z;
				if (obstaclePool [i].transform.position.z > diveShip.transform.position.z) {
					if (tmpDistance < minDistance) {
							minDistance = tmpDistance;
							minOrient = obstaclePool [i].GetComponent<Obstacle> ().orientation;
					}
				}
			}
			scanner.SetValues (diveShip.transform.position, 49.0f);
			scanner.SetBarrier (minOrient);
		}
	}
}
