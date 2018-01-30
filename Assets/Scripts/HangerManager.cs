using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HangerManager : MonoBehaviour {
	public int requisition;
	public int modifiedRequisition;
	public List<Boi> crew;


	public RecruitmentManager recruitmentManager;
	public Object[] texts;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt (PlayerPrefs.GetString ("player"), 100);
		requisition = PlayerPrefs.GetInt (PlayerPrefs.GetString ("player"));
		modifiedRequisition = requisition;

		Crew crewLoader = new Crew ();
		crewLoader = crewLoader.Load ("Assets/Resources/SaveData/" + PlayerPrefs.GetString ("player") + "/");
		crew = crewLoader.bois;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FinalConfirmation(){
		if (requisition >= 0) {
			recruitmentManager.FinalConfirmation ();
		}
	}

}
