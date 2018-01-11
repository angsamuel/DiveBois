using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoManager : MonoBehaviour {
	public Text nameText;


	public string name;
	public int age;
	public char gender;




	// Use this for initialization
	void Start () {
		
	}

	void ConfirmName(){
		name = nameText.text;
	}

	void ConfirmAll(){

	}

}
