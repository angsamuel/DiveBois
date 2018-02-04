using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffectController : MonoBehaviour {
	public GameObject redScreen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DamageEffect(){
		StartCoroutine (SeeColor (redScreen, 1f));
	}

	IEnumerator SeeColor(GameObject plane, float timeToComplete){

		plane.SetActive (true);

		float t = timeToComplete;
		Color rColor = plane.GetComponent<Image> ().color;
		while (t > 0) {
			plane.GetComponent<Image> ().color = new Color (rColor.r, rColor.g, rColor.b, t/timeToComplete);
			t -= Time.deltaTime;
			yield return null;
		}

		plane.SetActive (false);


	}
}
