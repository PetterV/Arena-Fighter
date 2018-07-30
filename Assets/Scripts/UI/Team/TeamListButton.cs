using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamListButton : MonoBehaviour {


	public Team team;

	public Text teamName;
	public Text managerName;
	public Text currentFunds;
	public Text monthlyCost;
	public Text monthlyGains;

	void Start(){
		//UpdateFighterButtonInfo ();
	}
	
	//Update the button with info
	public void UpdateTeamButtonInfo(){
		teamName.text = team.TeamName;
		managerName.text = team.TeamManager.FirstName + " " + team.TeamManager.LastName;
		currentFunds.text = team.TeamManager.Funds.ToString();
		monthlyCost.text = team.ThisMonthsCost.ToString();
		monthlyGains.text = team.ThisMonthsWinnings.ToString();
	}
}
