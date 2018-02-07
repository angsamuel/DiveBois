using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeDisplay : MonoBehaviour {
	public Text timeLeft;
	public Text timeToArrival;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetValues(int tl, int tta){
		timeLeft.text = (tl / 60).ToString () + ":";
		if (tl % 60 < 10) {
			timeLeft.text += "0" + (tl % 60).ToString ();
		} else {
			timeLeft.text += (tl % 60).ToString ();
		}

		if (tta > 0) {
			timeToArrival.text = (tta / 60).ToString () + ":";
			if (tta % 60 < 10) {
				timeToArrival.text += "0" + (tta % 60).ToString ();
			} else {
				timeToArrival.text += (tta % 60).ToString ();
			}
		} else {
			timeToArrival.text = "";
		}

	}
}
