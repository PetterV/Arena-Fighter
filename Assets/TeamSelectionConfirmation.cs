using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamSelectionConfirmation : MonoBehaviour {

	public TeamSelectionListButton SelectedTeamButton;

	public void ConfirmSelection(){
		SelectedTeamButton.SelectTeam ();
	}
}
