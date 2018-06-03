using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TeamManager
{
	public List<string> UsedTeamNames { get; set; }
	public List<Team> TeamsInLeague { get; set; }

	public TeamManager(){
		UsedTeamNames = new List<string>();
		//TODO: When correctly instantiating TeamsInLeague, Unity crashes/hangs with no error message
		// My current guess is that successfully instantiating it enables an infinite for loop that otherwise would have failed.
		// Look into this further 18/03/18
		TeamsInLeague = new List<Team>();
	}

    public Team GenerateTeam(int fighterSlots)
    {
        Team createdTeam = new Team(fighterSlots);
        return createdTeam;
    }
    public void PopulateLeagueWithTeams(int numberOfTeams)
    {
        int teamsGenerated = 0;

        while (teamsGenerated < numberOfTeams)
        {
			TeamsInLeague.Add(GenerateTeam(3));
            teamsGenerated++;
        }
    }

    public void AssignTeamToPlayer()
    {
		GameController GameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		GameObject TeamSelectionBox = GameController.TeamSelectionBox;
		TeamSelectionBox.SetActive(true);
		GameObject.Find ("TeamSelectionListContent").GetComponent<TeamSelectionListContent> ().SetUp (GameController.TeamManager.TeamsInLeague);
    }
}
