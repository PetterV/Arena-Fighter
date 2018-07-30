using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRelations {
	public List<Character> Friends;
	public List<Character> Rivals;

	public CharacterRelations(){
		Friends = new List<Character>();
		Rivals = new List<Character>();
	}

	public void AddRelationStrong(Character character, string thisType){
		if (thisType == "Friend" &!Friends.Contains (character)) {
			if (Rivals.Contains (character)) {
				Rivals.Remove (character);
			}
			Friends.Add (character);
		}
		if (thisType == "Rival" &!Rivals.Contains (character)) {
			if (Friends.Contains (character)) {
				Friends.Remove (character);
			}
			Rivals.Add (character);
		}
	}


	public void AddRelationWeak(Character character, string thisType){
		if (thisType == "Friend" &!Friends.Contains (character) &! Rivals.Contains(character)) {
			Friends.Add (character);
		}
		if (thisType == "Rival" &!Rivals.Contains (character) &! Friends.Contains(character)) {
			Rivals.Add (character);
		}
	}

	public bool CheckFriends(Character otherChar){
		if (Friends.Contains (otherChar)) {
			return true;
		} else {
			return false;
		}
	}

	public bool CheckRivals(Character otherChar){
		if (Rivals.Contains (otherChar)) {
			return true;
		} else {
			return false;
		}
	}
}
