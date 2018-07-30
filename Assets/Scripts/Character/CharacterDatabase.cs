using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CharacterDatabase {
	public int UsedIDs {get; set; }
	public List<Character> AllCharacters = new List<Character>();

	public void SetUpCharacterDatabase (){
		GameController GameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		foreach (Character f in GameController.CurrentLeagueFighterDatabase.AllFighters) {
			AllCharacters.Add (f);
		}
		foreach (Team t in GameController.TeamManager.TeamsInLeague) {
			AllCharacters.Add (t.TeamManager.ManagerCharacter);
		}
	}

	public void AssignID (Character character){
		UsedIDs = UsedIDs + 1;
		character.ID = UsedIDs;
	}

	public void OrderCharacterDatabaseByID(){
		AllCharacters.Sort ((x, y) => x.ID.CompareTo (y.ID));
	}
}
