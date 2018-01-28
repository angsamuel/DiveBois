using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangerManager : MonoBehaviour {
	public Text toolTip;
	public NameWizard nameWizard;




	// Use this for initialization
	public List<Boi> applicants;
	public GameObject applicantGrid;
	public GameObject applicantButton;



	void Start () {
		applicants = new List<Boi> ();
		GenerateApplicants ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GenerateApplicants(){
		for (int i = 0; i < 10; i++) {
			Boi newBoi = GenerateBoi ();
			applicants.Add (newBoi);
		}
		for (int i = 0; i < applicants.Count; i++) {
			GameObject newApplicantButton = Instantiate (applicantButton, applicantGrid.transform);
			newApplicantButton.GetComponent<ListButton> ().text.text = applicants [i].name;
		}
	}
		
	Boi GenerateBoi(){
		Boi newBoi = new Boi ();
		int age = Random.Range (16, 22);
		int sexNum = Random.Range (0, 2);
		string name = "";
		string sex = "";
		if (sexNum == 0) {
			name = nameWizard.RandomFemaleName () + " " + nameWizard.RandomLastName ();
			sex = "F";
		} else if(sexNum == 1) {
			name = nameWizard.RandomMaleName () + " " + nameWizard.RandomLastName ();
			sex = "M";
		}

		newBoi.name = name;
		newBoi.sex = sex;
		newBoi.age = age;
		newBoi.GenerateStartingStats ();

		return newBoi;

	}
}
/*
 * 
 * 
 * 
 * partially observable NDP*/


//had some ideas in proposal document
//first section of design proposal was on partial observability
// page 28
// observable stats, and non observable states. 
// if linear program assumes stationary property, then says that as soon as attacker enters unobserved region, from what y

//Lhi Rohn Sah
//