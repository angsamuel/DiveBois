using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HangerManager : MonoBehaviour {
	public int requisition;
	public int modifiedRequisition;



	public List<Boi> crew;


	public RecruitmentManager recruitmentManager;

	public DeploymentManager deploymentManager;
	public Object[] texts;


	public GameObject recruitPanel;
	public GameObject backgroundPanel;
	public GameObject summaryPanel;
	public GameObject deploymentPanel;
	public GameObject missionSummaryPanel;



	public void HideAllPanels(){
		backgroundPanel.transform.position = new Vector3 (3000, 3000, 0);
		deploymentPanel.transform.position = new Vector3 (3000, 3000, 0);
		missionSummaryPanel.transform.position = new Vector3 (3000, 3000, 0);
		summaryPanel.transform.position = new Vector3 (3000, 3000, 0);
	}

	public void OpenRecruitPanel(){
		recruitPanel.transform.localPosition = new Vector3 (0, 0, 0);
	}
		


	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt (PlayerPrefs.GetString ("player"), 100);
		requisition = PlayerPrefs.GetInt (PlayerPrefs.GetString ("player"));
		modifiedRequisition = requisition;

		Crew crewLoader = new Crew ();
		crewLoader = crewLoader.Load ("CREW");
		crew = crewLoader.bois;
		Debug.Log (crew.Count);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void FinalConfirmation(){

		Crew oldCrew = new Crew ();
		oldCrew = oldCrew.Load(("CREW"));
		Crew crew = new Crew();
		crew.bois = oldCrew.bois;

		

		for (int i = 0; i < recruitmentManager.applicants.Count; i++) {

			if (recruitmentManager.applicantsChosen [i] == true) {
				crew.bois.Add (recruitmentManager.applicants [i]);
			}
		}
		crew.Save ("CREW");
		Debug.Log ("SAVED CREW");

		summaryPanel.transform.localPosition = new Vector3 (0, 0, 0);
		Debug.Log (summaryPanel.transform.localPosition);
		backgroundPanel.transform.localPosition = new Vector3 (3000, 3000, 0);

	}

	public void ToMissionSummary(){
		HideAllPanels ();
		missionSummaryPanel.transform.position = new Vector3 (0, 0, 0);
	}

	public void ToDeployment(){
		HideAllPanels ();
		deploymentManager.Initiate ();
		deploymentPanel.transform.localPosition = new Vector3 (0, 0, 0);

	}

	//printer in room CSI 258, tray 2 listing, laserjet 600
	//student also mentioned print jobs
	//iTS-hood engr-cs1258
	//Edi Przybyla

	//Jim Baker down in facillities, 

}
