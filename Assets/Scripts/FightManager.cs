using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class FightManager
{
    public bool scheduleFight = false;
    public int fightCountdown;

	public static GameObject GameController = GameObject.Find("GameController");
	public static Date RealDate = GameController.GetComponent<GameController>().RealDate;
    

    public void DailyTickFightChecks()
    {
		RealDate = GameController.GetComponent<GameController> ().RealDate;

		foreach (MatchPlan match in GameController.GetComponent<GameController>().BookingManager.Calendar)
        {
            if (RealDate.Day == match.FightDate.Day && RealDate.Month == match.FightDate.Month && RealDate.Year == match.FightDate.Year)
            {
				MonoBehaviour.print("It is day " + RealDate.Day + ", month " + RealDate.Month + ", year " + RealDate.Year);
				MonoBehaviour.print("It's time for a fight!");
                if (match.OpenSlot == false)
                {
					FightSetup1v1(match.Defender, match.Challenger, match.FightPlanListButton);
                }
                //Otherwise the player has to choose a fighter
                else
                {
					match.MatchSelectionSetup(match.Defender);
                }
                match.HasBeen = true;
            }
        }
    }

    //TODO: 1v1FightSetup is all temp, I think.
	public void FightSetup1v1(Fighter defender, Fighter challenger, GameObject fightPlanButton)
    {
        //Arrange a fight
        string fightType = "championship_match";

		ArrangeFight1v1(defender, challenger, fightType, fightPlanButton);
    }
	public static void ArrangeFight1v1(Fighter f1, Fighter f2, string fightType, GameObject fightPlanButton)
    {
		Fight1v1 fight = new Fight1v1(f1, f2, fightType, fightPlanButton);
    }

    public static void PayOut(Audience audience, Fighter winner, Fighter loser, bool draw)
    {
        float totalMoneyFloat = audience.AudienceAppreciation * BasicValues.audienceAppreciationToFightMoneyFactor;
        Manager winningManager = winner.Team.TeamManager;
        Manager losingManager = loser.Team.TeamManager;

        float cutPercentage = totalMoneyFloat / 100;
        float winningManagerCutFloat;
        if (!draw)
        {
            winningManagerCutFloat = cutPercentage * BasicValues.winningPercentage;
        }
        else
        {
            winningManagerCutFloat = cutPercentage * 50;
        }
        float losingManagerCutFloat = totalMoneyFloat - winningManagerCutFloat;

        float winnerFighterCutPercentage = winningManagerCutFloat / 100;
        float winnerFighterCutFloat = winnerFighterCutPercentage * winner.FightMoneyCut;
        int winnerFighterCut = Convert.ToInt32(winnerFighterCutFloat);

        float loserFighterCutPercentage = winningManagerCutFloat / 100;
        float loserFighterCutFloat = loserFighterCutPercentage * loser.FightMoneyCut;
        int loserFighterCut = Convert.ToInt32(loserFighterCutFloat);

        winningManagerCutFloat = winningManagerCutFloat - winnerFighterCutFloat;
        losingManagerCutFloat = losingManagerCutFloat - loserFighterCutFloat;
        
        int winningManagerCut = Convert.ToInt32(winningManagerCutFloat);
        int losingManagerCut = Convert.ToInt32(losingManagerCutFloat);
        
        int totalMoney = winningManagerCut + winnerFighterCut + losingManagerCut + loserFighterCut;
        int winningSideCut = winningManagerCut + winnerFighterCut;
        int losingSideCut = losingManagerCut + loserFighterCut;

		winningManager.AddFunds(winningManagerCut);
		winner.Team.ThisMonthsWinnings = winner.Team.ThisMonthsWinnings + winningManagerCut;
		losingManager.AddFunds(losingManagerCut);
		loser.Team.ThisMonthsWinnings = winner.Team.ThisMonthsWinnings + losingManagerCut;

		MonoBehaviour.print("The Audience Appreciation was " + audience.AudienceAppreciation + ", resulting in a total of " + totalMoney + " gold.");
		MonoBehaviour.print("  of which manager " + winningManager.FirstName + " " + winningManager.LastName + " gets " + winningSideCut + ", of which " + winner.FirstName + " receives a cut of " + winnerFighterCut);
		MonoBehaviour.print("    leaving " + winningManager.FirstName + " with " + winningManagerCut + " gold" );
		MonoBehaviour.print("  " + losingManager.FirstName + " " + losingManager.LastName + " gets " + losingSideCut + ", of which " + loser.FirstName + " receives a cut of " + loserFighterCut);
		MonoBehaviour.print("    leaving " + losingManager.FirstName + " with " + losingManagerCut + " gold");
    }
}