using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class FighterDatabase
{
    public List<Character> AllFighters = new List<Character>();

    public void PopulateFighterDatabase(int numOfFightersInLeague, int defaultNumberOfTraits, int minLevel, int maxLevel)
    {
        int numOfFightersCount = AllFighters.Count;
        while(numOfFightersCount < numOfFightersInLeague)
        {
            AllFighters.Add(new Character(defaultNumberOfTraits, minLevel, maxLevel));
            numOfFightersCount++;
        }
    }

    public void PrintCompleteFighterList()
    {
		MonoBehaviour.print("All fighters:");
		foreach (Team t in GameObject.Find("GameController").GetComponent<GameController>().TeamManager.TeamsInLeague)
        {
			MonoBehaviour.print("  " + t.TeamName + ", managed by the " + t.TeamManager.Race.RaceName + " " + t.TeamManager.FirstName + " " + t.TeamManager.LastName);
            foreach (Character f in t.Fighters)
            {
				MonoBehaviour.print("    " + f.FirstName + " " + f.LastName + ", a " + f.Age + " year old " + f.Gender.Name + " " + f.Race.RaceName + " " + f.MyClass.Name + ", (Level " + f.Level + ", Fight Money Cut: " + f.FightMoneyCut + "%)");
            }
        }
    }

    public void YearlyAgeAllFighters()
    {
        foreach (Character f in AllFighters)
        {
            f.YearlyAging();
        }
    }
}
