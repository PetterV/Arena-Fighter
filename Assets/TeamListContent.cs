using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamListContent : MonoBehaviour {

	public GameObject teamButton;
	public List<Team> teams;

	public void SetUp (List<Team> teams) {
		PopulateList (teams);	
	}

	void PopulateList (List<Team> teams) {
		foreach (Team t in teams) {
			GameObject newButton = Instantiate (teamButton) as GameObject;
			newButton.transform.SetParent (teamButton.transform.parent);
			newButton.transform.localScale = teamButton.transform.localScale;
			TeamListButton newButtonInfo = newButton.GetComponent<TeamListButton> ();
			newButtonInfo.team = t;
			t.TeamListButton = newButtonInfo;
			newButton.SetActive(true);
			newButtonInfo.UpdateTeamButtonInfo();
			print ("Set new team button: " + t.TeamName);
		}
	}
}
