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

	bool tunnelActive = true;

	List<GameObject> blocks;
	public int blockLimit;


	// Use this for initialization
	void Start () {
		blocks = new List<GameObject> ();
		StartCoroutine (PlaceObstacles());
		StartCoroutine (PlaceBlocks());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator PlaceObstacles(){
		while (tunnelActive) {

			yield return new WaitForSeconds (5);
			float shipZ = ship.transform.position.z;

			Debug.Log ("placed obstacle");
			GameObject newBlockLeft = GameObject.Instantiate (tunnelBlock, new Vector3 (Random.Range (-tunnelWidth, tunnelWidth), Random.Range (-tunnelHeight, tunnelHeight), shipZ + generationDistance), Quaternion.identity);
			newBlockLeft.transform.localScale = new Vector3 (200, tunnelBlockSize / 1.5f, tunnelBlockSize);
		}
	}
	IEnumerator PlaceBlocks(){
		while (tunnelActive) {
			yield return new WaitForSeconds (.005f);

			float shipX = ship.transform.position.x;
			float shipY = ship.transform.position.y;
			float shipZ = ship.transform.position.z;

			if (blocks.Count < blockLimit) {
				GameObject newBlockLeft = GameObject.Instantiate (tunnelBlock, new Vector3 (-tunnelWidth, Random.Range(-tunnelHeight, tunnelHeight), shipZ + generationDistance), Quaternion.identity);
				newBlockLeft.transform.localScale = new Vector3 (Random.Range(minSlice,maxSlice), tunnelBlockSize, tunnelBlockSize);

				GameObject newBlockRight = GameObject.Instantiate (tunnelBlock, new Vector3 (tunnelWidth, Random.Range(-tunnelHeight, tunnelHeight), shipZ + generationDistance), Quaternion.identity);
				newBlockRight.transform.localScale = new Vector3 (Random.Range(minSlice,maxSlice),tunnelBlockSize,tunnelBlockSize);

				GameObject newBlockUp = GameObject.Instantiate (tunnelBlock, new Vector3 (Random.Range(-tunnelWidth, tunnelWidth), tunnelHeight, shipZ + generationDistance), Quaternion.identity);
				newBlockUp.transform.localScale = new Vector3 (tunnelBlockSize, Random.Range(minSlice,maxSlice),tunnelBlockSize);

				GameObject newBlockDown = GameObject.Instantiate (tunnelBlock, new Vector3 (Random.Range(-tunnelWidth, tunnelWidth),-tunnelHeight, shipZ + generationDistance), Quaternion.identity);
				newBlockDown.transform.localScale = new Vector3 (tunnelBlockSize, Random.Range(minSlice,maxSlice),tunnelBlockSize);

				blocks.Add (newBlockLeft); blocks.Add (newBlockRight); blocks.Add (newBlockUp); blocks.Add (newBlockDown);
			} else {
				blocks [0].transform.position = new Vector3 (-tunnelWidth, Random.Range (-tunnelHeight, tunnelHeight), shipZ + generationDistance);
				blocks [1].transform.position = new Vector3 (tunnelWidth, Random.Range(-tunnelHeight, tunnelHeight), shipZ + generationDistance);
				blocks [2].transform.position = new Vector3 (Random.Range(-tunnelWidth, tunnelWidth), tunnelHeight, shipZ + generationDistance);
				blocks [3].transform.position = new Vector3 (Random.Range(-tunnelWidth, tunnelWidth),-tunnelHeight, shipZ + generationDistance);

				blocks.Add (blocks [0]);blocks.Add (blocks [1]);blocks.Add (blocks [2]);blocks.Add (blocks [3]);
				blocks.RemoveAt (0);blocks.RemoveAt (0);blocks.RemoveAt (0);blocks.RemoveAt (0);
			}
		}
	}
}
