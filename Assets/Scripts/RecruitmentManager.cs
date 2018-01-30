using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitmentManager : MonoBehaviour {

	public HangerManager hangerManager;
	public NameWizard nameWizard;
	public List<string> grades;
	public List<Text> gradeTexts;

	public GameObject statsParent;


	// Use this for initialization
	public List<Boi> applicants;
	List<bool> applicantsChosen;
	public GameObject applicantGrid;
	public GameObject applicantButton;

	public GameObject acceptedGrid;

	int orderTotal;
	public int perRecruitCost;

	public Text requisitionText;
	public Text orderText;
	public Text remainingText;



	void Start () {
		applicantsChosen = new List<bool> ();
		applicants = new List<Boi> ();
		GenerateApplicants ();
		statsParent.transform.localScale = new Vector3 (0, 0, 0);


		Destroy (applicantButton);
	}

	// Update is called once per frame
	void Update () {

	}

	void GenerateApplicants(){
		for (int i = 0; i < 10; i++) {
			Boi newBoi = GenerateBoi ();
			applicants.Add (newBoi);
			applicantsChosen.Add (false);
		}
		for (int i = 0; i < applicants.Count; i++) {
			GameObject newApplicantButton = Instantiate (applicantButton, applicantGrid.transform);
			newApplicantButton.GetComponent<ListButton> ().text.text = applicants [i].name;
			newApplicantButton.GetComponent<ListButton> ().index = i;
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


	ListButton selectedRecruitButton;

	public Text recruitName, recruitAge, recruitSex;
	public void HoverRecruitButton(ListButton lb){
		//fill everything in
		statsParent.transform.localScale = new Vector3 (2, 2, 1);

		recruitName.text = applicants[lb.index].name;
		recruitAge.text = applicants [lb.index].age.ToString();
		recruitSex.text = applicants [lb.index].sex; 

		gradeTexts [0].text = grades[applicants [lb.index].combat];
		gradeTexts [1].text = grades[applicants [lb.index].psychology];
		gradeTexts [2].text = grades[applicants [lb.index].management];
		gradeTexts [3].text = grades[applicants [lb.index].computers];
		gradeTexts [4].text = grades[applicants [lb.index].medicine];
		gradeTexts [5].text = grades[applicants [lb.index].mechanics];

	}

	public void RecruitWithButton(ListButton lb){
		hangerManager.modifiedRequisition += orderTotal;

		if (!lb.chosen) {
			lb.transform.SetParent (acceptedGrid.transform);
			orderTotal += perRecruitCost;
		} else {
			lb.transform.SetParent(applicantGrid.transform);
			orderTotal -= perRecruitCost;
		}

		hangerManager.modifiedRequisition -= orderTotal;

		requisitionText.text = (hangerManager.modifiedRequisition + orderTotal).ToString(); 
		orderText.text = "-" + orderTotal.ToString ();
		remainingText.text = hangerManager.modifiedRequisition.ToString ();

		lb.chosen = !lb.chosen;
		applicantsChosen [lb.index] = lb.chosen;
		GameObject myEventSystem = GameObject.Find("EventSystem");
		myEventSystem .GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
	}

	public void FinalConfirmation(){
		Crew crew = new Crew();

		for (int i = 0; i < applicants.Count; i++) {

			if (applicantsChosen [i] == true) {
				crew.bois.Add (applicants [i]);
			}
		}
		crew.Save ("Assets/Resources/SaveData/" + PlayerPrefs.GetString ("player") + "/");
		Debug.Log ("SAVED CREW");
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