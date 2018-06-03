using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Manager
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public Race Race { get; set; }
	public Gender Gender { get; set; }
	public bool PlayerCharacter { get; set; }
	public int Funds { get; set; }
	public Team MyTeam { get; set; }

	public Manager(Team team)
    {
		PlayerCharacter = false;
        SetRace();
        SetGender();
        SetName();
		MyTeam = team;
        Funds = BasicValues.basicManagerFunds;
    }

    //TODO: Make SetRace and SetGender generic so it can be shared with Fighter, rather than duplicated
    private void SetRace()
    {
        RaceList races = new RaceList();
        List<Race> allRaces = races.GetRace();
        int numOfRaces = allRaces.Count;

        int raceSelection = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(numOfRaces);

        Race = allRaces[raceSelection];
    }

    private void SetGender()
    {
        GenderList genderList = new GenderList();
        List<Gender> allGenders = genderList.GetGender();
        //TODO: Make this weighted depending on race?
        int genderRoll = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(100);
        if (genderRoll < 6)
        {
            Gender = allGenders.FirstOrDefault(o => o.Name == "nonbinary");
        }
        else if (genderRoll < 53)
        {
            Gender = allGenders.FirstOrDefault(o => o.Name == "male");
        }
        else
        {
            Gender = allGenders.FirstOrDefault(o => o.Name == "female");
        }
    }
    private void SetName()
    {
        string[] firstNameList = new string[0];
        string[] lastNameList = new string[0];

        firstNameList = Race.GetFirstNames(Gender);
        lastNameList = Race.GetLastNames();

        int totalFirstNames = firstNameList.Length;
        int totalLastNames = lastNameList.Length;
        int firstNameAssignment = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(totalFirstNames);
        int lastNameAssignment = GameObject.Find("GameController").GetComponent<GameController>().GameRandom.Next(totalLastNames);

        FirstName = firstNameList[firstNameAssignment];
        LastName = lastNameList[lastNameAssignment];
    }

	public void AddFunds(int fundsToAdd){
		Funds = Funds + fundsToAdd;
		MyTeam.UpdateUI();
		//TODO: Add interface animations and updates here
	}
}
