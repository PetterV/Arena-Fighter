using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamSelectionListButton : MonoBehaviour {


	public Team team;

	public Text teamName;
	public Text managerName;
	public Text fighterOneName;
	//public Text fighterOneLevel;
	public Text fighterTwoName;
	//public Text fighterTwoLevel;
	public Text fighterThreeName;
	//public Text fighterThreeLevel;
	//public Text fighterFourName;
	//public Text fighterFourLevel;

	void Start(){
		//UpdateTeamButtonInfo ();
	}
	
	//Update the button with info
	public void UpdateTeamSelectionButtonInfo(){
		teamName.text = team.TeamName;
		managerName.text = team.TeamManager.FirstName + " " + team.TeamManager.LastName;
		fighterOneName.text = team.Fighters[0].FirstName + " " + team.Fighters[0].LastName;
		//fighterOneLevel.text = team.Fighters[0].Level.ToString();
		fighterTwoName.text = team.Fighters[1].FirstName + " " + team.Fighters[1].LastName;
		//fighterTwoLevel.text = team.Fighters[1].Level.ToString();
		fighterThreeName.text = team.Fighters[2].FirstName + " " + team.Fighters[2].LastName;
		//fighterThreeLevel.text = team.Fighters[2].Level.ToString();
		//fighterFourName.text = team.Fighters[3].FirstName + " " + team.Fighters[3].FirstName;
		//fighterThreeLevel.text = team.Fighters[3].Level.ToString();
	}

	public void TempSelected(){
		//Set the color of all team selection buttons back to standard
		foreach (GameObject button in GetComponentInParent<TeamSelectionListContent>().buttons) {
			button.GetComponent<Image> ().color = Color.white;
		}
		//Set this team selection button to highlight
		GetComponent<Image> ().color = Color.blue;
		//Set the team's button in the team list to default
		foreach (Team t in GameObject.Find ("GameController").GetComponent<GameController> ().TeamManager.TeamsInLeague) {
			t.TeamListButton.GetComponent<Image> ().color = Color.white;
			//Set all fighters to default
			foreach (Character f in t.Fighters) {
				f.FighterListButton.GetComponent<Image> ().color = Color.white;
			}
		}
		//Set the team's button in the team list to highlight color
		team.TeamListButton.GetComponent<Image> ().color = Color.blue;
		//Set all fighters in the team to highlight color
		foreach (Character f in team.Fighters) {
			f.FighterListButton.GetComponent<Image> ().color = Color.blue;
		}
		//Set this team selection button as the active one for the confirmation button
		//NOTE: This is a very simple bounce to that one and back again if it is clicked, to perform SelectTeam()
		GameObject.Find ("ConfirmTeamSelectionButton").GetComponent<TeamSelectionConfirmation> ().SelectedTeamButton = this;
	}

	public void SelectTeam(){
		team.PlayerTeam = true;
		GameController GameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		PlayerManager playerManager = GameController.PlayerManager;
		playerManager.PlayerTeam = team;
		playerManager.PlayerCharacter = playerManager.PlayerTeam.TeamManager;
		playerManager.PlayerCharacter.PlayerCharacter = true;

		GameObject TeamSelectionBox = GameController.TeamSelectionBox;
		TeamSelectionBox.SetActive(false);

		GameController.UnPauseTicks ();
	}

	public void OnPointerEnter(){
		team.TeamListButton.GetComponent<Image> ().color = Color.blue;
	}

	public void OnPointerExit(){
		team.TeamListButton.GetComponent<Image> ().color = Color.white;
	}
}
