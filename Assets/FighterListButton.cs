using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterListButton : MonoBehaviour {


	public Fighter fighter;
	public GameObject fighterPanel;

	public Text fighterName;
	public Text level;
	public Text health;
	public Text maxHealth;
	public Text energy;
	public Text maxEnergy;
	public Text currentActivity;
	
	//Update the button with info
	public void UpdateFighterButtonInfo(){
		fighterName.text = fighter.FirstName + " " + fighter.LastName;
		level.text = fighter.Level.ToString();
		health.text = fighter.CurrentHealth.ToString();
		maxHealth.text = fighter.MaxHealth.ToString();

		energy.text = fighter.CurrentEnergy.ToString();
		maxEnergy.text = fighter.MaxEnergy.ToString();
		if (fighter.CurrentActivity != null) {
			currentActivity.text = fighter.CurrentActivity.Name;
		} 
		else {
			currentActivity.text = "No Activity";
		}
	}

	public void ShowFighterInPanel(){
		fighterPanel.SetActive (true);
		fighterPanel.GetComponent<FighterDisplayPanel> ().fighter = fighter;
		fighterPanel.GetComponent<FighterDisplayPanel> ().UpdatePanelInfo ();
	}

	public void SelectFighterForFight(){
		GameObject FighterSelectionBox = GameObject.Find ("FighterSelectionBox");
		FighterSelectionBox.GetComponent<FighterSelectionBox> ().match.ChallengerSelected(fighter);
		FighterSelectionBox.SetActive (false);
	}
}
