using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelGenerator : MonoBehaviour {

	public int tunnelWidth;
	public int tunnelHeight;
	public int generationDistance;
	public float minSlice, maxSlice;


	public int tunnelBlockSize;
	public GameObject ship;
	public GameObject tunnelBlock;
	public GameObject tunnelObstacle;

	public bool tunnelActive = false;

	List<GameObject> blocks;
	List<GameObject> obstacles;

	public int blockLimit;

	enum ObstacleLocation {up, down, left, right};
	public List<string> obstacleQueue;


	// Use this for initialization
	void Start () {
		blocks = new List<GameObject> ();
		obstacles = new List<GameObject> ();
		obstacleQueue = new List<string> ();

		StartCoroutine (PlaceObstacles(20f));
		StartCoroutine (PlaceBlocks());

		StartCoroutine (OutOfBoundsCheck ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator OutOfBoundsCheck(){
		while (true) {
			yield return new WaitForSeconds (5);
			if (ship.transform.position.z > 25000) {
				ship.transform.position = new Vector3 (ship.transform.position.x, ship.transform.position.y, 0);
				for (int i = 0; i < blocks.Count; i++) {
					blocks [i].transform.Translate (0, 0, -25000);
				}
				for (int i = 0; i < obstacles.Count; i++) {
					obstacles [i].transform.Translate (0, 0, -25000);
				}
			}
		}

	}

	IEnumerator PlaceObstacles(float delay){
		yield return new WaitForSeconds (delay);
		StartCoroutine (ObstacleCleanup ());
		while (true) {
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
				newObstacle.transform.localScale = new Vector3 (obXScale, obYScale, 1);

				obstacles.Add (newObstacle);

				Debug.Log ("placed obstacle");
			}
			yield return new WaitForSeconds (2);
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
			yield return new WaitForSeconds (.5f);
		}
	}

	IEnumerator PlaceBlocks(){
		while (true) {
			yield return new WaitForSeconds (.0001f);

			if (tunnelActive) {

				//float shipX = ship.transform.position.x;
				//float shipY = ship.transform.position.y;
				float shipZ = ship.transform.position.z;

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
		}
	}
}
