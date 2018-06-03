using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class MatchPlan
{
	public Date FightDate { get; set; }
	public Fighter Challenger { get; set; }
	public Fighter Defender { get; set; }
	public bool HasBeen { get; set; }
	public bool OpenSlot { get; set; }
	public bool PlacedInList = false;
	public GameObject FightPlanListButton { get; set; }

    public MatchPlan()
    {
        FightDate = new Date();
        HasBeen = false;
    }

	public void MatchSelectionSetup(Fighter defender){
		Defender = defender;
		GameController GameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		GameObject FighterSelectionBox = GameObject.Find ("UIController").GetComponent<UIController>().FighterSelectionBox;

		GameController.PauseTicks ();
		FighterSelectionBox.GetComponent<FighterSelectionBox> ().match = this;
		FighterSelectionBox.SetActive (true);
		Team PlayerTeam = GameController.PlayerManager.PlayerTeam;
		List<Fighter> PlayerFighterList = PlayerTeam.Fighters;
		GameObject.Find ("FighterSelectionListContent").GetComponent<FighterListContent> ().SetUp (PlayerFighterList);
	}

	public void ChallengerSelected(Fighter challenger){
		Challenger = challenger;
		if (FightPlanListButton.GetComponent<FightPlanListButton> ().fighter1 == null) {
			FightPlanListButton.GetComponent<FightPlanListButton> ().fighter1 = challenger;
		} else {
			FightPlanListButton.GetComponent<FightPlanListButton> ().fighter2 = challenger;
		}
		GameObject.Find("GameController").GetComponent<GameController>().FightManager.FightSetup1v1(Defender, Challenger, FightPlanListButton);
	}

	public Fighter GetChallenger(Fighter defender)
    {
		GameController GameController = GameObject.Find ("GameController").GetComponent<GameController> ();

		MonoBehaviour.print("Fighters in " + GameController.PlayerManager.PlayerTeam.TeamName + ":");

        int fighterNumber = 1;
		foreach (Fighter f in GameController.PlayerManager.PlayerTeam.Fighters)
        {
			MonoBehaviour.print(fighterNumber + ": " + f.FirstName + " " + f.LastName);
			MonoBehaviour.print("   Health: " + f.CurrentHealth + ";  Energy:" + f.CurrentEnergy + ";");
            fighterNumber++;
        }

		MonoBehaviour.print("Enter the number of the challenger you want to send:");
        string fighterSelection;
        int intFighterSelection = 0;
		//TODO: TEMP, REPLACE WITH PROPER INPUT SYSTEM
		int fighterSelectionInt = GameController.GameRandom.Next(1, 4);
		//fighterSelection = Input.GetKeyDown();
		intFighterSelection = fighterSelectionInt;
        //int.TryParse(fighterSelection, out intFighterSelection);
		while (intFighterSelection > GameObject.Find("GameController").GetComponent<GameController>().PlayerManager.PlayerTeam.Fighters.Count || intFighterSelection < 1)
        {
			MonoBehaviour.print("Please enter a valid selection:");
			//TODO: TEMP, REPLACE WITH PROPER INPUT SYSTEM
			fighterSelectionInt = GameController.GameRandom.Next(1, 4);
			//fighterSelection = Input.GetKeyDown();
			intFighterSelection = fighterSelectionInt;
			//int.TryParse(fighterSelection, out intFighterSelection);
        }

        int actualFighterSelection = intFighterSelection - 1;

		Fighter challenger = GameObject.Find("GameController").GetComponent<GameController>().PlayerManager.PlayerTeam.Fighters[actualFighterSelection];

        return challenger;
    }
}
