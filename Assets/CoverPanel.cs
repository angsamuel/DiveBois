using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Drop(){
		GetComponent<Rigidbody2D> ().gravityScale = 5;
	}
}
