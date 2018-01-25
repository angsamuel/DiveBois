using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatsPanel : MonoBehaviour {
	int combat = 2;int psychology = 2; int management = 2; int computers = 2; int medicine = 2; int mechanics = 2;
	int credits = 5;

    public List<int> stats;
	public List<Text> statLabels;
	public List<string> grades;

	public Text creditsText;


	void Start(){
		stats = new List<int> (){ combat, psychology, management, computers, medicine, mechanics };
	}

	void Update(){

	}

	void UpdateUI(){
		stats = new List<int> (){ combat, psychology, management, computers, medicine, mechanics };
		for (int i = 0; i < stats.Count; i++) {
			statLabels [i].text = grades [stats [i]];
		}
		creditsText.text = credits.ToString();
	}

	public void TickStat(ref int stat, int num){
		int newStat = stat + num;
		int newCredits = credits - num;

		if (newStat >= 0 && newStat <= 5 && newCredits >= 0) {
			stat = newStat;
			credits = newCredits;
			UpdateUI ();

		}
	}

	void TickCombat(int num){
		TickStat (ref combat, num);
	}
	void TickPsychology(int num){
		TickStat (ref psychology, num);
	}
	void TickManagement(int num){
		TickStat (ref management, num);
	}
	void TickComputers(int num){
		TickStat (ref computers, num);
	}
	void TickMedicine(int num){
		TickStat (ref medicine, num);
	}
	void TickMechanics(int num){
		TickStat (ref mechanics, num);
	}

}