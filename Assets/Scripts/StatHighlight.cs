using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatHighlight : MonoBehaviour {
	public Color defaultColor;
	public Color highlightColor;
	public Text descriptionText;

	public string description;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseEnter(){
		GetComponent<Image> ().color = highlightColor;
		descriptionText.text = description;
	}

	void OnMouseExit(){
		GetComponent<Image> ().color = defaultColor;
		descriptionText.text = "...";
	}
}
