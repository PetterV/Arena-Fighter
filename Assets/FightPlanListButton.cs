using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightPlanListButton : MonoBehaviour {


	public MatchPlan matchPlan;
	public Fighter fighter1;
	public Fighter fighter2;

	public Text day;
	public Text month;
	public Text year;
	public Text fighter1Name;
	public Text fighter2Name;
	public Text fighter1Level;
	public Text fighter2Level;
	
	//Update the button with info
	public void UpdateFightPlanButtonInfo(){
		day.text = matchPlan.FightDate.Day.ToString();
		month.text = matchPlan.FightDate.Month.ToString();
		year.text = matchPlan.FightDate.Year.ToString();

		if (fighter1 != null) {
			fighter1Name.text = fighter1.FirstName + " " + fighter1.LastName;
			fighter1Level.text = fighter1.Level.ToString();
			if (fighter1.Team.PlayerTeam) {
				HighlightTextForPlayer (fighter1Name);
				HighlightTextForPlayer (fighter1Level);
			} 
		} else {
			fighter1Name.text = "Any Fighter";
			fighter1Level.text = "X";
			HighlightTextForPlayer (fighter1Name);
		}
		if (fighter2 != null) {
			fighter2Name.text = fighter2.FirstName + " " + fighter2.LastName;
			fighter2Level.text = fighter2.Level.ToString();
			if (fighter2.Team.PlayerTeam) {
				HighlightTextForPlayer (fighter2Name);
				HighlightTextForPlayer (fighter2Level);
			} 
		} else {
			fighter2Name.text = "Any Fighter";
			fighter2Level.text = "X";
			HighlightTextForPlayer (fighter2Name);
		}
	}

	void HighlightTextForPlayer(Text FighterText){
		FighterText.fontStyle = FontStyle.Bold;
		//Also highlight the button itself:
		gameObject.GetComponent<Image> ().color = Color.blue;
	}

	public void FightHasBeen(Fighter winner){
		//Fade the fights that have been
		if (!fighter1.Team.PlayerTeam && !fighter2.Team.PlayerTeam) {
			gameObject.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 0.2f);
		} else {
			gameObject.GetComponent<Image> ().color = new Color (0.3f, 0.7f, 1f, 0.2f);
		}
		//Update the fighter info to check that it matches with what has been
		fighter1Level.text = fighter1.Level.ToString ();
		fighter2Level.text = fighter2.Level.ToString ();
		fighter1Name.text = fighter1.FirstName + " " + fighter1.LastName;
		fighter2Name.text = fighter2.FirstName + " " + fighter2.LastName;
		if (winner != null) {
			if (winner == fighter1) {
				fighter1Name.text = "W: " + fighter1Name.text;
			} else {
				fighter2Name.text = "W: " + fighter2Name.text;
			}
		}
	}
}

