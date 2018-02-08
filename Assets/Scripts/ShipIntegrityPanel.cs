using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShipIntegrityPanel : MonoBehaviour {

	bool canToggle = true;
	bool mode = false;
	Vector3 position;

	public Text shieldsPercent, shieldsBar, armorPercent, armorBar;

	// Use this for initialization
	void Start () {
		position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetValues(float shield, float shieldMax, float armor, float armorMax){
		int shieldPercentNum = (int)((shield / shieldMax) * 100);
		int armorPercentNum = (int)((armor / armorMax) * 100);

		shieldsPercent.text = shieldPercentNum.ToString () + "%";
		string shieldsBarString = "";
		for (int i = 0; i < (shieldPercentNum / 10); i++) {
			shieldsBarString += "#";
		}
		shieldsBar.text = shieldsBarString;

		armorPercent.text = armorPercentNum.ToString () + "%";
		string armorBarString = "";
		for (int i = 0; i < (armorPercentNum / 10); i++) {
			armorBarString += "#";
		}
		armorBar.text = armorBarString;
	}

}

