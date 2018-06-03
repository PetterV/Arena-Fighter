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

	public BookingManager(){
		WeeksPlanned = 0;
		Calendar = new List<MatchPlan> ();
		RegularFightDefaultChance = 50;
		FreeSlotChance = 70;
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
            SetFightDate(newMatch, nextWeekToPlan);
            SetFighters(newMatch, freeSlot);
			GameController.BookingManager.Calendar.Add(newMatch);
        }
    }

    public void PrintCalendar()
    {
        foreach (MatchPlan match in Calendar)
        {
            if ( match.HasBeen == false && match.OpenSlot == false)
            {
				MonoBehaviour.print("On day " + match.FightDate.Day + " of month " + match.FightDate.Month + " of year " + match.FightDate.Year);
				MonoBehaviour.print("  " + match.Challenger.FirstName + " " + match.Challenger.LastName + " (Level " + match.Challenger.Level + ")");
				MonoBehaviour.print("   of " + match.Challenger.Team.TeamName);
				MonoBehaviour.print(" will be challenging ");
				MonoBehaviour.print("  " + match.Defender.FirstName + " " + match.Defender.LastName + " (Level " + match.Defender.Level + ")");
				MonoBehaviour.print("   of " + match.Defender.Team.TeamName);
            }
            else if (match.OpenSlot == true)
            {
				MonoBehaviour.print(match.Defender.FirstName + " " + match.Defender.LastName + " (Level " + match.Defender.Level + ")");
				MonoBehaviour.print(" of " + match.Defender.Team.TeamName);
				MonoBehaviour.print("will be taking on any challenger");
				MonoBehaviour.print("   on day " + match.FightDate.Day + " of month " + match.FightDate.Month + " of year " + match.FightDate.Year);
            }
        }
    }

    public static bool RegularFightChance()
    {
		bool scheduleFight = StandardFunctions.CheckPercentageChance(RegularFightDefaultChance);
        return scheduleFight;
    }

    public static void SetFightDate(MatchPlan matchToPlan, int weeksFromNow)
    {
        int daysAhead = weeksFromNow * 7 - 7;
		int dayOfWeek = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(1, 8);
        int daysToAdd = daysAhead + dayOfWeek;
        matchToPlan.FightDate.SetTodaysDate();
        matchToPlan.FightDate.IncreaseDate(daysToAdd);

		MonoBehaviour.print ("New match on:");
		matchToPlan.FightDate.PrintDate ();

    }
    
	public static void SetFighters(MatchPlan match, bool freeSlot)
    {
		GameController GameController = GameObject.Find ("GameController").GetComponent<GameController> ();

		List<Fighter> eligibleFighters = GameController.CurrentLeagueFighterDatabase.AllFighters;
		int selectDefender = GameController.GameRandom.Next(eligibleFighters.Count);
        match.Defender = eligibleFighters[selectDefender];

        //Remove fighters on the same team as the defender
        List<Fighter> eligibleChallengerFighters = eligibleFighters.Where(f => f.Team != match.Defender.Team).ToList();
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