using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

public class Team
{
    public int TeamSize { get; set; }
    public string TeamName { get; set; }
    public List<Character> Fighters { get; set; }
    public bool PlayerTeam { get; set; }
	public Manager TeamManager { get; set; }
    public int ThisMonthsCost { get; set; }
	public int ThisMonthsWinnings { get; set; }
	public TeamListButton TeamListButton { get; set; }

    public Team(int fighterSlots)
    {
		Character character = new Character (WorldMethods.traitsPerFighter, WorldMethods.minLevelForFighters, WorldMethods.maxLevelForFighters);
		Manager characterManager = character.MakeManager (this, character);
		TeamManager = characterManager;
        Fighters = new List<Character>();
        int fightersSet = 0;
        while (fightersSet < fighterSlots)
        {
            Character fighterToAdd = GetRandomFighterForTeam();
            Fighters.Add(fighterToAdd);
			fighterToAdd.Team = this;
            fighterToAdd.IsInTeam = true;
            fightersSet++;
        }
		//TODO: Figure this out later
        SetUniqueTeamName();
    }

    private Character GetRandomFighterForTeam()
    {
        List<Character> availableFighters = new List<Character>();
		foreach (Character f in GameObject.Find("GameController").GetComponent<GameController>().CurrentLeagueFighterDatabase.AllFighters)
        {
            if (f.IsInTeam == false)
            {
                availableFighters.Add(f);
            }
        }
        int randomFighterNumber = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(availableFighters.Count);
        Character fighterToAdd = availableFighters[randomFighterNumber];
        return fighterToAdd;
    }

    private void SetUniqueTeamName()
    {
		string filePath = Path.Combine (Application.streamingAssetsPath, "Text Files\\TeamNames.txt");
		string[] allTeamNamesArray = File.ReadAllLines(filePath);
        List<string> allTeamNames = new List<string>();
        allTeamNames = allTeamNamesArray.ToList<string>();
		List<string> usedTeamNames = GameObject.Find("GameController").GetComponent<GameController>().TeamManager.UsedTeamNames;

        //Remove already used names
        foreach (string name in allTeamNamesArray)
        {
            foreach (string usedName in usedTeamNames)
            {
                if (name == usedName)
                {
                    allTeamNames.Remove(name);
                }
            }
        }

        //Set the new name
        int nameSelection = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(allTeamNames.Count);

        TeamName = allTeamNames[nameSelection];
		GameObject.Find("GameController").GetComponent<GameController>().TeamManager.UsedTeamNames.Add(allTeamNames[nameSelection]);
    }

    public void PayDailyCosts()
    {
		int todaysCost = 0;
        foreach (Character f in Fighters)
        {
			todaysCost = todaysCost + GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(BasicValues.basicDailyCost, BasicValues.maxDailyCost + 1);

        }
		int todaysFinalCost = 0 - todaysCost;
		TeamManager.AddFunds (todaysFinalCost);
		ThisMonthsCost = ThisMonthsCost + todaysCost;
    }

	public void UpdateUI(){
		TeamListButton.UpdateTeamButtonInfo ();
	}
}
