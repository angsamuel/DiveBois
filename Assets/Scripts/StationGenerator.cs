using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationGenerator : MonoBehaviour {

	public GameObject innerRing;
	public GameObject centerRing;
	public GameObject outerRing;


	// Use this for initialization
	void Start () {
		innerRing.GetComponent<Rigidbody> ().angularVelocity = new Vector3 (-4, -4, -4);

		centerRing.GetComponent<Rigidbody> ().angularVelocity = new Vector3 (3, 3, 3);
		outerRing.GetComponent<Rigidbody> ().angularVelocity = new Vector3 (-2, -2, -2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
