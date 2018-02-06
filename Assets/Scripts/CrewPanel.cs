using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CrewPanel : MonoBehaviour {
	public Text leftName, backName, rightName;
	public Text hpLeft, mpLeft, hpBack, mpBack, hpRight, mpRight;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void SetValues(Boi leftBoi, Boi backBoi, Boi rightBoi){
		leftName.text = leftBoi.callsign;
		backName.text = backBoi.callsign;
		rightName.text = rightBoi.callsign;

		hpLeft.text = leftBoi.hp.ToString();
		mpLeft.text = leftBoi.mp.ToString();
		hpBack.text = backBoi.hp.ToString();
		mpBack.text = backBoi.mp.ToString();
		hpRight.text = rightBoi.hp.ToString();
		mpRight.text = rightBoi.mp.ToString();

	}
}
