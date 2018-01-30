using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummaryPanel : MonoBehaviour {

	public Text myName, nameToMatch, mySex;

	public List<Text> myGrades;
	public List<Text> gradesToMatch;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateSummary(){
		myName.text = nameToMatch.text;
		for (int i = 0; i < myGrades.Count; i++) {
			myGrades [i].text = gradesToMatch [i].text;
		}


	}

	public void SetSex(string s){
		mySex.text = s;
	}


}
