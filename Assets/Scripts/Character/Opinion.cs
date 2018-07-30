using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opinion {
	public double[,] OpinionList;
	CharacterDatabase CharacterDatabase;

	public Opinion(){
		CharacterDatabase = GameObject.Find ("GameController").GetComponent<GameController> ().CharacterDatabase;
		OpinionList = new double[CharacterDatabase.UsedIDs+1, CharacterDatabase.UsedIDs+1];
		OpinionList [0, 0] = 0.00;
		foreach (Character c in CharacterDatabase.AllCharacters) {
			foreach (Character o in CharacterDatabase.AllCharacters) {
				OpinionList [c.ID, o.ID] = CalculateOpinion (c, o);
			}
		}
	}

	public void CalculateReturnOpinion(Character owner, Character target){
		CalculateIndividualOpinion (owner, target);
		CalculateIndividualOpinion (target, owner);
	}

	public void CalculateIndividualOpinion (Character owner, Character target){
		OpinionList [owner.ID, target.ID] = CalculateOpinion (owner, target);
	}

	public double CalculateOpinion(Character owner, Character target){
		double opinion = 10.00;
		if (owner == target) {
			opinion = 100.00;
		}
		else{
			if (owner.Race.RaceName == target.Race.RaceName) {
				opinion = opinion + 10.00;
			}
			if (owner.MyClass.Name == target.MyClass.Name) {
				opinion = opinion + 10.00;
			}
			if (owner.Team == target.Team) {
				opinion = opinion + 10.00;
			}
			if (target.IsManager && owner.Team != null && owner.Team.TeamManager.ManagerCharacter == target) {
				opinion = opinion + 10.00;
			}
			if (owner.Relations.Friends.Contains (target)) {
				opinion = opinion + 50.00;
			}
			if (owner.Relations.Rivals.Contains (target)) {
				opinion = opinion - 50.00;
			}
		}
		return opinion;
	}
}
