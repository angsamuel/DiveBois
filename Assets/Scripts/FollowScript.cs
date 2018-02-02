using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {

	public GameObject target;

	private Vector3 offsetFromTarget;

	public bool followTarget = true;

	float frozenX;
	float frozenY;

	// Use this for initialization
	void Start () {
		frozenX = transform.position.x;
		frozenY = transform.position.y;
		offsetFromTarget = target.transform.position - transform.position;
	}

	// Update is called once per frame
	void Update () {


		if (followTarget) {
			transform.position = new Vector3(frozenX,frozenY,target.transform.position.z) - new Vector3(0,0,offsetFromTarget.z);
		}
	}

	void StopFollowing(){
		followTarget = false;
	}

	public void StartFollow(){
		if (!followTarget) {
			frozenX = transform.position.x;
			frozenY = transform.position.y;
			offsetFromTarget = target.transform.position - transform.position;
			followTarget = true;
		}
	}
}
	
//Duane Colthharp academic affairs,
//0032089
//2/28/65
//307 fox hall lane
//tiger paws, how long on tlearn

//8201

