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
	public Character ManagerCharacter { get; set; }

	public Manager(Team team, Character fighter)
    {
		ManagerCharacter = fighter;
		SetUp ();
		PlayerCharacter = false;
		MyTeam = team;
        Funds = BasicValues.basicManagerFunds;
    }

    private void SetUp()
    {
		Race = ManagerCharacter.Race;
		Gender = ManagerCharacter.MyGender;
		FirstName = ManagerCharacter.FirstName;
		LastName = ManagerCharacter.LastName;
    }
		
	public void AddFunds(int fundsToAdd){
		Funds = Funds + fundsToAdd;
		MyTeam.UpdateUI();
		//TODO: Add interface animations and updates here
	}
}
