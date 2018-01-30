using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeploymentManager : MonoBehaviour {


	public Text deployName, deployAge, deploySex;
	public List<Text> gradeTexts;

	public GameObject statsParent;

	public List<string> grades; 

	public GameObject deployButton;
	public GameObject deployGrid;
	public GameObject holdingGrid;

	int deployed = 0;

	List<Boi> crew;
	List<bool> deploymentsChosen;

	public Slider releaseSlider;
	public Button launchButton;

	public Image sliderBackground;
	public Image sliderHandle;
	public Color releaseRed;
	public Color releaseGreen;
	public Color handleGreen;
	public Text sliderText;

	public List<Image> crewBackgrounds;
	public List<SpriteRenderer> crewSprites;

	public GameObject podPicture;

	// Use this for initialization
	void Start () {

		Crew crewLoader = new Crew ();
		deploymentsChosen = new List<bool> ();
		crewLoader = crewLoader.Load ("Assets/Resources/SaveData/" + PlayerPrefs.GetString ("player") + "/");
		crew = crewLoader.bois;

		GenerateDeployments ();

		launchButton.transform.localScale = new Vector3 (0, 0, 0);
		for (int i = 0; i < crewSprites.Count; i++) {
			crewSprites [i].transform.localScale = new Vector3 (0, 0, 1);
		}

		for (int i = 0; i < deployed; i++) {
			crewBackgrounds [i].color = releaseRed;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (releaseSlider.value < 0.99) {
			launchButton.transform.localScale = new Vector3 (0, 0, 0);
			sliderText.color = Color.black;
			sliderText.text = "SLIDE TO LAUNCH  ===>";
			sliderBackground.color = releaseRed;
			sliderHandle.color = Color.white;
		} else {

			Debug.Log ("READY TO LAUNCH");
			launchButton.transform.localScale = new Vector3 (0.06f, 0.25f, 1);
			sliderHandle.color = handleGreen;
			sliderText.color = Color.white;
			sliderText.text = "READY";
			sliderBackground.color = releaseGreen;
		}
	}

	public void HoverDeployButton(ListButton lb){
		//fill everything in
		statsParent.transform.localScale = new Vector3 (1.8f, 1.8f, 1);

		deployName.text = crew[lb.index].name;
		deployAge.text = crew [lb.index].age.ToString();
		deploySex.text = crew [lb.index].sex; 

		gradeTexts [0].text = grades[crew [lb.index].combat];
		gradeTexts [1].text = grades[crew [lb.index].psychology];
		gradeTexts [2].text = grades[crew [lb.index].management];
		gradeTexts [3].text = grades[crew [lb.index].computers];
		gradeTexts [4].text = grades[crew [lb.index].medicine];
		gradeTexts [5].text = grades[crew [lb.index].mechanics];
	}

	public void ClickDeploymentButton(ListButton lb){

		if (!lb.chosen) {
			if (deployed < 3) {
				lb.transform.SetParent (deployGrid.transform);
				deployed += 1;

				lb.chosen = !lb.chosen;
				deploymentsChosen [lb.index] = lb.chosen;

			} else {
				//flash error or something
			}

			GameObject myEventSystem = GameObject.Find ("EventSystem");
			myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem> ().SetSelectedGameObject (null);
		} else {
			lb.transform.SetParent(holdingGrid.transform);
			deployed -= 1;


			lb.chosen = !lb.chosen;
			deploymentsChosen [lb.index] = lb.chosen;
			GameObject myEventSystem = GameObject.Find ("EventSystem");
			myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem> ().SetSelectedGameObject (null);
		}

		for (int i = 0; i < deployed; i++) {
			crewSprites [i].transform.localScale = new Vector3 (0, 0, 1);
		}
		for (int i = 0; i < deployed; i++) {
			crewSprites [i].transform.localScale = new Vector3 (22, 22, 1);
		}

		Debug.Log (deployed);
	}

	void GenerateDeployments(){

		for (int i = 0; i < crew.Count; i++) {
			GameObject newDeployButton = Instantiate (deployButton, holdingGrid.transform);
			newDeployButton.GetComponent<ListButton> ().text.text = crew [i].name;
			newDeployButton.GetComponent<ListButton> ().index = i;
			deploymentsChosen.Add (false);
		}
		deployButton.SetActive (false);
	}


	public GameObject holdingList;
	public GameObject deployList;
	public GameObject cover;
	public void LaunchMission(){
		podPicture.GetComponent<Rigidbody2D> ().gravityScale = 2f;

		for (int i = 0; i < deployed; i++) {
			crewBackgrounds [i].color = releaseGreen;
		}
		holdingList.transform.position = new Vector3 (1000, 1000, 1000);
		deployList.transform.position = new Vector3 (1000, 1000, 1000);
		cover.GetComponent<Rigidbody2D> ().gravityScale = 1f;

		statsParent.transform.localScale = new Vector3 (0, 0, 0);

	}

	//save crew, load scene
}
