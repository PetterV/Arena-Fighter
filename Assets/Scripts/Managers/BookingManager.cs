using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class BookingManager
{

	public static int WeeksPlanned { get; set; }
	public List<MatchPlan> Calendar { get; set; }
	public static float RegularFightDefaultChance { get; set; }
	public static int FreeSlotChance { get; set; }
	public Date LastPlannedDate { get; set; }

	public BookingManager(){
		LastPlannedDate = new Date ();
		LastPlannedDate.SetTodaysDate ();
		WeeksPlanned = 0;
		Calendar = new List<MatchPlan> ();
		RegularFightDefaultChance = 50;
		FreeSlotChance = 50;
	}

    public void PlanMatchesForWeeks(int weeksToPlan)
    {

        int weeksPlannedNow = 0;


        while (weeksPlannedNow < weeksToPlan)
        {
            CheckNextWeek();
            weeksPlannedNow++;
        }
    }

    public static void CheckNextWeek()
    {
		GameController GameController = GameObject.Find ("GameController").GetComponent<GameController> ();

		int nextWeekToPlan = GameController.TotalWeekCount + WeeksPlanned + 1;

        bool scheduleFight = RegularFightChance();

        if (scheduleFight)
        {
			bool freeSlot = StandardFunctions.CheckPercentageChance(FreeSlotChance);
            MatchPlan newMatch = new MatchPlan();
            newMatch.SetFightDate(nextWeekToPlan);
            SetFighters(newMatch, freeSlot);
			GameController.BookingManager.Calendar.Add(newMatch);
        }
		WeeksPlanned = WeeksPlanned++;
    }
		

    public static bool RegularFightChance()
    {
		bool scheduleFight = StandardFunctions.CheckPercentageChance(RegularFightDefaultChance);
        return scheduleFight;
    }

    
	public static void SetFighters(MatchPlan match, bool freeSlot)
    {
		GameController GameController = GameObject.Find ("GameController").GetComponent<GameController> ();

		List<Character> eligibleFighters = GameController.CurrentLeagueFighterDatabase.AllFighters;
		int selectDefender = GameController.GameRandom.Next(eligibleFighters.Count);
        match.Defender = eligibleFighters[selectDefender];

        //Remove fighters on the same team as the defender
        List<Character> eligibleChallengerFighters = eligibleFighters.Where(f => f.Team != match.Defender.Team).ToList();
        if ( !freeSlot || match.Defender.Team.PlayerTeam)
        {
			int selectChallenger = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(eligibleChallengerFighters.Count);
            match.Challenger = eligibleChallengerFighters[selectChallenger];
        }
        else
        {
			match.Challenger = null;
            match.OpenSlot = true;
        }
    }

	public void UpdateCalendarDisplay(){
		GameObject.Find ("FightsPlannedListContent").GetComponent<FightPlanListContent> ().AddMatchplansToList (Calendar);
	}
}