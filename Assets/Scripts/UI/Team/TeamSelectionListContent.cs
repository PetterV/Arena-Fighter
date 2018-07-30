using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamSelectionListContent : MonoBehaviour {

	public GameObject teamButton;
	public List<Team> teams;
	public List<GameObject> buttons;

	public void SetUp (List<Team> teams) {
		PopulateList (teams);	
	}

	void PopulateList (List<Team> teams) {
		foreach (Team t in teams) {
			GameObject newButton = Instantiate (teamButton) as GameObject;
			newButton.transform.SetParent (teamButton.transform.parent);
			newButton.transform.localScale = teamButton.transform.localScale;
			TeamSelectionListButton newButtonInfo = newButton.GetComponent<TeamSelectionListButton> ();
			newButtonInfo.team = t;
			newButton.SetActive(true);
			newButtonInfo.UpdateTeamSelectionButtonInfo();
			buttons.Add (newButton);
		}
	}
}
