﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
	public GameObject wallDecorator;
	int numDecorationsPerWall = 500;

	public GameObject obstacle;
	List<GameObject> decoratorPool;
	List<GameObject> obstaclePool;

	public FollowScript tunnelFollow;
	public DiveShip diveShip;

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

		StartCoroutine (SpawnObstacles());

	}
	
	// Update is called once per frame
	void Update () {
		if (diveShip.transform.position.z > tunnelFollow.transform.position.z) {
			tunnelFollow.StartFollow ();
		}
		CorrectLevel ();
	}

	float obstacleSpawnDelay = 3;
	IEnumerator SpawnObstacles(){
		while (obstacleSpawningEnabled) {
			yield return new WaitForSeconds (obstacleSpawnDelay);
			GameObject tmp = GameObject.Instantiate (obstacle, new Vector3 (0, 0, diveShip.transform.position.z + 5000), Quaternion.identity);
			int orientation = Random.Range (0, 4);
			if (orientation == 0) {
				tmp.transform.Translate (-50, 0, 0);
			}
			if (orientation == 1) {
				tmp.transform.Translate (50, 0, 0);
			}
			if (orientation == 2) {
				tmp.transform.Translate (0, -50, 0);
			}
			if (orientation == 3) {
				tmp.transform.Translate (0, 50, 0);
			}

			obstaclePool.Add (tmp);
			Debug.Log ("SPAWNED OBSTACLE");
		}

	}


	void CorrectLevel(){ //avoid floating point errors
		if (diveShip.transform.position.z > 15000) {
			diveShip.transform.Translate (0, 0, -30000);
			for (int i = 0; i < decoratorPool.Count; i++) {
				decoratorPool [i].transform.Translate (0, 0, -30000);
			}
			for (int i = 0; i < obstaclePool.Count; i++) {
				obstaclePool [i].transform.Translate (0, 0, -30000);
				if (obstaclePool [i].transform.position.z < diveShip) {

				}
			}
		}
	}
}
