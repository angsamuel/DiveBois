using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpeedPanel : MonoBehaviour {

	public Text percent, bar1, bar2, bar3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetValues(float speed, float minSpeed, float maxSpeed){
		int p = ((int)(((speed - minSpeed) / (maxSpeed-minSpeed)) * 100));
		if(p < 0){
			p = 0;
		}
		percent.text = p.ToString() + "%";

		bar1.text = "";
		bar2.text = "";
		bar3.text = "";
		for (int i = 0; i < p / 10; i++) {
			bar1.text += "#";
			bar2.text += "#";
			bar3.text += "#";
		}
	}
}
