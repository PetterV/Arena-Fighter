using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WorldMethods
{
	public static int numberOfFighters = 12;
	public static int traitsPerFighter = 3;
	public static int minLevelForFighters = 2;
	public static int maxLevelForFighters = 3;
	public static int WeekCount = 0;
	public bool readyToStart = false;

	public static GameController GameController { get; set; }
	public static Date RealDate {get; set; }

	public static void SetUpWorld()
	{
		GameController = GameObject.Find ("GameController").GetComponent<GameController>();

		RealDate = GameController.RealDate;

		GameObject.Find ("UIController").GetComponent<UIController> ().RealDate = RealDate;

		GameController.CurrentLeagueFighterDatabase.PopulateFighterDatabase(numberOfFighters, traitsPerFighter, minLevelForFighters, maxLevelForFighters);

		GameController.TeamManager.PopulateLeagueWithTeams(4);

		GameController.CurrentLeagueFighterDatabase.PrintCompleteFighterList();

		GameController.TeamManager.AssignTeamToPlayer();

		RealDate.PrintDate();

		//TODO: Find some way of delaying time until team selection has been done.
		//GameController.UnPauseTicks ();
	}

	public static void DailyTick()
	{
		RealDate = GameController.RealDate;

		int quarterTick = 0;
		while(quarterTick < 4)
		{
			foreach (Fighter f in GameController.CurrentLeagueFighterDatabase.AllFighters)
			{
				f.QuarterTickRecovery();
				if (f.CurrentActivity != null)
				{
					f.CurrentActivity.AttendingActivityTick(f);
				}
			}

			quarterTick++;
		}

		foreach (Team t in GameController.TeamManager.TeamsInLeague)
		{
			t.PayDailyCosts();
			foreach (Fighter f in t.Fighters) {
				//Update info on fighters
				f.UpdateUI ();
			}
		}

		GameController.FightManager.DailyTickFightChecks();

		//Check whether a week has passed.
		WeekCount++;
		if (WeekCount == 7)
		{
			WeeklyTick();
			WeekCount = 0;
			GameController.TotalWeekCount++;
		}
		//Make calendar time pass
		if (RealDate.Day < 31)
		{
			RealDate.Day++;
		}
		else
		{
			RealDate.Day = 1;
			if (RealDate.Month < 12)
			{
				MonthlyTick();
				RealDate.Month++;
			}
			else
			{
				YearlyTick();
				RealDate.Month = 1;
				RealDate.Year++;
			}

			MonoBehaviour.print ("It is now month " + RealDate.Month + " of year " + RealDate.Year);
		}
		//TODO: Should the update of the calendar be placed somewhere else? Creation of a new match, maybe?
		GameController.BookingManager.UpdateCalendarDisplay ();
		GameController.DaysProgressed++;
	}

	private static void WeeklyTick()
	{
		MonoBehaviour.print ("A week has passed");
		foreach (Fighter f in GameController.CurrentLeagueFighterDatabase.AllFighters)
		{
			GameController.ActivityManager.PickRandomActivity(f);
		}
	}

	private static void MonthlyTick()
	{
		MonoBehaviour.print ("A month has passed");
		MonoBehaviour.print(GameController.PlayerManager.PlayerTeam.TeamName + "'s members are performing the following activities:");
		foreach (Fighter f in GameController.CurrentLeagueFighterDatabase.AllFighters)
		{
			if (f.Team == GameController.PlayerManager.PlayerTeam)
			{
				MonoBehaviour.print("  " + f.FirstName + " " + f.LastName + " is currently " + f.CurrentActivity.Name);
			}
		}

		foreach(Team t in GameController.TeamManager.TeamsInLeague)
		{
			t.ThisMonthsCost = 0;
			t.ThisMonthsWinnings = 0;
		}

		GameController.BookingManager.PlanMatchesForWeeks(4);
		GameController.BookingManager.PrintCalendar();

	}

	private static void YearlyTick()
	{
		MonoBehaviour.print ("A Year has passed");
		GameController.CurrentLeagueFighterDatabase.YearlyAgeAllFighters();
	}

	public static void PrintDay()
	{
		MonoBehaviour.print("It is Day " + GameController.RealDate.Day);
	}

	public static void PrintMonth()
	{
		MonoBehaviour.print("It is Month " + GameController.RealDate.Month);
	}

	public static void PrintYear()
	{
		MonoBehaviour.print("It is Year " + GameController.RealDate.Year);
	}

	public static void PrintDate()
	{
		MonoBehaviour.print("It is Day " + GameController.RealDate.Day + " of Month " + GameController.RealDate.Month + " of Year " + GameController.RealDate.Year);
	}
}
