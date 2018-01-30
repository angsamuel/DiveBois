using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelGenerator : MonoBehaviour {
	public float tunnelLength = 10000f;
	public float distanceTraveled = 0;

	public float timeLeft = 300; 

	public float tunnelWidth;
	public float tunnelHeight;
	public int generationDistance;
	public float minSlice, maxSlice;


	public int tunnelBlockSize;
	public Ship ship;
	public GameObject tunnelBlock;
	public GameObject tunnelObstacle;
	public GameObject stationGenerator;

	public bool tunnelActive = false;

	List<GameObject> blocks;
	List<GameObject> sides;
	public List<GameObject> obstacles;

	public int blockLimit;

	enum ObstacleLocation {up, down, left, right};
	public List<string> obstacleQueue;

	bool outOfBoundsActive = true;
	bool endCheckActive = true;
	bool obstaclesEnabled = true;
	// Use this for initialization
	void Start () {
		blocks = new List<GameObject> ();
		obstacles = new List<GameObject> ();
		sides = new List<GameObject> ();
		obstacleQueue = new List<string> ();

		StartCoroutine (PlaceObstacles(20f));
		StartCoroutine (PlaceBlocks());

		StartCoroutine (OutOfBoundsCheck ());

		StartCoroutine (EndCheck ());
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
	}

	IEnumerator EndCheck(){
		while (endCheckActive) {


			if (tunnelLength - (distanceTraveled + ship.transform.position.z) < 5000) {
				obstaclesEnabled = false;
			}

			if (distanceTraveled + ship.transform.position.z > tunnelLength) {
				outOfBoundsActive = false;
				obstaclesEnabled = false;
				StartCoroutine (ShipSlowDown ());
				endCheckActive = false;
			}

			yield return new WaitForSeconds (0.1f);
		}
	}

	IEnumerator ShipSlowDown(){
		
		bool spawnedStationGenerator = false;
		while (true) {
			ship.speed = ship.speed * .99f;
			if (ship.speed < 500) {
				tunnelActive = false;
				for (int i = 0; i < sides.Count; i++) {
					sides [i].transform.localScale = new Vector3 (0, 0, 0);
				}
			}

			if (!spawnedStationGenerator && ship.speed < 350 ) {
				GameObject.Instantiate(stationGenerator, new Vector3(0,0,ship.transform.position.z + 4000), Quaternion.identity);
				spawnedStationGenerator = true;
			}

			if (ship.speed < 100) {
				ship.speed = 0;
			}
			yield return new WaitForSeconds(.1f);
		}
	}

	IEnumerator OutOfBoundsCheck(){
			while (outOfBoundsActive) {
				yield return new WaitForSeconds (1f);
				if (ship.transform.position.z > 5000) {
					distanceTraveled += ship.transform.position.z;

					for (int i = blocks.Count - 1; i >= 0; i--) {
						blocks [i].transform.Translate (0, 0, -ship.transform.position.z);
					}
					for (int i = obstacles.Count - 1; i >= 0; i--) {
						obstacles [i].transform.Translate (0, 0, -ship.transform.position.z);
					}

				for (int i = sides.Count - 1; i >= 0; i--) {
					sides [i].transform.Translate (0, 0, -ship.transform.position.z);
				}

					ship.transform.position = new Vector3 (ship.transform.position.x, ship.transform.position.y, 0);
				}
			}

	}

	IEnumerator PlaceObstacles(float delay){
		yield return new WaitForSeconds (delay);
		StartCoroutine (ObstacleCleanup ());
		while (obstaclesEnabled) {
			if (tunnelActive) {
				//horizontal or verticle
				int orientation = Random.Range(0,2);

				float shipZ = ship.transform.position.z;

				//location
				int location = Random.Range(0,2);

				float obXScale = 1; 
				float obYScale = 1;
				float obXPos = 0;
				float obYPos = 0;


				if (orientation == 0) { //verticle
					obYScale = tunnelHeight * 2;
					obXScale = tunnelWidth;
					if (location == 0) { //left
						obXPos = -tunnelWidth / 2;
						obYPos = 0;
						obstacleQueue.Add ("left");

					} else if (location == 1) {//right
						obXPos = tunnelWidth / 2;
						obYPos = 0;
						obstacleQueue.Add ("right");

					}

				} else if (orientation == 1) { //horizontal
					obYScale = tunnelHeight;
					obXScale = tunnelWidth * 2;
					if (location == 0) {//up
						obXPos = 0;
						obYPos = tunnelHeight / 2;
						obstacleQueue.Add ("up");

					} else if (location == 1) {//down
						obXPos = 0;
						obYPos = -tunnelHeight / 2;
						obstacleQueue.Add ("down");
					}
				}
					
				GameObject newObstacle = GameObject.Instantiate (tunnelObstacle, new Vector3 (obXPos, obYPos, shipZ + generationDistance), Quaternion.identity);	
				newObstacle.transform.localScale = new Vector3 (obXScale, obYScale, 50);

				obstacles.Add (newObstacle);
				}
			yield return new WaitForSeconds (5f);
		}
	}

	IEnumerator ObstacleCleanup(){
		while (true) {
			if (obstacles.Count > 0) {
				if (obstacles[0].transform.position.z < ship.transform.position.z) {
					GameObject tempObstacle = obstacles [0];
					obstacles.RemoveAt (0);
					obstacleQueue.RemoveAt (0);
					Destroy (tempObstacle);
				}
			}
			Debug.Log ("CLEANUP");
			yield return new WaitForSeconds (.05f);
		}
	}

	IEnumerator PlaceSides(){

		yield return new WaitForSeconds (10);
		while (true) {
			if (tunnelActive) {
				float shipZ = ship.transform.position.z;

				if (sides.Count < 500) {
					GameObject leftSide = GameObject.Instantiate (tunnelBlock, new Vector3 (-tunnelWidth, 0, shipZ + generationDistance), Quaternion.identity);
					leftSide.transform.localScale = new Vector3 (.1f, 1 + (tunnelHeight * 2.1f), 1000);
					GameObject rightSide = GameObject.Instantiate (tunnelBlock, new Vector3 (tunnelWidth, 0, shipZ + generationDistance), Quaternion.identity);
					rightSide.transform.localScale = new Vector3 (.1f, 1 + (tunnelHeight * 2.1f), 1000);
					GameObject upSide = GameObject.Instantiate (tunnelBlock, new Vector3 (0, tunnelHeight, shipZ + generationDistance), Quaternion.identity);
					upSide.transform.localScale = new Vector3 ((tunnelWidth * 2.1f), .1f, 1000);
					GameObject downSide = GameObject.Instantiate (tunnelBlock, new Vector3 (0, -tunnelHeight, shipZ + generationDistance), Quaternion.identity);
					downSide.transform.localScale = new Vector3 (1 + (tunnelWidth * 2.1f), .1f, 1000);


					sides.Add (leftSide);
					sides.Add (rightSide);
					sides.Add (upSide);
					sides.Add (downSide);

				} else {
					for (int i = 0; i < 4; i++) {
						sides [0].transform.position = new Vector3 (sides [0].transform.position.x, sides [0].transform.position.y, shipZ + generationDistance);
						sides.Add (sides [0]);
						sides.RemoveAt (0);
					}
				}
				Debug.Log ("placed side");
			}
			yield return new WaitForSeconds (0.1f);

		}
	}

	IEnumerator PlaceBlocks(){
		StartCoroutine (PlaceSides ());
		while (true) {
			yield return new WaitForSeconds (.0001f);

			if (tunnelActive) {


				//float shipX = ship.transform.position.x;
				//float shipY = ship.transform.position.y;
				float shipZ = ship.transform.position.z;
				for (int i = 0; i < 2; i++) {
					if (blocks.Count < blockLimit) {
						GameObject newBlockLeft = GameObject.Instantiate (tunnelBlock, new Vector3 (-tunnelWidth, Random.Range (-tunnelHeight, tunnelHeight), shipZ + generationDistance), Quaternion.identity);
						newBlockLeft.transform.localScale = new Vector3 (Random.Range (minSlice, maxSlice), tunnelBlockSize, tunnelBlockSize);

						GameObject newBlockRight = GameObject.Instantiate (tunnelBlock, new Vector3 (tunnelWidth, Random.Range (-tunnelHeight, tunnelHeight), shipZ + generationDistance), Quaternion.identity);
						newBlockRight.transform.localScale = new Vector3 (Random.Range (minSlice, maxSlice), tunnelBlockSize, tunnelBlockSize);

						GameObject newBlockUp = GameObject.Instantiate (tunnelBlock, new Vector3 (Random.Range (-tunnelWidth, tunnelWidth), tunnelHeight, shipZ + generationDistance), Quaternion.identity);
						newBlockUp.transform.localScale = new Vector3 (tunnelBlockSize, Random.Range (minSlice, maxSlice), tunnelBlockSize);

						GameObject newBlockDown = GameObject.Instantiate (tunnelBlock, new Vector3 (Random.Range (-tunnelWidth, tunnelWidth), -tunnelHeight, shipZ + generationDistance), Quaternion.identity);
						newBlockDown.transform.localScale = new Vector3 (tunnelBlockSize, Random.Range (minSlice, maxSlice), tunnelBlockSize);

						blocks.Add (newBlockLeft);
						blocks.Add (newBlockRight);
						blocks.Add (newBlockUp);
						blocks.Add (newBlockDown);




					} else {
						blocks [0].transform.position = new Vector3 (-tunnelWidth, Random.Range (-tunnelHeight, tunnelHeight), shipZ + generationDistance);
						blocks [1].transform.position = new Vector3 (tunnelWidth, Random.Range (-tunnelHeight, tunnelHeight), shipZ + generationDistance);
						blocks [2].transform.position = new Vector3 (Random.Range (-tunnelWidth, tunnelWidth), tunnelHeight, shipZ + generationDistance);
						blocks [3].transform.position = new Vector3 (Random.Range (-tunnelWidth, tunnelWidth), -tunnelHeight, shipZ + generationDistance);

						blocks.Add (blocks [0]);
						blocks.Add (blocks [1]);
						blocks.Add (blocks [2]);
						blocks.Add (blocks [3]);
						blocks.RemoveAt (0);
						blocks.RemoveAt (0);
						blocks.RemoveAt (0);
						blocks.RemoveAt (0);
					}
				}


				//add Sides

			}
		}
	}
}
