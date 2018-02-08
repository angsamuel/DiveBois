using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Scanner : MonoBehaviour {
	public Image scannerDot;
	public Image horzup;
	public Image horzdown;
	public Image vertleft;
	public Image vertright;

	// Use this for initialization
	void Start () {
		HideObstacles ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void SetValues(Vector2 position, float levelDimensions){
		Vector2 ne = (position * 169.0f/levelDimensions);
		scannerDot.rectTransform.localPosition = (position * 169.0f/levelDimensions) + new Vector2(183.5f, 183.5f);
	}

	void HideObstacles(){
		horzup.rectTransform.localScale = new Vector3 (0, 0, 0);
		vertleft.rectTransform.localScale = new Vector3 (0, 0, 0);
		horzdown.rectTransform.localScale = new Vector3 (0, 0, 0);
		vertright.rectTransform.localScale = new Vector3 (0, 0, 0);

	}

	public void SetBarrier(string direction){
		HideObstacles ();
		if (direction == "vertright") {
			vertright.rectTransform.localScale = new Vector3 (1, 1, 1);
		} else if (direction == "vertleft") {
			vertleft.rectTransform.localScale = new Vector3 (1, 1, 1);
		} else if (direction == "horzup") {
			horzup.rectTransform.localScale = new Vector3 (1, 1, 1);
		} else if (direction == "horzdown") {
			horzdown.rectTransform.localScale = new Vector3 (1, 1, 1);
		}
	}


}
