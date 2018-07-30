using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class MatchPlan
{
	public Date FightDate { get; set; }
	public Character Challenger { get; set; }
	public Character Defender { get; set; }
	public bool HasBeen { get; set; }
	public bool OpenSlot { get; set; }
	public bool PlacedInList = false;
	public GameObject FightPlanListButton { get; set; }

    public MatchPlan()
    {
        FightDate = new Date();
        HasBeen = false;
    }

	public void MatchSelectionSetup(Character defender){
		Defender = defender;
		GameController GameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		GameObject FighterSelectionBox = GameObject.Find ("UIController").GetComponent<UIController>().FighterSelectionBox;

		GameController.PauseTicks ();
		FighterSelectionBox.GetComponent<FighterSelectionBox> ().match = this;
		FighterSelectionBox.SetActive (true);
		Team PlayerTeam = GameController.PlayerManager.PlayerTeam;
		List<Character> PlayerFighterList = PlayerTeam.Fighters;
		GameObject.Find ("FighterSelectionListContent").GetComponent<FighterListContent> ().SetUp (PlayerFighterList);
	}

	public void ChallengerSelected(Character challenger){
		Challenger = challenger;
		if (FightPlanListButton.GetComponent<FightPlanListButton> ().fighter1 == null) {
			FightPlanListButton.GetComponent<FightPlanListButton> ().fighter1 = challenger;
		} else {
			FightPlanListButton.GetComponent<FightPlanListButton> ().fighter2 = challenger;
		}
		GameObject.Find("GameController").GetComponent<GameController>().FightManager.FightSetup1v1(Defender, Challenger, FightPlanListButton);
	}

	public void SetFightDate(int weeksFromNow)
	{
		BookingManager BookingManager = GameObject.Find ("GameController").GetComponent<GameController> ().BookingManager;
		int daysAhead = weeksFromNow * 7 - 7;
		int dayOfWeek = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(1, 8);
		int daysToAdd = daysAhead + dayOfWeek;
		FightDate.Day = BookingManager.LastPlannedDate.Day;
		FightDate.Month = BookingManager.LastPlannedDate.Month;
		FightDate.Year = BookingManager.LastPlannedDate.Year;
		FightDate.IncreaseDate(daysToAdd);
		BookingManager.LastPlannedDate.Day = FightDate.Day;
		BookingManager.LastPlannedDate.Month = FightDate.Month;
		BookingManager.LastPlannedDate.Year = FightDate.Year;
	}
}
